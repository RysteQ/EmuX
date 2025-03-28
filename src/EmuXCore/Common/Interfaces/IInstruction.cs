using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;

namespace EmuXCore.Common.Interfaces;

public interface IInstruction
{
    /// <summary>
    /// The execute method is meant for the IDE mode to easily debug / step through an application
    /// </summary>
    /// <param name="virtualMachine">The virtual machine the instruction should run on to emulate a real system</param>
    void Execute(IVirtualMachine virtualMachine);

    bool IsValid();

    /// <summary>
    /// Any class that implements the OperandGenerics interface
    /// </summary>
    IOperandDecoder OperandDecoder { get; init; }

    /// <summary>
    /// The opcode for the instruction, hardcoded using a lambda expression
    /// </summary>
    string Opcode { get; }

    InstructionVariant Variant { get; init; }
    IOperand? FirstOperand { get; init; }
    IOperand? SecondOperand { get; init; }
}