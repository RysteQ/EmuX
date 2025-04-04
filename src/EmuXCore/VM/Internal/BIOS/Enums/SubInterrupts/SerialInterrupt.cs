namespace EmuXCore.VM.Internal.BIOS.Enums.SubInterrupts;

public enum SerialInterrupt : byte
{
    InitialiseSerialPort = 0x_00,
    TransmitCharacter = 0x_01,
    ReceiveCharacter = 0x_02,
    SerialPortStatus = 0x_03,
}