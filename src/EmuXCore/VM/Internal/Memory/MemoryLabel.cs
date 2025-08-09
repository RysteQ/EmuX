using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.Memory;

public record MemoryLabel : IMemoryLabel
{
    public MemoryLabel(string labelName, int address, int line)
    {
        LabelName = labelName;
        Address = address;
        Line = line;
    }

    public string LabelName { get; init; }
    public int Address { get; init; }
    public int Line { get; init; }
}