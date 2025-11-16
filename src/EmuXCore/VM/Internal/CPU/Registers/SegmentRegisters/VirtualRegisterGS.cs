using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SegmentRegisters;

public class VirtualRegisterGS : IVirtualRegister
{
    public VirtualRegisterGS(IVirtualMachine? parentVirtualMachine = null)
    {
        _gs = (ushort)Random.Shared.Next();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => GS;

    public void Set(string register, ulong value)
    {
        register = register.ToUpper();

        if (register == nameof(GS))
        {
            GS = (ushort)value;
        }
        else
        {
            throw new ArgumentException($"Invalid register name, cannot find register of name {register} in [{nameof(GS)}]");
        }
    }

    public ushort GS
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(GS), Size.Word, false));

            return _gs;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(GS)], BitConverter.GetBytes(GS), BitConverter.GetBytes(value), nameof(GS));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(GS), Size.Word, true, GS, value));

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