using EmuX_Console.Libraries.Enums;
using EmuX_Console.Libraries.Interfaces;
using EmuX_Console.Programs.Interfaces;

namespace EmuX_Console.Programs.Implementations;

// TODO - Optimise this later

public class CodeEditor : ICodeEditor
{
    public CodeEditor(ITerminalIOHandler terminalIOHandler, bool showLines = true, ConsoleModifiers mainModifierKey = ConsoleModifiers.Control, ConsoleKey goToLineCharacter = ConsoleKey.G, ConsoleKey findStringCharacter= ConsoleKey.F, ConsoleKey replaceStringCharacter = ConsoleKey.R, ConsoleKey exitCharacter = ConsoleKey.Q)
    {
        TerminalIOHandler = terminalIOHandler;

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
        int terminalWidth = TerminalIOHandler.Width;
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
                if (TerminalIOHandler.XCursorPosition >= TerminalIOHandler.Width - 1)
                {
                    TerminalIOHandler.MoveCursorRelative(-1, 0);
                    RefreshLine();

                    continue;
                }

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
                TerminalIOHandler.Output(GenerateLinePrompt(i), OutputSeverity.Important);
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
                TerminalIOHandler.Output(GenerateLinePrompt(i), OutputSeverity.Important);
            }

            TerminalIOHandler.Output($"{_textLines[i]}\n", OutputSeverity.Normal);
        }

        _yCursorPosition++;
        _xCursorPosition = 0;

        TerminalIOHandler.MoveCursorAbsolute(XCursorPosition, YCursorPosition);
    }

    private void HandleBackspace()
    {
        if (_xCursorPosition == 0)
        {
            TerminalIOHandler.MoveCursorRelative(1, 0);
        
            return;
        }

        _textLines[_yCursorPosition] = _textLines[_yCursorPosition].Remove(_xCursorPosition - 1);
        _xCursorPosition--;

        RefreshLine();
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
            GoToLineInternal();
        }
        else if (key == FindStringCharacter)
        {
            FindStringInternal();
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

    private void GoToLineInternal()
    {
        string userInput = string.Empty;
        int lineToGoTo = 0;

        userInput = GetUserInput("Line: ");

        if (!int.TryParse(userInput, out lineToGoTo))
        {
            return;
        }

        InitCodeEditorInternal(string.Join('\n', _textLines));

        if (lineToGoTo <= 0 || lineToGoTo > _textLines.Count)
        {
            return;
        }

        TerminalIOHandler.MoveCursorAbsolute(GenerateLinePrompt(lineToGoTo).Length + _textLines[lineToGoTo].Length, lineToGoTo - 1);
        _yCursorPosition = lineToGoTo;
    }

    private void FindStringInternal()
    {
        string userInput = string.Empty;
        int currentFoundWordIndex = 0;
        int currentXCursorPosition = TerminalIOHandler.XCursorPosition;
        int currentYCursorPosition = TerminalIOHandler.YCursorPosition;
        List<int> foundWordLines = [];
        ConsoleKeyInfo inputKey;

        userInput = GetUserInput("Find: ");
        
        if (string.IsNullOrEmpty(userInput))
        {
            InitCodeEditorInternal(string.Join('\n', _textLines));

            return;
        }

        for (int i = 0; i < _textLines.Count; i++)
        {
            if (!_textLines[i].Contains(userInput))
            {
                continue;
            }

            foundWordLines.Add(i + 1);
        }

        if (!foundWordLines.Any())
        {
            return;
        }

        do
        {
            InitCodeEditorInternal(string.Join('\n', _textLines));
            TerminalIOHandler.MoveCursorAbsolute(GenerateLinePrompt(foundWordLines[currentFoundWordIndex]).Length + _textLines[foundWordLines[currentFoundWordIndex]].Length, foundWordLines[currentFoundWordIndex] - 1);
            _yCursorPosition = foundWordLines[currentFoundWordIndex];

            inputKey = TerminalIOHandler.GetUserKeyInput(true);

            if (inputKey.Key == ConsoleKey.DownArrow)
            {
                currentFoundWordIndex++;
            }
            else if (inputKey.Key == ConsoleKey.UpArrow)
            {
                currentFoundWordIndex--;
            }

            if (currentFoundWordIndex < 0)
            {
                currentFoundWordIndex = foundWordLines.Count - 1;
            }
            else if (currentFoundWordIndex >= foundWordLines.Count)
            {
                currentFoundWordIndex = 0;
            }
        } while (inputKey.Key == ConsoleKey.DownArrow || inputKey.Key == ConsoleKey.UpArrow);
    }

    private void RefreshLine()
    {
        TerminalIOHandler.MoveCursorAbsolute(0, _yCursorPosition);
        TerminalIOHandler.Output(string.Join(string.Empty, Enumerable.Repeat(' ', TerminalIOHandler.Width)), OutputSeverity.Normal);

        if (ShowLines)
        {
            TerminalIOHandler.MoveCursorAbsolute(0, _yCursorPosition);
            TerminalIOHandler.Output(GenerateLinePrompt(_yCursorPosition), OutputSeverity.Important);
        }

        TerminalIOHandler.Output(_textLines[_yCursorPosition], OutputSeverity.Normal);
    }

    private void MoveCursorAround(ConsoleKey keyPressed)
    {
        int linesOffset = 0;

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
                    _xCursorPosition = _textLines[_yCursorPosition].Length;
                }

                break;
            
            case ICodeEditor.MoveDown:
                if (_textLines.Count - 1 > _yCursorPosition)
                {
                    _yCursorPosition++;
                }
                else
                {
                    _xCursorPosition = _textLines[_yCursorPosition].Length;
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

        if (_textLines[_yCursorPosition].Length < _xCursorPosition)
        {
            if (ShowLines)
            {
                linesOffset = GenerateLinePrompt(_yCursorPosition).Length;
            }

            TerminalIOHandler.MoveCursorAbsolute(_textLines[_yCursorPosition].Length + linesOffset, _yCursorPosition);
        }
        else
        {
            TerminalIOHandler.MoveCursorAbsolute(GenerateLinePrompt(_yCursorPosition).Length + _xCursorPosition, _yCursorPosition);
        }
    }

    private string GenerateLinePrompt(int line)
    {
        return $"{line + 1} ~ ";
    }

    private string GetUserInput(string message)
    {
        string suffixSpaces = string.Empty;

        if (message.Length < TerminalIOHandler.Width)
        {
            suffixSpaces = string.Concat(Enumerable.Repeat<char>(' ', TerminalIOHandler.Width - message.Length));
        }

        TerminalIOHandler.MoveCursorAbsolute(0, TerminalIOHandler.Height);
        TerminalIOHandler.Output(message + suffixSpaces, OutputSeverity.Important, ConsoleColor.DarkGray);
        TerminalIOHandler.MoveCursorAbsolute(message.Length, TerminalIOHandler.Height);

        return TerminalIOHandler.GetUserInput(true, ConsoleColor.DarkGray);
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