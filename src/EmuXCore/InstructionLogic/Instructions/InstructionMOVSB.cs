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

public sealed class InstructionMOVSB : IInstruction
{
    public InstructionMOVSB(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor, ulong bytes)
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

        int sourceMemoryOffset = (int)virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI;
        int destinationMemoryOffset = (int)virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI;

        virtualMachine.SetByte(destinationMemoryOffset, virtualMachine.GetByte(sourceMemoryOffset));

        if (!virtualMachine.GetFlag(EFlags.DF))
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI++;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI++;
        }
        else
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI--;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI--;
        }

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
            typeof(PrefixREP)
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

    public string Opcode => "MOVSB";
    public ulong Bytes { get; private set; }

    public IOperandDecoder OperandDecoder { get; init; }
    public IFlagStateProcessor FlagStateProcessor { get; init; }
    public InstructionVariant Variant { get; init; }
    public IPrefix? Prefix { get; init; }
    public IOperand? FirstOperand { get; init; }
    public IOperand? SecondOperand { get; init; }
    public IOperand? ThirdOperand { get; init; }
}