using EmuXCore.Common.Interfaces;

namespace EmuXCore.Interpreter.Interfaces;

public interface ILexeme
{
    /// <summary>
    /// Converts the lexeme to its IInstruction counterpart
    /// </summary>
    /// <returns>The IInstruction</returns>
    IInstruction ToIInstruction();

    /// <summary>
    /// Converts the lexem to its ILabel counterpart
    /// </summary>
    /// <returns>The ILabel</returns>
    ILabel ToILabel();

    /// <summary>
    /// Checks if the operands are valid based on some common checks for all instruction types
    /// </summary>
    /// <returns>True if the operands are indeed valid</returns>
    bool AreInstructionOperandsValid();

    /// <summary>
    /// Checks if the label name is valid based on some common checks
    /// </summary>
    /// <returns>True if the label name is indeed valid</returns>
    bool IsLabelValid();

    /// <summary>
    /// The source code line
    /// </summary>
    ISourceCodeLine SourceCodeLine { get; init; }

    /// <summary>
    /// The prefix of the instruction
    /// </summary>
    string Prefix { get; init; }

    /// <summary>
    /// The opcode of the instruction
    /// </summary>
    string Opcode { get; init; }

    /// <summary>
    /// The first operand of the instruction, should be equal to string.Empty if this does not exist
    /// </summary>
    string FirstOperand { get; init; }

    /// <summary>
    /// The second operand of the instruction, should be equal to string.Empty if this does not exist
    /// </summary>
    string SecondOperand { get; init; }

    /// <summary>
    /// The third operand of the instruction, should be equal to string.Empty if this does not exist
    /// </summary>
    string ThirdOperand { get; init; }
}