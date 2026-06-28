using EmuXCore.InstructionLogic.Interfaces.Exceptions;
using EmuXCore.Common.Interfaces;
using System;

namespace EmuXCore.InstructionLogic.Interfaces;

/// <summary>
/// Used to check if a prefix implementation of type <see cref="IPrefix"/> exists and to look up the implementation of the prefix.
/// </summary>
public interface IPrefixLookup
{
    /// <summary>
    /// Checks if the prefix exists.
    /// </summary>
    /// <param name="prefix">The prefix name to look up.</param>
    /// <returns>True if the prefix exists, otherwise false.</returns>
    public bool DoesPrefixExist(string prefix);
    
    /// <summary>
    /// Gets the type of the prefix.
    /// </summary>
    /// <param name="prefix">The prefix name to look up.</param>
    /// <exception cref="InvalidPrefixException" />
    /// <returns>The type of the prefix.</returns>
    public Type GetPrefixType(string prefix);
}
