using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionLES(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        IVirtualRegister register = virtualMachine.CPU.GetRegister(FirstOperand!.FullOperand);

        register!.Set(virtualMachine.GetWord(OperandDecoder.GetPointerMemoryAddress(virtualMachine, SecondOperand)));
        virtualMachine.CPU.GetRegister<VirtualRegisterES>().ES = virtualMachine.GetWord(OperandDecoder.GetPointerMemoryAddress(virtualMachine, SecondOperand) + 2);
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.TwoOperandsRegisterMemory()
        ];

        if (Prefix != null)
        {
            return false;
        }

        if (FirstOperand?.OperandSize != SecondOperand?.OperandSize)
        {
            return false;
        }

        if (FirstOperand?.OperandSize != Size.Word)
        {
            return false;
        }

        if (SecondOperand != null)
        {
            if (!SecondOperand!.AreMemoryOffsetValid())
            {
                return false;
            }
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand?.Variant == Variant.SecondOperand && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "LES";

    public InstructionVariant Variant { get; init; } = variant;
    public IPrefix? Prefix { get; init; } = prefix;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}