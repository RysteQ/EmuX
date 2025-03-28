using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;

namespace EmuXCore.Instructions.Internal;

public class Operand(string fullOperand, OperandVariant variant, Size operandSize, int[] offsets, string memoryLabel) : IOperand
{
    public bool IsMemoryPointerValid()
    {
        throw new NotImplementedException();
    }

    public string FullOperand { get; init; } = fullOperand;
    public OperandVariant Variant { get; init; } = variant;
    public Size OperandSize { get; init; } = operandSize;
    public int[] Offsets { get; init; } = offsets;
    public string MemoryLabel { get; init; } = memoryLabel;
}