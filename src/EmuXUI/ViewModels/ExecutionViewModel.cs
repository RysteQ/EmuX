using Windows.UI;
using EmuXCore.Common.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXUI.Models.Events;
using EmuXUI.Models.Observable;
using EmuXUI.ViewModels.Internal;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Streams;
using static System.Net.Mime.MediaTypeNames;
using EmuXCore.VM.Interfaces.Events;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXUI.Popups;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXUI.ViewModels;

public sealed class ExecutionViewModel : BaseViewModel
{
    public ExecutionViewModel(IList<IInstruction> instructions, IVirtualMachine virtualMachine)
    {
        _instructions = instructions;
        _virtualMachine = virtualMachine;

        CommandExecuteCode = GenerateCommand(async () => await ExecuteCode());
        CommandStepToNextInstruction = GenerateCommand(async () => await StepInstruction());
        CommandUndoInstruction = GenerateCommand(async () => await UndoInstruction());
        CommandRedoInstruction = GenerateCommand(async () => await RedoInstruction());
        CommandResetInstruction = GenerateCommand(async () => await ResetExecution());
        CommandSearchMemory = GenerateCommand(async () => await SearchMemory());

        VideoOutput = new(_virtualMachine.GPU.Height, _virtualMachine.GPU.Width);
        SearchedBytes = [];
        Registers = [];

        _virtualMachine.MemoryAccessed += VirtualMachine_MemoryAccessed;

        InitRegisters();
    }

    private async Task ExecuteCode()
    {
        // TODO
    }

    private async Task StepInstruction()
    {
        // TODO
    }

    private async Task UndoInstruction()
    {
        // TODO
    }

    private async Task RedoInstruction()
    {
        // TODO
    }

    private async Task ResetExecution()
    {
        // TODO
    }

    private async Task SearchMemory()
    {
        SearchedBytes.Clear();

        for (int i = StartMemorySearchIndex; i < StartMemorySearchIndex + MemorySearchBytesToGet; i++)
        {
            SearchedBytes.Add(new(i, _virtualMachine.GetByte(i)));
            SearchedBytes.Last().ValueChanged += SearchedMemoryByte_ValueChanged;
        }

        OnPropertyChanged(nameof(SearchedBytes));
    }

    private void InitRegisters()
    {
        DispatcherQueue.GetForCurrentThread().TryEnqueue(() =>
        {
            ulong registerValue = 0;

            Registers.Clear();

            foreach (IVirtualRegister virtualRegister in _virtualMachine.CPU.Registers)
            {
                foreach (KeyValuePair<string, EmuXCore.Common.Enums.Size> register in virtualRegister.RegisterNamesAndSizes)
                {
                    registerValue = _virtualMachine.CPU.GetRegister(register.Key).Get();

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
                        registerValue = _virtualMachine.CPU.GetRegister(register.Key).Get() & 0x000000000000ff00;
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
            _virtualMachine.SetByte(writtenByte.Index, writtenByte.NewValue);
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

        selectedRegister = _virtualMachine.CPU.GetRegister(writtenToRegisterEvent.RegisterName);
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

    public async Task UpdateBitmap()
    {
        await VideoOutput.WriteEntireImage(_virtualMachine.GPU.Data);
        VideoOutput.Update();
    }

    public ICommand CommandExecuteCode { get; private set; }
    public ICommand CommandStepToNextInstruction { get; private set; }
    public ICommand CommandUndoInstruction { get; private set; }
    public ICommand CommandRedoInstruction { get; private set; }
    public ICommand CommandResetInstruction { get; private set; }
    public ICommand CommandSearchMemory { get; private set; }

    public Bitmap VideoOutput { get; private set; }
    public ObservableCollection<MemoryByte> SearchedBytes { get; set; }
    public ObservableCollection<Register> Registers { get; set; }

    public uint IOMemory { get => _virtualMachine.Memory.IO_MEMORY; }
    public uint GeneralPurposeMemory { get => _virtualMachine.Memory.GENERAL_PURPOSE_MEMORY; }

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

    private readonly IList<IInstruction> _instructions;
    private readonly IVirtualMachine _virtualMachine;
}