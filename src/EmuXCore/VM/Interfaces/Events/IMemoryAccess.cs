using EmuXCore.Common.Enums;

namespace EmuXCore.VM.Interfaces.Events;

/// <summary>
/// Raised for memory access operations, must be of type <see cref="EventArgs"/>.
/// </summary>
public interface IMemoryAccess
{
    /// <summary>
    /// The memory address that was accessed.
    /// </summary>
    public int MemoryAddress { get; init; }

    /// <summary>
    /// The size of the operation.
    /// </summary>
    public Size Size { get; init; }

    /// <summary>
    /// True if the operation was a READ operation, otherwise false for the WRITE operation.
    /// </summary>
    public bool ReadOrWrite { get; init; }

    /// <summary>
    /// The previous value of the memory location.
    /// </summary>
    public ulong PreviousValue { get; init; }

    /// <summary>
    /// The new value of the memory location.
    /// </summary>
    public ulong NewValue { get; init; }
}
