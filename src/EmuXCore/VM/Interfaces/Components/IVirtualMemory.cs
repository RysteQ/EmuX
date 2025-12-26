using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Interfaces.Components;

/// <summary>
/// The IVirtualMemory module meant to emulate the memory of a computer
/// </summary>
public interface IVirtualMemory : IVirtualComponent
{
    /// <summary>
    /// This property is used to find the labels and their properties during runtime and their name acting as a key to this collection
    /// </summary>
    IDictionary<string, IMemoryLabel> LabelMemoryLocations { get; set; }

    /// <summary>
    /// The RAM of the VMemory, must be the size of IO_MEMORY + GENERAL_PURPOSE_MEMORY. It is not recommended to read / write directly to RAM, please use the Read / Write methods instead
    /// </summary>
    byte[] RAM { get; set; }

    /// <summary>
    /// The size allocated for general purpose memory
    /// </summary>
    uint GENERAL_PURPOSE_MEMORY { get; }

    /// <summary>
    /// The size allocated for IO memory
    /// </summary>
    public uint IO_MEMORY { get; init; }
}