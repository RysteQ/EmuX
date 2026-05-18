using EmuXCore.Common.Interfaces;

namespace EmuXCore.Interpreter.Models.Interfaces;

/// <summary>
/// This is used during the late stages of the parsing process to check if the instruction is valid and to parse said instruction to its <see cref="IInstruction"/> counterpart.
/// </summary>
public interface ILexeme
{
    /// <summary>
    /// Converts the <see cref="ILexeme"/> to its <see cref="IInstruction"/> counterpart.
    /// </summary>
    /// <exception cref="InstructionNotFoundException" />
    /// <returns>The <see cref="IInstruction"/> implementation.</returns>
    IInstruction ToIInstruction();

    /// <summary>
    /// Converts the <see cref="ILexeme"/> to its <see cref="ILabel"/> counterpart.
    /// </summary>
    /// <returns>The <see cref="ILabel"/> implementation.</returns>
    ILabel ToILabel();

    /// <summary>
    /// Checks if the operands are valid based on some common checks for all <see cref="IInstruction"/> implementations.
    /// </summary>
    /// <returns>True if the operands are valid, otherwise false.</returns>
    bool AreInstructionOperandsValid();

    /// <summary>
    /// Checks if the <see cref="ILabel" /> name is valid based on some common checks.
    /// </summary>
    /// <returns>True if the label name is  valid, otherwise false.</returns>
    bool IsLabelValid();

    /// <summary>
    /// The <see cref="ISourceCodeLine"/>.
    /// </summary>
    ISourceCodeLine SourceCodeLine { get; init; }

    /// <summary>
    /// The prefix of the <see cref="IInstruction" />.
    /// </summary>
    string Prefix { get; init; }

    /// <summary>
    /// The opcode of the <see cref="IInstruction" />.
    /// </summary>
    string Opcode { get; init; }

    /// <summary>
    /// The first operand of the <see cref="IInstruction" />. <br />
    /// Empty if it does not exist.
    /// </summary>
    string FirstOperand { get; init; }

    /// <summary>
    /// The second operand of the <see cref="IInstruction" />. <br />
    /// Empty if it does not exist.
    string SecondOperand { get; init; }

    /// <summary>
    /// The third operand of the <see cref="IInstruction" />. <br />
    /// Empty if it does not exist.
    string ThirdOperand { get; init; }
}