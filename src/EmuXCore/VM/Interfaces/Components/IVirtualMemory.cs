using System.Collections.Generic;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Interfaces.Components;

/// <summary>
/// The IVirtualMemory responsible for emulating the memory of a real machine.
/// </summary>
public interface IVirtualMemory : IVirtualComponent
{
    /// <summary>
    /// Used to find the labels using their name as a key to retrieve their properties during runtime.
    /// </summary>
    IDictionary<string, IMemoryLabel> LabelMemoryLocations { get; set; }

    /// <summary>
    /// The RAM of the VMemory module, must be the size of IO_MEMORY + GENERAL_PURPOSE_MEMORY. <br/>
    /// It is not recommended to read / write directly to RAM, please use the Read / Write methods instead of the <see cref="IVirtualMachine" />
    /// </summary>
    byte[] RAM { get; set; }

    /// <summary>
    /// The size allocated for general purpose memory.
    /// </summary>
    uint GENERAL_PURPOSE_MEMORY { get; }

    /// <summary>
    /// The size allocated for IO memory.
    /// </summary>
    public uint IO_MEMORY { get; init; }
}