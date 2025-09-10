namespace EmuXCore.Interpreter.RCVM.Interfaces;

/// <summary>
/// An addon for the <c>IVirtualMachine</c> interface that records memory operations to keep track of change and undo / redo any of the changes
/// </summary>
public interface IRecordableContainarisedVirtualMachine
{
    /// <summary>
    /// Resets the VM to its initial state
    /// </summary>
    void ResetToInitialState();

    /// <summary>
    /// Undos a single action
    /// </summary>
    void UndoAction();

    /// <summary>
    /// Undos N amount of actions
    /// </summary>
    /// <param name="actionsToUndo">The amount of actions to undo</param>
    void UndoActions(int actionsToUndo);

    /// <summary>
    /// Redos a single action
    /// </summary>
    void RedoAction();

    /// <summary>
    /// Redos N amount of actions
    /// </summary>
    /// <param name="actionsToRedo">The amount of actions to redo</param>
    void RedoActions(int actionsToRedo);
}