using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRBX : IVirtualRegister
{
    public VirtualRegisterRBX(IVirtualMachine? parentVirtualMachine = null)
    {
        _rbx = (ulong)Random.Shared.NextInt64();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RBX;
    public void Set(ulong value) => RBX = value;

    private void RegisterRegisterUpdate(byte[] currentValue, byte[] newValue, string registerName)
    {
        ParentVirtualMachine?.Actions.Add([DIFactory.GenerateIVmAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[registerName], currentValue, newValue, registerName)]);
    }

    public ulong RBX
    {
        get => _rbx;
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(RBX), BitConverter.GetBytes(value), nameof(RBX));
            _rbx = value;
        }
    }

    public uint EBX
    {
        get => (uint)(RBX & 0x00000000ffffffff);
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(EBX), BitConverter.GetBytes(value), nameof(EBX));
            _rbx = value;
        }
    }

    public ushort BX
    {
        get => (ushort)(EBX & 0x0000ffff);
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(BX), BitConverter.GetBytes(value), nameof(BX));
            _rbx = (EBX & 0xffff0000) + value;
        }
    }

    public byte BH
    {
        get => (byte)((BX & 0xff00) >> 8);
        set
        {
            RegisterRegisterUpdate([BH], [value], nameof(BH));
            _rbx = (ushort)((BX & 0x00ff) + (value << 8));
        }
    }

    public byte BL
    {
        get => (byte)(BX & 0x00ff);
        set
        {
            RegisterRegisterUpdate([BL], [value], nameof(BL));
            _rbx = (ushort)((BX & 0xff00) + value);
        }
    }

    public string Name => "RBX";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RBX), Size.Qword },
        { nameof(EBX), Size.Dword },
        { nameof(BX), Size.Word },
        { nameof(BH), Size.Byte },
        { nameof(BL), Size.Byte }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rbx;
}