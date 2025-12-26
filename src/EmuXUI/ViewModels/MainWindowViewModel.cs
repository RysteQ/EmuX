using EmuXCore;
using EmuXCore.Interpreter.LexicalAnalysis.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.Device.USBDrives;
using EmuXUI.Enums;
using EmuXUI.Models.Logic;
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
        VirtualMachineConfigurationWindow virtualMachineConfigurationWindow = new(ref _virtualMachine);

        virtualMachineConfigurationWindow.SavedVirtualMachineConfiguration += VirtualMachineConfigurationWindow_SavedVirtualMachineConfiguration;

        virtualMachineConfigurationWindow.Activate();
    }

    private void VirtualMachineConfigurationWindow_SavedVirtualMachineConfiguration(object? sender, EventArgs e)
    {
        _virtualMachineConfiguration = (VirtualMachineConfiguration)e;
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

            if (!parserResult.Success)
            {
                new InfoPopup(InfoPopupSeverity.Warning, string.Join('\n', parserResult.Errors.ToArray())).Activate();

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
                new InfoPopup(InfoPopupSeverity.Warning, $"Cannot have same named labels, lines to look at [{string.Join('\n', sameNameLabels.Select(selector => selector.ToString()).ToList())}]").Activate();

                return;
            }

            _virtualMachine.Memory.LabelMemoryLocations.Clear();

            new ExecutionWindow(parserResult.Instructions, parserResult.Labels, _virtualMachine).Activate();
        }
        catch (Exception ex)
        {
            new InfoPopup(InfoPopupSeverity.Error, $"Exception: {ex.InnerException} - {ex.Message}").Activate();
        }
    }

    public ICommand CommandConfigureVirtualMachine { get; set; }
    public ICommand CommandExecuteCode { get; set; }

    public string SourceCode { get; set; }

    private VirtualMachineConfiguration _virtualMachineConfiguration;
    private IVirtualMachine _virtualMachine;
}