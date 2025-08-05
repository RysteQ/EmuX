namespace EmuX_Console.Libraries.Interfaces;

/// <summary>
/// The ICommandHandler is responsible for organising all the commands available for use in EmuX_Console
/// </summary>
public interface ICommandHandler
{
    /// <summary>
    /// Executes the given command
    /// </summary>
    /// <param name="command">The command to execute</param>
    /// <exception cref="InvalidOperationException">The command wasn't found</exception>
    void ExecuteCommand(string command);

    /// <summary>
    /// All available commands
    /// </summary>
    IReadOnlyCollection<ICommand> AvailableCommands { get; }

    /// <summary>
    /// The ITerminalIOHandler implementation used to communicate with the terminal
    /// </summary>
    ITerminalIOHandler TerminalIOHandler { get; init; }
}