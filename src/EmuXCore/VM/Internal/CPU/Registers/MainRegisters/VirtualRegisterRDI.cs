using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Interfaces.Exceptions;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRDI : IVirtualRegister
{
    public VirtualRegisterRDI(IVirtualMachine? parentVirtualMachine = null)
    {
        _rdi = (ulong)Random.Shared.NextInt64();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RDI;

    public void Set(string register, ulong value)
    {
        register = register.ToUpper();

        if (register == nameof(RDI))
        {
            RDI = value;
        }
        else if (register == nameof(EDI))
        {
            EDI = (uint)value;
        }
        else if (register == nameof(DI))
        {
            DI = (ushort)value;
        }
        else if (register == nameof(DIL))
        {
            DIL = (byte)value;
        }
        else
        {
            throw new VirtualRegisterNotFoundException($"Invalid register name, cannot find register of name {register} in [{nameof(RDI)} {nameof(EDI)} {nameof(DI)} {nameof(DIL)}]");
        }
    }

    public ulong RDI
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(RDI), Size.Qword, false));

            return _rdi;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(RDI)], BitConverter.GetBytes(RDI), BitConverter.GetBytes(value), nameof(RDI));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(RDI), Size.Qword, true, RDI, value));

            _rdi = value;
        }
    }

    public uint EDI
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(EDI), Size.Dword, false));

            return (uint)(_rdi & 0x00000000ffffffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(EDI)], BitConverter.GetBytes(EDI), BitConverter.GetBytes(value), nameof(EDI));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(EDI), Size.Dword, true, EDI, value));

            _rdi = value;
        }
    }

    public ushort DI
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(DI), Size.Word, false));

            return (ushort)((_rdi & 0x00000000ffffffff) & 0x0000ffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(DI)], BitConverter.GetBytes(DI), BitConverter.GetBytes(value), nameof(DI));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(DI), Size.Word, true, DI, value));

            _rdi = (_rdi & 0x_ff_ff_ff_ff_ff_ff_00_00) + value;
        }
    }

    public byte DIL
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(DIL), Size.Byte, false));

            return (byte)(((_rdi & 0x00000000ffffffff) & 0x0000ffff) & 0x00ff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(DIL)], [DIL], [value], nameof(DIL));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisterAccess(nameof(DIL), Size.Byte, true, DIL, value));

            _rdi = (_rdi & 0x_ff_ff_ff_ff_ff_ff_ff_00) + value;
        }
    }

    public string Name => "RDI";

    public IDictionary<string, Size> RegisterNamesAndSizes => new Dictionary<string, Size>
    {
        { nameof(RDI), Size.Qword },
        { nameof(EDI), Size.Dword },
        { nameof(DI), Size.Word },
        { nameof(DIL), Size.Byte }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rdi;
}