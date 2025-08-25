using System.Collections.ObjectModel;

namespace EmuXCore.Interpreter.Interfaces.Models;

public interface IInstructionEncoderResult
{
    byte[] Bytes { get; }
    ReadOnlyCollection<string> Errors { get; }
    bool Success { get; }
}