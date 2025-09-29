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

    private void RegisterRegisterUpdate(byte[] currentValue, byte[] newValue, string registerName)
    {
        ParentVirtualMachine?.Actions.Add([DIFactory.GenerateIVmAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[registerName], currentValue, newValue, registerName)]);
    }

    public ushort DS
    {
        get => _ds;
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(DS), BitConverter.GetBytes(value), nameof(DS));
            _ds = value;
        }
    }

    public string Name => "SS";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(DS), Size.Word }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ushort _ds;
}