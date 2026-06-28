using System;
using EmuXCore.VM.Interfaces.Actions;

namespace EmuXCore.VM.Interfaces.Exceptions;

/// <summary>
/// Raised when the action or anything related to it is invalid for the <see cref="IVmAction.Undo(IVirtualMachine)"/> or <see cref="IVmAction.Redo(IVirtualMachine)"/> methods.
/// </summary>
/// <param name="message">A detailed message of the exception.</param>
public sealed class InvalidActionException(string message) : Exception(message);