using EmuXCore.Common.Interfaces;

namespace EmuXCore.Interpreter.Interfaces.Models;

/// <summary>
/// This is used to hold the parsing results into a singular, easy to use, object
/// </summary>
public interface ILexerResult
{
    /// <summary>
    /// The instructions that have been parsed
    /// </summary>
    IList<IInstruction> Instructions { get; init; }

    /// <summary>
    /// The labels that have been parsed
    /// </summary>
    IList<ILabel> Labels { get; init; }

    /// <summary>
    /// A flag to indicate if the parsing was successful
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// A list of all errors encountered during the process of parsing the source code
    /// </summary>
    public IList<string> Errors { get; init; }
}