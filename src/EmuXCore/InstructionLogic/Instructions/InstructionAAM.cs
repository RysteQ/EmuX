using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionAAM(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        if (Variant == InstructionVariant.NoOperands())
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL / 0x0a);
            virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL %= 0x0a;
        }
        else
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL / OperandDecoder.GetOperandByte(virtualMachine, FirstOperand));
            virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL %= OperandDecoder.GetOperandByte(virtualMachine, FirstOperand);
        }

        virtualMachine.SetFlag(EFlags.SF, FlagStateProcessor.TestSignFlag(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL, Size.Byte));
        virtualMachine.SetFlag(EFlags.ZF, FlagStateProcessor.TestZeroFlag(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL));
        virtualMachine.SetFlag(EFlags.PF, FlagStateProcessor.TestParityFlag(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL));
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.NoOperands(),
            InstructionVariant.OneOperandValue()
        ];

        if (Prefix != null)
        {
            return false;
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && Variant.FirstOperand == FirstOperand?.Variant && SecondOperand == null && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "AAM";

    public InstructionVariant Variant { get; init; } = variant;
    public IPrefix? Prefix { get; init; } = prefix;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}