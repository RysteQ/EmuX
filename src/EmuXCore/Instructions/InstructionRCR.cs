using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU.Enums;

namespace EmuXCore.Instructions;

public sealed class InstructionRCR(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong valueToShift = OperandDecoder.GetOperandValue(virtualMachine, FirstOperand!);
        ulong bitsToShift = OperandDecoder.GetOperandValue(virtualMachine, SecondOperand!);
        bool currentCFValue = virtualMachine.GetFlag(EFlags.CF);

        valueToShift = valueToShift >> (int)(bitsToShift - 1);
        virtualMachine.SetFlag(EFlags.CF, valueToShift >> (((int)FirstOperand!.OperandSize * 8) - 1) == 1);
        valueToShift = valueToShift >> 1;

        if (FirstOperand!.Variant == OperandVariant.Register)
        {
            IVirtualRegister? virtualRegister = virtualMachine.CPU.GetRegister(FirstOperand!.FullOperand!) ?? throw new ArgumentNullException($"Couldn't find a register with the name {FirstOperand!.FullOperand}");

            virtualRegister.Set(valueToShift);
        }
        else
        {
            switch (SecondOperand!.OperandSize)
            {
                case Size.Byte: virtualMachine.SetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand!), (byte)valueToShift); break;
                case Size.Word: virtualMachine.SetWord(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand!), (ushort)valueToShift); break;
                case Size.Double: virtualMachine.SetDouble(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand!), (uint)valueToShift); break;
                case Size.Quad: virtualMachine.SetQuad(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand!), valueToShift); break;
            }
        }
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.TwoOperandsRegisterValue(),
            InstructionVariant.TwoOperandsMemoryValue(),
            InstructionVariant.TwoOperandsRegisterRegister(),
            InstructionVariant.TwoOperandsMemoryRegister()
        ];

        if (SecondOperand?.OperandSize != Size.Byte)
        {
            return false;
        }

        if (SecondOperand?.Variant == OperandVariant.Register && SecondOperand?.FullOperand.ToUpper() != "CL")
        {
            return false;
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand?.Variant == Variant.SecondOperand && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "RCR";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}