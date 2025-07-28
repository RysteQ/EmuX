using EmuXCore.Common.Interfaces;

namespace EmuXCore.Interpreter.Interfaces;

public interface ILexerResult
{
    IList<IInstruction> Instructions { get; init; }
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