using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionDEC : IInstruction
{
    public InstructionDEC(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor, ulong bytes)
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
        ulong valueToSet = 0;

        if (FirstOperand!.Variant == OperandVariant.Register)
        {
            IVirtualRegister register = virtualMachine.CPU.GetRegister(FirstOperand!.FullOperand);

            switch (FirstOperand!.OperandSize)
            {
                case Size.Byte: valueToSet = register!.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00 | (ulong)(((byte)register!.Get() & 0x_00_00_00_00_00_00_00_ff) - 1); break;
                case Size.Word: valueToSet = register!.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00 | (ulong)(((ushort)register!.Get() & 0x_00_00_00_00_00_00_ff_ff) - 1); break;
                case Size.Dword: valueToSet = register!.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00 | ((uint)register!.Get() & 0x_00_00_00_00_ff_ff_ff_ff) - 1; break;
                case Size.Qword: valueToSet = register!.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00 | register!.Get() - 1; break;
            }

            register!.Set(FirstOperand!.FullOperand, valueToSet);
        }
        else
        {
            switch (FirstOperand!.OperandSize)
            {
                case Size.Byte:
                    valueToSet = (byte)(OperandDecoder.GetOperandByte(virtualMachine, FirstOperand) - 1);
                    virtualMachine.SetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (byte)valueToSet);
                    break;

                case Size.Word:
                    valueToSet = (ushort)(OperandDecoder.GetOperandWord(virtualMachine, FirstOperand) - 1);
                    virtualMachine.SetWord(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (ushort)valueToSet);
                    break;

                case Size.Dword:
                    valueToSet = OperandDecoder.GetOperandDouble(virtualMachine, FirstOperand) - 1;
                    virtualMachine.SetDouble(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (uint)valueToSet);
                    break;

                case Size.Qword:
                    valueToSet = OperandDecoder.GetOperandQuad(virtualMachine, FirstOperand) - 1;
                    virtualMachine.SetQuad(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), valueToSet);
                    break;
            }
        }

        virtualMachine.SetFlag(EFlags.OF, FlagStateProcessor.TestOverflowFlag(valueToSet + 1, valueToSet + 1, valueToSet, FirstOperand!.OperandSize));
        virtualMachine.SetFlag(EFlags.SF, FlagStateProcessor.TestSignFlag(valueToSet, FirstOperand!.OperandSize));
        virtualMachine.SetFlag(EFlags.ZF, FlagStateProcessor.TestZeroFlag(valueToSet));
        virtualMachine.SetFlag(EFlags.AF, FlagStateProcessor.TestAuxilliaryFlag(valueToSet, 1, false));
        virtualMachine.SetFlag(EFlags.PF, FlagStateProcessor.TestParityFlag(valueToSet));

        virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP += Bytes;
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

    public string Opcode => "DEC";
    public ulong Bytes { get; private set; }

    public IOperandDecoder OperandDecoder { get; init; }
    public IFlagStateProcessor FlagStateProcessor { get; init; }
    public InstructionVariant Variant { get; init; }
    public IPrefix? Prefix { get; init; }
    public IOperand? FirstOperand { get; init; }
    public IOperand? SecondOperand { get; init; }
    public IOperand? ThirdOperand { get; init; }
}