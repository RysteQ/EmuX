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
    /// Checks if the operands are valid based on some common checks for all instruction types
    /// </summary>
    /// <returns>True if the operands are indeed valid</returns>
    bool AreOperandsValid();

    /// <summary>
    /// The source code line
    /// </summary>
    ISourceCodeLine SourceCodeLine { get; init; }
    
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
}