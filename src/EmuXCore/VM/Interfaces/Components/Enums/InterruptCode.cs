namespace EmuXCore.VM.Internal.BIOS.Enums;

public enum InterruptCode : byte
{
    Video = 0x_10,
    Disk = 0x_13,
    RTC = 0x_1a
}