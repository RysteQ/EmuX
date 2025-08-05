namespace EmuXCore.VM.Interfaces.Components.Internal;

/// <summary>
/// The IMemoryLabel is used to find a label and its address when a function that references it memory location is called
/// </summary>
public interface IMemoryLabel
{
    /// <summary>
    /// The name of the label
    /// </summary>
    string LabelName { get; init; }

    /// <summary>
    /// The address of the label in the IVirtualMemory module
    /// </summary>
    int Address { get; init; }

    /// <summary>
    /// The line the label can be found in the source code
    /// </summary>
    int Line { get; init; }
}