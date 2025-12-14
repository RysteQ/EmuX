using EmuXCore;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.Device.USBDrives;
using EmuXUI.Models.Logic;
using EmuXUI.Popups;
using EmuXUI.ViewModels.Internal;
using EmuXUI.Views;
using System;
using System.Windows.Input;

namespace EmuXUI.ViewModels;

public sealed class MainWindowViewModel : BaseViewModel
{
    public MainWindowViewModel()
    {
        ConfigureVirtualMachine();

        CommandConfigureVirtualMachine = GenerateCommand(DisplayVirtualMachineConfigurationWindow);
        CommandExecuteCode = GenerateCommand(DisplayExecutionWindow);
        CommandDisplayAboutEmuX = GenerateCommand(DisplayAboutWindow);
    }

    private void ConfigureVirtualMachine()
    {
        IVirtualMachineBuilder virtualMachineBuilder = DIFactory.GenerateIVirtualMachineBuilder();

        virtualMachineBuilder = virtualMachineBuilder
            .SetCpu(DIFactory.GenerateIVirtualCPU())
            .SetMemory(DIFactory.GenerateIVirtualMemory(65_536, 921_600, 1_048_576))
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

    private void DisplayExecutionWindow()
    {
        ExecutionWindow executionWindow = new(_virtualMachine);

        executionWindow.Activate();
    }

    private void DisplayAboutWindow()
    {
        AboutPopup aboutPopup = new();

        aboutPopup.Activate();
    }

    public string SourceCode { get; set; }

    public ICommand CommandConfigureVirtualMachine { get; set; }
    public ICommand CommandExecuteCode { get; set; }
    public ICommand CommandDisplayAboutEmuX { get; set; }

    private VirtualMachineConfiguration _virtualMachineConfiguration;
    private IVirtualMachine _virtualMachine;
}