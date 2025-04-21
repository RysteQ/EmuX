namespace EmuXCore.Interpreter.Interfaces;

public interface ILabel
{
    string LabelName { get; init; }
    int Line { get; init; }
}