using EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts;

namespace EmuXCore.VM.Interfaces.Events;

/// <summary>
/// Raised for GPU access operations, must be of type <see cref="EventArgs"/>.
/// </summary>
public interface IVideoCardAccess
{
    /// <summary>
    /// The shape that was drawn during the GPU access operation.
    /// </summary>
    VideoInterrupt Shape { get; init; }

    /// <summary>
    /// The start of the X coordinate for the shape that was drawn during the GPU access operation.
    /// </summary>
    ushort StartX { get; init; }

    /// <summary>
    /// The start of the Y coordinate for the shape that was drawn during the GPU access operation.
    /// </summary>
    ushort StartY { get; init; }

    /// <summary>
    /// The end of the X coordinate for the shape that was drawn during the GPU access operation.
    /// </summary>
    ushort EndX { get; init; }

    /// <summary>
    /// The end of the Y coordinate for the shape that was drawn during the GPU access operation.
    /// </summary>
    ushort EndY { get; init; }

    /// <summary>
    /// The RED in RGB value.
    /// </summary>
    byte Red { get; init; }

    /// <summary>
    /// The GREEN in RGB value.
    /// </summary>
    byte Green { get; init; }

    /// <summary>
    /// The BLUE in RGB value.
    /// </summary>
    byte Blue { get; init; }
}