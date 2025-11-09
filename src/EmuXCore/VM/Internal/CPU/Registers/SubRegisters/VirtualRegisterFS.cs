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

    public void Set(string register, ulong value)
    {
        register = register.ToUpper();

        if (register == nameof(FS))
        {
            FS = (ushort)value;
        }
        else
        {
            throw new ArgumentException($"Invalid register name, cannot find register of name {register} in [{nameof(FS)}]");
        }
    }

    public ushort FS
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(FS), Size.Word, false));

            return _fs;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(FS)], BitConverter.GetBytes(FS), BitConverter.GetBytes(value), nameof(FS));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(FS), Size.Word, true, FS, value));

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