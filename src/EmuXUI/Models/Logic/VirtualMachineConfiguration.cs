using EmuXUI.Models.Static;
using System;
using System.Collections.ObjectModel;

namespace EmuXUI.Models.Logic;

public sealed class VirtualMachineConfiguration : EventArgs
{
    public CpuSelection SelectedCPU { get; set; }
    public GpuSelection SelectedGPU { get; set; }
    public ObservableCollection<DiskSelection> Disks { get; set; }
    public ObservableCollection<DeviceSelection> Devices { get; set; }
    public uint SelectedIOMemoryInKbAmount { get; set; }
    public uint SelectedVideoMemoryInKbAmount { get; set; }
    public uint SelectedGeneralPurposeMemoryInKbAmount { get; set; }
}