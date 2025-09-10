using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;

namespace EmuXCore.Interpreter.Models;

public record ParserResult : IParserResult
{
    public ParserResult(IList<IInstruction> instructions, IList<ILabel> labels, IList<string> errors)
    {
        Instructions = instructions;
        Labels = labels;
        Errors = errors;
    }

    public IList<IInstruction> Instructions { get; init; }
    public IList<ILabel> Labels { get; init; }
    public bool Success { get => !Errors.Any(); }
    public IList<string> Errors { get; init; }
}