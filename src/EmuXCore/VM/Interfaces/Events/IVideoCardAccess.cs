using EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts;

namespace EmuXCore.VM.Interfaces.Events;

public interface IVideoCardAccess
{
    VideoInterrupt Shape { get; init; }
    ushort StartX { get; init; }
    ushort StartY { get; init; }
    ushort EndX { get; init; }
    ushort EndY { get; init; }
    byte Red { get; init; }
    byte Green { get; init; }
    byte Blue { get; init; }
}