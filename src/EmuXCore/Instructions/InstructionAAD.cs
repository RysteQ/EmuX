using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.Instructions;

public class InstructionAAD(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        byte al = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL;
        byte ah = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH;

        if (Variant == InstructionVariant.NoOperands())
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = (byte)((al + (ah * 0x0a)) & 0xff);
        }
        else
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = (byte)(al + ah * OperandDecoder.GetOperandByte(virtualMachine, FirstOperand) & 0xff);
        }

        virtualMachine.SetFlag(EFlags.SF, FlagStateProcessor.TestSignFlag(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL, Size.Byte));
        virtualMachine.SetFlag(EFlags.ZF, FlagStateProcessor.TestZeroFlag(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL));
        virtualMachine.SetFlag(EFlags.PF, FlagStateProcessor.TestParityFlag(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL));

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = 0;
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.NoOperands(),
            InstructionVariant.OneOperandValue(),
        ];

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && SecondOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "AAD";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}