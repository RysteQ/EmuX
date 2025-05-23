using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using System.Runtime;

namespace EmuXCore.Instructions;

public sealed class InstructionOUT(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        IVirtualRegister sourceRegister = virtualMachine.CPU.GetRegister(SecondOperand!.FullOperand) ?? throw new ArgumentNullException($"Couldn't find a register with the name {FirstOperand!.FullOperand}");
        int addressToWriteTo = (int)OperandDecoder.GetOperandValue(virtualMachine, FirstOperand);

        switch (SecondOperand!.OperandSize)
        {
            case Size.Byte: virtualMachine.SetByte(addressToWriteTo, (byte)sourceRegister.Get()); break;
            case Size.Word: virtualMachine.SetWord(addressToWriteTo, (ushort)sourceRegister.Get()); break;
            case Size.Double: virtualMachine.SetDouble(addressToWriteTo, (uint)sourceRegister.Get()); break;
        }
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.TwoOperandsRegisterRegister(),
            InstructionVariant.TwoOperandsValueRegister()
        ];

        if (SecondOperand?.OperandSize == Size.Quad)
        {
            return false;
        }

        if (!new List<string>(["AL", "AX", "EAX"]).Contains(SecondOperand?.FullOperand.ToUpper()))
        {
            return false;
        }

        if (Variant.FirstOperand == OperandVariant.Value)
        {
            if (FirstOperand?.OperandSize != Size.Byte)
            {
                return false;
            }
        }
        else
        {
            if (FirstOperand?.OperandSize != Size.Word)
            {
                return false;
            }

            if (FirstOperand?.FullOperand.ToUpper() != "DX")
            {
                return false;
            }
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand?.Variant == Variant.SecondOperand && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "OUT";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}