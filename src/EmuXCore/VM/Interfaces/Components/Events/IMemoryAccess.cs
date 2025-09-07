using EmuXCore.Common.Enums;

namespace EmuXCore.VM.Interfaces.Components.Events;

/// <summary>
/// To be used with a class that inherits from the <c>EventArgs</c> class for memory access event operations
/// </summary>
public interface IMemoryAccess
{
    /// <summary>
    /// The memory address that was accessed
    /// </summary>
    public int MemoryAddress { get; init; }

    /// <summary>
    /// The size of the operation
    /// </summary>
    public Size Size { get; init; }

    /// <summary>
    /// Indicates if the operation wrote or read from or to the memory, true if the operation was a READ operation, otherwise false
    /// </summary>
    public bool ReadOrWrite { get; init; }

    /// <summary>
    /// The previous value of the memory location
    /// </summary>
    public ulong PreviousValue { get; init; }

    /// <summary>
    /// The new value of the memory location
    /// </summary>
    public ulong NewValue { get; init; }
}
