namespace EmuXCore.VM.Internal.BIOS.Enums.SubInterrupts;

/// <summary>
/// All the available disk interrupts
/// </summary>
public enum DiskInterrupt : byte
{
    ReadTrack = 0x_02,
    WriteTrack = 0x_03
}