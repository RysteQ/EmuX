using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionIN(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong ioValue = virtualMachine.GetQuad((int)OperandDecoder.GetOperandValue(virtualMachine, SecondOperand));

        switch (FirstOperand!.OperandSize)
        {
            case Size.Byte: virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = (byte)ioValue; break;
            case Size.Word: virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = (ushort)ioValue; break;
            case Size.Dword: virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX = (uint)ioValue; break;
        }
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.TwoOperandsRegisterValue(),
            InstructionVariant.TwoOperandsRegisterRegister()
        ];

        if (Prefix != null)
        {
            return false;
        }

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
    public IPrefix? Prefix { get; init; } = prefix;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}