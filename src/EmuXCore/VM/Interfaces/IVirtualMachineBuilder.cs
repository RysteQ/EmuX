using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.BIOS;

namespace EmuXCore.VM.Interfaces;

/// <summary>
/// Due to the IVirtualMachine complexity the use of the IVirtualMachineBuilder is recommended when building an instance of the IVirtualMachine interface
/// </summary>
public interface IVirtualMachineBuilder
{
    /// <summary>
    /// Sets the CPU of the IVirtualMachine
    /// </summary>
    /// <param name="virtualCPU">The IVirtualCPU implementation</param>
    IVirtualMachineBuilder SetCpu(IVirtualCPU virtualCPU);

    /// <summary>
    /// Sets the memory of the IVirtualMachine
    /// </summary>
    /// <param name="virtualMemory">The IVirtualMemory implementation</param>
    IVirtualMachineBuilder SetMemory(IVirtualMemory virtualMemory);

    /// <summary>
    /// Sets the GPU of the IVirtualMachine
    /// </summary>
    /// <param name="virtualGPU">The IVirtualGPU implementation</param>
    IVirtualMachineBuilder SetGPU(IVirtualGPU virtualGPU);

    /// <summary>
    /// Adds a IVirtualDisk to the IVirtualMachine
    /// </summary>
    /// <param name="virtualDisk">The IVirtualDisk implementation</param>
    IVirtualMachineBuilder AddDisk(IVirtualDisk virtualDisk);

    /// <summary>
    /// Sets the BIOS of the IVirtualMachine
    /// </summary>
    /// <param name="virtualBios">The IVirtualBIOS implementation</param>
    IVirtualMachineBuilder SetBios(IVirtualBIOS virtualBios);

    /// <summary>
    /// Sets the RTC of the IVirtualMachine
    /// </summary>
    /// <param name="virtualRTC">The IVirtualRTC implementation</param>
    IVirtualMachineBuilder SetRTC(IVirtualRTC virtualRTC);

    /// <summary>
    /// Adds a IVirtualDevice to the IVirtualMachine
    /// </summary>
    /// <param name="virtualDevice">The IVirtualDevice implementation</param>
    IVirtualMachineBuilder AddVirtualDevice(IVirtualDevice virtualDevice);

    /// <summary>
    /// Builds an instance of the IVirtualMachine
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if a necessary module (IVirtualCPU, IVirtualMemory, IVirtualGPU, IVirtualBIOS, IVirtualRTC) has not been supplied beforehand</exception>
    /// <returns>The new instance of the IVirtualMachine</returns>
    IVirtualMachine Build();
}