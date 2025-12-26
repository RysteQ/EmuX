using EmuXCore;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.Device.USBDrives;
using EmuXUI.Models.Logic;
using EmuXUI.ViewModels.Internal;
using EmuXUI.Views;
using System;
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
        new ExecutionWindow([], _virtualMachine).Activate(); // TODO
    }

    public string SourceCode { get; set; }

    public ICommand CommandConfigureVirtualMachine { get; set; }
    public ICommand CommandExecuteCode { get; set; }

    private VirtualMachineConfiguration _virtualMachineConfiguration;
    private IVirtualMachine _virtualMachine;
}