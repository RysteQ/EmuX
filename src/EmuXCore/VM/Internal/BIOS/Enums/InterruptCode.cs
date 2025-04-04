namespace EmuXCore.VM.Internal.BIOS.Enums;

public enum InterruptCode : byte
{
    Keyboard = 0x_09,
    Video = 0x_10,
    Disk = 0x_13,
    Serial = 0x_14,
    RTC = 0x_1a
}