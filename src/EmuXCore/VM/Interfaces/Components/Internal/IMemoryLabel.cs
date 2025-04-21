namespace EmuXCore.VM.Interfaces.Components.Internal;

public interface IMemoryLabel
{
    string LabelName { get; init; }
    int Address { get; init; }
    int Line { get; init; }
}