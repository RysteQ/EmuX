using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionLDS : IInstruction
{
    public InstructionLDS(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor)
    {
        Variant = variant;
        Prefix = prefix;
        FirstOperand = firstOperand;
        SecondOperand = secondOperand;
        ThirdOperand = thirdOperand;
        OperandDecoder = operandDecoder;
        FlagStateProcessor = flagStateProcessor;
    }

    public void Execute(IVirtualMachine virtualMachine)
    {
        IVirtualRegister register = virtualMachine.CPU.GetRegister(FirstOperand!.FullOperand);

        register!.Set(FirstOperand!.FullOperand, virtualMachine.GetWord(OperandDecoder.GetPointerMemoryAddress(virtualMachine, SecondOperand!)));
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = virtualMachine.GetWord(OperandDecoder.GetPointerMemoryAddress(virtualMachine, SecondOperand!) + 2);
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

    public string Opcode => "LDS";

    public IOperandDecoder OperandDecoder { get; init; }
    public IFlagStateProcessor FlagStateProcessor { get; init; }
    public InstructionVariant Variant { get; init; }
    public IPrefix? Prefix { get; init; }
    public IOperand? FirstOperand { get; init; }
    public IOperand? SecondOperand { get; init; }
    public IOperand? ThirdOperand { get; init; }
}