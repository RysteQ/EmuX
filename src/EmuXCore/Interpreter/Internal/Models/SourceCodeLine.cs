using EmuXCore.Interpreter.Interfaces;

namespace EmuXCore.Interpreter.Internal.Models;

public record SourceCodeLine : ISourceCodeLine
{
    public SourceCodeLine(string sourceCode, int line)
    {
        SourceCode = sourceCode;
        Line = line;
    }

    public string SourceCode { get; init; }
    public int Line { get; init; }
}