using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.Instructions;

public sealed class InstructionPUSH(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong valueToPush = OperandDecoder.GetOperandValue(virtualMachine, FirstOperand!);

        switch (FirstOperand!.OperandSize)
        {
            case Size.Byte: virtualMachine.PushByte((byte)valueToPush); break;
            case Size.Word: virtualMachine.PushWord((ushort)valueToPush); break;
            case Size.Double: virtualMachine.PushDouble((uint)valueToPush); break;
            case Size.Quad: virtualMachine.PushQuad(valueToPush); break;
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

        if (Variant.FirstOperand == OperandVariant.Register || Variant.FirstOperand == OperandVariant.Memory)
        {
            if (FirstOperand?.OperandSize == Size.Byte)
            {
                return false;
            }
        }
        else
        {
            if (FirstOperand?.OperandSize == Size.Quad)
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
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}