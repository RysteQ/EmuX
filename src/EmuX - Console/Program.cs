using EmuX_Console.Libraries;
using EmuX_Console.Libraries.Enums;
using EmuX_Console.Libraries.Interfaces;

string input = string.Empty;
string sourceCode = string.Empty;
ITerminalIOHandler terminalIOHandler = new TerminalIOHandler("> ", ConsoleKey.UpArrow, ConsoleKey.DownArrow);

Console.Clear();

terminalIOHandler.Output("-== EmuX nano ==-", OutputSeverity.Important);
terminalIOHandler.Output("Enter 'h' for all the available commands\n", OutputSeverity.Normal);

while (true)
{
    input = terminalIOHandler.GetUserInput();

    switch (input.Split(' ').First())
    {
        case "h":
            terminalIOHandler.Output("h - prints out all available commands", OutputSeverity.Normal);
            terminalIOHandler.Output("v - prints out the version of EmuX nano", OutputSeverity.Normal);
            terminalIOHandler.Output("c - clears the console window", OutputSeverity.Normal);
            terminalIOHandler.Output("e - exits EmuX nano", OutputSeverity.Normal);
            terminalIOHandler.Output("o [file] - load a file in memory", OutputSeverity.Normal);
            terminalIOHandler.Output("sc - show the source code loaded in memory", OutputSeverity.Normal);
            break;

        case "v":
            terminalIOHandler.Output("Version 1.0.0", OutputSeverity.Normal);
            break;

        case "c":
            Console.Clear();
            break;

        case "e":
            terminalIOHandler.Output("Are you sure you want to exit EmuX nano (y/n)", OutputSeverity.Important);

            if (terminalIOHandler.GetUserKeyInput() == 'y')
            {
                Environment.Exit(0);
            }

            break;

        case "o":
            if (!File.Exists(string.Join(' ', input.Split(' ').Skip(1))))
            {
                terminalIOHandler.Output($"Error, file {string.Join(' ', input.Split(' ').Skip(1))} does not exist", OutputSeverity.Error);
                break;
            }
            else if (!string.Join(' ', input.Split(' ').Skip(1)).EndsWith("asm"))
            {
                terminalIOHandler.Output($"Error, file {string.Join(' ', input.Split(' ').Skip(1))} is not of type .asm", OutputSeverity.Error);
                break;
            }

            sourceCode = File.ReadAllText(string.Join(' ', input.Split(' ').Skip(1)));
            
            break;

        case "sc":
            if (string.IsNullOrEmpty(sourceCode))
            {
                terminalIOHandler.Output("No source code detected in memory, either open a file or write an application in the EmuX editor", OutputSeverity.Error);
                break;
            }

            terminalIOHandler.Output(sourceCode, OutputSeverity.Normal);
            
            break;

        case "":
            break;

        default:
            terminalIOHandler.Output($"Error, {input} is not recognised as a command, type help to view all available commands", OutputSeverity.Error);
            break;
    }
}