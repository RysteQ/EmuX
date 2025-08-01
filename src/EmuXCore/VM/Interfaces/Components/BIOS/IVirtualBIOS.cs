﻿using EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts;
using EmuXCore.VM.Interfaces.Components.Enums.SubInterrupts;

namespace EmuXCore.VM.Interfaces.Components.BIOS;

/// <summary>
/// The wrapper BIOS interface for all the sub-interrupt calls
/// </summary>
public interface IVirtualBIOS : IVirtualComponent
{
    /// <summary>
    /// Handles the disk interrupts
    /// </summary>
    /// <param name="interruptCode">The disk interrupt function to execute</param>
    /// <exception cref="ArgumentNullException">Thrown if the property ParentVirtualMachine is null</exception>
    void HandleDiskInterrupt(DiskInterrupt interruptCode);

    /// <summary>
    /// Handles the disk interrupts <br/>
    /// Warning: Calling this method will always set the CF EFlags value to false
    /// </summary>
    /// <param name="interruptCode">The RTC interrupt function to execute</param>
    /// <exception cref="ArgumentNullException">Thrown if the property ParentVirtualMachine is null</exception>
    void HandleRTCInterrupt(RTCInterrupt interruptCode);

    /// <summary>
    /// Handles the video interrupts <br/>
    /// </summary>
    /// <param name="interruptCode">The video interrupt function to execute</param>
    /// <exception cref="ArgumentNullException">Thrown if the property ParentVirtualMachine is null</exception>
    void HandleVideoInterrupt(VideoInterrupt interruptCode);

    /// <summary>
    /// Handles the video interrupts <br/>
    /// </summary>
    /// <param name="interruptCode">The device interrupt function to execute</param>
    /// <exception cref="ArgumentNullException">Thrown if the property ParentVirtualMachine is null</exception>
    void HandleDeviceInterrupt(DeviceInterrupt interruptCode);
}