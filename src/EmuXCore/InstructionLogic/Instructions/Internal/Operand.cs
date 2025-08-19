using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;

namespace EmuXCore.InstructionLogic.Instructions.Internal;

public class Operand : IOperand
{
    public Operand(string fullOperand, OperandVariant variant, Size operandSize, IMemoryOffset[] offsets)
    {
        FullOperand = fullOperand;
        Variant = variant;
        OperandSize = operandSize;
        Offsets = offsets;
    }

    public bool AreMemoryOffsetValid()
    {
        Dictionary<MemoryOffsetType, int> offsetTypeAndMaximumAllowedOccurences = new()
        {
            { MemoryOffsetType.Label, 1},
            { MemoryOffsetType.Register, 2},
            { MemoryOffsetType.Integer, 10}, // Can be raised to whatever (I don't care), but if you have more than 10 integer offset rethinks your life decisions honestly
            { MemoryOffsetType.NaN, 1}
        };

        if (Offsets.Length == 0)
        {
            return true;
        }

        if (Offsets.Length == 1 && (Offsets.First().Type == MemoryOffsetType.Label || Offsets.First().Type == MemoryOffsetType.Register || Offsets.First().Type == MemoryOffsetType.Integer))
        {
            return true;
        }

        foreach (KeyValuePair<MemoryOffsetType, int> offsetType in offsetTypeAndMaximumAllowedOccurences)
        {
            if (Offsets.Where(selectedOffset => selectedOffset.Type == offsetType.Key).Count() > offsetType.Value)
            {
                return false;
            }
        }

        return Offsets.Count(selectedOffset => selectedOffset.Operand == MemoryOffsetOperand.Multiplication) <= 1;
    }

    public string FullOperand { get; init; }
    public OperandVariant Variant { get; init; }
    public Size OperandSize { get; init; }
    public IMemoryOffset[] Offsets { get; init; }
}