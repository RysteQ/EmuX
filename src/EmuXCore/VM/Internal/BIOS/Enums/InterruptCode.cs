namespace EmuXCore.VM.Internal.BIOS.Enums;

public enum InterruptCode : byte
{
    Keyboard = 0x_09,
    Disk = 0x_13,
    RTC = 0x_1a
}