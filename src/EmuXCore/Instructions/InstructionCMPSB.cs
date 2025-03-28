using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.Instructions;

public class InstructionCMPSB(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperandDecoder operandDecoder) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong firstOperandValue = virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI;
        ulong secondOperandValue = virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI;
        ulong temp = firstOperandValue - secondOperandValue;

        if (virtualMachine.GetFlag(EFlagsEnum.DF))
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI++;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI++;
        }
        else
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI--;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI--;
        }

        virtualMachine.SetFlag(EFlagsEnum.CF, VirtualRegisterEFLAGS.TestCarryFlag(firstOperandValue, secondOperandValue, Size.Quad));
        virtualMachine.SetFlag(EFlagsEnum.OF, VirtualRegisterEFLAGS.TestOverflowFlag(firstOperandValue, secondOperandValue, Size.Quad));
        virtualMachine.SetFlag(EFlagsEnum.SF, VirtualRegisterEFLAGS.TestSignFlag(~temp, Size.Quad));
        virtualMachine.SetFlag(EFlagsEnum.ZF, VirtualRegisterEFLAGS.TestZeroFlag(temp));
        virtualMachine.SetFlag(EFlagsEnum.AF, VirtualRegisterEFLAGS.TestAuxilliaryFlag(firstOperandValue, secondOperandValue));
        virtualMachine.SetFlag(EFlagsEnum.PF, VirtualRegisterEFLAGS.TestParityFlag(temp));
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

    public string Opcode => "CMPSB";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
}