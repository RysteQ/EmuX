using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionCALL(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        switch (FirstOperand!.OperandSize)
        {
            case Size.Byte: throw new ArgumentException($"Cannot call when the size of the operand is of type {nameof(Size.Byte)}");
            case Size.Word: virtualMachine.PushWord(virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().IP); break;
            case Size.Dword: virtualMachine.PushDouble(virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().EIP); break;
            case Size.Qword: virtualMachine.PushQuad(virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP); break;
            default: throw new ArgumentException($"Unknown operand size for {nameof(FirstOperand.OperandSize)}");
        }

        virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP = (ulong)OperandDecoder.GetInstructionMemoryAddress(virtualMachine.Memory, FirstOperand!);
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.OneOperandMemory()
        ];

        if (Prefix != null)
        {
            return false;
        }

        if (FirstOperand != null)
        {
            if (!FirstOperand!.AreMemoryOffsetValid())
            {
                return false;
            }
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && Variant.FirstOperand == FirstOperand?.Variant && SecondOperand == null && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "CALL";

    public InstructionVariant Variant { get; init; } = variant;
    public IPrefix? Prefix { get; init; } = prefix;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}