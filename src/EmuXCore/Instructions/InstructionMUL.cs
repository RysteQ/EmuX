using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.Instructions;

public sealed class InstructionMUL(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong upperhalf = 0;

        switch (FirstOperand.OperandSize)
        {
            case Size.Byte:
                virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = (ushort)(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL * (byte)OperandDecoder.GetOperandValue(virtualMachine, FirstOperand));
                upperhalf = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX;

                break;

            case Size.Word:
                virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = (ushort)((virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX * (ushort)OperandDecoder.GetOperandValue(virtualMachine, FirstOperand)) & 0x_ff_ff_00_00);
                virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = (ushort)((virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX * (ushort)OperandDecoder.GetOperandValue(virtualMachine, FirstOperand)) & 0x_00_00_ff_ff);
                upperhalf = virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX;

                break;

            case Size.Double:
                virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = (uint)((virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX * (uint)OperandDecoder.GetOperandValue(virtualMachine, FirstOperand)) & 0x_ff_ff_ff_ff_00_00_00_00);
                virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX = (uint)((virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX * (uint)OperandDecoder.GetOperandValue(virtualMachine, FirstOperand)) & 0x_00_00_00_00_ff_ff_ff_ff);
                upperhalf = virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX;

                break;

            case Size.Quad:
                virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().RDX = (virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX * OperandDecoder.GetOperandValue(virtualMachine, FirstOperand)) & 0x_ff_ff_ff_ff_00_00_00_00;
                virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = (virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX * OperandDecoder.GetOperandValue(virtualMachine, FirstOperand)) & 0x_00_00_00_00_ff_ff_ff_ff;
                upperhalf = virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().RDX;

                break;
        }

        virtualMachine.SetFlag(EFlags.OF, upperhalf != 0);
        virtualMachine.SetFlag(EFlags.CF, upperhalf != 0);
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.OneOperandRegister(),
            InstructionVariant.OneOperandMemory()
        ];

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand == null && ThirdOperand == null;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "MUL";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}