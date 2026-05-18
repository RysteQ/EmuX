using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Interfaces.Exceptions;

/// <summary>
/// Raised when the requested <see cref="IVirtualDisk.Platters>"/> is not accessible.
/// </summary>
/// <param name="message">A detailed message of the exception.</param>
public class InvalidDiskPlatterException(string message) : Exception(message);