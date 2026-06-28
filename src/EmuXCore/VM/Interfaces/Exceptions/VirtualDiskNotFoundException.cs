using System;
using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Interfaces.Exceptions;

/// <summary>
/// Raised when the requested <see cref="IVirtualDisk>"/> is not found.
/// </summary>
/// <param name="message">A detailed message of the exception.</param>
public sealed class VirtualDiskNotFoundException(string message) : Exception(message);