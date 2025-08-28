using EmuX_Console.Libraries.Enums;
using EmuX_Console.Libraries.Interfaces;
using EmuXCore.Common.Enums;
using EmuXCore.VM;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU;

namespace EmuX_Console.Libraries.Implementations;

public class TerminalIOHandler : ITerminalIOHandler
{
    public TerminalIOHandler(string prompt, ConsoleKey forwardLookbackKey, ConsoleKey backwardLookbackKey)
    {
        if (forwardLookbackKey == backwardLookbackKey)
        {
            throw new InvalidOperationException("The forward lookback key cannot be the same as the backward lookback key");
        }

        Prompt = prompt;
        BackwardLookbackKey = backwardLookbackKey;
        ForwardLookbackKey = forwardLookbackKey;

        ColourPrompt = ConsoleColor.Blue;
        ColoutInput = ConsoleColor.Green;
        ColourOutput = ConsoleColor.White;
        ColourHighlight = ConsoleColor.Yellow;

        _inputHistory = [];
        MAXIMUM_LOOKBACK_DEPTH = 100;
    }

    public string GetUserInput()
    {
        DisplayPrompt();

        return InternalGetUserInput();
    }

    public string GetUserInput(ConsoleColor backgroundColour)
    {
        ConsoleColor currentConsoleBackgroundColour = BackgroundColour;
        string userInput = string.Empty;

        BackgroundColour = backgroundColour;
        userInput = GetUserInput();
        BackgroundColour = currentConsoleBackgroundColour;

        return userInput;
    }

    public string GetUserInput(bool hidePrompt)
    {
        if (!hidePrompt)
        {
            DisplayPrompt();
        }

        return InternalGetUserInput();
    }

    public string GetUserInput(bool hidePrompt, ConsoleColor backgroundColour)
    {
        ConsoleColor currentConsoleBackgroundColour = BackgroundColour;
        string userInput = string.Empty;

        BackgroundColour = backgroundColour;
        userInput = GetUserInput(hidePrompt);
        BackgroundColour = currentConsoleBackgroundColour;

        return userInput;
    }

    public ConsoleKeyInfo GetUserKeyInput()
    {
        DisplayPrompt();

        return Console.ReadKey();
    }

    public ConsoleKeyInfo GetUserKeyInput(bool hidePrompt)
    {
        if (!hidePrompt)
        {
            DisplayPrompt();
        }

        return Console.ReadKey();
    }

    public void ClearTerminal()
    {
        Console.Clear();
    }

    public void MoveCursorAbsolute(int x, int y)
    {
        if (x >= Console.BufferWidth)
        {
            Console.BufferWidth = x + 1;
        }

        if (y >= Console.BufferHeight)
        {
            Console.BufferHeight = y + 1;
        }

        Console.SetCursorPosition(x, y);
    }

    public void MoveCursorRelative(int x, int y)
    {
        (int Left, int Top) currentCursorPosition = Console.GetCursorPosition();


        if (x + currentCursorPosition.Left >= Console.BufferWidth)
        {
            Console.BufferWidth = x + currentCursorPosition.Left + 1;
        }

        if (y + currentCursorPosition.Top >= Console.BufferHeight)
        {
            Console.BufferHeight = y + currentCursorPosition.Top + 1;
        }

        Console.SetCursorPosition(currentCursorPosition.Left + x, currentCursorPosition.Top + y);
    }

    public void Output(string output, OutputSeverity severity)
    {
        HandleForegroundColourSeverity(severity);
        Console.Write(output);
    }

    public void Output(string output, OutputSeverity severity, ConsoleColor backgroundColour)
    {
        ConsoleColor currentConsoleBackgroundColour = BackgroundColour;

        BackgroundColour = backgroundColour;
        Output(output, severity);
        BackgroundColour = currentConsoleBackgroundColour;
    }

    public void Output(IVirtualMachine virtualMachine)
    {
        ForegroundColour = ColourOutput;
        Console.Write("Memory: ");
        ForegroundColour = ColourHighlight;
        Console.Write(virtualMachine.Memory.GENERAL_PURPOSE_MEMORY);
        ForegroundColour = ColourOutput;
        Console.WriteLine(" bytes");

        Console.Write("GPU: ");
        ForegroundColour = ColourHighlight;
        Console.Write(virtualMachine.GPU.Data.Length);
        ForegroundColour = ColourOutput;
        Console.WriteLine(" bytes");

        foreach (IVirtualDisk virtualDisk in virtualMachine.Disks)
        {
            Console.Write($"Disk ({virtualDisk.DiskNumber}): ");
            ForegroundColour = ColourHighlight;
            Console.Write(virtualDisk.TotalBytes);
            ForegroundColour = ColourOutput;
            Console.WriteLine(" bytes");
        }

        foreach (IVirtualDevice virtualDevice in virtualMachine.Devices)
        {
            Console.Write($"Device ({virtualDevice.DeviceId}): ");
            ForegroundColour = ColourHighlight;
            Console.WriteLine(virtualDevice.Status);
            ForegroundColour = ColourOutput;
        }
    }

    public void Output(IVirtualMachine virtualMachine, ConsoleColor backgroundColour)
    {
        ConsoleColor currentConsoleBackgroundColour = BackgroundColour;

        BackgroundColour = backgroundColour;
        Output(virtualMachine);
        BackgroundColour = currentConsoleBackgroundColour;
    }

    public void Output(IVirtualCPU virtualCPU)
    {
        List<int> largestRegistersNameLength = new(virtualCPU.Registers.Select(selectedRegister => selectedRegister.RegisterNamesAndSizes.Count).Max());
        List<List<string>> registerNames = virtualCPU.Registers.Select(selectedRegister => selectedRegister.RegisterNamesAndSizes.Keys.ToList()).ToList();
        string previousSeparator = string.Empty;
        string currentSeparator = string.Empty;

        for (int i = 0; i < largestRegistersNameLength.Capacity; i++)
        {
            largestRegistersNameLength.Add(registerNames.Where(selectedRegisterNameList => selectedRegisterNameList.Count > i).Select(selectedRegisterNameList => selectedRegisterNameList[i].Length).Max());
        }

        foreach (IVirtualRegister register in virtualCPU.Registers)
        {
            foreach (int largestRegisterNameLength in largestRegistersNameLength.Take(register.RegisterNamesAndSizes.Count))
            {
                currentSeparator += '+' + string.Concat(Enumerable.Repeat('-', largestRegisterNameLength + 2));
            }

            Console.WriteLine(currentSeparator.Length > previousSeparator.Length ? currentSeparator + '+' : previousSeparator + '+');
            previousSeparator = currentSeparator;
            currentSeparator = string.Empty;

            for (int i = 0; i < register.RegisterNamesAndSizes.Count; i++)
            {
                Console.Write("| ");
                ForegroundColour = ColourHighlight;
                Console.Write("{0,-" + largestRegistersNameLength[i] + "}", register.RegisterNamesAndSizes.Skip(i).First().Key);
                ForegroundColour = ColourOutput;
                Console.Write(" ");
            }

            Console.WriteLine("|");
        }

        Console.WriteLine(previousSeparator + '+');
    }

    public void Output(IVirtualCPU virtualCPU, ConsoleColor backgroundColour)
    {
        ConsoleColor currentConsoleBackgroundColour = BackgroundColour;

        BackgroundColour = backgroundColour;
        Output(virtualCPU);
        BackgroundColour = currentConsoleBackgroundColour;
    }

    public void Output(IVirtualRegister virtualRegister, OutputFormat format)
    {
        ulong virtualRegisterMaximumValue = virtualRegister.Get();
        int maximumNumberLength = 0;
        int maximumRegisterNameLength = 0;
        int maximumLength = 0;
        string registerValue = string.Empty;

        maximumNumberLength = format switch
        {
            OutputFormat.Integer => virtualRegisterMaximumValue.ToString().Length + 2,
            OutputFormat.Binary => (int)virtualRegister.RegisterNamesAndSizes.First().Value * 8 + (int)virtualRegister.RegisterNamesAndSizes.First().Value - 1,
            OutputFormat.Hexadecimal => (int)virtualRegister.RegisterNamesAndSizes.First().Value * 2 + (int)virtualRegister.RegisterNamesAndSizes.First().Value - 1,
            _ => throw new InvalidCastException($"Length calculation unit for {format} has not been implemented"),
        };

        maximumRegisterNameLength += virtualRegister.RegisterNamesAndSizes.Select(selectedEntry => selectedEntry.Key.Length).Max();
        maximumLength = maximumNumberLength + maximumRegisterNameLength;

        Console.WriteLine('+' + string.Concat(Enumerable.Repeat('-', maximumRegisterNameLength + 2)) + '+' + string.Concat(Enumerable.Repeat('-', maximumNumberLength + 2)) + '+');

        foreach (KeyValuePair<string, Size> entry in virtualRegister.RegisterNamesAndSizes)
        {
            foreach (byte element in BitConverter.GetBytes(CastIntegerToSize(virtualRegisterMaximumValue, entry.Value)).Reverse())
            {
                registerValue += ConvertNumberToBase(element, format);

                if (format != OutputFormat.Integer)
                {
                    registerValue += ' ';
                }
            }

            Console.Write("| {0,-" + maximumRegisterNameLength + "} | ", entry.Key);
            ForegroundColour = ColourHighlight;
            Console.Write("{0," + maximumNumberLength + "}", registerValue.Trim());
            ForegroundColour = ColourOutput;
            Console.WriteLine(" |");
            Console.WriteLine('+' + string.Concat(Enumerable.Repeat('-', maximumRegisterNameLength + 2)) + '+' + string.Concat(Enumerable.Repeat('-', maximumNumberLength + 2)) + '+');

            registerValue = string.Empty;
        }
    }

    public void Output(IVirtualRegister virtualRegister, OutputFormat format, ConsoleColor backgroundColour)
    {
        ConsoleColor currentConsoleBackgroundColour = BackgroundColour;

        BackgroundColour = backgroundColour;
        Output(virtualRegister, format);
        BackgroundColour = currentConsoleBackgroundColour;
    }

    public void Output(byte[] buffer, OutputFormat format, OutputSeverity severity)
    {
        string toDisplay = string.Empty;

        foreach (byte element in buffer)
        {
            toDisplay += ' ' + ConvertNumberToBase(element, format);
        }

        HandleForegroundColourSeverity(severity);
        Console.WriteLine(toDisplay);
    }

    public void Output(byte[] buffer, OutputFormat format, OutputSeverity severity, ConsoleColor backgroundColour)
    {
        ConsoleColor currentConsoleBackgroundColour = BackgroundColour;

        BackgroundColour = backgroundColour;
        Output(buffer, format, severity);
        BackgroundColour = currentConsoleBackgroundColour;
    }

    public void NewLine()
    {
        Console.WriteLine();
    }

    private string InternalGetUserInput()
    {
        ConsoleKeyInfo inputCharacter;
        string input = string.Empty;

        do
        {
            inputCharacter = Console.ReadKey();

            if (inputCharacter.Key == BackwardLookbackKey || inputCharacter.Key == ForwardLookbackKey)
            {
                input = HandleLoopbackRequest(inputCharacter.Key == BackwardLookbackKey, input.Length);
            }
            else if (inputCharacter.Key == ConsoleKey.Backspace)
            {
                Console.Write(' ');
                Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, Console.GetCursorPosition().Top);

                input = string.Join(string.Empty, input.SkipLast(1));
            }
            else
            {
                input += inputCharacter.KeyChar;
            }
        } while (inputCharacter.Key != ConsoleKey.Enter);

        input = input.Trim();

        HandleLookbackAddition(input);
        Console.WriteLine();

        return input;
    }

    private void DisplayPrompt()
    {
        ForegroundColour = ColourPrompt;
        Console.Write(Prompt);
        ForegroundColour = ColoutInput;
    }

    private string HandleLoopbackRequest(bool forward, int currentInputLength)
    {
        Console.SetCursorPosition(Prompt.Length, Console.GetCursorPosition().Top);
        Console.Write(string.Concat(Enumerable.Repeat(' ', currentInputLength)));
        Console.SetCursorPosition(Prompt.Length, Console.GetCursorPosition().Top);

        lookbackIndex = forward ? lookbackIndex + 1 : lookbackIndex - 1;

        if (lookbackIndex == -1)
        {
            lookbackIndex = _inputHistory.Count - 1;
        }
        else if (lookbackIndex >= _inputHistory.Count)
        {
            lookbackIndex = 0;
        }

        Console.Write(_inputHistory[lookbackIndex]);
        Console.SetCursorPosition(Prompt.Length + _inputHistory[lookbackIndex].Length - 1, Console.GetCursorPosition().Top); // This adjust the cursor, without it the cursor is at position [0:y]

        return _inputHistory[lookbackIndex];
    }

    private void HandleLookbackAddition(string newValue)
    {
        _inputHistory.Add(newValue);

        if (MAXIMUM_LOOKBACK_DEPTH < _inputHistory.Count)
        {
            InputHistory.RemoveAt(0);
        }

        lookbackIndex = _inputHistory.Count;
    }

    private void HandleForegroundColourSeverity(OutputSeverity severity)
    {
        switch (severity)
        {
            case OutputSeverity.Normal: ForegroundColour = ColourOutput; break;
            case OutputSeverity.Important: ForegroundColour = ColourHighlight; break;
            case OutputSeverity.Error: ForegroundColour = ColourError; break;
        }
    }

    private string ConvertNumberToBase(ulong number, OutputFormat format)
    {
        return format switch
        {
            OutputFormat.Integer => string.Join(' ', number.ToString()),
            OutputFormat.Binary => string.Join(' ', number.ToString("B").PadLeft(8, '0')),
            OutputFormat.Hexadecimal => string.Join(' ', number.ToString("X2").PadLeft(2, '0')),
            _ => throw new NotImplementedException($"Base convertion logic for {format} has not been implemented"),
        };
    }

    private ulong CastIntegerToSize(ulong number, Size size)
    {
        return size switch
        {
            Size.Byte => (byte)number,
            Size.Word => (ushort)number,
            Size.Dword => (uint)number,
            Size.Qword => number,
            _ => throw new InvalidCastException($"The number {number} cannot be casted to {size}")
        };
    }

    public string Prompt { get; init; }

    public IList<string> InputHistory => _inputHistory;
    public ConsoleKey BackwardLookbackKey { get; init; }
    public ConsoleKey ForwardLookbackKey { get; init; }

    public ConsoleColor ColourError => ConsoleColor.Red;
    public ConsoleColor ColourPrompt { get; set; }
    public ConsoleColor ColoutInput { get; set; }
    public ConsoleColor ColourOutput { get; set; }
    public ConsoleColor ColourHighlight { get; set; }

    public int Width => Console.WindowWidth;
    public int Height => Console.WindowHeight;
    public int XCursorPosition { get => Console.GetCursorPosition().Left; }
    public int YCursorPosition { get => Console.GetCursorPosition().Top; }

    public ConsoleColor ForegroundColour { get => Console.ForegroundColor; set => Console.ForegroundColor = value; }
    public ConsoleColor BackgroundColour { get => Console.BackgroundColor; set => Console.BackgroundColor = value; }

    private List<string> _inputHistory;
    private int lookbackIndex = 0;

    private readonly int MAXIMUM_LOOKBACK_DEPTH;
}