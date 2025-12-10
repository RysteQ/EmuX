using EmuXCore;
using EmuXCore.VM.Internal.Device.USBDrives;
using EmuXUI.Models;
using EmuXUI.Models.Static;
using EmuXUI.ViewModels.Internal;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace EmuXUI.ViewModels;

public sealed class VirtualMachineConfigurationViewModel : BaseViewModel
{
    public VirtualMachineConfigurationViewModel()
    {
        CommandDeleteDisk = GenerateCommand(DeleteDisk);
        CommandAddDisk = GenerateCommand(AddDisk);
        CommandDeleteDevice = GenerateCommand(DeleteDevice);
        CommandAddDevice = GenerateCommand(AddDevice);

        AvailableCPUs = new
        ([
            new("CPU Mk1", DIFactory.GenerateIVirtualCPU())
        ]);

        AvailableGPUs = new
        ([
            new("GPU Mk1", DIFactory.GenerateIVirtualGPU())
        ]);

        Disks =
        [
            new(DIFactory.GenerateIVirtualDisk(1, 16, 16, 64))
        ];

        Devices =
        [
            new("USB Drive 64Kb", typeof(UsbDrive64Kb))
        ];

        SelectedIOMemoryInKbAmount = 65_536;
        SelectedVideoMemoryInKbAmount = 921_600;
        SelectedGeneralPurposeMemoryInKbAmount = 1_048_576;

        SelectedCPU = AvailableCPUs.First();
        SelectedGPU = AvailableGPUs.First();

        OnPropertyChanged(nameof(SelectedCPU));
        OnPropertyChanged(nameof(SelectedGPU));
    }
    
    private void DeleteDisk()
    {
        if (SelectedDisk == null)
        {
            return;
        }

        Disks.Remove(SelectedDisk);
        OnPropertyChanged(nameof(Disks));
    }

    private void AddDisk()
    {

    }

    private void DeleteDevice()
    {
        if (SelectedDevice == null)
        {
            return;
        }

        Devices.Remove(SelectedDevice);
        OnPropertyChanged(nameof(Devices));
    }

    private void AddDevice()
    {

    }

    public ICommand CommandDeleteDisk { get; private set; }
    public ICommand CommandAddDisk { get; private set; }
    public ICommand CommandDeleteDevice { get; private set; }
    public ICommand CommandAddDevice { get; private set; }

    public ObservableCollection<CpuSelection> AvailableCPUs { get; init; }
    public CpuSelection SelectedCPU { get; set; }

    public ObservableCollection<GpuSelection> AvailableGPUs { get; init; }
    public GpuSelection SelectedGPU { get; set; }

    public ObservableCollection<DiskSelection> Disks { get; set; }
    public DiskSelection? SelectedDisk { get; set; }

    public ObservableCollection<DeviceSelection> Devices { get; set; }
    public DeviceSelection? SelectedDevice { get; set; }

    public uint SelectedIOMemoryInKbAmount { get; set; }
    public uint SelectedVideoMemoryInKbAmount { get; set; }
    public uint SelectedGeneralPurposeMemoryInKbAmount { get; set; }
}
