using EmuXCore;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Internal.Device.USBDrives;
using EmuXUI.Models.Logic;
using EmuXUI.Models.Observable;
using EmuXUI.Models.Static;
using EmuXUI.Popups;
using EmuXUI.ViewModels.Internal;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace EmuXUI.ViewModels;

public sealed class VirtualMachineConfigurationViewModel : BaseViewModel
{
    public VirtualMachineConfigurationViewModel(ref IVirtualMachine virtualMachine)
    {
        CommandDeleteDisk = GenerateCommand(DeleteDisk);
        CommandAddDisk = GenerateCommand(AddDisk);
        CommandDeleteDevice = GenerateCommand(DeleteDevice);
        CommandAddDevice = GenerateCommand(AddDevice);
        CommandRestoreDefaultValue = GenerateCommand(InitialiseDefaultValues);
        CommandSave = GenerateCommand(SaveVirtualMachine);

        AvailableCPUs = new
        ([
            new("CPU Mk1", DIFactory.GenerateIVirtualCPU())
        ]);

        AvailableGPUs = new
        ([
            new("GPU Mk1", DIFactory.GenerateIVirtualGPU())
        ]);

        _virtualMachine = virtualMachine;

        InitialiseDefaultValues();
    }

    private void DeleteDisk()
    {
        if (SelectedDisk == null)
        {
            return;
        }

        VirtualMachineConfiguration.Disks.Remove(SelectedDisk);
        OnPropertyChanged(nameof(VirtualMachineConfiguration.Disks));
    }

    private void AddDisk()
    {
        byte diskNumber = 1;

        if (VirtualMachineConfiguration.Disks.Count > 100)
        {
            new InfoPopup(Enums.InfoPopupSeverity.Error, "Cannot have more than 100 disks").Activate();

            return;
        }

        if (VirtualMachineConfiguration.Disks.Any())
        {
            diskNumber = VirtualMachineConfiguration.Disks.Last().VirtualDisk.DiskNumber;
        }

        VirtualMachineConfiguration.Disks.Add(new(DIFactory.GenerateIVirtualDisk(diskNumber, (byte)NewDisk.Platters, (ushort)NewDisk.Tracks, (byte)NewDisk.Sectors)));
        OnPropertyChanged(nameof(VirtualMachineConfiguration.Disks));
    }

    private void DeleteDevice()
    {
        if (SelectedDevice == null)
        {
            return;
        }

        VirtualMachineConfiguration.Devices.Remove(SelectedDevice);
        OnPropertyChanged(nameof(VirtualMachineConfiguration.Devices));
    }

    private void AddDevice()
    {
        if (VirtualMachineConfiguration.Devices.Count > 100)
        {
            new InfoPopup(Enums.InfoPopupSeverity.Error, "Cannot have more than 100 devices").Activate();

            return;
        }

        if (NewDevice == null)
        {
            return;
        }

        VirtualMachineConfiguration.Devices.Add(NewDevice);
        OnPropertyChanged(nameof(VirtualMachineConfiguration.Devices));
    }

    private void InitialiseDefaultValues()
    {
        VirtualMachineConfiguration = new()
        {
            Disks =
            [
                new(DIFactory.GenerateIVirtualDisk(1, 16, 16, 64))
            ],

            Devices =
            [
                new("USB Drive 64Kb", typeof(UsbDrive64Kb))
            ],

            SelectedIOMemoryInKbAmount = 65_536,
            SelectedVideoMemoryInKbAmount = 921_600,
            SelectedGeneralPurposeMemoryInKbAmount = 1_048_576
        };

        AvailableDevices =
        [
            new("USB Drive 64Kb", typeof(UsbDrive64Kb))
        ];

        NewDisk = new();
        VirtualMachineConfiguration.SelectedCPU = AvailableCPUs.First();
        VirtualMachineConfiguration.SelectedGPU = AvailableGPUs.First();
        SelectedDisk = null;
        SelectedDevice = null;
        NewDevice = null;

        OnPropertyChanged(nameof(VirtualMachineConfiguration.Disks));
        OnPropertyChanged(nameof(VirtualMachineConfiguration.Devices));
        OnPropertyChanged(nameof(AvailableDevices));

        OnPropertyChanged(nameof(NewDisk));
        OnPropertyChanged(nameof(VirtualMachineConfiguration.SelectedCPU));
        OnPropertyChanged(nameof(VirtualMachineConfiguration.SelectedGPU));
        OnPropertyChanged(nameof(SelectedDisk));
        OnPropertyChanged(nameof(SelectedDevice));
        OnPropertyChanged(nameof(NewDevice));
    }

    private void SaveVirtualMachine()
    {
        IVirtualMachineBuilder virtualMachineBuilder = DIFactory.GenerateIVirtualMachineBuilder();
        
        virtualMachineBuilder = virtualMachineBuilder
            .SetCpu(VirtualMachineConfiguration.SelectedCPU.CPU)
            .SetMemory(DIFactory.GenerateIVirtualMemory(VirtualMachineConfiguration.SelectedIOMemoryInKbAmount * 1024, VirtualMachineConfiguration.SelectedVideoMemoryInKbAmount * 1024, VirtualMachineConfiguration.SelectedGeneralPurposeMemoryInKbAmount * 1024))
            .SetBios(DIFactory.GenerateIVirtualBIOS(DIFactory.GenerateIDiskInterruptHandler(), DIFactory.GenerateIRTCInterruptHandler(), DIFactory.GenerateIVideoInterruptHandler(), DIFactory.GenerateIDeviceInterruptHandler()))
            .SetRTC(DIFactory.GenerateIVirtualRTC())
            .SetGPU(VirtualMachineConfiguration.SelectedGPU.GPU)
            .AddVirtualDevice(DIFactory.GenerateIVirtualDevice<UsbDrive64Kb>(1));

        foreach (DiskSelection disk in VirtualMachineConfiguration.Disks)
        {
            virtualMachineBuilder = virtualMachineBuilder.AddDisk(disk.VirtualDisk);
        }

        for (int i = 0; i < VirtualMachineConfiguration.Devices.Count; i++)
        {
            virtualMachineBuilder = virtualMachineBuilder.AddVirtualDevice((IVirtualDevice)Activator.CreateInstance(VirtualMachineConfiguration.Devices[i].DeviceType, (ushort)(i + 1), null)!);
        }

        _virtualMachine = virtualMachineBuilder.Build();
    }

    public ICommand CommandDeleteDisk { get; init; }
    public ICommand CommandAddDisk { get; init; }
    public ICommand CommandDeleteDevice { get; init; }
    public ICommand CommandAddDevice { get; init; }
    public ICommand CommandRestoreDefaultValue { get; init; }
    public ICommand CommandSave { get; init; }

    public ObservableCollection<CpuSelection> AvailableCPUs { get; init; }
    public ObservableCollection<GpuSelection> AvailableGPUs { get; init; }
    public DiskSelection? SelectedDisk { get; set; }
    public DiskCreation NewDisk { get; set; }
    public ObservableCollection<DeviceSelection> AvailableDevices { get; set; }
    public DeviceSelection? SelectedDevice { get; set; }
    public DeviceSelection? NewDevice { get; set; }

    public VirtualMachineConfiguration VirtualMachineConfiguration { get; set; }

    private IVirtualMachine _virtualMachine;
}
