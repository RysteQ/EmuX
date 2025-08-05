using EmuXCore.VM.Enums;

namespace EmuXCore.VM.Interfaces.Components;

/// <summary>
/// The IVirtualDevice interface is meant to give a generic foundation for all devices that can be created by the developer.
/// </summary>
public interface IVirtualDevice : IVirtualComponent
{
    /// <summary>
    /// Executes the logic of the device and writes or reads from and to the local memory
    /// </summary>
    void Execute();

    /// <summary>
    /// The ID of the device, must be unique
    /// </summary>
    ushort DeviceId { get; init; }

    /// <summary>
    /// The status of the device
    /// </summary>
    DeviceStatus Status { get; set; }

    /// <summary>
    /// The data of the device that is accessible through the memory map
    /// </summary>
    byte[] Data { get; set; }
}