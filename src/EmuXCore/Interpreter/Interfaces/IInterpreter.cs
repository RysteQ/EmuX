using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCore.VM.Interfaces;

namespace EmuXCore.Interpreter.Interfaces;

/// <summary>
/// The IInterpreter serves as a way to manage code execution in its IL state
/// </summary>
public interface IInterpreter
{
    /// <summary>
    /// Executes all instructions
    /// </summary>
    void Execute();

    /// <summary>
    /// Executes the next instruction
    /// </summary>
    void ExecuteStep();

    /// <summary>
    /// Just like execute step but if the instruction is a call instruction, it will execute all instructions until the return instruction of the call instruction is reached
    /// </summary>
    void ExecuteStepOver();

    /// <summary>
    /// Undo a single action
    /// </summary>
    void UndoAction();

    /// <summary>
    /// Undo N actions
    /// </summary>
    /// <param name="actions">The amount of actions to undo</param>
    void UndoActions(int actions);

    /// <summary>
    /// Redo a single action
    /// </summary>
    void RedoAction();

    /// <summary>
    /// Redo N actions
    /// </summary>
    /// <param name="actions">The amount of actions to redo</param>
    void RedoActions(int actions);

    /// <summary>
    /// Resets the execution of the program and the IVirtualMachine to its previous state
    /// </summary>
    void ResetExecution();

    /// <summary>
    /// The <c>IVirtualMachine</c> implementation to run the code under
    /// </summary>
    public IVirtualMachine VirtualMachine { get; set; }

    /// <summary>
    /// The instructions to execute
    /// </summary>
    public IList<IInstruction> Instructions { get; set; }

    /// <summary>
    /// The labels required for the code to run
    /// </summary>
    public IList<ILabel> Labels { get; set; }

    /// <summary>
    /// The current instruction being executed 
    /// </summary>
    public IInstruction CurrentInstruction { get; }

    /// <summary>
    /// The current instruction index being executed, -1 if no instruction has been executed / all actions have been undone
    /// </summary>
    public int CurrentInstructionIndex { get; }

    /// <summary>
    /// Useful for the Execute() method to indicate when the execution of the code has finished or not
    /// </summary>
    public bool ExecutingCode { get; }
}