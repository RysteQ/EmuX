using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

public class VirtualRegisterES : IVirtualRegister
{
    public VirtualRegisterES(IVirtualMachine? parentVirtualMachine = null)
    {
        _es = (ushort)Random.Shared.Next();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => ES;

    public void Set(string register, ulong value)
    {
        register = register.ToUpper();

        if (register == nameof(ES))
        {
            ES = (ushort)value;
        }
        else
        {
            throw new ArgumentException($"Invalid register name, cannot find register of name {register} in [{nameof(ES)}]");
        }
    }

    public ushort ES
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(ES), Size.Word, false));

            return _es;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(ES)], BitConverter.GetBytes(ES), BitConverter.GetBytes(value), nameof(ES));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(ES), Size.Word, true, ES, value));

            _es = value;
        }
    }

    public string Name => "ES";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(ES), Size.Word }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ushort _es;
}