using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.InstructionLogic.Prefixes;
using EmuXCore.Interpreter.Encoder.Interfaces.Logic;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionCMPSB : IInstruction
{
    public InstructionCMPSB(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor, ulong bytes)
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
        Prefix?.Loop(this, virtualMachine);

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

        virtualMachine.SetFlag(EFlags.CF, FlagStateProcessor.TestCarryFlag(firstOperandValue, secondOperandValue, Size.Qword));
        virtualMachine.SetFlag(EFlags.OF, FlagStateProcessor.TestOverflowFlag(firstOperandValue, secondOperandValue, temp, Size.Qword));
        virtualMachine.SetFlag(EFlags.SF, FlagStateProcessor.TestSignFlag(~temp, Size.Qword));
        virtualMachine.SetFlag(EFlags.ZF, FlagStateProcessor.TestZeroFlag(temp));
        virtualMachine.SetFlag(EFlags.AF, FlagStateProcessor.TestAuxilliaryFlag(firstOperandValue, secondOperandValue, false));
        virtualMachine.SetFlag(EFlags.PF, FlagStateProcessor.TestParityFlag(temp));

        virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP += Bytes;
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

    public string Opcode => "CMPSB";
    public ulong Bytes { get; private set; }

    public IOperandDecoder OperandDecoder { get; init; }
    public IFlagStateProcessor FlagStateProcessor { get; init; }
    public InstructionVariant Variant { get; init; }
    public IPrefix? Prefix { get; init; }
    public IOperand? FirstOperand { get; init; }
    public IOperand? SecondOperand { get; init; }
    public IOperand? ThirdOperand { get; init; }
}