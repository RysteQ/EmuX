using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces;

namespace EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

public class VirtualRegisterEFLAGS : IVirtualRegister
{
    public VirtualRegisterEFLAGS()
    {
        EFLAGS = 0x00000002;
    }

    public ulong Get() => EFLAGS;
    public void Set(ulong value) => EFLAGS = (uint)value;

    public uint EFLAGS { get; set; }

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