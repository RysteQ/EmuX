using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

public class VirtualRegisterFS : IVirtualRegister
{
    public VirtualRegisterFS()
    {
        FS = (ushort)Random.Shared.Next();
    }

    public ulong Get() => FS;
    public void Set(ulong value) => FS = (ushort)value;

    public ushort FS { get; set; }

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(FS), Size.Word }
    };
}