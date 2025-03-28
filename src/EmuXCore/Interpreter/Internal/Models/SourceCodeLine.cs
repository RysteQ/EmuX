using EmuXCore.Interpreter.Interfaces;

namespace EmuXCore.Interpreter.Internal.Models;

public class SourceCodeLine(string sourceCode, int line) : ISourceCodeLine
{
    public string SourceCode { get; init; } = sourceCode;
    public int Line { get; init; } = line;
}