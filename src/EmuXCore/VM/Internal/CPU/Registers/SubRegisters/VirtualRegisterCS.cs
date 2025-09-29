using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using System.Security.Cryptography;

namespace EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

public class VirtualRegisterCS : IVirtualRegister
{
    public VirtualRegisterCS(IVirtualMachine? parentVirtualMachine = null)
    {
        _cs = (ushort)Random.Shared.Next();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => CS;
    public void Set(ulong value) => CS = (ushort)value;

    private void RegisterRegisterUpdate(byte[] currentValue, byte[] newValue, string registerName)
    {
        ParentVirtualMachine?.Actions.Add([DIFactory.GenerateIVmAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[registerName], currentValue, newValue, registerName)]);
    }

    public ushort CS
    {
        get => _cs;
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(CS), BitConverter.GetBytes(value), nameof(CS));
            _cs = value;
        }
    }

    public string Name => "CS";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(CS), Size.Word }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ushort _cs;
}