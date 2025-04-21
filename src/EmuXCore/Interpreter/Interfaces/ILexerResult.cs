using EmuXCore.Common.Interfaces;

namespace EmuXCore.Interpreter.Interfaces;

public interface ILexerResult
{
    IList<IInstruction> Instructions { get; init; }
    IList<ILabel> Labels { get; init; }
    public bool Success { get; }
    public IList<string> Errors { get; init; }
}