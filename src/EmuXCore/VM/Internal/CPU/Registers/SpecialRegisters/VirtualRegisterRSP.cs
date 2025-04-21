using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers;

public class VirtualRegisterRSP : IVirtualRegister
{
    public ulong Get() => RSP;
    public void Set(ulong value) => RSP = value;

    public ulong RSP { get; set; }

    public uint ESP
    {
        get => (uint)(RSP & 0x00000000ffffffff);
        set => RSP = value;
    }

    public ushort SP
    {
        get => (ushort)(ESP & 0x0000ffff);
        set => ESP = (ESP & 0xffff0000) + value;
    }

    public byte SPL
    {
        get => (byte)(SP & 0x00ff);
        set => SP = (ushort)((SP & 0xff00) + value);
    }

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RSP), Size.Quad },
        { nameof(ESP), Size.Double },
        { nameof(SP), Size.Word },
        { nameof(SPL), Size.Byte }
    };
}