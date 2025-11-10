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

public sealed class InstructionNEG : IInstruction
{
    public InstructionNEG(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor, IInstructionEncoder instructionEncoder)
    {
        Variant = variant;
        Prefix = prefix;
        FirstOperand = firstOperand;
        SecondOperand = secondOperand;
        ThirdOperand = thirdOperand;
        OperandDecoder = operandDecoder;
        FlagStateProcessor = flagStateProcessor;

        Bytes = (ulong)instructionEncoder.Parse([this]).Bytes.First().Length;
    }

    public void Execute(IVirtualMachine virtualMachine)
    {
        if (FirstOperand!.Variant == OperandVariant.Register)
        {
            IVirtualRegister register = virtualMachine.CPU.GetRegister(FirstOperand.FullOperand);

            switch (FirstOperand.OperandSize)
            {
                case Size.Byte: register!.Set(FirstOperand!.FullOperand, (0x_00_00_00_00_00_00_00_80 & ~(0x_00_00_00_00_00_00_00_80 & register!.Get())) + (0x_7f_ff_ff_ff_ff_ff_ff_7f & register!.Get())); break;
                case Size.Word: register!.Set(FirstOperand!.FullOperand, (0x_00_00_00_00_00_00_00_80 & ~(0x_00_00_00_00_00_00_80_00 & register!.Get())) + (0x_7f_ff_ff_ff_ff_ff_7f_ff & register!.Get())); break;
                case Size.Dword: register!.Set(FirstOperand!.FullOperand, (0x_00_00_00_00_00_00_00_80 & ~(0x_00_00_00_00_80_00_00_00 & register!.Get())) + (0x_0f_ff_ff_ff_7f_ff_ff_ff & register!.Get())); break;
                case Size.Qword: register!.Set(FirstOperand!.FullOperand, (0x_00_00_00_00_00_00_00_80 & ~(0x_80_00_00_00_00_00_00_00 & register!.Get())) + (0x_7f_ff_ff_ff_ff_ff_ff_ff & register!.Get())); break;
            }
        }
        else
        {
            switch (FirstOperand.OperandSize)
            {
                case Size.Byte: virtualMachine.SetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (byte)((0x_80 & ~(0x_80 & virtualMachine.GetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand)))) + (1 << virtualMachine.GetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand)) >> 1))); break;
                case Size.Word: virtualMachine.SetWord(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (ushort)((0x_80_00 & ~(0x_80_00 & virtualMachine.GetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand)))) + (0x_7f_ff & virtualMachine.GetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand))))); break;
                case Size.Dword: virtualMachine.SetDouble(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (uint)((0x_80_00_00_00 & ~(0x_80_00_00_00 & virtualMachine.GetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand)))) + (0x_7f_ff_ff_ff & virtualMachine.GetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand))))); break;
                case Size.Qword: virtualMachine.SetQuad(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand), (0x_80_00_00_00_00_00_00_00 & ~(0x_80_00_00_00_00_00_00_00 & virtualMachine.GetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand)))) + (ulong)(0x_7f_ff_ff_ff_ff_ff_ff_ff & virtualMachine.GetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand)))); break;
            }
        }

        virtualMachine.SetFlag(EFlags.CF, OperandDecoder.GetOperandValue(virtualMachine, FirstOperand) != 0);

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

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand == null && ThirdOperand == null;
    }

    public string Opcode => "NEG";
    public ulong Bytes { get; private set; }

    public IOperandDecoder OperandDecoder { get; init; }
    public IFlagStateProcessor FlagStateProcessor { get; init; }
    public InstructionVariant Variant { get; init; }
    public IPrefix? Prefix { get; init; }
    public IOperand? FirstOperand { get; init; }
    public IOperand? SecondOperand { get; init; }
    public IOperand? ThirdOperand { get; init; }
}