﻿using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.InstructionLogic.Prefixes;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionSCASB : IInstruction
{
    public InstructionSCASB(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor)
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
        Prefix?.Loop(this, virtualMachine);

        ulong temp = 0;
        int memoryOffset = 0;

        if (Variant == InstructionVariant.OneOperandMemory())
        {
            memoryOffset = (int)OperandDecoder.GetOperandValue(virtualMachine, FirstOperand);
        }
        else
        {
            memoryOffset = (int)((virtualMachine.CPU.GetRegister<VirtualRegisterES>().ES << 32) + virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().EDI);
        }

        temp = (ulong)(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL - virtualMachine.GetByte(memoryOffset));
        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().FLAGS = (ushort)((virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().FLAGS & 0x_ff_00) + ((byte)(temp & 0b_1101_0101)) | 0b_0000_0000_0000_0010);
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = virtualMachine.GetByte(memoryOffset);

        if (virtualMachine.GetFlag(EFlags.DF))
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().EDI++;
        }
        else
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().EDI--;
        }

        virtualMachine.SetFlag(EFlags.OF, FlagStateProcessor.TestOverflowFlag(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL > virtualMachine.GetByte(memoryOffset) ? (ulong)(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL - virtualMachine.GetByte(memoryOffset)) : (ulong)(virtualMachine.GetByte(memoryOffset) - virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL), temp, Size.Byte));
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.NoOperands(),
            InstructionVariant.OneOperandMemory(),
        ];

        Type[] allowedPrefixTypes =
        [
            typeof(PrefixREPE),
            typeof(PrefixREPNE)
        ];

        if (Prefix != null)
        {
            if (!allowedPrefixTypes.Any(selectedPrefixType => selectedPrefixType == Prefix.Type))
            {
                return false;
            }
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand == null && ThirdOperand == null;
    }

    public string Opcode => "SCASB";

    public IOperandDecoder OperandDecoder { get; init; }
    public IFlagStateProcessor FlagStateProcessor { get; init; }
    public InstructionVariant Variant { get; init; }
    public IPrefix? Prefix { get; init; }
    public IOperand? FirstOperand { get; init; }
    public IOperand? SecondOperand { get; init; }
    public IOperand? ThirdOperand { get; init; }
}