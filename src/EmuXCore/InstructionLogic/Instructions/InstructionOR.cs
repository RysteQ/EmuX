using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionOR(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong sourceValue = OperandDecoder.GetOperandValue(virtualMachine, FirstOperand);
        ulong destinationValue = OperandDecoder.GetOperandValue(virtualMachine, SecondOperand);

        if (FirstOperand.Variant == OperandVariant.Register)
        {
            IVirtualRegister register = virtualMachine.CPU.GetRegister(FirstOperand.FullOperand);

            register!.Set(sourceValue | destinationValue);
        }
        else
        {
            switch (FirstOperand.OperandSize)
            {
                case Size.Byte: virtualMachine.SetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (byte)(sourceValue | destinationValue)); break;
                case Size.Word: virtualMachine.SetWord(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (ushort)(sourceValue | destinationValue)); break;
                case Size.Dword: virtualMachine.SetDouble(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (uint)(sourceValue | destinationValue)); break;
                case Size.Qword: virtualMachine.SetQuad(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), sourceValue | destinationValue); break;
            }
        }
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.TwoOperandsRegisterValue(),
            InstructionVariant.TwoOperandsRegisterRegister(),
            InstructionVariant.TwoOperandsRegisterMemory(),
            InstructionVariant.TwoOperandsMemoryValue(),
            InstructionVariant.TwoOperandsMemoryRegister()
        ];

        if (Prefix != null)
        {
            return false;
        }

        // r/m - i
        if ((Variant.FirstOperand == OperandVariant.Register || Variant.FirstOperand == OperandVariant.Memory) && Variant.FirstOperand == FirstOperand?.Variant
            && Variant.SecondOperand == OperandVariant.Value && Variant.SecondOperand == SecondOperand?.Variant)
        {
            if (FirstOperand?.OperandSize == Size.Qword && SecondOperand?.OperandSize == Size.Qword)
            {
                return false;
            }

            if (FirstOperand?.OperandSize < SecondOperand?.OperandSize)
            {
                return false;
            }

            if (!FirstOperand!.AreMemoryOffsetValid())
            {
                return false;
            }
        }

        // r/m - r
        if ((Variant.FirstOperand == OperandVariant.Register || Variant.FirstOperand == OperandVariant.Memory) && Variant.FirstOperand == FirstOperand?.Variant
            && Variant.SecondOperand == OperandVariant.Register && Variant.SecondOperand == SecondOperand?.Variant)
        {
            if (FirstOperand?.OperandSize != SecondOperand?.OperandSize)
            {
                return false;
            }

            if (!FirstOperand!.AreMemoryOffsetValid())
            {
                return false;
            }
        }

        // r - r/m
        if (Variant.FirstOperand == OperandVariant.Register && Variant.FirstOperand == FirstOperand?.Variant
            && (Variant.SecondOperand == OperandVariant.Register || Variant.SecondOperand == OperandVariant.Memory) && Variant.SecondOperand == SecondOperand?.Variant)
        {
            if (FirstOperand?.OperandSize != SecondOperand?.OperandSize)
            {
                return false;
            }

            if (!SecondOperand!.AreMemoryOffsetValid())
            {
                return false;
            }
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand?.Variant == Variant.SecondOperand && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "OR";

    public InstructionVariant Variant { get; init; } = variant;
    public IPrefix? Prefix { get; init; } = prefix;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}