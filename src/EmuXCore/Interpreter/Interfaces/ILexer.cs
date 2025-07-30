namespace EmuXCore.Interpreter.Interfaces;

/// <summary>
/// The ILexer is responsible for parsing the source code into the IL represenation of said code
/// </summary>
public interface ILexer
{
    /// <summary>
    /// Parses the source code into executable instructions. If any error in encountered then the appropriate logs and status will be saved to the Success property and ErrorLog property
    /// </summary>
    /// <param name="codeToParse">The source code to parse</param>
    /// <returns>The result of the parse process</returns>
    ILexerResult Parse(string codeToParse);
}