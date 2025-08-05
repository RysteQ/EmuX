using EmuX_Console.Libraries;
using EmuX_Console.Libraries.Enums;
using EmuX_Console.Libraries.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

string input = string.Empty;
ITerminalIOHandler terminalIOHandler = new TerminalIOHandler("> ", ConsoleKey.UpArrow, ConsoleKey.DownArrow);

Console.Clear();

terminalIOHandler.Output([255, 69, 0], OutputFormat.Integer, OutputSeverity.Normal);
terminalIOHandler.Output([255, 69, 0], OutputFormat.Binary, OutputSeverity.Normal);
terminalIOHandler.Output([255, 69, 0], OutputFormat.Hexadecimal, OutputSeverity.Normal);
terminalIOHandler.Output(new VirtualRegisterRAX(), OutputFormat.Integer);
terminalIOHandler.Output(new VirtualRegisterRAX(), OutputFormat.Binary);
terminalIOHandler.Output(new VirtualRegisterRAX(), OutputFormat.Hexadecimal);

while (true)
{
    input = terminalIOHandler.GetUserInput();

    terminalIOHandler.Output("Input -> " + input, OutputSeverity.Normal);
    terminalIOHandler.Output("Input -> " + input, OutputSeverity.Error);
}