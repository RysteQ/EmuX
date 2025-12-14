using EmuXCore.VM.Interfaces.Components;
using EmuXUI.Models.Internal;
using System;

namespace EmuXUI.Models.Static;

public sealed class DeviceSelection
{
    public DeviceSelection(string deviceName, Type deviceType)
    {
        DeviceName = deviceName;
        DeviceType = deviceType;
    }

    public string DeviceName { get; init; }
    public Type DeviceType { get; init; }
}
