namespace EmuXCore.VM.Internal.BIOS.Enums.SubInterrupts;

public enum DiskInterrupt : byte
{
    ReadTrack = 0x_02,
    WriteTrack = 0x_03
}