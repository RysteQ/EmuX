using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

public class VirtualRegisterEFLAGS : IVirtualRegister
{
    public VirtualRegisterEFLAGS()
    {
        RFLAGS = 0x0000000000000002;
    }

    public ulong Get() => RFLAGS;
    public void Set(ulong value) => RFLAGS = value;

    public ulong RFLAGS { get; set; }

    public uint EFLAGS
    {
        get => (uint)(RFLAGS & 0x00000000ffffffff);
        set => RFLAGS = value;
    }

    public ushort FLAGS
    {
        get => (ushort)(EFLAGS & 0x0000ffff);
        set => EFLAGS = (EFLAGS & 0xffff0000) + value;
    }

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(EFLAGS), Size.Quad },
        { nameof(FLAGS), Size.Double }
    };
}