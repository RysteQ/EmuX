using EmuXCore.Interpreter.Interfaces;

namespace EmuXCore.Interpreter.Internal.Models;

public class Label(string labelName, int line) : ILabel
{
    public string LabelName { get; init; } = labelName;
    public int Line { get; init; } = line;
}