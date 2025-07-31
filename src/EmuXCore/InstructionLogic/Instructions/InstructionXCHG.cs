using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionXCHG(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong firstOperandValue = OperandDecoder.GetOperandValue(virtualMachine, FirstOperand);

        if (Variant.FirstOperand == OperandVariant.Register)
        {
            IVirtualRegister register = virtualMachine.CPU.GetRegister(FirstOperand.FullOperand);

            register.Set(OperandDecoder.GetOperandValue(virtualMachine, SecondOperand));
        }
        else
        {
            switch (FirstOperand.OperandSize)
            {
                case Size.Byte: virtualMachine.SetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (byte)OperandDecoder.GetOperandValue(virtualMachine, SecondOperand)); break;
                case Size.Word: virtualMachine.SetWord(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (ushort)OperandDecoder.GetOperandValue(virtualMachine, SecondOperand)); break;
                case Size.Dword: virtualMachine.SetDouble(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (uint)OperandDecoder.GetOperandValue(virtualMachine, SecondOperand)); break;
                case Size.Qword: virtualMachine.SetQuad(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), OperandDecoder.GetOperandValue(virtualMachine, SecondOperand)); break;
            }
        }

        if (Variant.SecondOperand == OperandVariant.Register)
        {
            IVirtualRegister register = virtualMachine.CPU.GetRegister(SecondOperand.FullOperand);

            register.Set(firstOperandValue);
        }
        else
        {
            switch (FirstOperand.OperandSize)
            {
                case Size.Byte: virtualMachine.SetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine, SecondOperand), (byte)firstOperandValue); break;
                case Size.Word: virtualMachine.SetWord(OperandDecoder.GetPointerMemoryAddress(virtualMachine, SecondOperand), (ushort)firstOperandValue); break;
                case Size.Dword: virtualMachine.SetDouble(OperandDecoder.GetPointerMemoryAddress(virtualMachine, SecondOperand), (uint)firstOperandValue); break;
                case Size.Qword: virtualMachine.SetQuad(OperandDecoder.GetPointerMemoryAddress(virtualMachine, SecondOperand), firstOperandValue); break;
            }
        }
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.TwoOperandsRegisterRegister(),
            InstructionVariant.TwoOperandsRegisterMemory(),
            InstructionVariant.TwoOperandsMemoryRegister(),
        ];

        if (Prefix != null)
        {
            return false;
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand?.Variant == Variant.SecondOperand && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "XCHG";

    public InstructionVariant Variant { get; init; } = variant;
    public IPrefix? Prefix { get; init; } = prefix;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}