using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionDAS : IInstruction
{
    public InstructionDAS(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor, ulong bytes)
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
        byte oldAl = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL;
        bool oldCf = virtualMachine.GetFlag(EFlags.CF);

        virtualMachine.SetFlag(EFlags.CF, false);

        if ((oldAl & 0x0f) > 9 || virtualMachine.GetFlag(EFlags.AF))
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL -= 6;
            virtualMachine.SetFlag(EFlags.CF, oldCf || oldAl < 6);
            virtualMachine.SetFlag(EFlags.AF, true);
        }
        else
        {
            virtualMachine.SetFlag(EFlags.AF, false);
        }

        if (oldAl > 0x99 || oldCf)
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL -= 0x60;
            virtualMachine.SetFlag(EFlags.CF, true);
        }

        virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP += Bytes;
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.NoOperands()
        ];

        if (Prefix != null)
        {
            return false;
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand == null && SecondOperand == null && ThirdOperand == null;
    }

    public string Opcode => "DAS";
    public ulong Bytes { get; private set; }

    public IOperandDecoder OperandDecoder { get; init; }
    public IFlagStateProcessor FlagStateProcessor { get; init; }
    public InstructionVariant Variant { get; init; }
    public IPrefix? Prefix { get; init; }
    public IOperand? FirstOperand { get; init; }
    public IOperand? SecondOperand { get; init; }
    public IOperand? ThirdOperand { get; init; }
}