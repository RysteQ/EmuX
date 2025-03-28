using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.Instructions;

public class InstructionCLI(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperandDecoder operandDecoder) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        if (virtualMachine.GetIOPL() < 3)
        {
            virtualMachine.SetFlag(EFlagsEnum.VIF, false);
        }
        else
        {
            virtualMachine.SetFlag(EFlagsEnum.IF, false);
        }
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.NoOperands()
        ];

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id);
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;

    public string Opcode => "CWDE";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
}