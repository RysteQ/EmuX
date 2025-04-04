namespace EmuXCore.VM.Internal.BIOS.Enums.SubInterrupts;

public enum VideoInterrupt : byte
{
    SetCursorShape = 0x_01,
    SetCursorPosition = 0x_02,
    GetCursorPositionAndShape = 0x_03,
    ScrollUpp = 0x_06,
    ScrollDown = 0x_07,
    ReadCharacterAtCursorPosition = 0x_08,
    WriteCharacterAtCursorPosition = 0x_09,
    SetForegroundColour = 0x_10,
    SetBackgroundColour = 0x_11,
    WritePixel = 0x_12,
    ReadPixel = 0x_13,
    WriteString = 0x_0a,
    ClearScreen = 0x_0b,
}