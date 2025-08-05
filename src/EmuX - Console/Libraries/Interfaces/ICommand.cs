namespace EmuX_Console.Libraries.Interfaces;

/// <summary>
/// The ICommand interface is used to contain any given command to a singular object for easy lookup and execution of said command
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Executes the logic of the command
    /// </summary>
    /// <param name="parameters">All if any parameters are required</param>
    void Execute(params object[] parameters);
    
    /// <summary>
    /// The string used to name the command
    /// </summary>
    string Name { get; }

    /// <summary>
    /// A short description of what the command does
    /// </summary>
    string Help { get; }

    /// <summary>
    /// The ITerminalIOHandler implementation used to communicate with the terminal
    /// </summary>
    ITerminalIOHandler TerminalIOHandler { get; init; }
}