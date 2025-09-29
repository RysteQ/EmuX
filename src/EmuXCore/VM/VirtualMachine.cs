using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Actions;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.BIOS;
using EmuXCore.VM.Interfaces.Components.BIOS.Enums;
using EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts;
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

    public virtual void SetFlag(EFlags flag, bool value)
    {
        bool previousValue = (CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS & (uint)flag) != 0;

        CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS & ~(uint)flag;
        CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS | (uint)(value ? (uint)flag : 0);

        FlagAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIFlagAccess(flag, false, (CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS & (uint)flag) != 0, false, value, false));
    }

    public virtual void SetIOPL(bool firstBit, bool secondBit)
    {
        bool firstBitPreviousValue = (CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS | (uint)((firstBit ? 1 : 0) << 13)) != 0;
        bool secondBitPreviousValue = (CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS | (uint)((secondBit ? 1 : 0) << 12)) != 0;

        CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS & ~(uint)EFlags.IOPL;
        CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS | (uint)((firstBit ? 1 : 0) << 13);
        CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS | (uint)((secondBit ? 1 : 0) << 12);

        FlagAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIFlagAccess(EFlags.IOPL, false, firstBitPreviousValue, secondBitPreviousValue, firstBit, secondBit));
    }

    public virtual bool GetFlag(EFlags flag)
    {
        FlagAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIFlagAccess(flag, true, (CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS & (uint)flag) != 0, false, (CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS & (uint)flag) != 0, false));

        return (CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS & (uint)flag) != 0;
    }

    public virtual byte GetIOPL()
    {
        bool firstBitCurrentValue = ((byte)((CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS & (uint)EFlags.IOPL) >> 12) & 0b_0000_0010) == 1;
        bool secondBitCurrentValue = ((byte)((CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS & (uint)EFlags.IOPL) >> 12) & 0b_0000_0001) == 1;

        FlagAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIFlagAccess(EFlags.IOPL, true, firstBitCurrentValue, secondBitCurrentValue, firstBitCurrentValue, secondBitCurrentValue));

        return (byte)((CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS & (uint)EFlags.IOPL) >> 12);
    }

    public virtual void SetByte(int memoryLocation, byte value)
    {
        MemoryAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIMemoryAccess(memoryLocation, Size.Byte, false, Memory.RAM[memoryLocation], value));

        Memory.RAM[memoryLocation] = value;
    }

    public virtual void SetWord(int memoryLocation, ushort value)
    {
        MemoryAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIMemoryAccess(memoryLocation, Size.Word, false, (ulong)(
                                                                                                                    (Memory.RAM[memoryLocation + 1] << 8) +
                                                                                                                    Memory.RAM[memoryLocation]), value));

        Memory.RAM[memoryLocation] = (byte)(value & 0x_00ff);
        Memory.RAM[memoryLocation + 1] = (byte)((value & 0x_ff00) >> 8);
    }

    public virtual void SetDouble(int memoryLocation, uint value)
    {
        MemoryAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIMemoryAccess(memoryLocation, Size.Dword, false, (ulong)(
                                                                                                                    (Memory.RAM[memoryLocation + 1] << 24) +
                                                                                                                    (Memory.RAM[memoryLocation + 1] << 16) +
                                                                                                                    (Memory.RAM[memoryLocation + 1] << 8) +
                                                                                                                    Memory.RAM[memoryLocation]), value));

        Memory.RAM[memoryLocation] = (byte)(value & 0x_0000_00ff);
        Memory.RAM[memoryLocation + 1] = (byte)((value & 0x_0000_ff00) >> 8);
        Memory.RAM[memoryLocation + 2] = (byte)((value & 0x_00ff_0000) >> 16);
        Memory.RAM[memoryLocation + 3] = (byte)((value & 0x_ff00_0000) >> 24);
    }

    public virtual void SetQuad(int memoryLocation, ulong value)
    {
        MemoryAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIMemoryAccess(memoryLocation, Size.Qword, false, (ulong)(
                                                                                                                    (Memory.RAM[memoryLocation + 1] << 56) +
                                                                                                                    (Memory.RAM[memoryLocation + 1] << 48) +
                                                                                                                    (Memory.RAM[memoryLocation + 1] << 40) +
                                                                                                                    (Memory.RAM[memoryLocation + 1] << 32) +
                                                                                                                    (Memory.RAM[memoryLocation + 1] << 24) +
                                                                                                                    (Memory.RAM[memoryLocation + 1] << 16) +
                                                                                                                    (Memory.RAM[memoryLocation + 1] << 8) +
                                                                                                                    Memory.RAM[memoryLocation]), value));

        Memory.RAM[memoryLocation] = (byte)(value & 0x_0000_0000_0000_00ff);
        Memory.RAM[memoryLocation + 1] = (byte)((value & 0x_0000_0000_0000_ff00) >> 8);
        Memory.RAM[memoryLocation + 2] = (byte)((value & 0x_0000_0000_00ff_0000) >> 16);
        Memory.RAM[memoryLocation + 3] = (byte)((value & 0x_0000_0000_ff00_0000) >> 24);
        Memory.RAM[memoryLocation + 4] = (byte)((value & 0x_0000_00ff_0000_0000) >> 32);
        Memory.RAM[memoryLocation + 5] = (byte)((value & 0x_0000_ff00_0000_0000) >> 40);
        Memory.RAM[memoryLocation + 6] = (byte)((value & 0x_00ff_0000_0000_0000) >> 48);
        Memory.RAM[memoryLocation + 7] = (byte)((value & 0x_ff00_0000_0000_0000) >> 56);
    }

    public virtual byte GetByte(int memoryLocation)
    {
        MemoryAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIMemoryAccess(memoryLocation, Size.Byte, true, Memory.RAM[memoryLocation], Memory.RAM[memoryLocation]));

        return Memory.RAM[memoryLocation];
    }

    public virtual ushort GetWord(int memoryLocation)
    {
        ushort[] bytes =
        [
            (ushort)Memory.RAM[memoryLocation],
            (ushort)(Memory.RAM[memoryLocation + 1] << 8)
        ];

        MemoryAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIMemoryAccess(memoryLocation, Size.Word, true, (ulong)bytes.Sum(selectedByte => selectedByte), (ulong)bytes.Sum(selectedByte => selectedByte)));

        return (ushort)bytes.Sum(selectedByte => selectedByte);
    }

    public virtual uint GetDouble(int memoryLocation)
    {
        uint[] bytes =
        [
            (uint)Memory.RAM[memoryLocation],
            (uint)(Memory.RAM[memoryLocation + 1] << 8),
            (uint)(Memory.RAM[memoryLocation + 2] << 16),
            (uint)(Memory.RAM[memoryLocation + 3] << 24)
        ];

        MemoryAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIMemoryAccess(memoryLocation, Size.Dword, true, (ulong)bytes.Sum(selectedByte => selectedByte), (ulong)bytes.Sum(selectedByte => selectedByte)));

        return (uint)bytes.Sum(selectedByte => selectedByte);
    }

    public virtual ulong GetQuad(int memoryLocation)
    {
        uint[] lowerBytes =
        [
            (uint)Memory.RAM[memoryLocation],
            (uint)(Memory.RAM[memoryLocation + 1] << 8),
            (uint)(Memory.RAM[memoryLocation + 2] << 16),
            (uint)(Memory.RAM[memoryLocation + 3] << 24)
        ];

        uint[] upperBytes =
        [
            (uint)(Memory.RAM[memoryLocation + 4] << 32),
            (uint)(Memory.RAM[memoryLocation + 5] << 40),
            (uint)(Memory.RAM[memoryLocation + 6] << 48),
            (uint)(Memory.RAM[memoryLocation + 7] << 56)
        ];

        MemoryAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIMemoryAccess(memoryLocation, Size.Qword, true, (((ulong)upperBytes.Sum(selectedByte => selectedByte)) << 32) + (ulong)lowerBytes.Sum(selectedByte => selectedByte), (((ulong)upperBytes.Sum(selectedByte => selectedByte)) << 32) + (ulong)lowerBytes.Sum(selectedByte => selectedByte)));

        return (((ulong)upperBytes.Sum(selectedByte => selectedByte)) << 32) + (ulong)lowerBytes.Sum(selectedByte => selectedByte);
    }

    public virtual void PushByte(byte value)
    {
        StackAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIStackAccess(Size.Byte, true, value));

        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 4] = value;
        CPU.GetRegister<VirtualRegisterRSP>().RSP -= 4;
    }

    public virtual void PushWord(ushort value)
    {
        StackAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIStackAccess(Size.Word, true, value));

        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 1] = (byte)((0x_ff_00 & value) >> 8);
        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 2] = (byte)(0x_00_ff & value);
        CPU.GetRegister<VirtualRegisterRSP>().RSP -= 2;
    }

    public virtual void PushDouble(uint value)
    {
        StackAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIStackAccess(Size.Dword, true, value));

        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 1] = (byte)((0x_ff00_0000 & value) >> 24);
        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 2] = (byte)((0x_00ff_0000 & value) >> 16);
        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 3] = (byte)((0x_0000_ff00 & value) >> 8);
        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 4] = (byte)((0x_0000_00ff & value));
        CPU.GetRegister<VirtualRegisterRSP>().RSP -= 4;
    }

    public virtual void PushQuad(ulong value)
    {
        StackAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIStackAccess(Size.Qword, true, value));

        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 1] = (byte)((0x_ff00_0000_0000_0000 & value) >> 56);
        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 2] = (byte)((0x_00ff_0000_0000_0000 & value) >> 48);
        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 3] = (byte)((0x_0000_ff00_0000_0000 & value) >> 40);
        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 4] = (byte)((0x_0000_00ff_0000_0000 & value) >> 32);
        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 5] = (byte)((0x_0000_0000_ff00_0000 & value) >> 24);
        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 6] = (byte)((0x_0000_0000_00ff_0000 & value) >> 16);
        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 7] = (byte)((0x_0000_0000_0000_ff00 & value) >> 8);
        Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 8] = (byte)((0x_0000_0000_0000_00ff & value));
        CPU.GetRegister<VirtualRegisterRSP>().RSP -= 8;
    }

    public virtual byte PopByte()
    {
        StackAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIStackAccess(Size.Byte, false, Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP]));

        CPU.GetRegister<VirtualRegisterRSP>().RSP += 4;

        return Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP - 4];
    }

    public virtual ushort PopWord()
    {
        ushort[] bytes =
        [
            (ushort)Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP],
            (ushort)(Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP + 1] << 8)
        ];

        StackAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIStackAccess(Size.Word, false, (ulong)bytes.Sum(selectedByte => selectedByte)));

        CPU.GetRegister<VirtualRegisterRSP>().RSP += 2;

        return (ushort)bytes.Sum(selectedByte => selectedByte);
    }

    public virtual uint PopDouble()
    {
        uint[] bytes =
        [
            (uint)Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP],
            (uint)(Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP + 1] << 8),
            (uint)(Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP + 2] << 16),
            (uint)(Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP + 3] << 24)
        ];

        StackAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIStackAccess(Size.Dword, false, (ulong)bytes.Sum(selectedByte => selectedByte)));

        CPU.GetRegister<VirtualRegisterRSP>().RSP += 4;

        return (uint)bytes.Sum(selectedByte => selectedByte);
    }

    public virtual ulong PopQuad()
    {
        uint[] lowerBytes =
        [
            (uint)Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP],
            (uint)(Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP + 1] << 8),
            (uint)(Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP + 2] << 16),
            (uint)(Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP + 3] << 24)
        ];

        uint[] upperBytes =
        [
            (uint)(Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP + 4] << 32),
            (uint)(Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP + 5] << 40),
            (uint)(Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP + 6] << 48),
            (uint)(Memory.RAM[CPU.GetRegister<VirtualRegisterRSP>().RSP + 7] << 56)
        ];

        StackAccessed?.Invoke(this, (EventArgs)DIFactory.GenerateIStackAccess(Size.Qword, false, (((ulong)upperBytes.Sum(selectedByte => selectedByte)) << 32) + (ulong)lowerBytes.Sum(selectedByte => selectedByte)));

        CPU.GetRegister<VirtualRegisterRSP>().RSP += 8;

        return (((ulong)upperBytes.Sum(selectedByte => selectedByte)) << 32) + (ulong)lowerBytes.Sum(selectedByte => selectedByte);
    }

    public virtual void Interrupt(InterruptCode interruptCode, object subInterruptCode)
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

    public void RegisterAction(VmActionCategory action, Size size, byte[] previousValue, byte[] newValue, string? registerName = null, int? memoryPointer = null, int? deviceId = null)
    {
        Actions.Add([DIFactory.GenerateIVmAction(action, size, previousValue, newValue, registerName, memoryPointer, deviceId)]);
    }

    public event EventHandler? MemoryAccessed;
    public event EventHandler? StackAccessed;
    public event EventHandler? FlagAccessed;

    public List<List<IVmAction>> Actions { get; set; }

    public IVirtualMemory Memory { get; init; }
    public IVirtualCPU CPU { get; init; }
    public IVirtualDisk[] Disks { get; init; }
    public IVirtualBIOS BIOS { get; init; }
    public IVirtualRTC RTC { get; init; }
    public IVirtualGPU GPU { get; init; }
    public IVirtualDevice[] Devices { get; init; }
}