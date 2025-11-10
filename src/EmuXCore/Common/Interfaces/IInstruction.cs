using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Interfaces;

namespace EmuXCore.Common.Interfaces;

/// <summary>
/// The main interface used to emulate instructions and their logic
/// </summary>
public interface IInstruction
{
    /// <summary>
    /// Executes the instruction logic
    /// </summary>
    /// <param name="virtualMachine">The virtual machine the instruction is executed in</param>
    void Execute(IVirtualMachine virtualMachine);

    /// <summary>
    /// Checks if the operands and the variant of the instruction are valid
    /// </summary>
    /// <returns>True if valid, otherwise false</returns>
    bool IsValid();

    /// <summary>
    /// Used to process the operand data
    /// </summary>
    IOperandDecoder OperandDecoder { get; init; }

    /// <summary>
    /// Used to process the flags when executing the instruction
    /// </summary>
    IFlagStateProcessor FlagStateProcessor { get; init; }

    /// <summary>
    /// The mnemonic of the instruction
    /// </summary>
    string Opcode { get; }

    /// <summary>
    /// The amount of bytes used for the instruction
    /// </summary>
    ulong Bytes { get; }

    /// <summary>
    /// The variant of the instruction
    /// </summary>
    InstructionVariant Variant { get; init; }

    /// <summary>
    /// The prefix of the instruction, can be null to signify it does not exist
    /// </summary>
    IPrefix? Prefix { get; init; }

    /// <summary>
    /// The first operand of the instruction, can be null to signify it does not exist
    /// </summary>
    IOperand? FirstOperand { get; init; }

    /// <summary>
    /// The second operand of the instruction, can be null to signify it does not exist
    /// </summary>
    IOperand? SecondOperand { get; init; }

    /// <summary>
    /// The third operand of the instruction, can be null to signify it does not exist
    /// </summary>
    IOperand? ThirdOperand { get; init; }
}