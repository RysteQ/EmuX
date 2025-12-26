using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRDX : IVirtualRegister
{
    public VirtualRegisterRDX(IVirtualMachine? parentVirtualMachine = null)
    {
        _rdx = (ulong)Random.Shared.NextInt64();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RDX;

    public void Set(string register, ulong value)
    {
        register = register.ToUpper();

        if (register == nameof(RDX))
        {
            RDX = value;
        }
        else if (register == nameof(EDX))
        {
            EDX = (uint)value;
        }
        else if (register == nameof(DX))
        {
            DX = (ushort)value;
        }
        else if (register == nameof(DH))
        {
            DH = (byte)value;
        }
        else if (register == nameof(DH))
        {
            DL = (byte)value;
        }
        else
        {
            throw new ArgumentException($"Invalid register name, cannot find register of name {register} in [{nameof(RDX)} {nameof(EDX)} {nameof(DX)} {nameof(DH)} {nameof(DL)}]");
        }
    }

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

            return (uint)(_rdx & 0x00000000ffffffff);
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

            return (ushort)((_rdx & 0x00000000ffffffff) & 0x0000ffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(DX)], BitConverter.GetBytes(DX), BitConverter.GetBytes(value), nameof(DX));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(DX), Size.Word, true, DX, value));

            _rdx = (_rdx & 0x_ff_ff_ff_ff_ff_ff_00_00) + value;
        }
    }

    public byte DH
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(DH), Size.Byte, false));

            return (byte)((((_rdx & 0x00000000ffffffff) & 0x0000ffff) & 0xff00) >> 8);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(DH)], [DH], [value], nameof(DH));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(DH), Size.Byte, true, DH, value));

            _rdx = (_rdx & 0x_ff_ff_ff_ff_ff_ff_00_ff) + (ulong)(value << 8);
        }
    }

    public byte DL
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(DL), Size.Byte, false));

            return (byte)(((_rdx & 0x00000000ffffffff) & 0x0000ffff) & 0x00ff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(DL)], [DL], [value], nameof(DL));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(DL), Size.Byte, true, DL, value));

            _rdx = (_rdx & 0x_ff_ff_ff_ff_ff_ff_ff_00) + value;
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