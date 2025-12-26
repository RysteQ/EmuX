using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionIN : IInstruction
{
    public InstructionIN(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor, ulong bytes)
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
        ulong ioValue = virtualMachine.GetQuad((int)OperandDecoder.GetOperandValue(virtualMachine, SecondOperand!));

        switch (FirstOperand!.OperandSize)
        {
            case Size.Byte: virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = (byte)ioValue; break;
            case Size.Word: virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = (ushort)ioValue; break;
            case Size.Dword: virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX = (uint)ioValue; break;
        }

        virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP += Bytes;
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

    public string Opcode => "IN";
    public ulong Bytes { get; private set; }

    public IOperandDecoder OperandDecoder { get; init; }
    public IFlagStateProcessor FlagStateProcessor { get; init; }
    public InstructionVariant Variant { get; init; }
    public IPrefix? Prefix { get; init; }
    public IOperand? FirstOperand { get; init; }
    public IOperand? SecondOperand { get; init; }
    public IOperand? ThirdOperand { get; init; }
}