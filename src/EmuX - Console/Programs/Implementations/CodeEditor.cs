using EmuX_Console.Libraries.Enums;
using EmuX_Console.Libraries.Interfaces;
using EmuX_Console.Programs.Interfaces;

namespace EmuX_Console.Programs.Implementations;

// TODO - Optimise this later

public class CodeEditor : ICodeEditor
{
    public CodeEditor(ITerminalIOHandler terminalIOHandler, string text = "", bool showLines = true, ConsoleModifiers mainModifierKey = ConsoleModifiers.Control, ConsoleKey goToLineCharacter = ConsoleKey.G, ConsoleKey findStringCharacter= ConsoleKey.F, ConsoleKey replaceStringCharacter = ConsoleKey.R, ConsoleKey exitCharacter = ConsoleKey.Q)
    {
        TerminalIOHandler = terminalIOHandler;
        _textLines = [.. text.Split('\n')];

        // Forbidden characters
        if (GoToLineCharacter == ConsoleKey.Escape || FindStringCharacter == ConsoleKey.Escape || ReplaceStringCharacter == ConsoleKey.Escape || ExitCharacter == ConsoleKey.Escape)
        {
            throw new ArgumentException("Invalid special key arguments, the escape character is to not be used code editor function calls");
        }

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

        InitCodeEditorInternal(text);

        while (_running)
        {
            userInput = TerminalIOHandler.GetUserKeyInput(true);

            if (userInput.Modifiers == MainModifierKey)
            {
                HandleModifierKey(userInput.Key);
            }
            else if (userInput.Key == ICodeEditor.MoveUp || userInput.Key == ICodeEditor.MoveLeft || userInput.Key == ICodeEditor.MoveDown || userInput.Key == ICodeEditor.MoveRight)
            {
                MoveCursorAround(userInput.Key);
            }
            else if (userInput.Key == ConsoleKey.Enter)
            {
                InsertNewLine();
            }
            else if (userInput.Key == ConsoleKey.Backspace)
            {
                HandleBackspace();
            }
            else
            {
                InsertCharacter(userInput.KeyChar);
            }
        }

        ExitCodeEditor();
    }

    private void InitCodeEditorInternal(string text)
    {
        _textLines = [.. text.Split('\n')];

        TerminalIOHandler.ClearTerminal();

        for (int i = 0; i < _textLines.Count; i++)
        {
            if (ShowLines)
            {
                TerminalIOHandler.Output($"{i + 1} ~ ", OutputSeverity.Important);
            }

            TerminalIOHandler.Output($"{_textLines[i]}\n", OutputSeverity.Normal);
        }

        _running = true;
        _xCursorPosition = _textLines.Last().Length;
        _yCursorPosition = _textLines.Count - 1;
        TerminalIOHandler.MoveCursorAbsolute(XCursorPosition, YCursorPosition);
    }

    private void InsertNewLine()
    {
        if (_xCursorPosition == _textLines[_yCursorPosition].Length - 1)
        {
            if (_yCursorPosition == _textLines.Count - 1)
            {
                _textLines.Add(string.Empty);
            }
            else
            {
                _textLines.Insert(_yCursorPosition + 1, string.Empty);
            }
        }
        else
        {
            if (_yCursorPosition == _textLines.Count - 1)
            {
                _textLines.Add(string.Empty);
            }
            else
            {
                _textLines.Insert(_yCursorPosition + 1, string.Empty);
            }

            _textLines[_yCursorPosition + 1] = _textLines[_yCursorPosition].Substring(_xCursorPosition);
            _textLines[_yCursorPosition] = _textLines[_yCursorPosition].Substring(0, _xCursorPosition);
            
            RefreshLine();
        }

        TerminalIOHandler.MoveCursorAbsolute(0, _yCursorPosition);

        for (int i = _yCursorPosition; i < _textLines.Count; i++)
        {
            if (ShowLines)
            {
                TerminalIOHandler.Output($"{i + 1} ~ ", OutputSeverity.Important);
            }

            TerminalIOHandler.Output($"{_textLines[i]}\n", OutputSeverity.Normal);
        }

        _yCursorPosition++;
        _xCursorPosition = 0;

        TerminalIOHandler.MoveCursorAbsolute(XCursorPosition, YCursorPosition);
    }

    private void HandleBackspace()
    {
        // TODO
    }

    private void InsertCharacter(char character)
    {
        _textLines[_yCursorPosition] = _textLines[_yCursorPosition].Insert(_xCursorPosition, character.ToString());
        _xCursorPosition++;
        RefreshLine();
    }

    private void HandleModifierKey(ConsoleKey key)
    {
        if (key == GoToLineCharacter)
        {
            // TODO
        }
        else if (key == FindStringCharacter)
        {
            // TODO
        }
        else if (key == ReplaceStringCharacter)
        {
            // TODO
        }
        else if (key == ExitCharacter)
        {
            _running = false;
        }
    }

    private void RefreshLine()
    {
        TerminalIOHandler.MoveCursorAbsolute(0, _yCursorPosition);
        TerminalIOHandler.Output(string.Join(string.Empty, Enumerable.Repeat(' ', TerminalIOHandler.Width)), OutputSeverity.Normal);

        if (ShowLines)
        {
            TerminalIOHandler.Output($"{_yCursorPosition + 1} ~ ", OutputSeverity.Important);
        }

        TerminalIOHandler.Output(_textLines[_yCursorPosition], OutputSeverity.Normal);
    }

    private void MoveCursorAround(ConsoleKey keyPressed)
    {
        switch (keyPressed)
        {
            case ICodeEditor.MoveUp:
                if (_yCursorPosition != 0)
                {
                    _yCursorPosition--;
                }
                else
                {
                    _xCursorPosition = 0;
                }

                break;

            case ICodeEditor.MoveLeft:
                if (_xCursorPosition != 0)
                {
                    _xCursorPosition--;
                }
                else if (_yCursorPosition != 0)
                {
                    _yCursorPosition--;
                    _xCursorPosition = _textLines[_yCursorPosition].Length - 1;
                }

                break;
            
            case ICodeEditor.MoveDown:
                if (_textLines.Count - 1 > _yCursorPosition)
                {
                    _yCursorPosition++;
                }
                else
                {
                    _xCursorPosition = _textLines[_yCursorPosition].Length - 1;
                }

                break;
            
            case ICodeEditor.MoveRight:
                if (_xCursorPosition < _textLines[_yCursorPosition].Length)
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

        TerminalIOHandler.MoveCursorAbsolute(XCursorPosition, YCursorPosition);
    }

    private void ExitCodeEditor()
    {
        TerminalIOHandler.ClearTerminal();
    }

    public string Text => string.Join('\n', _textLines);
    public int XCursorPosition => ShowLines ? _xCursorPosition + 3 + (_yCursorPosition + 1).ToString().Length : _xCursorPosition;
    public int YCursorPosition => _yCursorPosition;

    public bool ShowLines { get; init; }
    public ConsoleModifiers MainModifierKey { get; init; }
    public ConsoleKey GoToLineCharacter { get; init; }
    public ConsoleKey FindStringCharacter { get; init; }
    public ConsoleKey ReplaceStringCharacter { get; init; }
    public ConsoleKey ExitCharacter { get; init; }
    public ITerminalIOHandler TerminalIOHandler { get; init; }

    private List<string> _textLines = [];
    private int _yCursorPosition = 0;
    private int _xCursorPosition = 0;
    private bool _running = false;
}