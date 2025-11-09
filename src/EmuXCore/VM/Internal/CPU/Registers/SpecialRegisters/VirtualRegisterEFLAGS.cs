using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

public class VirtualRegisterEFLAGS : IVirtualRegister
{
    public VirtualRegisterEFLAGS(IVirtualMachine? parentVirtualMachine = null)
    {
        _rflags = 0x0000000000000002;
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RFLAGS;

    public void Set(string register, ulong value)
    {
        register = register.ToUpper();

        if (register == nameof(RFLAGS))
        {
            RFLAGS = value;
        }
        else if (register == nameof(EFLAGS))
        {
            EFLAGS = (uint)value;
        }
        else if (register == nameof(FLAGS))
        {
            FLAGS = (ushort)value;
        }
        else
        {
            throw new ArgumentException($"Invalid register name, cannot find register of name {register} in [{nameof(RFLAGS)} {nameof(EFLAGS)} {nameof(FLAGS)}]");
        }
    }

    public ulong RFLAGS
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RFLAGS), Size.Qword, false));

            return _rflags;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(RFLAGS)], BitConverter.GetBytes(RFLAGS), BitConverter.GetBytes(value), nameof(RFLAGS));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RFLAGS), Size.Qword, true, RFLAGS, value));

            _rflags = value;
        }
    }

    public uint EFLAGS
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(EFLAGS), Size.Dword, false));

            return (uint)(_rflags & 0x00000000ffffffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(EFLAGS)], BitConverter.GetBytes(EFLAGS), BitConverter.GetBytes(value), nameof(EFLAGS));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(EFLAGS), Size.Dword, true, FLAGS, value));

            _rflags = value;
        }
    }

    public ushort FLAGS
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(FLAGS), Size.Word, false));

            return (ushort)((_rflags & 0x00000000ffffffff) & 0x0000ffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(FLAGS)], BitConverter.GetBytes(FLAGS), BitConverter.GetBytes(value), nameof(FLAGS));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(FLAGS), Size.Word, true, FLAGS, value));

            _rflags = ((_rflags & 0x00000000ffffffff) & 0xffff0000) + value;
        }
    }

    public string Name => "RFLAGS";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RFLAGS), Size.Qword },
        { nameof(EFLAGS), Size.Dword },
        { nameof(FLAGS), Size.Dword }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rflags;
}