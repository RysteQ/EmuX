using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces.Exceptions;

namespace EmuXCore.VM.Interfaces.Components;

/// <summary>
/// The interface is meant to give a generic level of a foundation for all virtualised devices.
/// </summary>
public interface IVirtualDevice : IVirtualComponent
{
    /// <summary>
    /// Executes the logic of the device. <br/>
    /// </summary>
    /// <exception cref="VirtualMachineNotFoundException" />
    void Execute();

    /// <summary>
    /// The ID of the device, must be unique.
    /// </summary>
    ushort DeviceId { get; init; }

    /// <summary>
    /// The status of the device.
    /// </summary>
    DeviceStatus Status { get; set; }

    /// <summary>
    /// The data of the device that is accessible through the memory map.
    /// </summary>
    byte[] Data { get; set; }
}