using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers;

public class VirtualRegisterRSP : IVirtualRegister
{
    public VirtualRegisterRSP(IVirtualMachine? parentVirtualMachine = null)
    {
        _rsp = 0;
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RSP;

    public void Set(string register, ulong value)
    {
        register = register.ToUpper();

        if (register == nameof(RSP))
        {
            RSP = value;
        }
        else if (register == nameof(ESP))
        {
            ESP = (uint)value;
        }
        else if (register == nameof(SP))
        {
            SP = (ushort)value;
        }
        else if (register == nameof(SPL))
        {
            SPL = (byte)value;
        }
        else
        {
            throw new ArgumentException($"Invalid register name, cannot find register of name {register} in [{nameof(RSP)} {nameof(ESP)} {nameof(SP)} {nameof(SPL)}]");
        }
    }

    public ulong RSP
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RSP), Size.Qword, false));

            return _rsp;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(RSP)], BitConverter.GetBytes(RSP), BitConverter.GetBytes(value), nameof(RSP));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RSP), Size.Qword, true, RSP, value));

            _rsp = value;
        }
    }

    public uint ESP
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(ESP), Size.Dword, false));

            return (uint)(_rsp & 0x00000000ffffffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(ESP)], BitConverter.GetBytes(ESP), BitConverter.GetBytes(value), nameof(ESP));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(ESP), Size.Dword, true, ESP, value));

            _rsp = value;
        }
    }

    public ushort SP
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(SP), Size.Word, false));

            return (ushort)((_rsp & 0x00000000ffffffff) & 0x0000ffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(SP)], BitConverter.GetBytes(SP), BitConverter.GetBytes(value), nameof(SP));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(SP), Size.Word, true, SP, value));

            _rsp = ((_rsp & 0x00000000ffffffff) & 0xffff0000) + value;
        }
    }

    public byte SPL
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(SPL), Size.Byte, false));
            
            return (byte)(((_rsp & 0x00000000ffffffff) & 0x0000ffff) & 0x00ff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(SPL)], [SPL], [value], nameof(SP));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(SPL), Size.Byte, true, SPL, value));

            _rsp = (ushort)((((_rsp & 0x00000000ffffffff) & 0x0000ffff) & 0xff00) + value);
        }
    }

    public string Name => "RSP";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RSP), Size.Qword },
        { nameof(ESP), Size.Dword },
        { nameof(SP), Size.Word },
        { nameof(SPL), Size.Byte }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rsp;
}