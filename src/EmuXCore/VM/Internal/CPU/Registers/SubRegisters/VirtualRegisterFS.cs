using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

public class VirtualRegisterFS : IVirtualRegister
{
    public VirtualRegisterFS(IVirtualMachine? parentVirtualMachine = null)
    {
        _fs = (ushort)Random.Shared.Next();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => FS;
    public void Set(ulong value) => FS = (ushort)value;

    public ushort FS
    {
        get => _fs;
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(FS)], BitConverter.GetBytes(FS), BitConverter.GetBytes(value), nameof(FS));
            _fs = value;
        }
    }

    public string Name => "FS";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(FS), Size.Word }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ushort _fs;
}