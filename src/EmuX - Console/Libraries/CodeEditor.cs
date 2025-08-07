using EmuX_Console.Libraries.Enums;
using EmuX_Console.Libraries.Interfaces;

namespace EmuX_Console.Libraries;

// TODO - Optimise this later

public class CodeEditor : ICodeEditor
{
    public CodeEditor(ITerminalIOHandler terminalIOHandler, string text = "", bool showLines = true, ConsoleModifiers mainModifierKey = ConsoleModifiers.Control, ConsoleKey goToLineCharacter = ConsoleKey.G, ConsoleKey findStringCharacter= ConsoleKey.F, ConsoleKey replaceStringCharacter = ConsoleKey.R, ConsoleKey exitCharacter = ConsoleKey.Escape)
    {
        TerminalIOHandler = terminalIOHandler;
        _textLines = [.. text.Split('\n')];
        _end = _textLines.Count;

        ShowLines = showLines;
        MainModifierKey = mainModifierKey;
        GoToLineCharacter = goToLineCharacter;
        FindStringCharacter = findStringCharacter;
        ReplaceStringCharacter = replaceStringCharacter;
        ExitCharacter = exitCharacter;
    }

    public void Init(string text = "")
    {
        ConsoleKeyInfo userInput;

        _textLines = [.. text.Split('\n')];
        _end = _textLines.Count;

        Console.Clear(); // TODO - Move all Console.Clear() method calls to a new method inside of ITerminalIOHandler

        for (int i = 0; i < _end; i++)
        {
            TerminalIOHandler.Output($"{i + 1} ~ ", OutputSeverity.Important);
            TerminalIOHandler.Output($"{_textLines[i]}\n", OutputSeverity.Normal);
        }

        _xCursorPosition = _end + 4 + _textLines.Last().Length;
        _yCursorPosition = _textLines.Count - 1;
        Console.SetCursorPosition(_xCursorPosition, _yCursorPosition); // TODO - Same here with relative positioning maybe and absolute positioning as well ?

        while (true)
        {
            userInput = TerminalIOHandler.GetUserKeyInput(true);

            if (userInput.Modifiers == MainModifierKey)
            {
                // TODO
                continue;
            }
            else
            {
                MoveCursorAround(userInput.Key);
            }

            RefreshLine();
        }
    }

    private void RefreshLine()
    {
        if (ShowLines)
        {
            // TODO
        }

        Console.SetCursorPosition(0, _yCursorPosition);
        TerminalIOHandler.Output(_textLines[_yCursorPosition], OutputSeverity.Normal);
    }

    private void MoveCursorAround(ConsoleKey keyPressed)
    {
        switch (keyPressed)
        {
            case ConsoleKey.UpArrow:
                if (_yCursorPosition != 0)
                {
                    _yCursorPosition--;
                }
                else
                {
                    _xCursorPosition = 0;
                }

                break;

            case ConsoleKey.LeftArrow:
                if (_xCursorPosition != 0)
                {
                    _xCursorPosition--;
                }
                else if (_yCursorPosition != 0)
                {
                    _yCursorPosition--;
                    _xCursorPosition = _textLines[_xCursorPosition].Length - 1;
                }

                break;
            
            case ConsoleKey.DownArrow:
                if (_textLines.Count - 1 > _yCursorPosition)
                {
                    _yCursorPosition++;
                }
                else
                {
                    _xCursorPosition = _textLines[_yCursorPosition].Length - 1;
                }

                break;
            
            case ConsoleKey.RightArrow:
                if (_xCursorPosition < _textLines[_yCursorPosition].Length - 1)
                {
                    _xCursorPosition++;
                }
                else if (_textLines.Count - 1 > _yCursorPosition)
                {
                    _yCursorPosition++;
                    _xCursorPosition = 0;
                }

                break;
        }
    }

    public string Text => string.Join('\n', _textLines);

    public bool ShowLines { get; init; }
    public ConsoleModifiers MainModifierKey { get; init; }
    public ConsoleKey GoToLineCharacter { get; init; }
    public ConsoleKey FindStringCharacter { get; init; }
    public ConsoleKey ReplaceStringCharacter { get; init; }
    public ConsoleKey ExitCharacter { get; init; }
    public ITerminalIOHandler TerminalIOHandler { get; init; }

    private List<string> _textLines = [];
    private int _start = 0;
    private int _end = 0;
    private int _yCursorPosition = 0;
    private int _xCursorPosition = 0;
}