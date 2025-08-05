using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionMUL : IInstruction
{
    public InstructionMUL(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor)
    {
        Variant = variant;
        Prefix = prefix;
        FirstOperand = firstOperand;
        SecondOperand = secondOperand;
        ThirdOperand = thirdOperand;
        OperandDecoder = operandDecoder;
        FlagStateProcessor = flagStateProcessor;
    }

    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong upperhalf = 0;

        switch (FirstOperand!.OperandSize)
        {
            case Size.Byte:
                virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = (ushort)(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL * (byte)OperandDecoder.GetOperandValue(virtualMachine, FirstOperand));
                upperhalf = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX;

                break;

            case Size.Word:
                virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = (ushort)(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX * (ushort)OperandDecoder.GetOperandValue(virtualMachine, FirstOperand) & 0x_ff_ff_00_00);
                virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = (ushort)(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX * (ushort)OperandDecoder.GetOperandValue(virtualMachine, FirstOperand) & 0x_00_00_ff_ff);
                upperhalf = virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX;

                break;

            case Size.Dword:
                virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = (uint)(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX * (uint)OperandDecoder.GetOperandValue(virtualMachine, FirstOperand) & 0x_ff_ff_ff_ff_00_00_00_00);
                virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX * (uint)OperandDecoder.GetOperandValue(virtualMachine, FirstOperand) & 0x_00_00_00_00_ff_ff_ff_ff;
                upperhalf = virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX;

                break;

            case Size.Qword:
                virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().RDX = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX * OperandDecoder.GetOperandValue(virtualMachine, FirstOperand) & 0x_ff_ff_ff_ff_00_00_00_00;
                virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX * OperandDecoder.GetOperandValue(virtualMachine, FirstOperand) & 0x_00_00_00_00_ff_ff_ff_ff;
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

        if (Prefix != null)
        {
            return false;
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand == null && ThirdOperand == null;
    }

    public string Opcode => "MUL";

    public IOperandDecoder OperandDecoder { get; init; }
    public IFlagStateProcessor FlagStateProcessor { get; init; }
    public InstructionVariant Variant { get; init; }
    public IPrefix? Prefix { get; init; }
    public IOperand? FirstOperand { get; init; }
    public IOperand? SecondOperand { get; init; }
    public IOperand? ThirdOperand { get; init; }
}