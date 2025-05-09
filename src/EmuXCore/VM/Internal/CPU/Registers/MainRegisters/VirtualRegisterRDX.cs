using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRDX : IVirtualRegister
{
    public VirtualRegisterRDX()
    {
        RDX = (ulong)Random.Shared.NextInt64();
    }

    public ulong Get() => RDX;
    public void Set(ulong value) => RDX = value;

    public ulong RDX { get; set; }

    public uint EDX
    {
        get => (uint)(RDX & 0x00000000ffffffff);
        set => RDX = value;
    }

    public ushort DX
    {
        get => (ushort)(EDX & 0x0000ffff);
        set => EDX = (EDX & 0xffff0000) + value;
    }

    public byte DH
    {
        get => (byte)((DX & 0xff00) >> 8);
        set => DX = (ushort)((DX & 0x00ff) + (value << 8));
    }

    public byte DL
    {
        get => (byte)(DX & 0x00ff);
        set => DX = (ushort)((DX & 0xff00) + value);
    }

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RDX), Size.Quad },
        { nameof(EDX), Size.Double },
        { nameof(DX), Size.Word },
        { nameof(DH), Size.Byte },
        { nameof(DL), Size.Byte }
    };
}