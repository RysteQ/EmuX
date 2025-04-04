using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;

namespace EmuXCore.Instructions;

public class InstructionADC(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong firstOperandValue = OperandDecoder.GetOperandQuad(virtualMachine, FirstOperand);
        ulong valueToSet = 0;

        if (Variant.FirstOperand == OperandVariant.Register)
        {
            IVirtualRegister? register = virtualMachine.CPU.GetRegister(FirstOperand.FullOperand);
            valueToSet = register.Get() + OperandDecoder.GetOperandQuad(virtualMachine, SecondOperand);

            if (virtualMachine.GetFlag(EFlags.CF))
            {
                valueToSet++;
            }

            register.Set(valueToSet);
        }
        else
        {
            valueToSet = OperandDecoder.GetOperandQuad(virtualMachine, FirstOperand) + OperandDecoder.GetOperandQuad(virtualMachine, SecondOperand);

            if (virtualMachine.GetFlag(EFlags.CF))
            {
                valueToSet++;
            }

            switch (SecondOperand?.OperandSize)
            {
                case Size.Byte: virtualMachine.SetByte((int)OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), (byte)valueToSet); break;
                case Size.Word: virtualMachine.SetWord((int)OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), (ushort)valueToSet); break;
                case Size.Double: virtualMachine.SetDouble((int)OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), (uint)valueToSet); break;
                case Size.Quad: virtualMachine.SetQuad((int)OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), (ulong)valueToSet); break;
            }
        }

        virtualMachine.SetFlag(EFlags.CF, FlagStateProcessor.TestCarryFlag(firstOperandValue, valueToSet - firstOperandValue, FirstOperand.OperandSize));
        virtualMachine.SetFlag(EFlags.OF, FlagStateProcessor.TestOverflowFlag(firstOperandValue, valueToSet - firstOperandValue, valueToSet, FirstOperand.OperandSize));
        virtualMachine.SetFlag(EFlags.SF, FlagStateProcessor.TestSignFlag(valueToSet, FirstOperand.OperandSize));
        virtualMachine.SetFlag(EFlags.ZF, FlagStateProcessor.TestZeroFlag(valueToSet));
        virtualMachine.SetFlag(EFlags.AF, FlagStateProcessor.TestAuxilliaryFlag(firstOperandValue, valueToSet - firstOperandValue));
        virtualMachine.SetFlag(EFlags.PF, FlagStateProcessor.TestParityFlag(valueToSet));
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

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand?.Variant == Variant.SecondOperand;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "ADC";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}