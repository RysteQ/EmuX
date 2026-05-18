using EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts;

namespace EmuXCore.VM.Interfaces.Components.BIOS.Enums;

/// <summary>
/// All the available main interrupt codes to categorise the functionality into small singular compartments of code (an example is <see cref="VideoInterrupt"/>).
/// </summary>
public enum InterruptCode : byte
{
    Video = 0x_10,
    Disk = 0x_13,
    RTC = 0x_1a,
    Device = 0x_14
}