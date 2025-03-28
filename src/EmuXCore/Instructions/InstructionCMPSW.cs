using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.Instructions;

public class InstructionCMPSW(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperandDecoder operandDecoder) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong firstOperandValue = virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI;
        ulong secondOperandValue = virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI;
        ulong temp = firstOperandValue - secondOperandValue;

        if (virtualMachine.GetFlag(EFlagsEnum.DF))
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI += 2;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI += 2;
        }
        else
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI -= 2;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI -= 2;
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

    public string Opcode => "CMPSW";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
}