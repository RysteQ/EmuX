using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.Models.Interfaces;

namespace EmuXCore.Interpreter.Models;

public class Token : IToken
{
    public Token(TokenType type, string sourceCode)
    {
        Type = type;
        SourceCode = sourceCode;
    }

    public TokenType Type { get; init; }
    public string SourceCode { get; init; }
}