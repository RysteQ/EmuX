using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Interfaces;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionPUSH(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong valueToPush = OperandDecoder.GetOperandValue(virtualMachine, FirstOperand!);

        switch (FirstOperand!.OperandSize)
        {
            case Size.Byte: virtualMachine.PushByte((byte)valueToPush); break;
            case Size.Word: virtualMachine.PushWord((ushort)valueToPush); break;
            case Size.Dword: virtualMachine.PushDouble((uint)valueToPush); break;
            case Size.Qword: virtualMachine.PushQuad(valueToPush); break;
        }
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.OneOperandRegister(),
            InstructionVariant.OneOperandMemory(),
            InstructionVariant.OneOperandValue()
        ];

        if (Prefix != null)
        {
            return false;
        }

        if (Variant.FirstOperand == OperandVariant.Register || Variant.FirstOperand == OperandVariant.Memory)
        {
            if (FirstOperand?.OperandSize == Size.Byte)
            {
                return false;
            }
        }
        else
        {
            if (FirstOperand?.OperandSize == Size.Qword)
            {
                return false;
            }
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand == null && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "PUSH";

    public InstructionVariant Variant { get; init; } = variant;
    public IPrefix? Prefix { get; init; } = prefix;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}