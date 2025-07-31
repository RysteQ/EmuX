using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Internal.BIOS.Interfaces;

/// <summary>
/// The device interrupt handler is used to handle all the device sub-interrupt code function calls
/// </summary>
public interface IDeviceInterruptHandler
{
    /// <summary>
    /// Executes the device logic <br/>
    /// Device ID: AL
    /// </summary>
    /// <param name="virtualCPU">The IVirtualCPU module to get the necessary information from</param>
    /// <param name="virtualDevices">All of the IVirtualDevices the IVirtualMachine implementation instance has</param>
    /// <exception cref="ArgumentNullException">Thrown if the virtual device with the specified ID cannot be found</exception>
    void ExecuteLogic(IVirtualCPU virtualCPU, IVirtualDevice[] virtualDevices);
}