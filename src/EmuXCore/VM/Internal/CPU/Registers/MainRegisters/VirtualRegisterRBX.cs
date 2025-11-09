using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRBX : IVirtualRegister
{
    public VirtualRegisterRBX(IVirtualMachine? parentVirtualMachine = null)
    {
        _rbx = (ulong)Random.Shared.NextInt64();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RBX;

    public void Set(string register, ulong value)
    {
        register = register.ToUpper();

        if (register == nameof(RBX))
        {
            RBX = value;
        }
        else if (register == nameof(EBX))
        {
            EBX = (uint)value;
        }
        else if (register == nameof(BX))
        {
            BX = (ushort)value;
        }
        else if (register == nameof(BH))
        {
            BH = (byte)value;
        }
        else if (register == nameof(BH))
        {
            BL = (byte)value;
        }
        else
        {
            throw new ArgumentException($"Invalid register name, cannot find register of name {register} in [{nameof(RBX)} {nameof(EBX)} {nameof(BX)} {nameof(BH)} {nameof(BL)}]");
        }
    }

    public ulong RBX
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RBX), Size.Qword, false));

            return _rbx;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(RBX)], BitConverter.GetBytes(RBX), BitConverter.GetBytes(value), nameof(RBX));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RBX), Size.Qword, true, RBX, value));

            _rbx = value;
        }
    }

    public uint EBX
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(EBX), Size.Dword, false));

            return (uint)(RBX & 0x00000000ffffffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(EBX)], BitConverter.GetBytes(EBX), BitConverter.GetBytes(value), nameof(EBX));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(EBX), Size.Dword, true, EBX, value));
            
            _rbx = value;
        }
    }

    public ushort BX
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(BX), Size.Word, false));

            return (ushort)(EBX & 0x0000ffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(BX)], BitConverter.GetBytes(BX), BitConverter.GetBytes(value), nameof(BX));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(BX), Size.Word, true, BX, value));
            
            _rbx = (EBX & 0xffff0000) + value;
        }
    }

    public byte BH
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(BH), Size.Byte, false));

            return (byte)((BX & 0xff00) >> 8);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(BH)], [BH], [value], nameof(BH));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(BH), Size.Byte, true, BH, value));

            _rbx = (ushort)((BX & 0x00ff) + (value << 8));
        }
    }

    public byte BL
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(BL), Size.Byte, false));

            return (byte)(BX & 0x00ff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(BL)], [BL], [value], nameof(BL));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(BL), Size.Byte, true, BL, value));

            _rbx = (ushort)((BX & 0xff00) + value);
        }
    }

    public string Name => "RBX";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RBX), Size.Qword },
        { nameof(EBX), Size.Dword },
        { nameof(BX), Size.Word },
        { nameof(BH), Size.Byte },
        { nameof(BL), Size.Byte }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rbx;
}