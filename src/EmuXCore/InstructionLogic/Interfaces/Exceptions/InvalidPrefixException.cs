using EmuXCore.Common.Interfaces;

namespace EmuXCore.InstructionLogic.Interfaces.Exceptions;

/// <summary>
/// Raised when the requested <see cref="IPrefix"/> type does not exist.
/// </summary>
/// <param name="message">A detailed message of the exception.</param>
public sealed class InvalidPrefixException(string message) : Exception(message);