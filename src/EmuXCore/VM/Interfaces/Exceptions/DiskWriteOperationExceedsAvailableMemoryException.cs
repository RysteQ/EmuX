using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Interfaces.Exceptions;

/// <summary>
/// Raised when a <see cref="IVirtualDisk>"/> write operation exceeds the available memory.
/// </summary>
/// <param name="message">A detailed message of the exception.</param>
public sealed class DiskWriteOperationExceedsAvailableMemoryException(string message) : Exception(message);