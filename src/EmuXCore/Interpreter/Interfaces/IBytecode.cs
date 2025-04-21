using EmuXCore.Common.Interfaces;

namespace EmuXCore.Interpreter.Interfaces;

public interface IBytecode
{
    Type Type { get => Instruction != null ? typeof(IInstruction) : typeof(ILabel); }

    IInstruction? Instruction { get; init; }
    ILabel? Label { get; init; }
}