using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRDI : IVirtualRegister
{
    public VirtualRegisterRDI()
    {
        RDI = (ulong)Random.Shared.NextInt64();
    }

    public ulong Get() => RDI;
    public void Set(ulong value) => RDI = value;

    public ulong RDI { get; set; }

    public uint EDI
    {
        get => (uint)(RDI & 0x00000000ffffffff);
        set => RDI = value;
    }

    public ushort DI
    {
        get => (ushort)(EDI & 0x0000ffff);
        set => EDI = (EDI & 0xffff0000) + value;
    }

    public byte DIL
    {
        get => (byte)(DI & 0x00ff);
        set => DI = (ushort)((DI & 0xff00) + value);
    }

    public string Name => "RDI";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RDI), Size.Qword },
        { nameof(EDI), Size.Dword },
        { nameof(DI), Size.Word },
        { nameof(DIL), Size.Byte }
    };
}