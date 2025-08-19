using EmuXCore.Interpreter.Interfaces.Models;

namespace EmuXCore.Interpreter.Interfaces.Logic;

/// <summary>
/// The IParser is responsible for grammatical rules and parsing the lexer tokens
/// </summary>
public interface IParser
{
    /// <summary>
    /// Parses the source code into executable instructions. If any error in encountered then the appropriate logs and status will be saved to the Success property and ErrorLog property
    /// </summary>
    /// <param name="tokens">The source code to parse</param>
    /// <returns>The result of the parse process</returns>
    ILexerResult Parse(IList<IToken> tokens);
}