using EmuXCore.Common.Enums;

namespace EmuXCore.VM.Interfaces.Components.Events;

/// <summary>
/// To be used with a class that inherits from the <c>EventArgs</c> class for memory access event operations
/// </summary>
public interface IStackAccess
{
    /// <summary>
    /// The size of the operation
    /// </summary>
    public Size Size { get; init; }

    /// <summary>
    /// Indicates if the operation pushed or popped from or to the stack, true if the operation was a PUSH operation, otherwise false
    /// </summary>
    public bool PushOrPop { get; init; }

    /// <summary>
    /// The value of the operation
    /// </summary>
    public ulong Value { get; init; }
}