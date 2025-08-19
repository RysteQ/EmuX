namespace EmuXCore.Interpreter.Interfaces.Models;

/// <summary>
/// This is used to hold the raw source code to be parsed into a singular object per line of said source code
/// </summary>
public interface ISourceCodeLine
{
    /// <summary>
    /// The source code line as is
    /// </summary>
    string SourceCode { get; init; }

    /// <summary>
    /// The line index that this line exists in the source code text
    /// </summary>
    int Line { get; init; }
}