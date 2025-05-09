using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU.Enums;

namespace EmuXCore.Instructions;

public sealed class InstructionAND(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong firstOperandValue = OperandDecoder.GetOperandValue(virtualMachine, FirstOperand);
        ulong secondOperandValue = OperandDecoder.GetOperandValue(virtualMachine, SecondOperand);

        if (Variant.FirstOperand == OperandVariant.Register)
        {
            IVirtualRegister? register = virtualMachine.CPU.GetRegister(FirstOperand!.FullOperand) ?? throw new ArgumentNullException($"Couldn't find a register with the name {FirstOperand!.FullOperand}");
            
            register!.Set(register!.Get() & OperandDecoder.GetOperandValue(virtualMachine, SecondOperand));
        }
        else
        {
            ulong valueToSet = OperandDecoder.GetOperandValue(virtualMachine, FirstOperand) & OperandDecoder.GetOperandValue(virtualMachine, SecondOperand);

            switch (SecondOperand!.OperandSize)
            {
                case Size.Byte: virtualMachine.SetByte((int)OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (byte)valueToSet); break;
                case Size.Word: virtualMachine.SetWord((int)OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (ushort)valueToSet); break;
                case Size.Double: virtualMachine.SetDouble((int)OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (uint)valueToSet); break;
                case Size.Quad: virtualMachine.SetQuad((int)OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), valueToSet); break;
            }
        }

        virtualMachine.SetFlag(EFlags.CF, false);
        virtualMachine.SetFlag(EFlags.OF, false);
        virtualMachine.SetFlag(EFlags.SF, FlagStateProcessor.TestSignFlag(firstOperandValue & secondOperandValue, FirstOperand!.OperandSize));
        virtualMachine.SetFlag(EFlags.ZF, FlagStateProcessor.TestZeroFlag(firstOperandValue & secondOperandValue));
        virtualMachine.SetFlag(EFlags.PF, FlagStateProcessor.TestParityFlag(firstOperandValue & secondOperandValue));
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.TwoOperandsRegisterValue(),
            InstructionVariant.TwoOperandsRegisterRegister(),
            InstructionVariant.TwoOperandsMemoryValue(),
            InstructionVariant.TwoOperandsMemoryRegister()
        ];

        // r/m - i
        if ((Variant.FirstOperand == OperandVariant.Register || Variant.FirstOperand == OperandVariant.Memory) && Variant.FirstOperand == FirstOperand?.Variant
            && Variant.SecondOperand == OperandVariant.Value && Variant.SecondOperand == SecondOperand?.Variant)
        {
            if (FirstOperand.OperandSize == Size.Quad && SecondOperand?.OperandSize == Size.Quad)
            {
                return false;
            }

            if (FirstOperand.OperandSize < SecondOperand?.OperandSize)
            {
                return false;
            }

            if (!FirstOperand!.AreMemoryOffsetValid())
            {
                return false;
            }
        }

        // r/m - r
        if ((Variant.FirstOperand == OperandVariant.Register || Variant.FirstOperand == OperandVariant.Memory) && Variant.FirstOperand == FirstOperand?.Variant
            && Variant.SecondOperand == OperandVariant.Register && Variant.SecondOperand == SecondOperand?.Variant)
        {
            if (FirstOperand?.OperandSize != SecondOperand?.OperandSize)
            {
                return false;
            }

            if (!FirstOperand!.AreMemoryOffsetValid())
            {
                return false;
            }
        }

        // r - r/m
        if (Variant.FirstOperand == OperandVariant.Register && Variant.FirstOperand == FirstOperand?.Variant
            && (Variant.SecondOperand == OperandVariant.Register || Variant.SecondOperand == OperandVariant.Memory) && Variant.SecondOperand == SecondOperand?.Variant)
        {
            if (FirstOperand?.OperandSize != SecondOperand?.OperandSize)
            {
                return false;
            }

            if (!SecondOperand!.AreMemoryOffsetValid())
            {
                return false;
            }
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand?.Variant == Variant.SecondOperand && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "AND";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}