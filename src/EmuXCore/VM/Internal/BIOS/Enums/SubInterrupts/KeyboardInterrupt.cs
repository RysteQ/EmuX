namespace EmuXCore.VM.Internal.BIOS.Enums.SubInterrupts;

public enum KeyboardInterrupt : byte
{
    ReadCharacterBlocking = 0x_00,
    ReadCharacterNonBlocking = 0x_01
}