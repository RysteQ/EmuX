using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Interfaces.Components;

public interface IVirtualMemory : IVirtualComponent
{
    public Dictionary<string, IMemoryLabel> LabelMemoryLocations { get; set; }

    /// <summary>
    /// The RAM of the VMemory, must be the size of VIDEO_MEMORY + GENERAL_PURPOSE_MEMORY. It is not recommended to read / write directly to RAM, please use the Read / Write methods instead
    /// </summary>
    byte[] RAM { get; set; }

    /// <summary>
    /// The size allocated for the video memory
    /// </summary>
    uint VIDEO_MEMORY { get; }

    /// <summary>
    /// The size allocated for general purpose memory
    /// </summary>
    uint GENERAL_PURPOSE_MEMORY { get; }
}