namespace EmuXCore.VM.Interfaces.Components.Internal;

/// <summary>
/// The IMemoryLabel to store the properties of a label.
/// </summary>
public interface IMemoryLabel
{
    /// <summary>
    /// The name of the label.
    /// </summary>
    string LabelName { get; init; }

    /// <summary>
    /// The address of the label in the <see cref="IVirtualMemory"/> module.
    /// </summary>
    int Address { get; init; }

    /// <summary>
    /// The line the label can be found in the source code.
    /// </summary>
    int Line { get; init; }
}