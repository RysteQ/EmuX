namespace EmuXCore.VM.Internal.BIOS.Enums;

/// <summary>
/// All the available main interrupt codes to categorise the functionality into small singular compartments of code
/// </summary>
public enum InterruptCode : byte
{
    Video = 0x_10,
    Disk = 0x_13,
    RTC = 0x_1a
}