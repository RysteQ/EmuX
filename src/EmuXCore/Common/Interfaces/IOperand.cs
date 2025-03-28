using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces;

namespace EmuXCore.Common.Interfaces;

// TODO - Add IsMemoryPointerValid method

public interface IOperand
{
    /// <summary>
    /// Checks if the compination of register / offsets / memory labels is valid when the operand is of type memory pointer
    /// </summary>
    /// <returns>True if the memory pointer parameters are valid</returns>
    bool IsMemoryPointerValid();

    string FullOperand { get; init; }
    OperandVariant Variant { get; init; }
    Size OperandSize { get; init; }
    int[] Offsets { get; init; }
    string MemoryLabel { get; init; }
}