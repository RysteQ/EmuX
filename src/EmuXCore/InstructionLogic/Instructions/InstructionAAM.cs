using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionAAM : IInstruction
{
    public InstructionAAM(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor, ulong bytes)
    {
        Variant = variant;
        Prefix = prefix;
        FirstOperand = firstOperand;
        SecondOperand = secondOperand;
        ThirdOperand = thirdOperand;
        OperandDecoder = operandDecoder;
        FlagStateProcessor = flagStateProcessor;
        Bytes = bytes;
    }

    public void Execute(IVirtualMachine virtualMachine)
    {
        if (Variant == InstructionVariant.NoOperands())
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL / 0x0a);
            virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL %= 0x0a;
        }
        else
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL / OperandDecoder.GetOperandByte(virtualMachine, FirstOperand!));
            virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL %= OperandDecoder.GetOperandByte(virtualMachine, FirstOperand!);
        }

        virtualMachine.SetFlag(EFlags.SF, FlagStateProcessor.TestSignFlag(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL, Size.Byte));
        virtualMachine.SetFlag(EFlags.ZF, FlagStateProcessor.TestZeroFlag(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL));
        virtualMachine.SetFlag(EFlags.PF, FlagStateProcessor.TestParityFlag(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL));

        virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP += Bytes;
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

    public string Opcode => "AAM";
    public ulong Bytes { get; private set; }

    public IOperandDecoder OperandDecoder { get; init; }
    public IFlagStateProcessor FlagStateProcessor { get; init; }
    public InstructionVariant Variant { get; init; }
    public IPrefix? Prefix { get; init; }
    public IOperand? FirstOperand { get; init; }
    public IOperand? SecondOperand { get; init; }
    public IOperand? ThirdOperand { get; init; }
}