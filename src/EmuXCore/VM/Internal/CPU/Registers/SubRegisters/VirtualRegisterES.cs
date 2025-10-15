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
    public void Set(ulong value) => ES = (ushort)value;

    public ushort ES
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(ES), Size.Word));

            return _es;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(ES)], BitConverter.GetBytes(ES), BitConverter.GetBytes(value), nameof(ES));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(ES), Size.Word, ES, value));

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