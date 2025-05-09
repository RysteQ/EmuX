using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRBX : IVirtualRegister
{
    public VirtualRegisterRBX()
    {
        RBX = (ulong)Random.Shared.NextInt64();
    }

    public ulong Get() => RBX;
    public void Set(ulong value) => RBX = value;

    public ulong RBX { get; set; }

    public uint EBX
    {
        get => (uint)(RBX & 0x00000000ffffffff);
        set => RBX = value;
    }

    public ushort BX
    {
        get => (ushort)(EBX & 0x0000ffff);
        set => EBX = (EBX & 0xffff0000) + value;
    }

    public byte BH
    {
        get => (byte)((BX & 0xff00) >> 8);
        set => BX = (ushort)((BX & 0x00ff) + (value << 8));
    }

    public byte BL
    {
        get => (byte)(BX & 0x00ff);
        set => BX = (ushort)((BX & 0xff00) + value);
    }

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RBX), Size.Quad },
        { nameof(EBX), Size.Double },
        { nameof(BX), Size.Word },
        { nameof(BH), Size.Byte },
        { nameof(BL), Size.Byte }
    };
}