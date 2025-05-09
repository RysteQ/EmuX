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

    string FullOperand { get; init; }
    OperandVariant Variant { get; init; }
    Size OperandSize { get; init; }
    IMemoryOffset[] Offsets { get; init; }
}