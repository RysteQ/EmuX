using System;
using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Interfaces.Exceptions;

/// <summary>
/// Raised when the requested <see cref="IVirtualDisk.Sectors>"/> sector is not accessible.
/// </summary>
/// <param name="message">A detailed message of the exception.</param>
public sealed class InvalidDiskSectorException(string message) : Exception(message);