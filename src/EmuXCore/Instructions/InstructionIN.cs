using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.Instructions;

public sealed class InstructionIN(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong ioValue = virtualMachine.GetQuad((int)OperandDecoder.GetOperandValue(virtualMachine, SecondOperand));

        switch (FirstOperand!.OperandSize)
        {
            case Size.Byte: virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = (byte)ioValue; break;
            case Size.Word: virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = (ushort)ioValue; break;
            case Size.Double: virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX = (uint)ioValue; break;
        }
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.TwoOperandsRegisterValue(),
            InstructionVariant.TwoOperandsRegisterRegister()
        ];

        if (!new List<string>(["AL", "AX", "EAX"]).Any(selectedRegister => selectedRegister == FirstOperand?.FullOperand.ToUpper().Trim()))
        {
            return false;
        }

        if (Variant == InstructionVariant.TwoOperandsRegisterValue())
        {
            if (SecondOperand?.OperandSize != Size.Byte)
            {
                return false;
            }
        }

        if (Variant == InstructionVariant.TwoOperandsRegisterRegister())
        {
            if (SecondOperand?.FullOperand.ToUpper().Trim() != "DX")
            {
                return false;
            }
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand?.Variant == Variant.SecondOperand && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "IN";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}