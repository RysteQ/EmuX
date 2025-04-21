using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

public class VirtualRegisterES : IVirtualRegister
{
    public VirtualRegisterES()
    {
        ES = (ushort)Random.Shared.Next();
    }

    public ulong Get() => ES;
    public void Set(ulong value) => ES = (ushort)value;

    public ushort ES { get; set; }

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(ES), Size.Word }
    };
}