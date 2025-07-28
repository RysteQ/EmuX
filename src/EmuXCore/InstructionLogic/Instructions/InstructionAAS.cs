using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionAAS(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        if ((virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL & 0x0f) > 9 || virtualMachine.GetFlag(EFlags.AF))
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX -= 6;
            virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH--;
            virtualMachine.SetFlag(EFlags.CF, true);
            virtualMachine.SetFlag(EFlags.AF, true);
        }
        else
        {
            virtualMachine.SetFlag(EFlags.CF, false);
            virtualMachine.SetFlag(EFlags.AF, false);
        }

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = (byte)(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL & 0x0f);
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.NoOperands()
        ];

        if (Prefix != null)
        {
            return false;
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand == null && SecondOperand == null && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "AAS";

    public InstructionVariant Variant { get; init; } = variant;
    public IPrefix? Prefix { get; init; } = prefix;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}