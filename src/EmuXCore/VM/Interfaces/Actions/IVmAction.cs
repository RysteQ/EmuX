using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces.Exceptions;

namespace EmuXCore.VM.Interfaces.Actions;

/// <summary>
/// Records all of the actions taken to modify the state of the <see cref="IVirtualMachine"/>.
/// </summary>
public interface IVmAction
{
    /// <summary>
    /// Undos the action taken.
    /// </summary>
    /// <param name="virtualMachineInstance">The virtual machine instance to undo the action.</param>
    /// <exception cref="InvalidActionException" />
    void Undo(IVirtualMachine virtualMachineInstance);

    /// <summary>
    /// Redos the actions taken.
    /// </summary>
    /// <param name="virtualMachineInstance">The virtual machine instance to redo the action.</param>
    /// <exception cref="InvalidActionException" />
    void Redo(IVirtualMachine virtualMachineInstance);

    /// <summary>
    /// The action method.
    /// </summary>
    public VmActionCategory Action { get; init; }

    /// <summary>
    /// The size of the action.
    /// </summary>
    public Size Size { get; init; }

    /// <summary>
    /// The name of the register if a register was modified. <br/>
    /// Null if the action did not modify anything in any register.
    /// </summary>
    public string? RegisterName { get; init; }

    /// <summary>
    /// The memory location the action has taken place at. <br/>
    /// Null if the action did not modify anything in memory.
    /// </summary>
    public int? MemoryPointer { get; init; }

    /// <summary>
    /// The device ID the action refers to. <br/>
    /// Null if the action did not modify anything in any connected device.
    /// </summary>
    public int? DeviceId { get; init; }

    /// <summary>
    /// The disk ID the action refers to. <br/>
    /// Null if the action did not modify anything in any connected disk.
    /// </summary>
    public byte? DiskId { get; init; }

    /// <summary>
    /// The previous value. <br/>
    /// Note: It is an array to account for disk related actions.
    /// </summary>
    public byte[] PreviousValue { get; init; }

    /// <summary>
    /// The new value. <br/>
    /// Note: It is an array to account for disk related actions.
    /// </summary>
    public byte[] NewValue { get; init; }
}