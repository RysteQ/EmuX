using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

public class VirtualRegisterRIP : IVirtualRegister
{
    public VirtualRegisterRIP(IVirtualMachine? parentVirtualMachine = null)
    {
        _rip = 0;
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RIP;

    public void Set(string register, ulong value)
    {
        register = register.ToUpper();

        if (register == nameof(RIP))
        {
            RIP = value;
        }
        else if (register == nameof(EIP))
        {
            EIP = (uint)value;
        }
        else if (register == nameof(IP))
        {
            IP = (ushort)value;
        }
        else
        {
            throw new ArgumentException($"Invalid register name, cannot find register of name {register} in [{nameof(RIP)} {nameof(EIP)} {nameof(IP)}]");
        }
    }

    public ulong RIP
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RIP), Size.Qword, false));

            return _rip;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(RIP)], BitConverter.GetBytes(RIP), BitConverter.GetBytes(value), nameof(RIP));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RIP), Size.Qword, true, RIP, value));

            _rip = value;
        }
    }

    public uint EIP
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(EIP), Size.Dword, false));

            return (uint)(_rip & 0x00000000ffffffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(EIP)], BitConverter.GetBytes(EIP), BitConverter.GetBytes(value), nameof(EIP));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(EIP), Size.Dword, true, EIP, value));

            _rip = value;
        }
    }

    public ushort IP
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(IP), Size.Word, false));

            return (ushort)((_rip & 0x00000000ffffffff) & 0x0000ffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(IP)], BitConverter.GetBytes(IP), BitConverter.GetBytes(value), nameof(IP));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(IP), Size.Word, true, IP, value));

            _rip = ((_rip & 0x00000000ffffffff) & 0xffff0000) + value;
        }
    }

    public string Name => "RIP";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RIP), Size.Qword },
        { nameof(EIP), Size.Dword },
        { nameof(IP), Size.Word }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rip;
}