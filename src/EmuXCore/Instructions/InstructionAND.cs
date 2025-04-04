﻿using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.Instructions;

public class InstructionAND(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong firstOperandValue = OperandDecoder.GetOperandQuad(virtualMachine, FirstOperand);
        ulong secondOperandValue = OperandDecoder.GetOperandQuad(virtualMachine, SecondOperand);

        if (Variant.FirstOperand == OperandVariant.Register)
        {
            IVirtualRegister? register = virtualMachine.CPU.GetRegister(FirstOperand?.FullOperand);

            register.Set(register.Get() & OperandDecoder.GetOperandQuad(virtualMachine, SecondOperand));
        }
        else
        {
            ulong valueToSet = OperandDecoder.GetOperandQuad(virtualMachine, FirstOperand) & OperandDecoder.GetOperandQuad(virtualMachine, SecondOperand);

            switch (SecondOperand?.OperandSize)
            {
                case Size.Byte: virtualMachine.SetByte((int)OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), (byte)valueToSet); break;
                case Size.Word: virtualMachine.SetWord((int)OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), (ushort)valueToSet); break;
                case Size.Double: virtualMachine.SetDouble((int)OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), (uint)valueToSet); break;
                case Size.Quad: virtualMachine.SetQuad((int)OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), valueToSet); break;
            }
        }

        virtualMachine.SetFlag(EFlags.CF, false);
        virtualMachine.SetFlag(EFlags.OF, false);
        virtualMachine.SetFlag(EFlags.SF, FlagStateProcessor.TestSignFlag(firstOperandValue & secondOperandValue, FirstOperand.OperandSize));
        virtualMachine.SetFlag(EFlags.ZF, FlagStateProcessor.TestZeroFlag(firstOperandValue & secondOperandValue));
        virtualMachine.SetFlag(EFlags.PF, FlagStateProcessor.TestParityFlag(firstOperandValue & secondOperandValue));
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

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand?.Variant == Variant.SecondOperand;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "AND";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}