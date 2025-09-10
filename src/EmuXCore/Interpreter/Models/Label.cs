using EmuXCore.Interpreter.Models.Interfaces;

namespace EmuXCore.Interpreter.Models;

public record Label : ILabel
{
    public Label(string labelName, int line)
    {
        Name = labelName;
        Line = line;
    }

    public string Name { get; init; }
    public int Line { get; init; }
}