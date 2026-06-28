using System;
using EmuXCore.Common.Interfaces;

namespace EmuXCore.InstructionLogic.Interfaces.Exceptions;

/// <summary>
/// Raised when the requested <see cref="IInstruction"/> type does not exist.
/// </summary>
/// <param name="message">A detailed message of the exception.</param>
public sealed class InvalidInstructionException(string message) : Exception(message);