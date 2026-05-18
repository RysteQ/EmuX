using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Enums;

/// <summary>
/// The <see cref="IVirtualDevice"/> status is used to indicate if a device is operational or not.
/// </summary>
public enum DeviceStatus : byte
{
    Operational,
    NonOperational,
    NaN
}