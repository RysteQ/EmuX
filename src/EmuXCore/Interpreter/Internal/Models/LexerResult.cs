using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Interfaces.Models;

namespace EmuXCore.Interpreter.Internal.Models;

public record LexerResult : ILexerResult
{
    public LexerResult(IList<IInstruction> instructions, IList<ILabel> labels, IList<string> errors)
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