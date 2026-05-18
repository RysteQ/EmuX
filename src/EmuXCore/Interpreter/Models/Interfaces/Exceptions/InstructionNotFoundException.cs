using EmuXCore.Interpreter.Models.Interfaces;

namespace EmuXCore.Common.Interfaces;

/// <summary>
/// Raised when the requested <see cref="IInstruction>"/> implementation cannot be found.
/// </summary>
/// <param name="message">A detailed message of the exception.</param>
public sealed class InstructionNotFoundException(string message) : Exception(message);