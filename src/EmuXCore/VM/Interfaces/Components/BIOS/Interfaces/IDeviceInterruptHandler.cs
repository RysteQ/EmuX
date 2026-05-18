using EmuXCore.VM.Interfaces.Exceptions;

namespace EmuXCore.VM.Interfaces.Components.BIOS.Interfaces;

/// <summary>
/// The <see cref="IVirtualDevice"/> interrupt handler.
/// </summary>
public interface IDeviceInterruptHandler
{
    /// <summary>
    /// Executes the device logic <br/>
    /// Device ID: AL
    /// </summary>
    /// <param name="virtualCPU">The <see cref="IVirtualCPU"/> module to get any necessary information from for the logic execution./</param>
    /// <param name="virtualDevices">All of the virtual devices of type <see cref="IVirtualDevice"/> the <see cref="IVirtualMachine"/> implementation instance has.</param>
    /// <exception cref="VirtualDeviceNotFoundException" />
    void ExecuteLogic(IVirtualCPU virtualCPU, IVirtualDevice[] virtualDevices);
}