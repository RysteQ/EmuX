using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.Instructions;

public class InstructionDEC(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperandDecoder operandDecoder) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong valueToSet = 0;

        if (FirstOperand.Variant == OperandVariant.Register)
        {
            IVirtualRegister? register = virtualMachine.CPU.GetRegister(FirstOperand.FullOperand);

            switch (FirstOperand.OperandSize)
            {
                case Size.Byte: valueToSet = (register.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00) | (ulong)(((byte)register.Get() & 0x_00_00_00_00_00_00_00_ff) - 1); break;
                case Size.Word: valueToSet = (register.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00) | (ulong)(((ushort)register.Get() & 0x_00_00_00_00_00_00_ff_ff) - 1); break;
                case Size.Double: valueToSet = (register.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00) | (((uint)register.Get() & 0x_00_00_00_00_ff_ff_ff_ff) - 1); break;
                case Size.Quad: valueToSet = (register.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00) | (register.Get() - 1); break;
            }

            register.Set(valueToSet);
        }
        else
        {
            IVirtualRegister? register = virtualMachine.CPU.GetRegister(FirstOperand.FullOperand);

            switch (FirstOperand.OperandSize)
            {
                case Size.Byte:
                    valueToSet = (byte)(OperandDecoder.GetOperandByte(virtualMachine, FirstOperand) - 1);
                    virtualMachine.SetByte(OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), (byte)valueToSet);
                    break;
                
                case Size.Word:
                    valueToSet = (ushort)(OperandDecoder.GetOperandWord(virtualMachine, FirstOperand) - 1);
                    virtualMachine.SetWord(OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), (ushort)valueToSet);
                    break;
                
                case Size.Double:
                    valueToSet = OperandDecoder.GetOperandDouble(virtualMachine, FirstOperand) - 1;
                    virtualMachine.SetDouble(OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), (uint)valueToSet);
                    break;
                
                case Size.Quad:
                    valueToSet = OperandDecoder.GetOperandQuad(virtualMachine, FirstOperand) - 1;
                    virtualMachine.SetQuad(OperandDecoder.GetPointerMemoryAddress(virtualMachine.Memory, FirstOperand), valueToSet);
                    break;
            }
        }

        virtualMachine.SetFlag(EFlagsEnum.OF, VirtualRegisterEFLAGS.TestOverflowFlag)
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

    public string Opcode => "DEC";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
}