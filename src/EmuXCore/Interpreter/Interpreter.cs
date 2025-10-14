using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Actions;

namespace EmuXCore.Interpreter;

public class Interpreter : IInterpreter
{
    public void Execute()
    {
        foreach (IInstruction instruction in Instructions)
        {
            ExecuteStep();
        }
    }

    public void ExecuteStep()
    {
        if (!_instructions.Any() || _currentInstructionIndex == _instructions.Count)
        {
            return;
        }

        _instructions[_currentInstructionIndex].Execute(_virtualMachine);

        _actions.Add(_currentInstructionIndex, [.. _virtualMachine.Actions.TakeLast(_virtualMachine.Actions.Count - _actions.Values.Sum(selectedRecord => selectedRecord.Count))]);
        _currentInstructionIndex++;
    }

    public void UndoAction()
    {
        if (_currentInstructionIndex == 0)
        {
            return;
        }

        for (int i = _actions[_currentInstructionIndex - 1].Count - 1; i >= 0; i--)
        {
            _actions[_currentInstructionIndex - 1][i].Undo(_virtualMachine);
        }

        _currentInstructionIndex--;
    }

    public void UndoActions(int actions)
    {
        for (int i = 0; i < actions; i++)
        {
            UndoAction();
        }
    }

    public void RedoAction()
    {
        if (_currentInstructionIndex == _actions.Count)
        {
            return;
        }

        for (int i = _actions[_currentInstructionIndex].Count - 1; i >= 0; i--)
        {
            _actions[_currentInstructionIndex][i].Redo(_virtualMachine);
        }

        _currentInstructionIndex++;
    }

    public void RedoActions(int actions)
    {
        for (int i = 0; i < actions; i++)
        {
            RedoAction();
        }
    }

    public void ResetExecution()
    {
        for (int i = _virtualMachine.Actions.Count - 1; i >= 0; i--)
        {
            _virtualMachine.Actions[i].Undo(_virtualMachine);
        }

        _virtualMachine.Actions.Clear();
        _actions.Clear();
        _currentInstructionIndex = 0;
    }

    private void ConfigureCriticalProperties()
    {
        if (_virtualMachine == null)
        {
            throw new ArgumentNullException($"Property {nameof(VirtualMachine)} cannot be null");
        }

        _actions.Clear();
        _currentInstructionIndex = 0;
    }

    public IVirtualMachine VirtualMachine
    {
        get => _virtualMachine;
        set
        {
            _virtualMachine = value;
            ConfigureCriticalProperties();
        }
    }

    public IList<IInstruction> Instructions
    {
        get => _instructions;
        set
        {
            _instructions = value;
            ConfigureCriticalProperties();
        }
    }

    public IList<ILabel> Labels
    {
        get => _labels;
        set
        {
            _labels = value;
            ConfigureCriticalProperties();
        }
    }

    public IInstruction CurrentInstruction => Instructions[_currentInstructionIndex];
    public int CurrentInstructionIndex => _currentInstructionIndex;

    private IVirtualMachine _virtualMachine;
    private IList<IInstruction> _instructions;
    private IList<ILabel> _labels;
    private Dictionary<int, List<IVmAction>> _actions = [];
    private int _currentInstructionIndex;
}