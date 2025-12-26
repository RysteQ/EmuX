using EmuXCore.Common.Interfaces;

namespace EmuXUI.Models.Static;

public sealed class SourceCodeLine
{
    public SourceCodeLine(int line, string sourceCode, IInstruction instruction)
    {
        Line = line;
        SourceCode = sourceCode;
        Instruction = instruction;
    }

    public int Line { get; set; }
    public string SourceCode { get; init; }
    public IInstruction Instruction { get; init; }
}