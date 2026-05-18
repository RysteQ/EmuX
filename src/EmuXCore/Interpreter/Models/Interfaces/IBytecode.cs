using EmuXCore.Common.Interfaces;

namespace EmuXCore.Interpreter.Models.Interfaces;

/// <summary>
/// This is used during the parsing process of the source code to an IL representation of it, its main purpose is for data transfer.
/// </summary>
public interface IBytecode
{
    /// <summary>
    /// The type of the bytecode since it can either be an <see cref="IInstruction"/> or <see cref="ILabel"/> before it gets processed in a later stage.
    /// </summary>
    Type Type { get => Instruction != null ? typeof(IInstruction) : typeof(ILabel); }

    /// <summary>
    /// The <see cref="IInstruction"/> the bytecode refers to, null if it isn't of type <see cref="IInstruction"/>.
    /// </summary>
    IInstruction? Instruction { get; init; }

    /// <summary>
    /// The <see cref="ILabel"/> the bytecode refers to, null if it isn't of type <see cref="ILabel"/>.
    /// </summary>
    ILabel? Label { get; init; }
}