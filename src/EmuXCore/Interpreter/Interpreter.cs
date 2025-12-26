using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Actions;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

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
        ulong currentInstructionPointer = VirtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP;

        if (!Instructions.Any() || _currentInstructionIndex == Instructions.Count - 1)
        {
            return;
        }

        _memoryInstructionLookupTable[VirtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP].Execute(VirtualMachine);
        _currentInstructionIndex = _memoryInstructionLookupTable.Select(selector => selector.Key).ToList().IndexOf(currentInstructionPointer);

        if (!_actions.ContainsKey(_currentInstructionIndex))
        {
            _actions.Add(_currentInstructionIndex, [.. VirtualMachine.Actions.TakeLast(VirtualMachine.Actions.Count - _actions.Values.Sum(selectedRecord => selectedRecord.Count))]);
        }
    }

    public void UndoAction()
    {
        if (_currentInstructionIndex == -1)
        {
            return;
        }

        for (int i = _actions[_currentInstructionIndex].Count - 1; i >= 0; i--)
        {
            _actions[_currentInstructionIndex][i].Undo(VirtualMachine);
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
        if (_currentInstructionIndex >= _actions.Count - 1)
        {
            return;
        }

        for (int i = _actions[_currentInstructionIndex + 1].Count - 1; i >= 0; i--)
        {
            _actions[_currentInstructionIndex + 1][i].Redo(VirtualMachine);
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
        for (int i = VirtualMachine.Actions.Count - 1; i >= 0; i--)
        {
            VirtualMachine.Actions[i].Undo(VirtualMachine);
        }

        VirtualMachine.Actions.Clear();
        _actions.Clear();
        _currentInstructionIndex = 0;
    }

    private void ConfigureInstructionsLookupTable()
    {
        if (Instructions == null)
        {
            return;
        }

        _memoryInstructionLookupTable.Clear();

        for (int i = 0; i < Instructions.Count; i++)
        {
            _memoryInstructionLookupTable.Add((ulong)(i * 5), Instructions[i]);
        }
    }

    public IVirtualMachine VirtualMachine
    {
        get => field;
        set
        {
            field = value;

            ConfigureInstructionsLookupTable();
        }
    }

    public IList<IInstruction> Instructions
    {
        get => field;
        set
        {
            field = value;

            ConfigureInstructionsLookupTable();
        }
    }

    public IList<ILabel> Labels
    {
        get => field;
        set
        {
            field = value;

            ConfigureInstructionsLookupTable();
        }
    }


    public IInstruction CurrentInstruction => Instructions[_currentInstructionIndex];
    public int CurrentInstructionIndex => _currentInstructionIndex;

    private Dictionary<ulong, IInstruction> _memoryInstructionLookupTable = [];
    private Dictionary<int, List<IVmAction>> _actions = [];
    private int _currentInstructionIndex;
}