using EmuXUI.Models.Static;
using System;
using System.Collections.Generic;

namespace EmuXUI.Models.Logic;

public sealed class VirtualMachineConfiguration : EventArgs
{
    public CpuSelection SelectedCPU { get; set; }
    public GpuSelection SelectedGPU { get; set; }
    public List<DiskSelection> Disks { get; set; }
    public List<DeviceSelection> Devices { get; set; }
    public int SelectedIOMemoryInKbAmount { get; set; }
    public int SelectedGeneralPurposeMemoryInKbAmount { get; set; }
}