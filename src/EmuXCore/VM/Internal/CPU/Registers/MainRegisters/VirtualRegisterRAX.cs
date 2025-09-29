using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRAX : IVirtualRegister
{
    public VirtualRegisterRAX(IVirtualMachine? parentVirtualMachine = null)
    {
        _rax = (ulong)Random.Shared.NextInt64();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RAX;
    public void Set(ulong value) => RAX = value;

    public ulong RAX
    {
        get => _rax;
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(RAX)], BitConverter.GetBytes(RAX), BitConverter.GetBytes(value), nameof(RAX));
            _rax = value;
        }
    }

    public uint EAX
    {
        get => (uint)(RAX & 0x00000000ffffffff);
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(EAX)], BitConverter.GetBytes(EAX), BitConverter.GetBytes(value), nameof(EAX));
            _rax = value;
        }
    }

    public ushort AX
    {
        get => (ushort)(EAX & 0x0000ffff);
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(AX)], BitConverter.GetBytes(AX), BitConverter.GetBytes(value), nameof(AX));
            _rax = (EAX & 0xffff0000) + value;
        }
    }

    public byte AH
    {
        get => (byte)((AX & 0xff00) >> 8);
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(AH)], [AH], [value], nameof(AH));
            _rax = (ushort)((AX & 0x00ff) + (value << 8));
        }
    }

    public byte AL
    {
        get => (byte)(AX & 0x00ff);
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(AL)], [AL], [value], nameof(AL));
            _rax = (ushort)((AX & 0xff00) + value);
        }
    }

    public string Name => "RAX";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RAX), Size.Qword },
        { nameof(EAX), Size.Dword },
        { nameof(AX), Size.Word },
        { nameof(AH), Size.Byte },
        { nameof(AL), Size.Byte }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rax;
}