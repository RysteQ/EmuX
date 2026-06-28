using System;

namespace EmuXCore.VM.Interfaces.Exceptions;

/// <summary>
/// Raised when the <see cref="IVirtualMachine"/> is not found.
/// </summary>
/// <param name="message">A detailed message of the exception.</param>
public sealed class VirtualMachineNotFoundException(string message) : Exception(message);