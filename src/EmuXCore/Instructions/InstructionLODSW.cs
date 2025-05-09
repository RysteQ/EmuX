using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuXCore.Instructions;

public sealed class InstructionLODSW(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        int memoryOffset = (int)((virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS << 32) + virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().ESI);

        if (virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL == virtualMachine.GetByte(memoryOffset))
        {
            if (!virtualMachine.GetFlag(EFlags.DF))
            {
                virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().ESI += 2;
            }
            else
            {
                virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().ESI -= 2;
            }
        }
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.NoOperands()
        ];

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand == null && SecondOperand == null && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "LODSW";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}