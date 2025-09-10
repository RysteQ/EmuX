using System.Collections.ObjectModel;

namespace EmuXCore.Interpreter.Models.Interfaces;

public interface IInstructionEncoderResult
{
    byte[] Bytes { get; }
    ReadOnlyCollection<string> Errors { get; }
    bool Success { get; }
}