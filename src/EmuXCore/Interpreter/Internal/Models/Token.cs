using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuXCore.Interpreter.Internal.Models;

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