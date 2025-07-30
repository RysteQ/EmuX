namespace EmuXCore.Common.Enums;

/// <summary>
/// Describes the size of anything in the codebase, either that is an operand or that is a write instruction
/// </summary>
public enum Size : byte
{
    Byte = 1,
    Word = 2,
    Dword = 4,
    Qword = 8,

    NaN = 0
}