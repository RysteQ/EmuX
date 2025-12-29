using EmuXCore;
using EmuXCore.VM.Internal.Device.USBDrives;
using EmuXUI.Enums;
using EmuXUI.Models.Logic;
using EmuXUI.Models.Observable;
using EmuXUI.Models.Static;
using EmuXUI.Popups;
using EmuXUI.ViewModels.Internal;
using Microsoft.Windows.AI.ContentSafety;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace EmuXUI.ViewModels;

public sealed class VirtualMachineConfigurationViewModel : BaseViewModel
{
    public VirtualMachineConfigurationViewModel(VirtualMachineConfiguration currentVirtualMachineConfiguration)
    {
        VirtualMachineConfiguration = currentVirtualMachineConfiguration;

        CommandDeleteDisk = GenerateCommand(DeleteDisk);
        CommandAddDisk = GenerateCommand(AddDisk);
        CommandDeleteDevice = GenerateCommand(DeleteDevice);
        CommandAddDevice = GenerateCommand(AddDevice);
        CommandRestoreDefaultValue = GenerateCommand(RestoreDefaultValues);
        CommandSaveConfiguration = GenerateCommand(SaveConfiguration);

        AvailableCPUs = new
        ([
            new("CPU Mk1", DIFactory.GenerateIVirtualCPU())
        ]);

        AvailableGPUs = new
        ([
            new("GPU Mk1", DIFactory.GenerateIVirtualGPU())
        ]);

        RestoreDefaultValues();
    }

    private void DeleteDisk()
    {
        if (SelectedDisk == null)
        {
            return;
        }

        if (Disks.Count == 1)
        {
            InfoPopup.Show(InfoPopupSeverity.Warning, "At least one disk is required");

            return;
        }

        Disks.Remove(SelectedDisk);
    }

    private void AddDisk()
    {
        byte diskNumber = 1;

        if (Disks.Count > 100)
        {
            InfoPopup.Show(InfoPopupSeverity.Error, "Cannot have more than 100 disks");

            return;
        }

        if (Disks.Any())
        {
            diskNumber = Disks.Last().VirtualDisk.DiskNumber;
        }

        Disks.Add(new(DIFactory.GenerateIVirtualDisk(diskNumber, (byte)NewDisk.Platters, (ushort)NewDisk.Tracks, (byte)NewDisk.Sectors)));
    }

    private void DeleteDevice()
    {
        if (SelectedDevice == null)
        {
            return;
        }

        Devices.Remove(SelectedDevice);
    }

    private void AddDevice()
    {
        if (Devices.Count > 100)
        {
            InfoPopup.Show(InfoPopupSeverity.Error, "Cannot have more than 100 devices");

            return;
        }

        if (NewDevice == null)
        {
            return;
        }

        Devices.Add(NewDevice);
    }

    private void RestoreDefaultValues()
    {
        if (Devices == null)
        {
            Devices = [];
        }
        else
        {
            Devices.Clear();
        }

        if (Disks == null)
        {
            Disks = [];
        }
        else
        {
            Disks.Clear();
        }

        AvailableDevices =
        [
            new("USB Drive 64Kb", typeof(UsbDrive64Kb))
        ];

        foreach (DeviceSelection device in VirtualMachineConfiguration.Devices)
        {
            if (device.DeviceType == typeof(UsbDrive64Kb))
            {
                Devices.Add(new("USB Drive 64Kb", typeof(UsbDrive64Kb)));
            }
            else
            {
                InfoPopup.Show(InfoPopupSeverity.Error, $"Device type {device.DeviceType} is not recognisable");
            }
        }

        foreach (DiskSelection disk in VirtualMachineConfiguration.Disks)
        {
            Disks.Add(disk);
        }

        SelectedIOMemoryInKbAmount = VirtualMachineConfiguration.SelectedIOMemoryInKbAmount;
        SelectedGeneralPurposeMemoryInKbAmount = VirtualMachineConfiguration.SelectedGeneralPurposeMemoryInKbAmount;

        NewDisk = new();
        SelectedCPU = AvailableCPUs.First(record => record.CPU.GetType() == VirtualMachineConfiguration.SelectedCPU.CPU.GetType());
        SelectedGPU = AvailableGPUs.First(record => record.GPU.GetType() == VirtualMachineConfiguration.SelectedGPU.GPU.GetType());
        SelectedDisk = null;
        SelectedDevice = null;
        NewDevice = null;

        OnPropertyChanged(nameof(SelectedCPU));
        OnPropertyChanged(nameof(SelectedGPU));
    }

    private void SaveConfiguration()
    {
        VirtualMachineConfiguration.SelectedCPU = SelectedCPU;
        VirtualMachineConfiguration.SelectedGPU = SelectedGPU;
        VirtualMachineConfiguration.Disks = Disks.ToList();
        VirtualMachineConfiguration.Devices = Devices.ToList();
        VirtualMachineConfiguration.SelectedIOMemoryInKbAmount = SelectedIOMemoryInKbAmount;
        VirtualMachineConfiguration.SelectedGeneralPurposeMemoryInKbAmount = SelectedGeneralPurposeMemoryInKbAmount;

        SavedConfiguration?.Invoke(this, VirtualMachineConfiguration);
    }

    public ICommand CommandDeleteDisk { get; init; }
    public ICommand CommandAddDisk { get; init; }
    public ICommand CommandDeleteDevice { get; init; }
    public ICommand CommandAddDevice { get; init; }
    public ICommand CommandRestoreDefaultValue { get; init; }
    public ICommand CommandSaveConfiguration { get; init; }

    public event EventHandler? SavedConfiguration;

    public CpuSelection SelectedCPU
    {
        get => field;
        set => field = value;
    }
    
    public GpuSelection SelectedGPU
    {
        get => field;
        set => field = value;
    }

    public ObservableCollection<DiskSelection> Disks
    {
        get => field;
        set
        {
            OnPropertyChanged(ref field, value);
        }
    }

    public ObservableCollection<DeviceSelection> Devices
    {
        get => field;
        set => OnPropertyChanged(ref field, value);
    }

    public int SelectedIOMemoryInKbAmount
    {
        get => field;
        set
        {
            if (value > MAXIMUM_IO_MEMORY_KB)
            {
                InfoPopup.Show(InfoPopupSeverity.Warning, $"IO memory cannot exceed {MAXIMUM_IO_MEMORY_KB} kilobytes");

                OnPropertyChanged(ref field, MAXIMUM_IO_MEMORY_KB);

                return;
            }

            OnPropertyChanged(ref field, value);
        }
    }
    
    public int SelectedGeneralPurposeMemoryInKbAmount
    {
        get => field;
        set
        {
            if (value > MAXIMUM_GP_MEMORY_KB)
            {
                InfoPopup.Show(InfoPopupSeverity.Warning, $"General purpose memory cannot exceed {MAXIMUM_GP_MEMORY_KB} kilobytes");

                OnPropertyChanged(ref field, MAXIMUM_GP_MEMORY_KB);

                return;
            }

            OnPropertyChanged(ref field, value);
        }
    }

    public ObservableCollection<CpuSelection> AvailableCPUs { get; init; }
    public ObservableCollection<GpuSelection> AvailableGPUs { get; init; }
    public DiskSelection? SelectedDisk { get; set; }
    public DiskCreation NewDisk { get; set; }
    public ObservableCollection<DeviceSelection> AvailableDevices { get; set; }
    public DeviceSelection? SelectedDevice { get; set; }
    public DeviceSelection? NewDevice { get; set; }

    public VirtualMachineConfiguration VirtualMachineConfiguration { get; init; }

    private const int MAXIMUM_IO_MEMORY_KB = 512;
    private const int MAXIMUM_GP_MEMORY_KB = 2048;
}
