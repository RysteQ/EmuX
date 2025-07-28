using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuXCore.VM.Internal.Device.USBDrives;

/// <summary>
/// This is an example of an IVirtualDevice for future contributions
/// </summary>
public class UsbDrive64Kb : IVirtualDevice
{
    public UsbDrive64Kb(ushort deviceId, IVirtualMachine? parentVirtualMachine = null)
    {
        DeviceId = deviceId;
        ParentVirtualMachine = parentVirtualMachine;
    }

    public void Execute() { }

    public IVirtualMachine? ParentVirtualMachine { get; set; }
    public ushort DeviceId { get; init; }
    public DeviceStatus Status { get; set; } = DeviceStatus.NonOperational;
    public byte[] Data { get; set; } = new byte[ushort.MaxValue];
}