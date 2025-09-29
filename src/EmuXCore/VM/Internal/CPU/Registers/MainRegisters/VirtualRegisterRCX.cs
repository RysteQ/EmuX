using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRCX : IVirtualRegister
{
    public VirtualRegisterRCX(IVirtualMachine? parentVirtualMachine = null)
    {
        _rcx = (ulong)Random.Shared.NextInt64();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RCX;
    public void Set(ulong value) => RCX = value;

    private void RegisterRegisterUpdate(byte[] currentValue, byte[] newValue, string registerName)
    {
        ParentVirtualMachine?.Actions.Add([DIFactory.GenerateIVmAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[registerName], currentValue, newValue, registerName)]);
    }

    public ulong RCX
    {
        get => _rcx;
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(RCX), BitConverter.GetBytes(value), nameof(RCX));
            _rcx = value;
        }
    }

    public uint ECX
    {
        get => (uint)(RCX & 0x00000000ffffffff);
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(ECX), BitConverter.GetBytes(value), nameof(ECX));
            _rcx = value;
        }
    }

    public ushort CX
    {
        get => (ushort)(ECX & 0x0000ffff);
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(CX), BitConverter.GetBytes(value), nameof(CX));
            _rcx = (ECX & 0xffff0000) + value;
        }
    }

    public byte CH
    {
        get => (byte)((CX & 0xff00) >> 8);
        set
        {
            RegisterRegisterUpdate([CH], [value], nameof(CH));
            _rcx = (ushort)((CX & 0x00ff) + (value << 8));
        }
    }

    public byte CL
    {
        get => (byte)(CX & 0x00ff);
        set
        {
            RegisterRegisterUpdate([CL], [value], nameof(CL));
            _rcx = (ushort)((CX & 0xff00) + value);
        }
    }

    public string Name => "RCX";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RCX), Size.Qword },
        { nameof(ECX), Size.Dword },
        { nameof(CX), Size.Word },
        { nameof(CH), Size.Byte },
        { nameof(CL), Size.Byte }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rcx;
}