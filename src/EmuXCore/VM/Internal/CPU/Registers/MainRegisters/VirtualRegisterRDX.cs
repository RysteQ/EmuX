using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using System.Runtime.Intrinsics.Arm;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRDX : IVirtualRegister
{
    public VirtualRegisterRDX(IVirtualMachine? parentVirtualMachine = null)
    {
        _rdx = (ulong)Random.Shared.NextInt64();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RDX;
    public void Set(ulong value) => RDX = value;

    private void RegisterRegisterUpdate(byte[] currentValue, byte[] newValue, string registerName)
    {
        ParentVirtualMachine?.Actions.Add([DIFactory.GenerateIVmAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[registerName], currentValue, newValue, registerName)]);
    }

    public ulong RDX
    {
        get => _rdx;
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(RDX), BitConverter.GetBytes(value), nameof(RDX));
            _rdx = value;
        }
    }

    public uint EDX
    {
        get => (uint)(RDX & 0x00000000ffffffff);
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(EDX), BitConverter.GetBytes(value), nameof(EDX));
            _rdx = value;
        }
    }

    public ushort DX
    {
        get => (ushort)(EDX & 0x0000ffff);
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(DX), BitConverter.GetBytes(value), nameof(DX));
            _rdx = (EDX & 0xffff0000) + value;
        }
    }

    public byte DH
    {
        get => (byte)((DX & 0xff00) >> 8);
        set
        {
            RegisterRegisterUpdate([DH], [value], nameof(DH));
            _rdx = (ushort)((DX & 0x00ff) + (value << 8));
        }
    }

    public byte DL
    {
        get => (byte)(DX & 0x00ff);
        set
        {
            RegisterRegisterUpdate([DL], [value], nameof(DL));
            _rdx = (ushort)((DX & 0xff00) + value);
        }
    }

    public string Name => "RDX";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RDX), Size.Qword },
        { nameof(EDX), Size.Dword },
        { nameof(DX), Size.Word },
        { nameof(DH), Size.Byte },
        { nameof(DL), Size.Byte }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rdx;
}