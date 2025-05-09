using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers;

public class VirtualRegisterRBP : IVirtualRegister
{
    public ulong Get() => RBP;
    public void Set(ulong value) => RBP = value;

    public ulong RBP { get; set; }

    public uint EBP
    {
        get => (uint)(RBP & 0x00000000ffffffff);
        set => RBP = value;
    }

    public ushort BP
    {
        get => (ushort)(EBP & 0x0000ffff);
        set => EBP = (EBP & 0xffff0000) + value;
    }

    public byte BPL
    {
        get => (byte)(BP & 0x00ff);
        set => BP = (ushort)((BP & 0xff00) + value);
    }

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RBP), Size.Quad },
        { nameof(EBP), Size.Double },
        { nameof(BP), Size.Word },
        { nameof(BPL), Size.Byte }
    };
}