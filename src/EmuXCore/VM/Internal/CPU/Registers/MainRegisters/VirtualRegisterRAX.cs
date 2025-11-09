using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRAX : IVirtualRegister
{
    public VirtualRegisterRAX(IVirtualMachine? parentVirtualMachine = null)
    {
        _rax = (ulong)Random.Shared.NextInt64();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RAX;
    
    public void Set(string register, ulong value)
    {
        register = register.ToUpper();

        if (register == nameof(RAX))
        {
            RAX = value;
        }
        else if (register == nameof(EAX))
        {
            EAX = (uint)value;
        }
        else if (register == nameof(AX))
        {
            AX = (ushort)value;
        }
        else if (register == nameof(AH))
        {
            AH = (byte)value;
        }
        else if (register == nameof(AL))
        {
            AL = (byte)value;
        }
        else
        {
            throw new ArgumentException($"Invalid register name, cannot find register of name {register} in [{nameof(RAX)} {nameof(EAX)} {nameof(AX)} {nameof(AH)} {nameof(AL)}]");
        }
    }

    public ulong RAX
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RAX), Size.Qword, false));

            return _rax;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(RAX)], BitConverter.GetBytes(RAX), BitConverter.GetBytes(value), nameof(RAX));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RAX), Size.Qword, true, RAX, value));

            _rax = value;
        }
    }

    public uint EAX
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(EAX), Size.Dword, false));

            return (uint)(RAX & 0x00000000ffffffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(EAX)], BitConverter.GetBytes(EAX), BitConverter.GetBytes(value), nameof(EAX));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(EAX), Size.Dword, true, EAX, value));

            _rax = value;
        }
    }

    public ushort AX
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(AX), Size.Word, false));

            return (ushort)(EAX & 0x0000ffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(AX)], BitConverter.GetBytes(AX), BitConverter.GetBytes(value), nameof(AX));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(AX), Size.Word, true, AX, value));

            _rax = (EAX & 0xffff0000) + value;
        }
    }

    public byte AH
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(AH), Size.Byte, false));

            return (byte)((AX & 0xff00) >> 8);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(AH)], [AH], [value], nameof(AH));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(AH), Size.Byte, true, AH, value));

            _rax = (ushort)((AX & 0x00ff) + (value << 8));
        }
    }

    public byte AL
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(AL), Size.Byte, false));

            return (byte)(AX & 0x00ff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(AL)], [AL], [value], nameof(AL));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(AL), Size.Byte, true, AL, value));

            _rax = (ushort)((AX & 0xff00) + value);
        }
    }

    public string Name => "RAX";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RAX), Size.Qword },
        { nameof(EAX), Size.Dword },
        { nameof(AX), Size.Word },
        { nameof(AH), Size.Byte },
        { nameof(AL), Size.Byte }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rax;
}