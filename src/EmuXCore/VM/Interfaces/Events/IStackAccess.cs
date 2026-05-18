using EmuXCore.Common.Enums;

namespace EmuXCore.VM.Interfaces.Events;

/// <summary>
/// Raised for stack access operations, must be of type <see cref="EventArgs"/>.
/// </summary>
public interface IStackAccess
{
    /// <summary>
    /// The size of the operation.
    /// </summary>
    public Size Size { get; init; }

    /// <summary>
    /// True if the operation was a PUSH operation, otherwise false for the POP operation.
    /// </summary>
    public bool PushOrPop { get; init; }

    /// <summary>
    /// The value of the operation.
    /// </summary>
    public ulong Value { get; init; }
}