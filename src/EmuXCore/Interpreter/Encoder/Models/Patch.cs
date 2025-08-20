using EmuXCore.Common.Enums;
using EmuXCore.Interpreter.Interfaces.Models;

namespace EmuXCore.Interpreter.Encoder.Models;

public record Patch : IPatch
{
    public Patch(ulong offset, Size size, bool subtract)
    {
        Offset = offset;
        Size = size;
        Subtract = subtract;
    }

    public ulong Offset { get; init; }
    public Size Size { get; init; }
    public bool Subtract { get; init; }
}