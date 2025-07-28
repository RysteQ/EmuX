using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.InstructionLogic.Prefixes;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionCMPSW(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        Prefix?.Loop(this, virtualMachine);

        ulong firstOperandValue = virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI;
        ulong secondOperandValue = virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI;
        ulong temp = firstOperandValue - secondOperandValue;

        if (virtualMachine.GetFlag(EFlags.DF))
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI += 2;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI += 2;
        }
        else
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI -= 2;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI -= 2;
        }

        virtualMachine.SetFlag(EFlags.CF, FlagStateProcessor.TestCarryFlag(firstOperandValue, secondOperandValue, Size.Qword));
        virtualMachine.SetFlag(EFlags.OF, FlagStateProcessor.TestOverflowFlag(firstOperandValue, secondOperandValue, firstOperandValue < secondOperandValue ? temp : ~temp, Size.Qword));
        virtualMachine.SetFlag(EFlags.SF, FlagStateProcessor.TestSignFlag(~temp, Size.Qword));
        virtualMachine.SetFlag(EFlags.ZF, FlagStateProcessor.TestZeroFlag(temp));
        virtualMachine.SetFlag(EFlags.AF, FlagStateProcessor.TestAuxilliaryFlag(firstOperandValue, secondOperandValue, false));
        virtualMachine.SetFlag(EFlags.PF, FlagStateProcessor.TestParityFlag(temp));
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.NoOperands()
        ];

        Type[] allowedPrefixTypes =
        [
            typeof(PrefixREPE),
            typeof(PrefixREPNE),
            typeof(PrefixREPNZ),
            typeof(PrefixREPZ)
        ];

        if (Prefix != null)
        {
            if (!allowedPrefixTypes.Any(selectedPrefixType => selectedPrefixType == Prefix.Type))
            {
                return false;
            }
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand == null && SecondOperand == null && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "CMPSW";

    public InstructionVariant Variant { get; init; } = variant;
    public IPrefix? Prefix { get; init; } = prefix;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}