using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Interfaces;

namespace EmuXCore.Interpreter.Internal.Models;

public record LexerResult(IList<IInstruction> instructions, IList<ILabel> labels, IList<string> errors) : ILexerResult
{
    public IList<IInstruction> Instructions { get; init; } = instructions;
    public IList<ILabel> Labels { get; init; } = labels;
    public bool Success { get => !Errors.Any(); }
    public IList<string> Errors { get; init; } = errors;
}