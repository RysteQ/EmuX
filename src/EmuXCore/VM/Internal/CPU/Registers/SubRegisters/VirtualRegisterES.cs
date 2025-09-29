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

    private void RegisterRegisterUpdate(byte[] currentValue, byte[] newValue, string registerName)
    {
        ParentVirtualMachine?.Actions.Add([DIFactory.GenerateIVmAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[registerName], currentValue, newValue, registerName)]);
    }

    public ushort ES
    {
        get => _es;
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(ES), BitConverter.GetBytes(value), nameof(ES));
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