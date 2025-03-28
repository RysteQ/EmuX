using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.Instructions;

public class InstructionDAS(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperandDecoder operandDecoder) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        byte oldAl = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL;
        bool oldCf = virtualMachine.GetFlag(EFlagsEnum.CF);

        virtualMachine.SetFlag(EFlagsEnum.CF, false);

        if ((oldAl & 0x0f) > 9 || virtualMachine.GetFlag(EFlagsEnum.AF))
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL -= 6;
            virtualMachine.SetFlag(EFlagsEnum.CF, oldCf || oldAl < 6);
            virtualMachine.SetFlag(EFlagsEnum.AF, true);
        }
        else
        {
            virtualMachine.SetFlag(EFlagsEnum.AF, false);
        }

        if (oldAl > 0x99 || oldCf)
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL -= 0x60;
            virtualMachine.SetFlag(EFlagsEnum.CF, true);
        }
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.NoOperands()
        ];

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand == null && SecondOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;

    public string Opcode => "DAS";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
}