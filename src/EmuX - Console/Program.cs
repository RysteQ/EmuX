using EmuX_Console.Libraries;
using EmuX_Console.Libraries.Enums;
using EmuX_Console.Libraries.Implementations;
using EmuX_Console.Libraries.Interfaces;
using EmuX_Console.Programs.Implementations;
using EmuX_Console.Programs.Interfaces;

string input = string.Empty;
string sourceCode = string.Empty;
ITerminalIOHandler terminalIOHandler = new TerminalIOHandler("> ", ConsoleKey.UpArrow, ConsoleKey.DownArrow);
ICodeEditor codeEditor = new CodeEditor(terminalIOHandler);

Console.Clear();

terminalIOHandler.Output("-== EmuX nano ==-\n", OutputSeverity.Important);
terminalIOHandler.Output("Enter 'help' for all the available commands\n", OutputSeverity.Normal);

terminalIOHandler.BackgroundColour = ConsoleColor.Black;

while (true)
{
    input = terminalIOHandler.GetUserInput();

    switch (input.Split(' ').First())
    {
        case "help":
            terminalIOHandler.Output("help - prints out all available commands\n", OutputSeverity.Normal);
            terminalIOHandler.Output("version - prints out the version of EmuX nano\n", OutputSeverity.Normal);
            terminalIOHandler.Output("clear - clears the console window\n", OutputSeverity.Normal);
            terminalIOHandler.Output("exit - exits EmuX nano\n", OutputSeverity.Normal);
            terminalIOHandler.Output("open [file] - load a file in memory\n", OutputSeverity.Normal);
            terminalIOHandler.Output("sc - show the source code loaded in memory\n", OutputSeverity.Normal);
            terminalIOHandler.Output("edit - opens the build in editor\n", OutputSeverity.Normal);
            break;

        case "version":
            terminalIOHandler.Output("Version 1.0.0\n", OutputSeverity.Normal);
            break;

        case "clear":
            Console.Clear();
            break;

        case "exit":
            terminalIOHandler.Output("Are you sure you want to exit EmuX nano (y/n)\n", OutputSeverity.Important);

            if (terminalIOHandler.GetUserKeyInput().KeyChar == 'y')
            {
                Environment.Exit(0);
            }

            break;

        case "open":
            input = string.Join(' ', input.Split(' ').Skip(1));

            if (!File.Exists(input))
            {
                terminalIOHandler.Output($"Error, file {input} does not exist\n", OutputSeverity.Error);
                break;
            }
            else if (!input.EndsWith("asm"))
            {
                terminalIOHandler.Output($"Error, file {input} is not of type .asm\n", OutputSeverity.Error);
                break;
            }

            sourceCode = File.ReadAllText(input);
            
            break;

        case "sc":
            if (string.IsNullOrEmpty(sourceCode))
            {
                terminalIOHandler.Output("No source code detected in memory, either open a file or write an application in the EmuX editor\n", OutputSeverity.Error);
                break;
            }

            terminalIOHandler.Output($"{sourceCode}\n", OutputSeverity.Normal);
            
            break;

        case "edit":
            codeEditor.Init(sourceCode);
            sourceCode = codeEditor.Text;

            break;

        case "":
            break;

        default:
            terminalIOHandler.Output($"Error, {input} is not recognised as a command, type help to view all available commands\n", OutputSeverity.Error);
            break;
    }
}