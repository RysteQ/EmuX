using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Interfaces.Exceptions;

/// <summary>
/// Raised when the requested <see cref="IVirtualRegister"/> is not found.
/// </summary>
/// <param name="message">A detailed message of the exception.</param>
public sealed class VirtualRegisterNotFoundException(string message) : Exception(message);