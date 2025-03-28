using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.Instructions;

public class InstructionAAM(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperandDecoder operandDecoder) : IInstruction
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

        virtualMachine.SetFlag(EFlagsEnum.SF, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL >> 7 == 1);
        virtualMachine.SetFlag(EFlagsEnum.ZF, VirtualRegisterEFLAGS.TestZeroFlag(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL));
        virtualMachine.SetFlag(EFlagsEnum.PF, VirtualRegisterEFLAGS.TestParityFlag(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL));
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.NoOperands(),
            InstructionVariant.OneOperandValue()
        ];

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && SecondOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;

    public string Opcode => "AAM";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
}