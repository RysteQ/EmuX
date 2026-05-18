using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Interfaces.Exceptions;

/// <summary>
/// Raised when the requested <see cref="IVirtualDisk.Tracks>"/> track is not accessible.
/// </summary>
/// <param name="message">A detailed message of the exception.</param>
public sealed class InvalidDiskTrackException(string message) : Exception(message);