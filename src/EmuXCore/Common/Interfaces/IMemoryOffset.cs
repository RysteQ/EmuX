using EmuXCore.Common.Enums;

namespace EmuXCore.Common.Interfaces;

public interface IMemoryOffset
{
    public MemoryOffsetType Type { get; init; }
    public MemoryOffsetOperand Operand { get; init; }
    public string FullOperand { get; init; }
}