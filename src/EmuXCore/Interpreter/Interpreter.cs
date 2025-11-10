using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Encoder.Interfaces.Logic;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Actions;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;
using System.Linq;

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

        _memoryInstructionLookupTable[_virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP].Execute(_virtualMachine);

        if (!_actions.ContainsKey(_currentInstructionIndex))
        {
            _actions.Add(_currentInstructionIndex, [.. _virtualMachine.Actions.TakeLast(_virtualMachine.Actions.Count - _actions.Values.Sum(selectedRecord => selectedRecord.Count))]);
        }

        _currentInstructionIndex = _memoryInstructionLookupTable.Select(selector => selector.Key).ToList().IndexOf(_virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP);
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
        ulong memoryOffset = 0;

        if (_virtualMachine == null)
        {
            throw new ArgumentNullException($"Property {nameof(VirtualMachine)} cannot be null");
        }

        _instructionEncoder = DIFactory.GenerateIInstructionEncoder(_virtualMachine, DIFactory.GenerateIOperandDecoder());
        _memoryInstructionLookupTable.Clear();
        _actions.Clear();
        _currentInstructionIndex = 0;

        if (_instructions.Any())
        {
            _instructionEncoderResult = _instructionEncoder.Parse(_instructions);

            for (int i = 0; i < _instructions.Count; i++)
            {
                _memoryInstructionLookupTable.Add(memoryOffset, _instructions[i]);
                memoryOffset += (ulong)(_instructionEncoderResult.Bytes[i].Length);
            }
        }
    }

    private ulong NextInstructionMemoryAddress(ulong currentInstructionMemoryAddress)
    {
        List<ulong> instructionMemoryAddresses = _memoryInstructionLookupTable.Select(selectedRecord => selectedRecord.Key).ToList();
        int currentInstructionMemoryAddressIndex = instructionMemoryAddresses.IndexOf(currentInstructionMemoryAddress);
        return instructionMemoryAddresses[currentInstructionMemoryAddressIndex + 1];
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

    private IInstructionEncoder _instructionEncoder;
    private IInstructionEncoderResult _instructionEncoderResult;
    private Dictionary<ulong, IInstruction> _memoryInstructionLookupTable = [];
    private IVirtualMachine _virtualMachine;
    private IList<IInstruction> _instructions = [];
    private IList<ILabel> _labels = [];
    private Dictionary<int, List<IVmAction>> _actions = [];
    private int _currentInstructionIndex;
}