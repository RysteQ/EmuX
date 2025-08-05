namespace EmuXCore.Common.Enums;

/// <summary>
/// Used for calculating the memory offset by indicating the operations needed for the expression
/// </summary>
public enum MemoryOffsetOperand
{
    Addition,
    Subtraction,
    Multiplication,

    // Indicates the first offset to be used as a base
    NaN
}