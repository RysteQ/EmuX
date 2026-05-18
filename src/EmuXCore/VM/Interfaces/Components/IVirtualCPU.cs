using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Interfaces.Exceptions;

namespace EmuXCore.VM.Interfaces.Components;

/// <summary>
/// The IVirtualCPU module is meant to emulate the CPU component of a real machine.
/// </summary>
public interface IVirtualCPU : IVirtualComponent
{
    /// <summary>
    /// Finds the specified register and returns it.
    /// </summary>
    /// <typeparam name="T">The register to retrieve.</typeparam>
    /// <exception cref="VirtualRegisterNotFoundException" />
    /// <returns>The found register.</returns>
    T GetRegister<T>() where T : IVirtualRegister;

    /// <summary>
    /// Finds the specified register and returns it.
    /// </summary>
    /// <typeparam name="T">The register to retrieve.</typeparam>
    /// <exception cref="VirtualRegisterNotFoundException" />
    /// <returns>The found register.</returns>
    IVirtualRegister GetRegister(string registerName);

    /// <summary>
    /// All of the registers of the VCPU.
    /// </summary>
    IReadOnlyCollection<IVirtualRegister> Registers { get; init; }

    /// <summary>
    /// A flag indicating if the processor has halted the execution of instructions.
    /// </summary>
    bool Halted { get; set; }
}