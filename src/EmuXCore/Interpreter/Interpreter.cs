using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Actions;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.Interpreter;

public class Interpreter : IInterpreter
{
    public Interpreter(Type callInstruction, Type retInstruction)
    {
        _callInstruction = callInstruction;
        _retInstruction = retInstruction;
    }

    public void Execute()
    {
        _executingCode = true;

        while (_memoryInstructionLookupTable.ContainsKey(VirtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP))
        {
            ExecuteStep();
        }

        _executingCode = false;
    }

    public void ExecuteStepOver()
    {
        ulong currentInstructionPointer = VirtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP;

        if (!Instructions.Any() || _currentInstructionIndex == Instructions.Count || !_memoryInstructionLookupTable.ContainsKey(currentInstructionPointer))
        {
            _currentInstructionIndex = -1;

            return;
        }

        if (_memoryInstructionLookupTable[VirtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP].GetType() == _callInstruction)
        {
            while (_memoryInstructionLookupTable[VirtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP].GetType() != _retInstruction)
            {
                ExecuteStep();
            }
        }

        ExecuteStep();
    }

    public void ExecuteStep()
    {
        ulong currentInstructionPointer = VirtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP;

        if (!Instructions.Any() || _currentInstructionIndex == Instructions.Count || !_memoryInstructionLookupTable.ContainsKey(currentInstructionPointer))
        {
            _currentInstructionIndex = -1;

            return;
        }

        _currentInstructionIndex = _memoryInstructionLookupTable.Select(selector => selector.Key).ToList().IndexOf(currentInstructionPointer);
        _memoryInstructionLookupTable[VirtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP].Execute(VirtualMachine);

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

        _currentInstructionIndex = -1;
    }

    private void ComputeLabelMemoryLocations()
    {
        int currentLine = 1;
        int offset = 0;

        if (Labels == null)
        {
            return;
        }

        VirtualMachine.Memory.LabelMemoryLocations.Clear();

        foreach (ILabel label in Labels)
        {
            for (int i = currentLine; i < label.Line; i++)
            {
                offset += 5; // DUMMY VALUE
            }

            offset -= 5;

            VirtualMachine.Memory.LabelMemoryLocations.Add(label.Name, DIFactory.GenerateIMemoryLabel(label.Name, offset + 5, label.Line));

            currentLine = label.Line;
        }
    }

    public IVirtualMachine VirtualMachine
    {
        get => field;
        set
        {
            field = value;

            ConfigureInstructionsLookupTable();
            ComputeLabelMemoryLocations();
        }
    }

    public IList<IInstruction> Instructions
    {
        get => field;
        set
        {
            field = value;

            ConfigureInstructionsLookupTable();
            ComputeLabelMemoryLocations();
        }
    }

    public IList<ILabel> Labels
    {
        get => field;
        set
        {
            field = value;

            ConfigureInstructionsLookupTable();
            ComputeLabelMemoryLocations();
        }
    }


    public IInstruction CurrentInstruction => Instructions[_currentInstructionIndex];
    public int CurrentInstructionIndex => _currentInstructionIndex;
    public bool ExecutingCode => _executingCode;

    private Dictionary<ulong, IInstruction> _memoryInstructionLookupTable = [];
    private Dictionary<int, List<IVmAction>> _actions = [];
    private int _currentInstructionIndex = -1;
    private bool _executingCode = false;
    private readonly Type _callInstruction;
    private readonly Type _retInstruction;
}