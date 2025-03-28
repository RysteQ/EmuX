namespace EmuXCore.Instructions.Internal.Enums;

public enum ModEncoding : byte
{
    Memory = 0b00,
    MemoryDisplacementByte = 0b01,
    MemoryDisplacementDouble = 0b10,
    DirectRegister = 0b11,
    NaN // Internal use
}