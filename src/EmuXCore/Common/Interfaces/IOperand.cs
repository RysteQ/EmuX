using EmuXCore.Common.Enums;

namespace EmuXCore.Common.Interfaces;

/// <summary>
/// This is the operand structure of all operands for all instructions
/// </summary>
public interface IOperand
{
    /// <summary>
    /// Checks if the Offsets property has proper values or not given the following allowed patterns <br/>
    /// LABEL <br/>
    /// REG <br/>
    /// INT <br/>
    /// LABEL + INT <br/>
    /// LABEL + REG + INT <br/>
    /// LABEL + REG + REG + INT <br/>
    /// LABEl + REG + REG * SCALE + INT <br/>
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