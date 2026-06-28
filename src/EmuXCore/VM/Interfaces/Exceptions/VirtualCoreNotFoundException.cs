using System;
using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Interfaces.Exceptions;

/// <summary>
/// Raised when the requested core of the <see cref="IVirtualCPU>"/> is not found.
/// </summary>
/// <param name="message">A detailed message of the exception.</param>
public sealed class VirtualCoreNotFoundException(string message) : Exception(message);