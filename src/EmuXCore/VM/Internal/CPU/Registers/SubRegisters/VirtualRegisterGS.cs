using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

public class VirtualRegisterGS : IVirtualRegister
{
    public VirtualRegisterGS()
    {
        GS = (ushort)Random.Shared.Next();
    }

    public ulong Get() => GS;
    public void Set(ulong value) => GS = (ushort)value;

    public ushort GS { get; set; }

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(GS), Size.Word }
    };
}