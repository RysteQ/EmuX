using EmuXCore.Common.Enums;

namespace EmuXCore.Interpreter.Encoder.Interfaces.Models;

public interface IPatch
{
    ulong Offset { get; init; }
    Size Size { get; init; }
    bool Subtract { get; init; }
}