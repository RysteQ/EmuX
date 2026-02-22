using EmuXCore;
using EmuXCore.Interpreter.LexicalAnalysis.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Internal.Device.USBDrives;
using EmuXUI.Enums;
using EmuXUI.Models.Logic;
using EmuXUI.Models.Static;
using EmuXUI.Popups;
using EmuXUI.ViewModels.Internal;
using EmuXUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmuXUI.ViewModels;

public sealed class MainWindowViewModel : BaseViewModel
{
    public MainWindowViewModel()
    {
        ConfigureVirtualMachine();

        CommandConfigureVirtualMachine = GenerateCommand(DisplayVirtualMachineConfigurationWindow);
        CommandExecuteCode = GenerateCommand(async () => await DisplayExecutionWindow());
        Breakpoints = [];

        SourceCode = string.Empty;
    }

    private void ConfigureVirtualMachine()
    {
        IVirtualMachineBuilder virtualMachineBuilder = DIFactory.GenerateIVirtualMachineBuilder();

        virtualMachineBuilder = virtualMachineBuilder
            .SetCpu(DIFactory.GenerateIVirtualCPU())
            .SetMemory(DIFactory.GenerateIVirtualMemory(65_536, 1_048_576))
            .SetBios(DIFactory.GenerateIVirtualBIOS(DIFactory.GenerateIDiskInterruptHandler(), DIFactory.GenerateIRTCInterruptHandler(), DIFactory.GenerateIVideoInterruptHandler(), DIFactory.GenerateIDeviceInterruptHandler()))
            .SetRTC(DIFactory.GenerateIVirtualRTC())
            .AddDisk(DIFactory.GenerateIVirtualDisk(1, 16, 16, 255))
            .SetGPU(DIFactory.GenerateIVirtualGPU())
            .AddVirtualDevice(DIFactory.GenerateIVirtualDevice<UsbDrive64Kb>(1));

        _virtualMachine = virtualMachineBuilder.Build();
    }

    private void DisplayVirtualMachineConfigurationWindow()
    {
        VirtualMachineConfigurationWindow virtualMachineConfigurationWindow = new
        (
            new()
            {
                SelectedCPU = new(string.Empty, _virtualMachine.CPU),
                SelectedGPU = new(string.Empty, _virtualMachine.GPU),
                Disks = _virtualMachine.Disks.Select(selector => new DiskSelection(selector)).ToList(),
                Devices = _virtualMachine.Devices.Select(selector => new DeviceSelection(string.Empty, selector.GetType())).ToList(),
                SelectedIOMemoryInKbAmount = (int)_virtualMachine.Memory.IO_MEMORY / 1024,
                SelectedGeneralPurposeMemoryInKbAmount = (int)_virtualMachine.Memory.GENERAL_PURPOSE_MEMORY / 1024
            }
        );

        virtualMachineConfigurationWindow.SavedVirtualMachineConfiguration += VirtualMachineConfigurationWindow_SavedVirtualMachineConfiguration;

        virtualMachineConfigurationWindow.Activate();
    }

    private void VirtualMachineConfigurationWindow_SavedVirtualMachineConfiguration(object? sender, EventArgs e)
    {
        IVirtualMachineBuilder virtualMachineBuilder = DIFactory.GenerateIVirtualMachineBuilder();
        VirtualMachineConfiguration _virtualMachineConfiguration = (VirtualMachineConfiguration)e;

        virtualMachineBuilder = virtualMachineBuilder
            .SetCpu(_virtualMachineConfiguration.SelectedCPU.CPU)
            .SetMemory(DIFactory.GenerateIVirtualMemory((uint)_virtualMachineConfiguration.SelectedIOMemoryInKbAmount * 1024, (uint)_virtualMachineConfiguration.SelectedGeneralPurposeMemoryInKbAmount * 1024))
            .SetBios(DIFactory.GenerateIVirtualBIOS(DIFactory.GenerateIDiskInterruptHandler(), DIFactory.GenerateIRTCInterruptHandler(), DIFactory.GenerateIVideoInterruptHandler(), DIFactory.GenerateIDeviceInterruptHandler()))
            .SetRTC(DIFactory.GenerateIVirtualRTC())
            .SetGPU(_virtualMachineConfiguration.SelectedGPU.GPU);

        foreach (DiskSelection disk in _virtualMachineConfiguration.Disks)
        {
            virtualMachineBuilder = virtualMachineBuilder.AddDisk(disk.VirtualDisk);
        }

        for (int i = 0; i < _virtualMachineConfiguration.Devices.Count; i++)
        {
            virtualMachineBuilder = virtualMachineBuilder.AddVirtualDevice((IVirtualDevice)Activator.CreateInstance(_virtualMachineConfiguration.Devices[i].DeviceType, (ushort)(i + 1), null)!);
        }

        _virtualMachine = virtualMachineBuilder.Build();
    }

    private async Task DisplayExecutionWindow()
    {
        try
        {
            ILexer lexer = DIFactory.GenerateILexer(DIFactory.GenerateIVirtualCPU(), DIFactory.GenerateIInstructionLookup(), DIFactory.GenerateIPrefixLookup());
            IParser parser = DIFactory.GenerateIParser(_virtualMachine, DIFactory.GenerateIInstructionLookup(), DIFactory.GenerateIPrefixLookup());
            IList<IToken> tokens = lexer.Tokenize(SourceCode);
            IList<int> sameNameLabels = [];
            IParserResult parserResult = parser.Parse(tokens);
            string instructionErrors = string.Empty;

            if (!parserResult.Success)
            {
                InfoPopup.Show(InfoPopupSeverity.Warning, string.Join('\n', parserResult.Errors.ToArray()));

                return;
            }

            for (int i = 0; i < parserResult.Labels.Count; i++)
            {
                for (int j = i + 1; j < parserResult.Labels.Count; j++)
                {
                    if (parserResult.Labels[i].Name == parserResult.Labels[j].Name)
                    {
                        sameNameLabels.Add(j);
                    }
                }
            }

            if (sameNameLabels.Any())
            {
                InfoPopup.Show(InfoPopupSeverity.Warning, $"Cannot have same named labels, lines to look at [{string.Join('\n', sameNameLabels.Select(selector => selector.ToString()).ToList())}]");

                return;
            }

            for (int i = 0; i < parserResult.Instructions.Count; i++)
            {
                if (!parserResult.Instructions[i].IsValid())
                {
                    instructionErrors += $"Instruction {i + 1} is not valid\n";
                }
            }

            if (string.IsNullOrEmpty(instructionErrors))
            {
                _virtualMachine.Memory.LabelMemoryLocations.Clear();

                new ExecutionWindow(parserResult.Instructions, parserResult.Labels, Breakpoints, _virtualMachine).Activate();
            }
            else
            {
                InfoPopup.Show(InfoPopupSeverity.Warning, instructionErrors);
            }
        }
        catch (Exception ex)
        {
            InfoPopup.Show(InfoPopupSeverity.Error, $"Exception: {ex.InnerException} - {ex.Message}");
        }
    }

    public ICommand CommandConfigureVirtualMachine { get; set; }
    public ICommand CommandExecuteCode { get; set; }

    public string SourceCode { get; set; }
    public IList<int> Breakpoints { get; set; }

    private IVirtualMachine _virtualMachine;
}