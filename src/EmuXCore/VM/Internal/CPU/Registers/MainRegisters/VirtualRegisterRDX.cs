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

    public ulong RDX
    {
        get => _rdx;
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(RDX)], BitConverter.GetBytes(RDX), BitConverter.GetBytes(value), nameof(RDX));
            _rdx = value;
        }
    }

    public uint EDX
    {
        get => (uint)(RDX & 0x00000000ffffffff);
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(EDX)], BitConverter.GetBytes(EDX), BitConverter.GetBytes(value), nameof(EDX));
            _rdx = value;
        }
    }

    public ushort DX
    {
        get => (ushort)(EDX & 0x0000ffff);
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(DX)], BitConverter.GetBytes(DX), BitConverter.GetBytes(value), nameof(DX));
            _rdx = (EDX & 0xffff0000) + value;
        }
    }

    public byte DH
    {
        get => (byte)((DX & 0xff00) >> 8);
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(DH)], [DH], [value], nameof(DH));
            _rdx = (ushort)((DX & 0x00ff) + (value << 8));
        }
    }

    public byte DL
    {
        get => (byte)(DX & 0x00ff);
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(DL)], [DL], [value], nameof(DL));
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