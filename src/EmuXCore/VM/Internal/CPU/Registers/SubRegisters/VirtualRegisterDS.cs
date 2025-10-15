using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

public class VirtualRegisterDS : IVirtualRegister
{
    public VirtualRegisterDS(IVirtualMachine? parentVirtualMachine = null)
    {
        _ds = (ushort)Random.Shared.Next();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => DS;
    public void Set(ulong value) => DS = (ushort)value;

    public ushort DS
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(DS), Size.Word));

            return _ds;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(DS)], BitConverter.GetBytes(DS), BitConverter.GetBytes(value), nameof(DS));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(DS), Size.Word, DS, value));

            _ds = value;
        }
    }

    public string Name => "DS";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(DS), Size.Word }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ushort _ds;
}