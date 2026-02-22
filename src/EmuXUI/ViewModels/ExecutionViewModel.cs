using EmuXCore;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.Interpreter;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Interfaces.Events;
using EmuXUI.Enums;
using EmuXUI.Models.Events;
using EmuXUI.Models.Observable;
using EmuXUI.Models.Static;
using EmuXUI.Popups;
using EmuXUI.ViewModels.Internal;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmuXUI.ViewModels;

public sealed class ExecutionViewModel : BaseViewModel
{
    public ExecutionViewModel(IList<IInstruction> instructions, IList<ILabel> labels, IList<int> breakpoints, IVirtualMachine virtualMachine)
    {
        CommandExecuteCode = GenerateCommand(async () => await ExecuteCode());
        CommandStepToNextInstruction = GenerateCommand(async () => await StepInstruction());
        CommandUndoInstruction = GenerateCommand(async () => await UndoInstruction());
        CommandRedoInstruction = GenerateCommand(async () => await RedoInstruction());
        CommandResetInstruction = GenerateCommand(async () => await ResetExecution());
        CommandSearchMemory = GenerateCommand(async () => await SearchMemory());
        
        _interpreter = DIFactory.GenerateIInterpreter();
        _interpreter.VirtualMachine = virtualMachine;
        _interpreter.Instructions = instructions;
        _interpreter.Labels = labels;

        CurrentInstructionIndex = 0;
        VideoOutput = new(_interpreter.VirtualMachine.GPU.Height, _interpreter.VirtualMachine.GPU.Width);
        SourceCodeLines = [];
        SearchedBytes = [];
        Registers = [];

        _interpreter.VirtualMachine.MemoryAccessed += VirtualMachine_MemoryAccessed;
        _interpreter.VirtualMachine.RegisterAccessed += VirtualMachine_RegisterAccessed;
        _interpreter.VirtualMachine.VideoCardAccessed += VirtualMachine_VideoCardAccessed;

        InitInstructions(breakpoints);
        InitRegisters();
    }

    private async Task ExecuteCode()
    {
        bool advanced = false;

        try
        {
            do
            {
                if (_interpreter.CurrentInstructionIndex != -1)
                {
                    if (SourceCodeLines[_interpreter.CurrentInstructionIndex].Breakpoint && advanced)
                    {
                        await Task.Run(() =>
                        {
                            Console.Beep();
                        });

                        return;
                    }
                }

                advanced = true;
                _interpreter.ExecuteStep();

                UpdateCurrentInstructionIndex();
            } while (_interpreter.CurrentInstructionIndex != -1);

            CurrentInstructionIndex = SourceCodeLines.Last().Line - 1;

            await Task.Run(() =>
            {
                Console.Beep();
            });
        }
        catch (Exception ex)
        {
            InfoPopup.Show(InfoPopupSeverity.Error, $"Exception: {ex.InnerException} - {ex.Message}\n\nStack trace\n\n{ex.StackTrace}");

            CommandResetInstruction.Execute(null);
        }
    }

    private async Task StepInstruction()
    {
        try
        {
            _interpreter.ExecuteStep();

            if (SourceCodeLines[_interpreter.CurrentInstructionIndex].Breakpoint)
            {
                await Task.Run(() =>
                {
                    Console.Beep();
                });
            }

            UpdateCurrentInstructionIndex();
        }
        catch (Exception ex)
        {
            InfoPopup.Show(InfoPopupSeverity.Error, $"Exception: {ex.InnerException} - {ex.Message}\n\nStack trace\n\n{ex.StackTrace}");

            CommandResetInstruction.Execute(null);
        }
    }

    private async Task UndoInstruction()
    {
        try
        {
            if (_interpreter.CurrentInstructionIndex == 0)
            {
                await Task.Run(() =>
                {
                    Console.Beep();
                });

                return;
            }

            _interpreter.UndoAction();

            if (SourceCodeLines[_interpreter.CurrentInstructionIndex].Breakpoint)
            {
                await Task.Run(() =>
                {
                    Console.Beep();
                });
            }

            UpdateCurrentInstructionIndex();
        }
        catch (Exception ex)
        {
            InfoPopup.Show(InfoPopupSeverity.Error, $"Exception: {ex.InnerException} - {ex.Message}\n\nStack trace\n\n{ex.StackTrace}");

            CommandResetInstruction.Execute(null);
        }
    }

    private async Task RedoInstruction()
    {
        try
        {
            _interpreter.RedoAction();

            UpdateCurrentInstructionIndex();
        }
        catch (Exception ex)
        {
            InfoPopup.Show(InfoPopupSeverity.Error, $"Exception: {ex.InnerException} - {ex.Message}\n\nStack trace\n\n{ex.StackTrace}");

            CommandResetInstruction.Execute(null);
        }
    }

    private async Task ResetExecution()
    {
        try
        {
            _interpreter.ResetExecution();

            UpdateCurrentInstructionIndex();
        }
        catch (Exception ex)
        {
            InfoPopup.Show(InfoPopupSeverity.Error, $"Exception: {ex.InnerException} - {ex.Message}\n\nStack trace\n\n{ex.StackTrace}");
        }
    }

    private async Task SearchMemory()
    {
        SearchedBytes.Clear();

        for (int i = StartMemorySearchIndex; i < StartMemorySearchIndex + MemorySearchBytesToGet; i++)
        {
            SearchedBytes.Add(new(i, _interpreter.VirtualMachine.GetByte(i)));
            SearchedBytes.Last().ValueChanged += SearchedMemoryByte_ValueChanged;
        }

        OnPropertyChanged(nameof(SearchedBytes));
    }

    private void InitInstructions(IList<int> breakpoints)
    {
        for (int i = 0; i < _interpreter.Instructions.Count; i++)
        {
            SourceCodeLines.Add(new(i + 1, false, ConvertInstructiongToString(_interpreter.Instructions[i]), _interpreter.Instructions[i]));
        }

        foreach (ILabel label in _interpreter.Labels)
        {
            SourceCodeLines.Insert(label.Line - 1, new(label.Line, false, $"{label.Name}:", null));
        }

        for (int i = 0; i < SourceCodeLines.Count; i++)
        {
            SourceCodeLines[i].Line = i + 1;
            
            if (breakpoints.Contains(SourceCodeLines[i].Line - 1) && SourceCodeLines[i].Instruction != null)
            {
                SourceCodeLines[i].Breakpoint = true;
            }
        }

        while (SourceCodeLines[CurrentInstructionIndex].SourceCode.EndsWith(':'))
        {
            CurrentInstructionIndex++;
        }
        
        OnPropertyChanged(nameof(SourceCodeLines));
    }

    private void UpdateCurrentInstructionIndex()
    {
        if (_interpreter.CurrentInstructionIndex == -1)
        {
            CurrentInstructionIndex = 0;

            return;
        }

        CurrentInstructionIndex = SourceCodeLines
            .Where(record => record.Instruction == _interpreter.CurrentInstruction)
            .First().Line - 1;
    }

    private void InitRegisters()
    {
        DispatcherQueue.GetForCurrentThread().TryEnqueue(() =>
        {
            ulong registerValue = 0;

            Registers.Clear();

            foreach (IVirtualRegister virtualRegister in _interpreter.VirtualMachine.CPU.Registers)
            {
                foreach (KeyValuePair<string, EmuXCore.Common.Enums.Size> register in virtualRegister.RegisterNamesAndSizes)
                {
                    registerValue = _interpreter.VirtualMachine.CPU.GetRegister(register.Key).Get();

                    registerValue = register.Value switch
                    {
                        EmuXCore.Common.Enums.Size.Byte => (byte)registerValue,
                        EmuXCore.Common.Enums.Size.Word => (ushort)registerValue,
                        EmuXCore.Common.Enums.Size.Dword => (uint)registerValue,
                        EmuXCore.Common.Enums.Size.Qword => registerValue,
                        _ => throw new ArgumentOutOfRangeException($"Invalid register size {register.Value} for register {register.Key}")
                    };

                    if (register.Key.EndsWith('H'))
                    {
                        registerValue = _interpreter.VirtualMachine.CPU.GetRegister(register.Key).Get() & 0x000000000000ff00;
                        registerValue = registerValue >> 8;
                    }

                    Registers.Add(new(register.Key, register.Value, registerValue));
                    Registers.Last().ValueChanged += Register_ValueChanged;
                }
            }

            OnPropertyChanged(nameof(Registers));
        });
    }

    private void SearchedMemoryByte_ValueChanged(object? sender, EventArgs e)
    {
        WrittenMemoryByteEvent writtenByte = (WrittenMemoryByteEvent)e;

        if (writtenByte.PreviousValue != writtenByte.NewValue)
        {
            _interpreter.VirtualMachine.SetByte(writtenByte.Index, writtenByte.NewValue);
        }
    }
    
    private void Register_ValueChanged(object? sender, EventArgs e)
    {
        WrittenToRegisterEvent writtenToRegisterEvent = (WrittenToRegisterEvent)e;
        EmuXCore.Common.Enums.Size registerSize = EmuXCore.Common.Enums.Size.NaN;
        IVirtualRegister selectedRegister;

        if (writtenToRegisterEvent.PreviousValue == writtenToRegisterEvent.NewValue)
        {
            return;
        }

        selectedRegister = _interpreter.VirtualMachine.CPU.GetRegister(writtenToRegisterEvent.RegisterName);
        registerSize = selectedRegister.RegisterNamesAndSizes[writtenToRegisterEvent.RegisterName];

        selectedRegister.Set(writtenToRegisterEvent.RegisterName, writtenToRegisterEvent.NewValue);

        InitRegisters();
    }

    private void VirtualMachine_MemoryAccessed(object? sender, EventArgs e)
    {
        IMemoryAccess memoryAccess = (IMemoryAccess)e;

        if (memoryAccess.ReadOrWrite)
        {
            return;
        }

        switch (memoryAccess.Size)
        {
            case EmuXCore.Common.Enums.Size.Byte:
                UpdateSearchedBytesDueToWriteEvent(memoryAccess.MemoryAddress, (byte)memoryAccess.NewValue);

                break;

            case EmuXCore.Common.Enums.Size.Word:
                UpdateSearchedBytesDueToWriteEvent(memoryAccess.MemoryAddress, 
                    (byte)(memoryAccess.NewValue & 0x_00_00_00_00_00_00_00_ff), 
                    (byte)((memoryAccess.NewValue & 0x_00_00_00_00_00_00_ff_00) >> 8)
                );

                break;

            case EmuXCore.Common.Enums.Size.Dword:
                UpdateSearchedBytesDueToWriteEvent(memoryAccess.MemoryAddress,
                    (byte)(memoryAccess.NewValue & 0x_00_00_00_00_00_00_00_ff),
                    (byte)((memoryAccess.NewValue & 0x_00_00_00_00_00_00_ff_00) >> 8),
                    (byte)((memoryAccess.NewValue & 0x_00_00_00_00_00_ff_00_00) >> 16),
                    (byte)((memoryAccess.NewValue & 0x_00_00_00_00_ff_00_00_00) >> 24)
                );

                break;
            
            case EmuXCore.Common.Enums.Size.Qword:
                UpdateSearchedBytesDueToWriteEvent(memoryAccess.MemoryAddress,
                    (byte)(memoryAccess.NewValue & 0x_00_00_00_00_00_00_00_ff),
                    (byte)((memoryAccess.NewValue & 0x_00_00_00_00_00_00_ff_00) >> 8),
                    (byte)((memoryAccess.NewValue & 0x_00_00_00_00_00_ff_00_00) >> 16),
                    (byte)((memoryAccess.NewValue & 0x_00_00_00_00_ff_00_00_00) >> 24),
                    (byte)((memoryAccess.NewValue & 0x_00_00_00_ff_00_00_00_00) >> 32),
                    (byte)((memoryAccess.NewValue & 0x_00_00_ff_00_00_00_00_00) >> 40),
                    (byte)((memoryAccess.NewValue & 0x_00_ff_00_00_00_00_00_00) >> 48),
                    (byte)((memoryAccess.NewValue & 0x_ff_00_00_00_00_00_00_00) >> 56)
                );

                break;
        }
    }

    private void VirtualMachine_RegisterAccessed(object? sender, EventArgs e)
    {
        IRegisterAccess registerAccess = (IRegisterAccess)e;

        if (!registerAccess.Write)
        {
            return;
        }

        InitRegisters();
    }


    private async void VirtualMachine_VideoCardAccessed(object? sender, EventArgs e)
    {
        await VideoOutput.WriteEntireImage(_interpreter.VirtualMachine.GPU.Data);
        VideoOutput.Update();
    }

    private void UpdateSearchedBytesDueToWriteEvent(int index, params byte[] bytes)
    {
        MemoryByte[] memoryBytes = SearchedBytes
            .Where(record => record.Index == index)
            .Take(bytes.Length)
            .ToArray();

        if (!memoryBytes.Any())
        {
            return;
        }

        for (int i = 0; i < bytes.Length; i++)
        {
            SearchedBytes[i].ValueChanged -= SearchedMemoryByte_ValueChanged;
            SearchedBytes[i].Value = bytes[i];
            SearchedBytes[i].ValueChanged += SearchedMemoryByte_ValueChanged;
        }
    }

    private string ConvertInstructiongToString(IInstruction instruction)
    {
        if (instruction.Variant == InstructionVariant.NoOperands())
        {
            return instruction.Opcode;
        }

        if (instruction.FirstOperand != null && instruction.SecondOperand == null && instruction.ThirdOperand == null)
        {
            return $"{instruction.Opcode} {instruction.FirstOperand?.FullOperand}";
        }

        if (instruction.FirstOperand != null && instruction.SecondOperand != null && instruction.ThirdOperand == null)
        {
            return $"{instruction.Opcode} {instruction.FirstOperand?.FullOperand}, {instruction.SecondOperand?.FullOperand}";
        }

        return $"{instruction.Opcode} {instruction.FirstOperand?.FullOperand}, {instruction.SecondOperand?.FullOperand}, {instruction.ThirdOperand?.FullOperand}";
    }

    public async Task UpdateBitmap()
    {
        await VideoOutput.WriteEntireImage(_interpreter.VirtualMachine.GPU.Data);
        VideoOutput.Update();
    }

    public ICommand CommandExecuteCode { get; private set; }
    public ICommand CommandStepToNextInstruction { get; private set; }
    public ICommand CommandUndoInstruction { get; private set; }
    public ICommand CommandRedoInstruction { get; private set; }
    public ICommand CommandResetInstruction { get; private set; }
    public ICommand CommandSearchMemory { get; private set; }

    public Bitmap VideoOutput { get; private set; }
    public ObservableCollection<SourceCodeLine> SourceCodeLines { get; set; }
    public ObservableCollection<MemoryByte> SearchedBytes { get; set; }
    public ObservableCollection<Register> Registers { get; set; }

    public int CurrentInstructionIndex
    {
        get => field;
        private set => OnPropertyChanged(ref field, value);
    }

    public uint IOMemory { get => _interpreter.VirtualMachine.Memory.IO_MEMORY; }
    public uint GeneralPurposeMemory { get => _interpreter.VirtualMachine.Memory.GENERAL_PURPOSE_MEMORY; }

    public int StartMemorySearchIndex
    {
        get => field;
        set => OnPropertyChanged(ref field, value);
    }
    
    public int MemorySearchBytesToGet
    {
        get => field;
        set => OnPropertyChanged(ref field, value);
    }

    private readonly IInterpreter _interpreter;
}