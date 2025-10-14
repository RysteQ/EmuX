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
    /// The previous value of the register
    /// </summary>
    public ulong PreviousValue { get; init; }
    
    /// <summary>
    /// The new value of the register
    /// </summary>
    public ulong NewValue { get; init; }
}