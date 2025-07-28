using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRSI : IVirtualRegister
{
    public VirtualRegisterRSI()
    {
        RSI = (ulong)Random.Shared.NextInt64();
    }

    public ulong Get() => RSI;
    public void Set(ulong value) => RSI = value;

    public ulong RSI { get; set; }

    public uint ESI
    {
        get => (uint)(RSI & 0x00000000ffffffff);
        set => RSI = value;
    }

    public ushort SI
    {
        get => (ushort)(ESI & 0x0000ffff);
        set => ESI = (ESI & 0xffff0000) + value;
    }

    public byte SIL
    {
        get => (byte)(SI & 0x00ff);
        set => SI = (ushort)((SI & 0xff00) + value);
    }

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RSI), Size.Qword },
        { nameof(ESI), Size.Dword },
        { nameof(SI), Size.Word },
        { nameof(SIL), Size.Byte }
    };
}