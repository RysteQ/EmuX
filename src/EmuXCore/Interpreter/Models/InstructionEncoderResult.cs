using EmuXCore.Interpreter.Models.Interfaces;
using System.Collections.ObjectModel;

namespace EmuXCore.Interpreter.Models;

public record InstructionEncoderResult : IInstructionEncoderResult
{
    public InstructionEncoderResult(byte[] bytes, ReadOnlyCollection<string> errors)
    {
        _bytes = bytes;
        _errors = errors;
    }

    public byte[] Bytes => _bytes;
    public ReadOnlyCollection<string> Errors => _errors;
    public bool Success => !_errors.Any();

    private byte[] _bytes;
    private ReadOnlyCollection<string> _errors;
}