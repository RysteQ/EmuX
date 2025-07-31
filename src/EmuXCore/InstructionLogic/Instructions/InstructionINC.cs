using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionINC(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(VM.Interfaces.IVirtualMachine virtualMachine)
    {
        ulong valueToSet = 0;

        if (FirstOperand!.Variant == OperandVariant.Register)
        {
            IVirtualRegister register = virtualMachine.CPU.GetRegister(FirstOperand!.FullOperand);

            switch (FirstOperand!.OperandSize)
            {
                case Size.Byte: valueToSet = register!.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00 | (ulong)(((byte)register!.Get() & 0x_00_00_00_00_00_00_00_ff) + 1); break;
                case Size.Word: valueToSet = register!.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00 | (ulong)(((ushort)register!.Get() & 0x_00_00_00_00_00_00_ff_ff) + 1); break;
                case Size.Dword: valueToSet = register!.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00 | ((uint)register!.Get() & 0x_00_00_00_00_ff_ff_ff_ff) + 1; break;
                case Size.Qword: valueToSet = register!.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00 | register!.Get() + 1; break;
            }

            register!.Set(valueToSet);
        }
        else
        {
            switch (FirstOperand!.OperandSize)
            {
                case Size.Byte:
                    valueToSet = (byte)(OperandDecoder.GetOperandByte(virtualMachine, FirstOperand) + 1);
                    virtualMachine.SetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (byte)valueToSet);
                    break;

                case Size.Word:
                    valueToSet = (ushort)(OperandDecoder.GetOperandWord(virtualMachine, FirstOperand) + 1);
                    virtualMachine.SetWord(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (ushort)valueToSet);
                    break;

                case Size.Dword:
                    valueToSet = OperandDecoder.GetOperandDouble(virtualMachine, FirstOperand) + 1;
                    virtualMachine.SetDouble(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (uint)valueToSet);
                    break;

                case Size.Qword:
                    valueToSet = OperandDecoder.GetOperandQuad(virtualMachine, FirstOperand) + 1;
                    virtualMachine.SetQuad(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), valueToSet);
                    break;
            }
        }

        virtualMachine.SetFlag(EFlags.OF, FlagStateProcessor.TestOverflowFlag(valueToSet - 1, valueToSet - 1, valueToSet, FirstOperand!.OperandSize));
        virtualMachine.SetFlag(EFlags.SF, FlagStateProcessor.TestSignFlag(valueToSet, FirstOperand!.OperandSize));
        virtualMachine.SetFlag(EFlags.ZF, FlagStateProcessor.TestZeroFlag(valueToSet));
        virtualMachine.SetFlag(EFlags.AF, FlagStateProcessor.TestAuxilliaryFlag(valueToSet, 1, true));
        virtualMachine.SetFlag(EFlags.PF, FlagStateProcessor.TestParityFlag(valueToSet));
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.OneOperandRegister(),
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

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand == null && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "INC";

    public InstructionVariant Variant { get; init; } = variant;
    public IPrefix? Prefix { get; init; } = prefix;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}