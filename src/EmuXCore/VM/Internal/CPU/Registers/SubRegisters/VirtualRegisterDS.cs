using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

public class VirtualRegisterDS : IVirtualRegister
{
    public VirtualRegisterDS()
    {
        DS = (ushort)Random.Shared.Next();
    }

    public ulong Get() => DS;
    public void Set(ulong value) => DS = (ushort)value;

    public ushort DS { get; set; }

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(DS), Size.Word }
    };
}