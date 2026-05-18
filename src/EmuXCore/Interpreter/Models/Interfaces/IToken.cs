using EmuXCore.Interpreter.Enums;

namespace EmuXCore.Interpreter.Models.Interfaces;

/// <summary>
/// The token required for the parsing stage of the lexer.
/// </summary>
public interface IToken
{
    /// <summary>
    /// The type of the token.
    /// </summary>
    TokenType Type { get; init; }

    /// <summary>
    /// The representation of the token as it is in the source code.
    /// </summary>
    string SourceCode { get; init; }
}