using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

public class VirtualRegisterSS : IVirtualRegister
{
    public VirtualRegisterSS(IVirtualMachine? parentVirtualMachine = null)
    {
        _ss = (ushort)Random.Shared.Next();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => SS;
    public void Set(ulong value) => SS = (ushort)value;

    public ushort SS
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(SS), Size.Word, false));

            return _ss;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(SS)], BitConverter.GetBytes(SS), BitConverter.GetBytes(value), nameof(SS));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(SS), Size.Word, true, SS, value));

            _ss = value;
        }
    }

    public string Name => "SS";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(SS), Size.Word }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ushort _ss;
}