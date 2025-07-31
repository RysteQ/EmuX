using EmuXCore.Interpreter.Interfaces;

namespace EmuXCore.Interpreter.Internal.Models;

public record Label(string labelName, int line) : ILabel
{
    public string Name { get; init; } = labelName;
    public int Line { get; init; } = line;
}