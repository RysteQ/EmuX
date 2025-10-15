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

    public ushort CS
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(CS), Size.Word));

            return CS;  
        }


        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(CS)], BitConverter.GetBytes(CS), BitConverter.GetBytes(value), nameof(CS));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(CS), Size.Word, CS, value));

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