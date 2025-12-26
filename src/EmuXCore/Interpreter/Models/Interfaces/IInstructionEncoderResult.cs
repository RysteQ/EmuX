namespace EmuXCore.Interpreter.Models.Interfaces;

public interface IInstructionEncoderResult
{
    IList<byte[]> Bytes { get; }
    IReadOnlyCollection<string> Errors { get; }
    bool Success { get; }
}