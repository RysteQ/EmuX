using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.Interpreter.Encoder.Interfaces.Logic;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionSAL : IInstruction
{
    public InstructionSAL(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor, ulong bytes)
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
        ulong valueToShift = OperandDecoder.GetOperandValue(virtualMachine, FirstOperand!);
        ulong bitsToShift = OperandDecoder.GetOperandValue(virtualMachine, SecondOperand!);
        ulong valueToSet = valueToShift;

        if (bitsToShift == 1)
        {
            virtualMachine.SetFlag(EFlags.OF, FlagStateProcessor.TestOverflowFlag(valueToShift, valueToShift - valueToShift >> 1, valueToShift >> 1, Size.Qword));
        }

        for (int i = 0; i < (int)bitsToShift; i++)
        {
            valueToSet = valueToSet << 1;

            virtualMachine.SetFlag(EFlags.CF, (valueToShift >> (int)FirstOperand!.OperandSize * 8 - (i + 1)) % 2 == 1);
        }

        if (FirstOperand!.Variant == OperandVariant.Register)
        {
            IVirtualRegister virtualRegister = virtualMachine.CPU.GetRegister(FirstOperand!.FullOperand!);

            switch (FirstOperand!.OperandSize)
            {
                case Size.Byte: virtualRegister.Set(FirstOperand!.FullOperand, (virtualRegister.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00) + valueToSet); break;
                case Size.Word: virtualRegister.Set(FirstOperand!.FullOperand, (virtualRegister.Get() & 0x_ff_ff_ff_ff_ff_ff_00_00) + valueToSet); break;
                case Size.Dword: virtualRegister.Set(FirstOperand!.FullOperand, (virtualRegister.Get() & 0x_ff_ff_ff_ff_00_00_00_00) + valueToSet); break;
                case Size.Qword: virtualRegister.Set(FirstOperand!.FullOperand, valueToSet); break;
            }
        }
        else
        {
            switch (SecondOperand!.OperandSize)
            {
                case Size.Byte: virtualMachine.SetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand!), (byte)valueToSet); break;
                case Size.Word: virtualMachine.SetWord(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand!), (ushort)valueToSet); break;
                case Size.Dword: virtualMachine.SetDouble(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand!), (uint)valueToSet); break;
                case Size.Qword: virtualMachine.SetQuad(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand!), valueToSet); break;
            }
        }

        virtualMachine.SetFlag(EFlags.SF, FlagStateProcessor.TestSignFlag(valueToSet, FirstOperand!.OperandSize));
        virtualMachine.SetFlag(EFlags.ZF, FlagStateProcessor.TestZeroFlag(valueToSet));
        virtualMachine.SetFlag(EFlags.PF, FlagStateProcessor.TestParityFlag(valueToSet));

        virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP += Bytes;
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.TwoOperandsRegisterValue(),
            InstructionVariant.TwoOperandsRegisterRegister(),
            InstructionVariant.TwoOperandsMemoryValue(),
            InstructionVariant.TwoOperandsMemoryRegister(),
        ];

        if (Prefix != null)
        {
            return false;
        }

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

    public string Opcode => "SAL";
    public ulong Bytes { get; private set; }

    public IOperandDecoder OperandDecoder { get; init; }
    public IFlagStateProcessor FlagStateProcessor { get; init; }
    public InstructionVariant Variant { get; init; }
    public IPrefix? Prefix { get; init; }
    public IOperand? FirstOperand { get; init; }
    public IOperand? SecondOperand { get; init; }
    public IOperand? ThirdOperand { get; init; }
}