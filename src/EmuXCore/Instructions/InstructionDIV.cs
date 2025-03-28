using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.Instructions;

public class InstructionDIV(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperandDecoder operandDecoder) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        if (FirstOperand.Variant == OperandVariant.Register)
        {
            IVirtualRegister? register = virtualMachine.CPU.GetRegister(FirstOperand.FullOperand);

            switch (FirstOperand.OperandSize)
            {
                case Size.Byte: register.Set((register.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00) | (ulong)(((byte)register.Get() & 0x_00_00_00_00_00_00_00_ff) - 1)); break;
                case Size.Word: register.Set((register.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00) | (ulong)(((ushort)register.Get() & 0x_00_00_00_00_00_00_ff_ff) - 1)); break;
                case Size.Double: register.Set((register.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00) | (ulong)(((uint)register.Get() & 0x_00_00_00_00_ff_ff_ff_ff) - 1)); break;
                case Size.Quad: register.Set((register.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00) | (register.Get() - 1)); break;
            }
        }
        else
        {
            IVirtualRegister? register = virtualMachine.CPU.GetRegister(FirstOperand.FullOperand);

            switch (FirstOperand.OperandSize)
            {
                case Size.Byte: virtualMachine.SetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), (byte)(OperandDecoder.GetOperandByte(virtualMachine, FirstOperand) - 1)); break;
                case Size.Word: virtualMachine.SetWord(OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), (ushort)(OperandDecoder.GetOperandWord(virtualMachine, FirstOperand) - 1)); break;
                case Size.Double: virtualMachine.SetDouble(OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), OperandDecoder.GetOperandDouble(virtualMachine, FirstOperand) - 1); break;
                case Size.Quad: virtualMachine.SetQuad(OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), OperandDecoder.GetOperandQuad(virtualMachine, FirstOperand) - 1); ; break;
            }
        }
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.OneOperandRegister(),
            InstructionVariant.OneOperandMemory()
        ];

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;

    public string Opcode => "DIV";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
}