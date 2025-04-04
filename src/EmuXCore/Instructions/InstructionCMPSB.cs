using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.Instructions;

public class InstructionCMPSB(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong firstOperandValue = virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI;
        ulong secondOperandValue = virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI;
        ulong temp = firstOperandValue - secondOperandValue;

        if (virtualMachine.GetFlag(EFlags.DF))
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI++;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI++;
        }
        else
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI--;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI--;
        }

        virtualMachine.SetFlag(EFlags.CF, FlagStateProcessor.TestCarryFlag(firstOperandValue, secondOperandValue, Size.Quad));
        virtualMachine.SetFlag(EFlags.OF, FlagStateProcessor.TestOverflowFlag(firstOperandValue, secondOperandValue, temp, Size.Quad));
        virtualMachine.SetFlag(EFlags.SF, FlagStateProcessor.TestSignFlag(~temp, Size.Quad));
        virtualMachine.SetFlag(EFlags.ZF, FlagStateProcessor.TestZeroFlag(temp));
        virtualMachine.SetFlag(EFlags.AF, FlagStateProcessor.TestAuxilliaryFlag(firstOperandValue, secondOperandValue));
        virtualMachine.SetFlag(EFlags.PF, FlagStateProcessor.TestParityFlag(temp));
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
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "CMPSB";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}