using EmuXCore.Interpreter.Enums;

namespace EmuXCore.Interpreter.Interfaces.Models;

/// <summary>
/// The token required for the parsing stage of the lexer parser stafe
/// </summary>
public interface IToken
{
    TokenType Type { get; init; }
    string SourceCode { get; init; }
}