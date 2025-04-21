using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
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
    public VirtualMachine(IVirtualCPU cpu, IVirtualMemory memory, IVirtualDisk[] disks, IVirtualBIOS bios, IVirtualRTC virtualRTC, IVirtualDevice[] virtualDevices)
    {
        CPU = cpu;
        Memory = memory;
        Disks = disks;
        BIOS = bios;
        RTC = virtualRTC;
        Devices = virtualDevices;

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
        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP] = value;
        CPU.GetRegister<VirtualRegisterRSP>().RSP--;
    }

    public void PushWord(ushort value)
    {
        PushByte((byte)(value & 0x_00ff));
        PushByte((byte)((value & 0x_ff00) >> 8));
    }

    public void PushDoubleWord(uint value)
    {
        PushWord((ushort)(value & 0x_0000_ffff));
        PushWord((ushort)((value & 0x_ffff_0000) >> 16));
    }

    public void PushQuadWord(ulong value)
    {
        PushDoubleWord((uint)(value & 0x_0000_0000_ffff_ffff));
        PushDoubleWord((uint)((value & 0x_ffff_ffff_0000_0000) >> 32));
    }

    public byte PopByte()
    {
        CPU.GetRegister<VirtualRegisterRSP>().RSP++;

        return Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP];
    }

    public ushort PopWord()
    {
        byte partOne = PopByte();
        byte partTwo = PopByte();

        return (ushort)(partOne << 16 + partTwo);
    }

    public uint PopDoubleWord()
    {
        ushort partOne = PopWord();
        ushort partTwo = PopWord();

        return (uint)(partOne << 32 + partTwo);
    }

    public ulong PopQuadWord()
    {
        int partOne = (int)PopDoubleWord();
        int partTwo = (int)PopDoubleWord();

        return (ulong)(partOne << 64 + partTwo);
    }

    public void Interrupt(InterruptCode interruptCode, object subInterruptCode)
    {
        Dictionary<InterruptCode, Type> interruptCodeLookup = new()
        {
            { InterruptCode.Disk, typeof(DiskInterrupt) },
            { InterruptCode.RTC, typeof(RTCInterrupt) },
        };

        if (!Enum.IsDefined(interruptCodeLookup[interruptCode], subInterruptCode))
        {
            throw new Exception($"Sub interrupt code {interruptCode} of interrupt {nameof(subInterruptCode)} has not been defined");
        }

        switch (interruptCode)
        {
            case InterruptCode.Disk: BIOS.HandleDiskInterrupt((DiskInterrupt)subInterruptCode); break;
            case InterruptCode.RTC: BIOS.HandleRTCInterrupt((RTCInterrupt)subInterruptCode); break;
            default: throw new NotImplementedException($"Interrupt code of type {nameof(interruptCode)} has not yet been implemented");
        }
    }

    public IVirtualMemory Memory { get; init; }
    public IVirtualCPU CPU { get; init; }
    public IVirtualDisk[] Disks { get; init; }
    public IVirtualBIOS BIOS { get; init; }
    public IVirtualRTC RTC { get; init; }
    public IVirtualDevice[] Devices { get; init; }
}