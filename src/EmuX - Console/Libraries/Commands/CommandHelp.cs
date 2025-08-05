using EmuX_Console.Libraries.Enums;
using EmuX_Console.Libraries.Interfaces;

namespace EmuX_Console.Libraries.Commands;

public class CommandHelp : ICommand
{
    public void Execute(params object[] parameters)
    {
        if (parameters.GetType() != typeof(IReadOnlyCollection<ICommand>))
        {
            throw new InvalidCastException($"Expected {nameof(IReadOnlyCollection<ICommand>)}, got {parameters.GetType()}");
        }
        else if (parameters.Length != 1)
        {
            throw new ArgumentException($"Expected one argument, got {parameters.Length}");
        }

        foreach (ICommand command in (IReadOnlyCollection<ICommand>)parameters[0])
        {
            TerminalIOHandler.Output($"{command.Name} \t {command.Help}", OutputSeverity.Normal);
        }
    }

    public string Name => "help";
    public string Help => "displays all available commands";

    public ITerminalIOHandler TerminalIOHandler { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
}