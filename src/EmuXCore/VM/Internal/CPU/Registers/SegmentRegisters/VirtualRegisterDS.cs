using System;
using System.Collections.Generic;
using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Interfaces.Exceptions;

namespace EmuXCore.VM.Internal.CPU.Registers.SegmentRegisters;

public class VirtualRegisterDS : IVirtualRegister
{
    public VirtualRegisterDS(IVirtualMachine? parentVirtualMachine = null)
    {
        _ds = (ushort)Random.Shared.Next();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => DS;

    public void Set(string register, ulong value)
    {
        register = register.ToUpper();

        if (register == nameof(DS))
        {
            DS = (ushort)value;
        }
        else
        {
            throw new VirtualRegisterNotFoundException($"Invalid register name, cannot find register of name {register} in [{nameof(DS)}]");
        }
    }

    public ushort DS
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(DS), Size.Word, false));

            return _ds;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(DS)], BitConverter.GetBytes(DS), BitConverter.GetBytes(value), nameof(DS));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(DS), Size.Word, true, DS, value));

            _ds = value;
        }
    }

    public string Name => "DS";

    public IDictionary<string, Size> RegisterNamesAndSizes => new Dictionary<string, Size>
    {
        { nameof(DS), Size.Word }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ushort _ds;
}