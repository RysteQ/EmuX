using EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts;
using EmuXCore.VM.Interfaces.Events;

namespace EmuXCore.VM.Events;

public class VideoCardAccess : EventArgs, IVideoCardAccess
{
    public VideoCardAccess(VideoInterrupt shape, ushort startX, ushort startY, ushort endX, ushort endY, byte red, byte green, byte blue)
    {
        Shape = shape;
        StartX = startX;
        StartY = startY;
        EndX = endX;
        EndY = endY;
        Red = red;
        Green = green;
        Blue = blue;
    }

    public VideoInterrupt Shape { get; init; }
    public ushort StartX { get; init; }
    public ushort StartY { get; init; }
    public ushort EndX { get; init; }
    public ushort EndY { get; init; }
    public byte Red { get; init; }
    public byte Green { get; init; }
    public byte Blue { get; init; }
}