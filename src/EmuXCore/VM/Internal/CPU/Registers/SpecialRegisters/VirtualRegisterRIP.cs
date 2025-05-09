using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

public class VirtualRegisterRIP : IVirtualRegister
{
    public ulong Get() => RIP;
    public void Set(ulong value) => RIP = value;

    public ulong RIP { get; set; }

    public uint EIP
    {
        get => (uint)(RIP & 0x00000000ffffffff);
        set => RIP = value;
    }

    public ushort IP
    {
        get => (ushort)(EIP & 0x0000ffff);
        set => EIP = (EIP & 0xffff0000) + value;
    }

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RIP), Size.Quad },
        { nameof(EIP), Size.Double },
        { nameof(IP), Size.Word }
    };
}