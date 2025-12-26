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

    public void Set(string register, ulong value)
    {
        register = register.ToUpper();

        if (register == nameof(RCX))
        {
            RCX = value;
        }
        else if (register == nameof(ECX))
        {
            ECX = (uint)value;
        }
        else if (register == nameof(CX))
        {
            CX = (ushort)value;
        }
        else if (register == nameof(CH))
        {
            CH = (byte)value;
        }
        else if (register == nameof(CL))
        {
            CL = (byte)value;
        }
        else
        {
            throw new ArgumentException($"Invalid register name, cannot find register of name {register} in [{nameof(RCX)} {nameof(ECX)} {nameof(CX)} {nameof(CH)} {nameof(CL)}]");
        }
    }

    public ulong RCX
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RCX), Size.Qword, false));

            return _rcx;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(RCX)], BitConverter.GetBytes(RCX), BitConverter.GetBytes(value), nameof(RCX));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RCX), Size.Qword, true, RCX, value));

            _rcx = value;
        }
    }

    public uint ECX
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(ECX), Size.Dword, false));

            return (uint)(_rcx & 0x00000000ffffffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(ECX)], BitConverter.GetBytes(ECX), BitConverter.GetBytes(value), nameof(ECX));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(ECX), Size.Dword, true, ECX, value));

            _rcx = value;
        }
    }

    public ushort CX
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(CX), Size.Word, false));

            return (ushort)((_rcx & 0x00000000ffffffff) & 0x0000ffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(CX)], BitConverter.GetBytes(CX), BitConverter.GetBytes(value), nameof(CX));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(CX), Size.Word, true, CX, value));

            _rcx = (_rcx & 0x_ff_ff_ff_ff_ff_ff_00_00) + value;
        }
    }

    public byte CH
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(CH), Size.Byte, false));

            return (byte)((((_rcx & 0x00000000ffffffff) & 0x0000ffff) & 0xff00) >> 8);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(CH)], [CH], [value], nameof(CH));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(CH), Size.Byte, true, CH, value));

            _rcx = (_rcx & 0x_ff_ff_ff_ff_ff_ff_00_ff) + (ulong)(value << 8);
        }
    }

    public byte CL
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(CL), Size.Byte, false));

            return (byte)(((_rcx & 0x00000000ffffffff) & 0x0000ffff) & 0x00ff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(CL)], [CL], [value], nameof(CL));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(CL), Size.Byte, true, CL, value));

            _rcx = (_rcx & 0x_ff_ff_ff_ff_ff_ff_ff_00) + value;
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