using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionSBB(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong firstOperandValue = OperandDecoder.GetOperandValue(virtualMachine, FirstOperand);
        ulong secondOperandValue = OperandDecoder.GetOperandValue(virtualMachine, SecondOperand);
        ulong valueToSet = firstOperandValue - secondOperandValue;

        if (virtualMachine.GetFlag(EFlags.CF))
        {
            valueToSet--;
        }

        if (Variant.FirstOperand == OperandVariant.Register)
        {
            IVirtualRegister? register = virtualMachine.CPU.GetRegister(FirstOperand!.FullOperand) ?? throw new ArgumentNullException($"Couldn't find a register with the name {FirstOperand!.FullOperand}");

            register!.Set(valueToSet);
        }
        else
        {
            switch (SecondOperand!.OperandSize)
            {
                case Size.Byte: virtualMachine.SetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (byte)valueToSet); break;
                case Size.Word: virtualMachine.SetWord(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (ushort)valueToSet); break;
                case Size.Dword: virtualMachine.SetDouble(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (uint)valueToSet); break;
                case Size.Qword: virtualMachine.SetQuad(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), valueToSet); break;
            }
        }

        virtualMachine.SetFlag(EFlags.CF, FlagStateProcessor.TestCarryFlag(firstOperandValue, valueToSet - firstOperandValue, FirstOperand!.OperandSize));
        virtualMachine.SetFlag(EFlags.OF, FlagStateProcessor.TestOverflowFlag(firstOperandValue, valueToSet - firstOperandValue, valueToSet, FirstOperand!.OperandSize));
        virtualMachine.SetFlag(EFlags.SF, FlagStateProcessor.TestSignFlag(valueToSet, FirstOperand!.OperandSize));
        virtualMachine.SetFlag(EFlags.ZF, FlagStateProcessor.TestZeroFlag(valueToSet));
        virtualMachine.SetFlag(EFlags.AF, FlagStateProcessor.TestAuxilliaryFlag(firstOperandValue, secondOperandValue, false));
        virtualMachine.SetFlag(EFlags.PF, FlagStateProcessor.TestParityFlag(valueToSet));
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.TwoOperandsRegisterValue(),
            InstructionVariant.TwoOperandsRegisterRegister(),
            InstructionVariant.TwoOperandsRegisterMemory(),
            InstructionVariant.TwoOperandsMemoryRegister(),
            InstructionVariant.TwoOperandsMemoryValue(),
        ];

        if (Prefix != null)
        {
            return false;
        }

        if (SecondOperand?.Variant == OperandVariant.Value && SecondOperand?.OperandSize == Size.Qword)
        {
            return false;
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand?.Variant == Variant.SecondOperand && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "SBB";

    public InstructionVariant Variant { get; init; } = variant;
    public IPrefix? Prefix { get; init; } = prefix;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}