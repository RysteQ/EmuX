using EmuXCore.Common.Enums;

namespace EmuXCore.Common.Interfaces;

public interface IOperand
{
    /// <summary>
    /// Checks if the Offsets property has proper values or not given the following allowed patterns
    /// LABEL
    /// REG
    /// INT
    /// LABEL + INT
    /// LABEL + REG + INT
    /// LABEL + REG + REG + INT
    /// LABEl + REG + REG * SCALE + INT
    /// </summary>
    /// <returns>True if the offsets are valid, otherwise false</returns>
    bool AreMemoryOffsetValid();

    /// <summary>
    /// The full operand
    /// </summary>
    string FullOperand { get; init; }

    /// <summary>
    /// The variant of the operand
    /// </summary>
    OperandVariant Variant { get; init; }

    /// <summary>
    /// The size of the operand
    /// </summary>
    Size OperandSize { get; init; }

    /// <summary>
    /// The offsets of the operand, used only for memory addressing
    /// </summary>
    IMemoryOffset[] Offsets { get; init; }
}