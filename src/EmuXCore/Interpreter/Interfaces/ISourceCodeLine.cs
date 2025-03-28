namespace EmuXCore.Interpreter.Interfaces;

public interface ISourceCodeLine
{
    string SourceCode { get; init; }
    int Line { get; init; }
}