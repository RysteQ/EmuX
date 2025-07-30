using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.BIOS;
using EmuXCore.VM.Interfaces.Components.Enums.SubInterrupts;
using EmuXCore.VM.Internal.BIOS.Enums;
using EmuXCore.VM.Internal.BIOS.Enums.SubInterrupts;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.VM;

/// <summary>
/// Please use the VirtualMachineBuilder instead of the VirtualMachine directly if you want to create a new instance of it
/// </summary>
public class VirtualMachine : IVirtualMachine
{
    public VirtualMachine(IVirtualCPU cpu, IVirtualMemory memory, IVirtualDisk[] disks, IVirtualBIOS bios, IVirtualRTC virtualRTC, IVirtualGPU virtualGPU, IVirtualDevice[] virtualDevices)
    {
        CPU = cpu;
        Memory = memory;
        Disks = disks;
        BIOS = bios;
        RTC = virtualRTC;
        Devices = virtualDevices;
        GPU = virtualGPU;

        CPU.GetRegister<VirtualRegisterRSP>().RSP = (ulong)(Memory.RAM.Length - 1);
    }

    public void SetFlag(EFlags flag, bool value)
    {
        CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS & ~(uint)flag;
        CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS | (uint)(value ? (uint)flag : 0);
    }

    /// <param name="firstBit">firstBit is the LSB</param>
    /// <param name="secondBit">secondBit is the MSB</param>
    public void SetIOPL(bool firstBit, bool secondBit)
    {
        CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS & ~(uint)EFlags.IOPL;
        CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS | (uint)((firstBit ? 1 : 0) << 13);
        CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS | (uint)((secondBit ? 1 : 0) << 12);
    }

    public bool GetFlag(EFlags flag)
    {
        return (CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS & (uint)flag) != 0;
    }

    public byte GetIOPL()
    {
        return (byte)((CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS & (uint)EFlags.IOPL) >> 12);
    }

    public void SetByte(int memoryLocation, byte value)
    {
        Memory.RAM[memoryLocation] = value;
    }

    public void SetWord(int memoryLocation, ushort value)
    {
        SetByte(memoryLocation, (byte)(value & 0x_00ff));
        SetByte(memoryLocation + 1, (byte)((value & 0x_ff00) >> 8));
    }

    public void SetDouble(int memoryLocation, uint value)
    {
        SetWord(memoryLocation, (ushort)(value & 0x_0000_ffff));
        SetWord(memoryLocation + 2, (ushort)((value & 0x_ffff_0000) >> 16));
    }

    public void SetQuad(int memoryLocation, ulong value)
    {
        SetDouble(memoryLocation, (uint)(value & 0x_0000_0000_ffff_ffff));
        SetDouble(memoryLocation + 4, (uint)((value & 0x_ffff_ffff_0000_0000) >> 32));
    }

    public byte GetByte(int memoryLocation)
    {
        return Memory.RAM[memoryLocation];
    }

    public ushort GetWord(int memoryLocation)
    {
        ushort partOne = GetByte(memoryLocation);
        ushort partTwo = (ushort)(GetByte(memoryLocation + 1) << 8);

        return (ushort)(partOne + partTwo);
    }

    public uint GetDouble(int memoryLocation)
    {
        uint partOne = GetWord(memoryLocation);
        uint partTwo = (uint)(GetWord(memoryLocation + 2) << 16);

        return partOne + partTwo;
    }

    public ulong GetQuad(int memoryLocation)
    {
        ulong partOne = (ulong)(GetDouble(memoryLocation));
        ulong partTwo = ((ulong)GetDouble(memoryLocation + 4)) << 32;

        return partOne + partTwo;
    }

    public void PushByte(byte value)
    {
        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 4] = value;
        CPU.GetRegister<VirtualRegisterRSP>().RSP -= 4;
    }

    public void PushWord(ushort value)
    {
        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 1] = (byte)((0x_ff_00 & value) >> 8);
        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 2] = (byte)(0x_00_ff & value);
        CPU.GetRegister<VirtualRegisterRSP>().RSP -= 2;
    }

    public void PushDouble(uint value)
    {
        PushWord((ushort)((value & 0x_ffff_0000) >> 16));
        PushWord((ushort)(value & 0x_0000_ffff));
    }

    public void PushQuad(ulong value)
    {
        PushDouble((uint)((value & 0x_ffff_ffff_0000_0000) >> 32));
        PushDouble((uint)(value & 0x_0000_0000_ffff_ffff));
    }

    public byte PopByte()
    {
        CPU.GetRegister<VirtualRegisterRSP>().RSP += 4;

        return Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 4];
    }

    public ushort PopWord()
    {
        byte MS = Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP + 1];
        byte LS = Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP];

        CPU.GetRegister<VirtualRegisterRSP>().RSP += 2;

        return (ushort)((MS << 8) + LS);
    }

    public uint PopDouble()
    {
        ushort LS = PopWord();
        ushort MS = PopWord();

        return (uint)((MS << 16) + LS);
    }

    public ulong PopQuad()
    {
        ulong LS = PopDouble();
        ulong MS = PopDouble();

        return (ulong)((MS << 32) + LS);
    }

    public void Interrupt(InterruptCode interruptCode, object subInterruptCode)
    {
        Dictionary<InterruptCode, Type> interruptCodeLookup = new()
        {
            { InterruptCode.Disk, typeof(DiskInterrupt) },
            { InterruptCode.RTC, typeof(RTCInterrupt) },
            { InterruptCode.Video, typeof(VideoInterrupt) },
        };

        if (!Enum.IsDefined(interruptCodeLookup[interruptCode], subInterruptCode))
        {
            throw new Exception($"Sub interrupt code {interruptCode} of interrupt {nameof(subInterruptCode)} has not been defined");
        }

        switch (interruptCode)
        {
            case InterruptCode.Disk: BIOS.HandleDiskInterrupt((DiskInterrupt)subInterruptCode); break;
            case InterruptCode.RTC: BIOS.HandleRTCInterrupt((RTCInterrupt)subInterruptCode); break;
            case InterruptCode.Video: BIOS.HandleVideoInterrupt((VideoInterrupt)subInterruptCode); break;
            default: throw new NotImplementedException($"Interrupt code of type {nameof(interruptCode)} has not yet been implemented");
        }
    }

    public IVirtualMemory Memory { get; init; }
    public IVirtualCPU CPU { get; init; }
    public IVirtualDisk[] Disks { get; init; }
    public IVirtualBIOS BIOS { get; init; }
    public IVirtualRTC RTC { get; init; }
    public IVirtualGPU GPU { get; init; }
    public IVirtualDevice[] Devices { get; init; }
}