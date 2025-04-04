﻿using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.BIOS.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.Instructions;

public class InstructionINT(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        byte interruptCode = OperandDecoder.GetOperandByte(virtualMachine, FirstOperand);
        byte subInterruptCode = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH;

        if (!Enum.IsDefined(typeof(InterruptCode), interruptCode))
        {
            throw new Exception($"Interrupt code {interruptCode} has not been defined");
        }

        virtualMachine.Interrupt((InterruptCode)interruptCode, subInterruptCode);
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.OneOperandValue()
        ];

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "INT";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}