using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.Instructions;

public class InstructionCMP(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperandDecoder operandDecoder) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong firstOperandValue = OperandDecoder.GetOperandQuad(virtualMachine, FirstOperand);
        ulong secondOperandValue = OperandDecoder.GetOperandQuad(virtualMachine, SecondOperand);

        virtualMachine.SetFlag(EFlagsEnum.CF, VirtualRegisterEFLAGS.TestCarryFlag(firstOperandValue, secondOperandValue, FirstOperand.OperandSize));
        virtualMachine.SetFlag(EFlagsEnum.OF, VirtualRegisterEFLAGS.TestOverflowFlag(firstOperandValue, secondOperandValue, FirstOperand.OperandSize));
        virtualMachine.SetFlag(EFlagsEnum.SF, VirtualRegisterEFLAGS.TestSignFlag(~(firstOperandValue - secondOperandValue), FirstOperand.OperandSize));
        virtualMachine.SetFlag(EFlagsEnum.ZF, VirtualRegisterEFLAGS.TestZeroFlag(firstOperandValue - secondOperandValue));
        virtualMachine.SetFlag(EFlagsEnum.AF, VirtualRegisterEFLAGS.TestAuxilliaryFlag(firstOperandValue, secondOperandValue));
        virtualMachine.SetFlag(EFlagsEnum.PF, VirtualRegisterEFLAGS.TestParityFlag(firstOperandValue - secondOperandValue));
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

    public string Opcode => "CMP";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
}