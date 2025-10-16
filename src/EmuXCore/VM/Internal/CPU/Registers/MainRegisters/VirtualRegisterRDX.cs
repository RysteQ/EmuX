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
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RDX), Size.Qword, false));

            return _rdx;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(RDX)], BitConverter.GetBytes(RDX), BitConverter.GetBytes(value), nameof(RDX));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RDX), Size.Qword, true, RDX, value));

            _rdx = value;
        }
    }

    public uint EDX
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(EDX), Size.Dword, false));

            return (uint)(RDX & 0x00000000ffffffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(EDX)], BitConverter.GetBytes(EDX), BitConverter.GetBytes(value), nameof(EDX));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(EDX), Size.Dword, true, EDX, value));

            _rdx = value;
        }
    }

    public ushort DX
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(DX), Size.Word, false));

            return (ushort)(EDX & 0x0000ffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(DX)], BitConverter.GetBytes(DX), BitConverter.GetBytes(value), nameof(DX));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(DX), Size.Word, true, DX, value));

            _rdx = (EDX & 0xffff0000) + value;
        }
    }

    public byte DH
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(DH), Size.Byte, false));

            return (byte)((DX & 0xff00) >> 8);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(DH)], [DH], [value], nameof(DH));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(DH), Size.Byte, true, DH, value));

            _rdx = (ushort)((DX & 0x00ff) + (value << 8));
        }
    }

    public byte DL
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(DL), Size.Byte, false));

            return (byte)(DX & 0x00ff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(DL)], [DL], [value], nameof(DL));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(DL), Size.Byte,  true, DL, value));

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