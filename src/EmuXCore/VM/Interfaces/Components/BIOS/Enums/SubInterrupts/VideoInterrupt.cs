namespace EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts;

/// <summary>
/// All the available video interrupts
/// </summary>
public enum VideoInterrupt : byte
{
    DrawPixel = 0x_0c,
    ReadPixel = 0x_0d,
    GetResolution = 0x_0f,
    DrawLine = 0x_50,
    DrawBox = 0x_51
}