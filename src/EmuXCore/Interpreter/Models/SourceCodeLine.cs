using EmuXCore.Interpreter.Models.Interfaces;

namespace EmuXCore.Interpreter.Models;

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