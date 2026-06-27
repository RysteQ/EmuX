using EmuXCore.InstructionLogic.Interfaces.Exceptions;
using EmuXCore.Common.Interfaces;

namespace EmuXCore.InstructionLogic.Interfaces;

/// <summary>
/// Used to check if a instruction implementation of type <see cref="IInstruction"/> exists and to look up the implementation of the prefix.
/// </summary>
public interface IInstructionLookup
{
    /// <summary>
    /// Checks if the instruction exists.
    /// </summary>
    /// <param name="instruction">The instruction name to look up.</param>
    /// <returns>True if the instruction exists, otherwise false.</returns>
    public bool DoesInstructionExist(string instruction);

    /// <summary>
    /// Gets the type of the instruction.
    /// </summary>
    /// <param name="instruction">The instruction name to look up.</param>
    /// <exception cref="InvalidInstructionException" />
    /// <returns>The type of the instruction.</returns>
    public Type GetInstructionType(string instruction);

    /// <summary>
    /// Gets all of the available instruction names.
    /// </summary>
    public string[] Instructions { get; }
}