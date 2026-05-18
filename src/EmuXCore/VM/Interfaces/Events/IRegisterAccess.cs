using EmuXCore.Common.Enums;

namespace EmuXCore.VM.Interfaces.Events;

/// <summary>
/// Raised for register access operations, must be of type <see cref="EventArgs"/>.
/// </summary>
public interface IRegisterAccess
{
    /// <summary>
    /// The name of the register that was accessed.
    /// </summary>
    public string RegisterName { get; init; }

    /// <summary>
    /// The size of the register that was accessed.
    /// </summary>
    public Size Size { get; init; }

    /// <summary>
    /// True if the operation was a READ operation, otherwise false for the WRITE operation.
    /// </summary>
    public bool Write { get; set; }

    /// <summary>
    /// The previous value of the register.
    /// </summary>
    public ulong PreviousValue { get; init; }

    /// <summary>
    /// The new value of the register.
    /// </summary>
    public ulong NewValue { get; init; }
}