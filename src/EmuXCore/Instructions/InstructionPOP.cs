using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU.Registers;

namespace EmuXCore.Instructions;

public sealed class InstructionPOP(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        if (Variant.FirstOperand == OperandVariant.Register)
        {
            IVirtualRegister register = virtualMachine.CPU.GetRegister(SecondOperand!.FullOperand) ?? throw new ArgumentNullException($"Couldn't find a register with the name {FirstOperand!.FullOperand}");

            switch (FirstOperand!.OperandSize)
            {
                case Size.Word: register.Set((0x_ff_ff_ff_ff_ff_ff_00_00 & register.Get()) + virtualMachine.PopWord()); break;
                case Size.Double: register.Set((0x_ff_ff_ff_ff_00_00_00_00 & register.Get()) + virtualMachine.PopDouble()); break;
                case Size.Quad: register.Set(virtualMachine.PopQuad()); break;
            }
        }
        else
        {
            int addressToWriteTo = OperandDecoder.GetPointerMemoryAddress(virtualMachine, FirstOperand);

            switch (FirstOperand!.OperandSize)
            {
                case Size.Word: virtualMachine.SetWord(addressToWriteTo, virtualMachine.PopWord()); break;
                case Size.Double: virtualMachine.SetDouble(addressToWriteTo, virtualMachine.PopDouble()); break;
                case Size.Quad: virtualMachine.SetQuad(addressToWriteTo, virtualMachine.PopQuad()); break;
            }
        }

        // stack alignment
        virtualMachine.CPU.GetRegister<VirtualRegisterRSP>().RSP -= virtualMachine.CPU.GetRegister<VirtualRegisterRSP>().RSP % 4;
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.OneOperandMemory(),
            InstructionVariant.OneOperandRegister()
        ];

        if (FirstOperand?.OperandSize == Size.Byte)
        {
            return false;
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand == null && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "POP";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}