namespace EmuXCore.Interpreter.Interfaces;

/// <summary>
/// This is used as a result of the parsing process since the parsing process can output either an IInstruction implementation instance or an ILabel implementation instance
/// </summary>
public interface ILabel
{
    /// <summary>
    /// The name of the label
    /// </summary>
    string Name { get; init; }
    
    /// <summary>
    /// The line the label is at in the source code
    /// </summary>
    int Line { get; init; }
}