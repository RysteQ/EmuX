using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.BIOS;

namespace EmuXCore.VM.Interfaces;

/// <summary>
/// Due to the <see cref="IVirtualMachine"/> complexity the use of the IVirtualMachineBuilder is recommended when building an instance of the IVirtualMachine interface.
/// </summary>
public interface IVirtualMachineBuilder
{
    /// <summary>
    /// Sets the CPU of the <see cref="IVirtualMachine"/>.
    /// </summary>
    /// <param name="virtualCPU">The <see cref="IVirtualCPU"/> implementation.</param>
    IVirtualMachineBuilder SetCPU(IVirtualCPU virtualCPU);
    
    /// <summary>
    /// Sets the GPU of the <see cref="IVirtualMachine"/>.
    /// </summary>
    /// <param name="virtualCPU">The <see cref="IVirtualGPU"/> implementation.</param>
    IVirtualMachineBuilder SetGPU(IVirtualGPU virtualGPU);

    /// <summary>
    /// Sets the memory of the <see cref="IVirtualMachine"/>.
    /// </summary>
    /// <param name="virtualCPU">The <see cref="IVirtualMemory"/> implementation.</param>
    IVirtualMachineBuilder SetMemory(IVirtualMemory virtualMemory);

    /// <summary>
    /// Adds a IVirtualDisk to the <see cref="IVirtualMachine"/>.
    /// </summary>
    /// <param name="virtualCPU">The <see cref="IVirtualDisk"/> implementation.</param>
    IVirtualMachineBuilder AddDisk(IVirtualDisk virtualDisk);

    /// <summary>
    /// Sets the BIOS of the <see cref="IVirtualMachine"/>.
    /// </summary>
    /// <param name="virtualCPU">The <see cref="IVirtualBIOS"/> implementation.</param>
    IVirtualMachineBuilder SetBios(IVirtualBIOS virtualBios);

    /// <summary>
    /// Sets the RTC of the <see cref="IVirtualMachine"/>.
    /// </summary>
    /// <param name="virtualCPU">The <see cref="IVirtualRTC"/> implementation.</param>
    IVirtualMachineBuilder SetRTC(IVirtualRTC virtualRTC);

    /// <summary>
    /// Adds a IVirtualDevice to the <see cref="IVirtualMachine"/>.
    /// </summary>
    /// <param name="virtualCPU">The <see cref="IVirtualDevice"/> implementation.</param>
    IVirtualMachineBuilder AddVirtualDevice(IVirtualDevice virtualDevice);

    /// <summary>
    /// Builds an instance of the <see cref="IVirtualMachine"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if a necessary module (<see cref="IVirtualCPU"/>, <see cref="IVirtualMemory"/>, <see cref="IVirtualGPU"/>, <see cref="IVirtualBIOS"/>, <see cref="IVirtualRTC"/>) has not been provided beforehand.</exception>
    /// <returns>The new instance of the <see cref="IVirtualMachine"/>.</returns>
    IVirtualMachine Build();
}