namespace EmuXCore.VM.Internal.BIOS.Enums.SubInterrupts;

public enum RTCInterrupt : byte
{
    ReadSystemClock = 0x_00,
    SetSystemClock = 0x_01,
    ReadRTC = 0x_02,
    SetRTC = 0x_03
}