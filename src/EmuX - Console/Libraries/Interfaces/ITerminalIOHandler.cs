using EmuX_Console.Libraries.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;
using System.Drawing;

namespace EmuX_Console.Libraries.Interfaces;

/// <summary>
/// The ITerminalIOHandler is used to interact with the IO of the console only
/// </summary>
public interface ITerminalIOHandler
{
    /// <summary>
    /// Displays the prompt for the user to enter any input they so desire <br/>
    /// There is also a history which if the user presses the LookbackKey then the previous input they entered will be selected <br/>
    /// The implementation of the ITerminalIOHandler should have an internal limit to the history size, the recommended maximum lookback size is 100.
    /// </summary>
    /// <returns>The user input</returns>
    string GetUserInput();

    /// <summary>
    /// Displays the prompt for the user to press a single key
    /// </summary>
    /// <returns>The key the user entered</returns>
    char GetUserKeyInput();

    /// <summary>
    /// Prints the given string to the console
    /// </summary>
    /// <param name="output">The string to print</param>
    /// <param name="severity">The severity of the message</param>
    void Output(string output, OutputSeverity severity);

    /// <summary>
    /// Prints the given virtual machine to the console
    /// <param name="virtualMachine">The virtual machine to print out</param>
    /// <param name="format">The integer format</param>
    /// <param name="severity">The severity of the message to adjust the output colour appropriately</param>
    void Output(IVirtualMachine virtualMachine);

    /// <summary>
    /// Prints the given virtual cpu to the console
    /// <param name="virtualCPU">The virtual cpu to print out</param>
    /// <param name="format">The integer format</param>
    /// <param name="severity">The severity of the message</param>
    void Output(IVirtualCPU virtualCPU);

    /// <summary>
    /// Prints the given virtual cpu to the console
    /// <param name="virtualRegister">The virtual register to print out</param>
    /// <param name="format">The integer format</param>
    /// <param name="severity">The severity of the message</param>
    void Output(IVirtualRegister virtualRegister, OutputFormat format);

    /// <summary>
    /// Prints the given byte buffer to the console
    /// <param name="buffer">The byte buffer to print out</param>
    /// <param name="format">The integer format</param>
    /// <param name="severity">The severity of the message</param>
    void Output(byte[] buffer, OutputFormat format, OutputSeverity severity);

    /// <summary>
    /// The input history <br/>
    /// WARNING: There is an upper limit depending in the implementation of the interface, so the last N input are available
    /// </summary>
    IList<string> InputHistory { get; }

    /// <summary>
    /// The input prompt
    /// </summary>
    string Prompt { get; init; }

    /// <summary>
    /// The key that triggers the lookback functionality backwards <br/>
    /// This should ALWAYS be different than the ForwardLookbackKey
    /// </summary>
    ConsoleKey BackwardLookbackKey { get; init; }

    /// <summary>
    /// The key that triggers the lookback functionality forwards <br/>
    /// This should ALWAYS be different than the BackwardLookbackKey
    /// </summary>
    ConsoleKey ForwardLookbackKey { get; init; }

    /// <summary>
    /// The error colour
    /// </summary>
    ConsoleColor ColourError { get; }

    /// <summary>
    /// The colour of the prompt, adjustable
    /// </summary>
    ConsoleColor ColourPrompt { get; set; }

    /// <summary>
    /// The colour of the user input, adjustable
    /// </summary>
    ConsoleColor ColoutInput { get; set; }

    /// <summary>
    /// The colour of the output for the Output() method, adjustable
    /// </summary>
    ConsoleColor ColourOutput { get; set; }

    /// <summary>
    /// The colour of the highlighted output for the Output() method, adjustable
    /// </summary>
    ConsoleColor ColourHighlight { get; set; }
}