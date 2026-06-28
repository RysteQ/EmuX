using System;
using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Interfaces.Exceptions;

/// <summary>
/// Raised when the requested <see cref="IVirtualDevice>"/> is not found.
/// </summary>
/// <param name="message">A detailed message of the exception.</param>
public sealed class VirtualDeviceNotFoundException(string message) : Exception(message);