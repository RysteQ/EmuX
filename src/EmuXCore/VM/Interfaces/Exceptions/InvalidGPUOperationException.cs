using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Interfaces.Exceptions;

/// <summary>
/// Raised when the requested <see cref="IVirtualGPU>"/> operation is invalid as it violates the GPUs operational constraints.
/// </summary>
/// <param name="message">A detailed message of the exception.</param>
public sealed class InvalidGPUOperationException(string message) : Exception(message);