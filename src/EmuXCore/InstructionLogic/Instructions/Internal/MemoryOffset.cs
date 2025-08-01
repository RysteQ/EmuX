using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;

namespace EmuXCore.InstructionLogic.Instructions.Internal;

public record MemoryOffset : IMemoryOffset
{
    public MemoryOffset(MemoryOffsetType type, MemoryOffsetOperand operand, string fullOperand)
    {
        Type = type;
        Operand = operand;
        FullOperand = fullOperand;
    }

    public MemoryOffsetType Type { get; init; }
    public MemoryOffsetOperand Operand { get; init; }
    public string FullOperand { get; init; }
}