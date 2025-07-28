namespace EmuXCore.Interpreter.Interfaces;

public interface ILabel
{
    string Name { get; init; }
    
    /// <summary>
    /// The line the label is at in the source code
    /// </summary>
    int Line { get; init; }
}