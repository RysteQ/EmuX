using EmuXCore.VM.Interfaces;

namespace EmuXCore.VM.Internal.Memory;

public class MemoryLabel(string labelName, int address, int line) : IMemoryLabel
{
    public string LabelName { get; init; } = labelName;
    public int Address { get; init; } = address;
    public int Line { get; init; } = line;
}