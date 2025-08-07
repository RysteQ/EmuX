namespace EmuX_Console.Libraries.Interfaces;

public interface ICodeEditor
{
    /// <summary>
    /// Initialise the code editor
    /// </summary>
    void Init(string text);

    /// <summary>
    /// The text the user entered or altered in the editor
    /// </summary>
    string Text { get; }

    /// <summary>
    /// A flag to indicate if the line numbers should be visible
    /// </summary>
    bool ShowLines { get; init; }

    /// <summary>
    /// The main modifier key is used to invoke different functions inside of the editor
    /// </summary>
    ConsoleModifiers MainModifierKey { get; init; }

    /// <summary>
    /// The second key combination to go to a certain line
    /// </summary>
    ConsoleKey GoToLineCharacter { get; init; }

    /// <summary>
    /// The second key combination to find a certain string inside of the Text property
    /// </summary>
    ConsoleKey FindStringCharacter { get; init; }

    /// <summary>
    /// The second key combination to go to find and replace a string with another string
    /// </summary>
    ConsoleKey ReplaceStringCharacter { get; init; }

    /// <summary>
    /// The second key combination to go exit the editor
    /// </summary>
    ConsoleKey ExitCharacter { get; init; }

    /// <summary>
    /// The TerminalIOHandler implementation provided
    /// </summary>
    ITerminalIOHandler TerminalIOHandler { get; init; }
}