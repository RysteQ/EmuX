using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;

namespace EmuXCore.VM.Interfaces.Actions;

/// <summary>
/// Serves as a way to record all of the actions taken to modify the state of the VM
/// </summary>
public interface IVmAction
{
    /// <summary>
    /// Undos the action taken
    /// </summary>
    /// <param name="virtualMachineInstance">The virtual machine instance to undo the action</param>
    void Undo(IVirtualMachine virtualMachineInstance);
    
    /// <summary>
    /// Redos the actions taken
    /// </summary>
    /// <param name="virtualMachineInstance">The virtual machine instance to redo the action</param>
    void Redo(IVirtualMachine virtualMachineInstance);

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
    /// The device ID the action refers to <br/>
    /// Null if it does not refer to a device
    /// </summary>
    public int? DeviceId { get; init; }

    /// <summary>
    /// The disk ID the action refers to <br/>
    /// Null if it does not refer to a disk
    /// </summary>
    public byte? DiskId { get; init; }

    /// <summary>
    /// The previous value <br/>
    /// It is an array to account for disk actions
    /// </summary>
    public byte[] PreviousValue { get; init; }

    /// <summary>
    /// The new value <br/>
    /// It is an array to account for disk actions
    /// </summary>
    public byte[] NewValue { get; init; }
}