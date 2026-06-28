using System;
using System.Collections.Generic;
using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Interfaces.Exceptions;

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
            throw new VirtualRegisterNotFoundException($"Invalid register name, cannot find register of name {register} in [{nameof(RBP)} {nameof(EBP)} {nameof(BP)} {nameof(BPL)}]");
        }
    }

    public ulong RBP
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(RBP), Size.Qword, false));

            return _rbp;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(RBP)], BitConverter.GetBytes(RBP), BitConverter.GetBytes(value), nameof(RBP));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(RBP), Size.Qword, true, RBP, value));

            _rbp = value;
        }
    }

    public uint EBP
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(EBP), Size.Dword, false));

            return (uint)(_rbp & 0x00000000ffffffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(EBP)], BitConverter.GetBytes(EBP), BitConverter.GetBytes(value), nameof(RBP));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(EBP), Size.Dword, true, EBP, value));

            _rbp = value;
        }
    }

    public ushort BP
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(BP), Size.Word, false));

            return (ushort)((_rbp & 0x00000000ffffffff) & 0x0000ffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(BP)], BitConverter.GetBytes(BP), BitConverter.GetBytes(value), nameof(BP));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(BP), Size.Word, true, BP, value));

            _rbp = (_rbp & 0x_ff_ff_ff_ff_ff_ff_00_00) + value;
        }
    }

    public byte BPL
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(BPL), Size.Byte, false));

            return (byte)(((_rbp & 0x00000000ffffffff) & 0x0000ffff) & 0x00ff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(BPL)], [BPL], [value], nameof(BPL));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(BPL), Size.Byte, true, BPL, value));

            _rbp = (_rbp & 0x_ff_ff_ff_ff_ff_ff_ff_00) + value;
        }
    }

    public string Name => "RBP";

    public IDictionary<string, Size> RegisterNamesAndSizes => new Dictionary<string, Size>
    {
        { nameof(RBP), Size.Qword },
        { nameof(EBP), Size.Dword },
        { nameof(BP), Size.Word },
        { nameof(BPL), Size.Byte }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rbp;
}