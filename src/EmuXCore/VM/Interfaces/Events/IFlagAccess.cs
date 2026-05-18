using EmuXCore.VM.Enums;

namespace EmuXCore.VM.Interfaces.Events;

/// <summary>
/// Raised for flag access operations, must be of type <see cref="EventArgs"/>.
/// </summary>
public interface IFlagAccess
{
    /// <summary>
    /// The flag that was accessed.
    /// </summary>
    public EFlags FlagAccessed { get; init; }

    /// <summary>
    /// True if the operation was a READ operation, otherwise false for the WRITE operation.
    /// </summary>
    public bool ReadOrWrite { get; init; }

    /// <summary>
    /// The previous value of the flag.
    /// </summary>
    public bool PreviousValue { get; init; }

    /// <summary>
    /// The second previous value for the flag, it is to be used only when accessing a 2 bit flag like IOPL.
    /// </summary>
    public bool AuxiliaryPreviousValue { get; init; }

    /// <summary>
    /// The new value of the flag.
    /// </summary>
    public bool NewValue { get; init; }

    /// <summary>
    /// The second new value for the flag, it is to be used only when accessing a 2 bit flag like IOPL.
    /// </summary>
    public bool AuxiliaryNewValue { get; init; }
}
