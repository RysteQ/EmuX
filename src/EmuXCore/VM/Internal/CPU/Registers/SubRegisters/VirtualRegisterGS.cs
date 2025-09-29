using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

public class VirtualRegisterGS : IVirtualRegister
{
    public VirtualRegisterGS(IVirtualMachine? parentVirtualMachine = null)
    {
        _gs = (ushort)Random.Shared.Next();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => GS;
    public void Set(ulong value) => GS = (ushort)value;

    private void RegisterRegisterUpdate(byte[] currentValue, byte[] newValue, string registerName)
    {
        ParentVirtualMachine?.Actions.Add([DIFactory.GenerateIVmAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[registerName], currentValue, newValue, registerName)]);
    }

    public ushort GS
    {
        get => _gs;
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(GS), BitConverter.GetBytes(value), nameof(GS));
            _gs = value;
        }
    }

    public string Name => "GS";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(GS), Size.Word }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ushort _gs;
}