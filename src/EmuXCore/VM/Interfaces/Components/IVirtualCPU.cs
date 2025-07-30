using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Interfaces.Components;

/// <summary>
/// The IVirtualCPU module is meant to emulate a real CPU
/// </summary>
public interface IVirtualCPU : IVirtualComponent
{
    /// <summary>
    /// Finds the specified register and returns it as type T
    /// </summary>
    /// <typeparam name="T">The register to cast it as</typeparam>
    /// <returns>The specified register caster as T</returns>
    /// <exception cref="KeyNotFoundException">If the register is not found then an exception is raised</exception>
    T GetRegister<T>() where T : IVirtualRegister;

    /// <summary>
    /// Finds the specified register and returns it as IVirtualRegister
    /// </summary>
    /// <param name="registerName">The register name, doesn't matter if it is AL or RAX or if it uppercase or lowercase, it will return the accumulator</param>
    /// <returns>The register if found, otherwise null</returns>
    IVirtualRegister? GetRegister(string registerName);

    /// <summary>
    /// All of the registers of the VCPU
    /// </summary>
    IReadOnlyCollection<IVirtualRegister> Registers { get; init; }

    /// <summary>
    /// A flag indicating if the processor has halted its execution of code
    /// </summary>
    bool Halted { get; set; }
}