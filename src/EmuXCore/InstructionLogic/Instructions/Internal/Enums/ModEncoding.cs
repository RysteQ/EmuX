namespace EmuXCore.InstructionLogic.Instructions.Internal.Enums;

public enum ModEncoding : byte
{
    Memory = 0b_00,
    MemoryDisplacementByte = 0b_01,
    MemoryDisplacementDouble = 0b_10,
    DirectRegister = 0b_11,
    NaN // Internal use
}