using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.Interpreter.Encoder.Interfaces.Logic;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionOUT : IInstruction
{
    public InstructionOUT(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor, IInstructionEncoder instructionEncoder)
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
        IVirtualRegister sourceRegister = virtualMachine.CPU.GetRegister(SecondOperand!.FullOperand);
        int addressToWriteTo = (int)OperandDecoder.GetOperandValue(virtualMachine, FirstOperand!);

        switch (SecondOperand!.OperandSize)
        {
            case Size.Byte: virtualMachine.SetByte(addressToWriteTo, (byte)sourceRegister.Get()); break;
            case Size.Word: virtualMachine.SetWord(addressToWriteTo, (ushort)sourceRegister.Get()); break;
            case Size.Dword: virtualMachine.SetDouble(addressToWriteTo, (uint)sourceRegister.Get()); break;
        }

        virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP += Bytes;
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.TwoOperandsRegisterRegister(),
            InstructionVariant.TwoOperandsValueRegister()
        ];

        if (Prefix != null)
        {
            return false;
        }

        if (SecondOperand?.OperandSize == Size.Qword)
        {
            return false;
        }

        if (!new List<string?>(["AL", "AX", "EAX"]).Contains(SecondOperand?.FullOperand.ToUpper()))
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

    public string Opcode => "OUT";
    public ulong Bytes { get; private set; }

    public IOperandDecoder OperandDecoder { get; init; }
    public IFlagStateProcessor FlagStateProcessor { get; init; }
    public InstructionVariant Variant { get; init; }
    public IPrefix? Prefix { get; init; }
    public IOperand? FirstOperand { get; init; }
    public IOperand? SecondOperand { get; init; }
    public IOperand? ThirdOperand { get; init; }
}