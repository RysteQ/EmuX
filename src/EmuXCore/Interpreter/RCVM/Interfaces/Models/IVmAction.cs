using EmuXCore.Common.Enums;
using EmuXCore.Interpreter.RCVM.Enums;

namespace EmuXCore.Interpreter.RCVM.Interfaces.Models;

/// <summary>
/// Serves as a way to record all of the actions taken to modify the state of the VM
/// </summary>
public interface IVmAction
{
    /// <summary>
    /// The action method
    /// </summary>
    public VmActionCategory Action { get; init; }

    /// <summary>
    /// The size of the action
    /// </summary>
    public Size Size { get; init; }

    /// <summary>
    /// The name of the register
    /// </summary>
    public string? RegisterName { get; init; }

    /// <summary>
    /// The memory location the action has taken place at <br/>
    /// Null if the action does not reference any memory but it instead references a register
    /// </summary>
    public int? MemoryPointer { get; init; }

    /// <summary>
    /// The previous value <br/>
    /// It is an array to account for disk actions
    /// </summary>
    public byte[] PreviousValue { get; init; }
}