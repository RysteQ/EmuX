using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using System.Reflection.PortableExecutable;

namespace EmuXCore.VM.Internal.CPU.Registers;

public class VirtualRegisterRBP : IVirtualRegister
{
    public VirtualRegisterRBP(IVirtualMachine? parentVirtualMachine = null)
    {
        _rbp = 0;
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RBP;

    public void Set(string register, ulong value)
    {
        register = register.ToUpper();

        if (register == nameof(RBP))
        {
            RBP = value;
        }
        else if (register == nameof(EBP))
        {
            EBP = (uint)value;
        }
        else if (register == nameof(BP))
        {
            BP = (ushort)value;
        }
        else if (register == nameof(BPL))
        {
            BPL = (byte)value;
        }
        else
        {
            throw new ArgumentException($"Invalid register name, cannot find register of name {register} in [{nameof(RBP)} {nameof(EBP)} {nameof(BP)} {nameof(BPL)}]");
        }
    }

    public ulong RBP
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RBP), Size.Qword, false));

            return _rbp;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(RBP)], BitConverter.GetBytes(RBP), BitConverter.GetBytes(value), nameof(RBP));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RBP), Size.Qword, true, RBP, value));

            _rbp = value;
        }
    }

    public uint EBP
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(EBP), Size.Dword, false));

            return (uint)(RBP & 0x00000000ffffffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(EBP)], BitConverter.GetBytes(EBP), BitConverter.GetBytes(value), nameof(RBP));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(EBP), Size.Dword, true, EBP, value));

            _rbp = value;
        }
    }

    public ushort BP
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(BP), Size.Word, false));

            return (ushort)(EBP & 0x0000ffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(BP)], BitConverter.GetBytes(BP), BitConverter.GetBytes(value), nameof(BP));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(BP), Size.Word, true, BP, value));

            _rbp = (EBP & 0xffff0000) + value;
        }
    }

    public byte BPL
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(BPL), Size.Byte, false));

            return (byte)(BP & 0x00ff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(BPL)], [BPL], [value], nameof(BPL));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(BPL), Size.Byte, true, BPL, value));

            _rbp = (ushort)((BP & 0xff00) + value);
        }
    }

    public string Name => "RBP";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RBP), Size.Qword },
        { nameof(EBP), Size.Dword },
        { nameof(BP), Size.Word },
        { nameof(BPL), Size.Byte }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rbp;
}