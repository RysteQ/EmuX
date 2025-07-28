using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;

namespace EmuXCore.InstructionLogic.Instructions.Internal;

public class MemoryOffset(MemoryOffsetType type, MemoryOffsetOperand operand, string fullOperand) : IMemoryOffset
{
    public MemoryOffsetType Type { get; init; } = type;
    public MemoryOffsetOperand Operand { get; init; } = operand;
    public string FullOperand { get; init; } = fullOperand;
}