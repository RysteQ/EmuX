using System;
using EmuXCore.VM.Interfaces.Components.BIOS;

namespace EmuXCore.VM.Interfaces.Exceptions;

/// <summary>
/// Raised when the requested <see cref="IVirtualBIOS>"/> interrupt category is not implemented.
/// </summary>
/// <param name="message">A detailed message of the exception.</param>
public sealed class VirtualBIOSInterruptNotImplementedException(string message) : Exception(message);