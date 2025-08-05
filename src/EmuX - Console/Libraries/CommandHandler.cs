using EmuX_Console.Libraries.Commands;
using EmuX_Console.Libraries.Interfaces;
using System.Collections.ObjectModel;

namespace EmuX_Console.Libraries;

public class CommandHandler : ICommandHandler
{
    public CommandHandler(ITerminalIOHandler terminalIOHandler)
    {
        TerminalIOHandler = terminalIOHandler;
    }

    public void ExecuteCommand(string command)
    {
        ICommand? commandToExecute = AvailableCommands.FirstOrDefault(selectedCommand => selectedCommand.Name == command);
    
        if (commandToExecute == null)
        {
            throw new InvalidOperationException($"Command {command} could not be found");
        }

        commandToExecute.Execute();
    }
    
    public IReadOnlyCollection<ICommand> AvailableCommands => _availableCommands;
    public ITerminalIOHandler TerminalIOHandler { get; init; }

    private ReadOnlyCollection<ICommand> _availableCommands = new([new CommandHelp()]);
}