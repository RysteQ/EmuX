using EmuXCore.Interpreter.Models.Interfaces;
using System.Collections.ObjectModel;

namespace EmuXCore.Interpreter.Models;

public record InstructionEncoderResult : IInstructionEncoderResult
{
    public InstructionEncoderResult(IList<byte[]> bytes, IReadOnlyCollection<string> errors)
    {
        _bytes = bytes;
        _errors = errors;
    }

    public IList<byte[]> Bytes => _bytes;
    public IReadOnlyCollection<string> Errors => _errors;
    public bool Success => !_errors.Any();

    private IList<byte[]> _bytes;
    private IReadOnlyCollection<string> _errors;
}