﻿using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionCMP : IInstruction
{
    public InstructionCMP(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor)
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
        ulong firstOperandValue = OperandDecoder.GetOperandValue(virtualMachine, FirstOperand);
        ulong secondOperandValue = OperandDecoder.GetOperandValue(virtualMachine, SecondOperand);

        virtualMachine.SetFlag(EFlags.CF, FlagStateProcessor.TestCarryFlag(firstOperandValue, secondOperandValue, FirstOperand!.OperandSize));
        virtualMachine.SetFlag(EFlags.OF, FlagStateProcessor.TestOverflowFlag(firstOperandValue, secondOperandValue, firstOperandValue < secondOperandValue ? ~(firstOperandValue - secondOperandValue) : firstOperandValue - secondOperandValue, FirstOperand!.OperandSize));
        virtualMachine.SetFlag(EFlags.SF, FlagStateProcessor.TestSignFlag(~(firstOperandValue - secondOperandValue), FirstOperand!.OperandSize));
        virtualMachine.SetFlag(EFlags.ZF, FlagStateProcessor.TestZeroFlag(firstOperandValue - secondOperandValue));
        virtualMachine.SetFlag(EFlags.AF, FlagStateProcessor.TestAuxilliaryFlag(firstOperandValue, secondOperandValue, false));
        virtualMachine.SetFlag(EFlags.PF, FlagStateProcessor.TestParityFlag(firstOperandValue - secondOperandValue));
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

        if (Prefix != null)
        {
            return false;
        }

        // r/m - i
        if ((Variant.FirstOperand == OperandVariant.Register || Variant.FirstOperand == OperandVariant.Memory) && Variant.FirstOperand == FirstOperand?.Variant
            && Variant.SecondOperand == OperandVariant.Value && Variant.SecondOperand == SecondOperand?.Variant)
        {
            if (FirstOperand.OperandSize == Size.Qword && SecondOperand?.OperandSize == Size.Qword)
            {
                return false;
            }

            if (FirstOperand.OperandSize < SecondOperand?.OperandSize)
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

    public string Opcode => "CMP";

    public IOperandDecoder OperandDecoder { get; init; }
    public IFlagStateProcessor FlagStateProcessor { get; init; }
    public InstructionVariant Variant { get; init; }
    public IPrefix? Prefix { get; init; }
    public IOperand? FirstOperand { get; init; }
    public IOperand? SecondOperand { get; init; }
    public IOperand? ThirdOperand { get; init; }
}