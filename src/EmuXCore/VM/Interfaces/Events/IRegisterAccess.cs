using EmuXCore.Common.Enums;

namespace EmuXCore.VM.Interfaces.Events;

/// <summary>
/// To be used with a class that inherits from the <c>EventArgs</c> class for register access event operations
/// </summary>
public interface IRegisterAccess
{
    /// <summary>
    /// The name of the register that got accessed
    /// </summary>
    public string RegisterName { get; init; }

    /// <summary>
    /// The size of the register that got accessed
    /// </summary>
    public Size Size { get; init; }

    /// <summary>
    /// True if the access was a write access, otherwise false
    /// </summary>
    public bool Write { get; set; }

    /// <summary>
    /// The previous value of the register <br/>
    /// Null if the action did not write to the register but read from it
    /// </summary>
    public ulong PreviousValue { get; init; }

    /// <summary>
    /// The new value of the register <br/>
    /// Null if the action did not write to the register but read from it
    /// </summary>
    public ulong NewValue { get; init; }
}