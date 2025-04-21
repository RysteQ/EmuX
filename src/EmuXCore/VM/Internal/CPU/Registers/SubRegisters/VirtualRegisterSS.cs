using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

public class VirtualRegisterSS : IVirtualRegister
{
    public VirtualRegisterSS()
    {
        SS = (ushort)Random.Shared.Next();
    }

    public ulong Get() => SS;
    public void Set(ulong value) => SS = (ushort)value;

    public ushort SS { get; set; }

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(SS), Size.Word }
    };
}