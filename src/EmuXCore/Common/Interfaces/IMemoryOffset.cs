using EmuXCore.Common.Enums;

namespace EmuXCore.Common.Interfaces;

/// <summary>
/// This is used to contain an entire token of a memory operand expression
/// </summary>
public interface IMemoryOffset
{
    /// <summary>
    /// The type of the memory offset, that being a label, register, displacement etc
    /// </summary>
    public MemoryOffsetType Type { get; init; }

    /// <summary>
    /// The operand (+ - * NaN) of the offset
    /// </summary>
    public MemoryOffsetOperand Operand { get; init; }

    /// <summary>
    /// The full operand of the memory offset as read from the source code
    /// </summary>
    public string FullOperand { get; init; }
}