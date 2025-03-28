using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.Instructions;

public class InstructionAAD(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperandDecoder operandDecoder) : IInstruction
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

        virtualMachine.SetFlag(EFlagsEnum.SF, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL >> 7 == 1);
        virtualMachine.SetFlag(EFlagsEnum.ZF, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL == 0);
        virtualMachine.SetFlag(EFlagsEnum.PF, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL % 2 == 0);

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

    public string Opcode => "AAD";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
}