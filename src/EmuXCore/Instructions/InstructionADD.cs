using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU.Enums;

namespace EmuXCore.Instructions;

public sealed class InstructionADD(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong firstOperandValue = OperandDecoder.GetOperandValue(virtualMachine, FirstOperand);
        ulong valueToSet = 0;

        if (Variant.FirstOperand == OperandVariant.Register)
        {
            IVirtualRegister? register = virtualMachine.CPU.GetRegister(FirstOperand!.FullOperand);
            valueToSet = register!.Get() + OperandDecoder.GetOperandValue(virtualMachine, SecondOperand);

            register!.Set(valueToSet);
        }
        else
        {
            valueToSet = OperandDecoder.GetOperandValue(virtualMachine, FirstOperand) + OperandDecoder.GetOperandValue(virtualMachine, SecondOperand);

            switch (SecondOperand!.OperandSize)
            {
                case Size.Byte: virtualMachine.SetByte((int)OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), (byte)valueToSet); break;
                case Size.Word: virtualMachine.SetWord((int)OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), (ushort)valueToSet); break;
                case Size.Double: virtualMachine.SetDouble((int)OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), (uint)valueToSet); break;
                case Size.Quad: virtualMachine.SetQuad((int)OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), (ulong)valueToSet); break;
            }
        }

        virtualMachine.SetFlag(EFlags.CF, FlagStateProcessor.TestCarryFlag(firstOperandValue, valueToSet - firstOperandValue, FirstOperand!.OperandSize));
        virtualMachine.SetFlag(EFlags.OF, FlagStateProcessor.TestOverflowFlag(firstOperandValue, valueToSet - firstOperandValue, valueToSet, FirstOperand!.OperandSize));
        virtualMachine.SetFlag(EFlags.SF, FlagStateProcessor.TestSignFlag(valueToSet, FirstOperand!.OperandSize));
        virtualMachine.SetFlag(EFlags.ZF, FlagStateProcessor.TestZeroFlag(valueToSet));
        virtualMachine.SetFlag(EFlags.AF, FlagStateProcessor.TestAuxilliaryFlag(firstOperandValue, valueToSet - firstOperandValue));
        virtualMachine.SetFlag(EFlags.PF, FlagStateProcessor.TestParityFlag(valueToSet));
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.TwoOperandsRegisterValue(),
            InstructionVariant.TwoOperandsRegisterRegister(),
            InstructionVariant.TwoOperandsMemoryValue(),
            InstructionVariant.TwoOperandsMemoryRegister()
        ];

        // r/m - i
        if ((Variant.FirstOperand == OperandVariant.Register || Variant.FirstOperand == OperandVariant.Memory) && Variant.FirstOperand == FirstOperand?.Variant
            && Variant.SecondOperand == OperandVariant.Value && Variant.SecondOperand == SecondOperand?.Variant)
        {
            if (FirstOperand.OperandSize == Size.Quad && SecondOperand?.OperandSize == Size.Quad)
            {
                return false;
            }

            if (FirstOperand.OperandSize < SecondOperand?.OperandSize)
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
        }

        // r - r/m
        if (Variant.FirstOperand == OperandVariant.Register && Variant.FirstOperand == FirstOperand?.Variant
            && (Variant.SecondOperand == OperandVariant.Register || Variant.SecondOperand == OperandVariant.Memory) && Variant.SecondOperand == SecondOperand?.Variant)
        {
            if (FirstOperand?.OperandSize != SecondOperand?.OperandSize)
            {
                return false;
            }
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand?.Variant == Variant.SecondOperand && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "ADD";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}