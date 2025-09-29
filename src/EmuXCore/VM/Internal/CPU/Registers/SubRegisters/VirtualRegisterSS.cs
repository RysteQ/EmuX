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

    private void RegisterRegisterUpdate(byte[] currentValue, byte[] newValue, string registerName)
    {
        ParentVirtualMachine?.Actions.Add([DIFactory.GenerateIVmAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[registerName], currentValue, newValue, registerName)]);
    }

    public ushort SS
    {
        get => _ss;
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(SS), BitConverter.GetBytes(value), nameof(SS));
            _ss = value;
        }
    }

    public string Name => "GS";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(SS), Size.Word }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ushort _ss;
}