using EmuXCore.Common.Interfaces;

namespace EmuXCore.Interpreter.Interfaces;

/// <summary>
/// This is used during the parsing process of the source code to an IL representation of it, its main purpose is for data transfer
/// </summary>
public interface IBytecode
{
    /// <summary>
    /// The type of the bytecode since it can either be an instruction or label before it gets processed in a later stage
    /// </summary>
    Type Type { get => Instruction != null ? typeof(IInstruction) : typeof(ILabel); }

    /// <summary>
    /// The instruction the bytecode refers to, null if it isn't of type IInstruction
    /// </summary>
    IInstruction? Instruction { get; init; }

    /// <summary>
    /// The label the bytecode refers to, null if it isn't of type ILabel
    /// </summary>
    ILabel? Label { get; init; }
}