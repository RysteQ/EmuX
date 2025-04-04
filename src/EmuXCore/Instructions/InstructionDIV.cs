using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.Instructions;

public class InstructionDIV(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong toDivideBy = OperandDecoder.GetOperandQuad(virtualMachine, FirstOperand);
        (ulong Quotient, ulong Remainder) divisionResult = (0, 0);

        if (toDivideBy == 0)
        {
            throw new Exception("Cannot divide by zero");
        }

        switch (FirstOperand.OperandSize)
        {
            case Size.Byte:
                divisionResult = ulong.DivRem(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX, toDivideBy);

                if (divisionResult.Quotient > byte.MaxValue)
                {
                    throw new Exception($"Result is larger than what a {Size.Byte} can handle");
                }

                virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = (byte)divisionResult.Quotient;
                virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)divisionResult.Remainder;

                break;
            
            case Size.Word:
                divisionResult = ulong.DivRem(((ulong)virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX << 16) | ((ulong)virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX), toDivideBy);

                if (divisionResult.Quotient > ushort.MaxValue)
                {
                    throw new Exception($"Result is larger than what a {Size.Word} can handle");
                }

                virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = (ushort)divisionResult.Quotient;
                virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = (ushort)divisionResult.Remainder;

                break;
            
            case Size.Double:
                divisionResult = ulong.DivRem(((ulong)virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX << 32) | ((ulong)virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX), toDivideBy);

                if (divisionResult.Quotient > uint.MaxValue)
                {
                    throw new Exception($"Result is larger than what a {Size.Double} can handle");
                }

                virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX = (uint)divisionResult.Quotient;
                virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = (uint)divisionResult.Remainder;

                break;
            
            case Size.Quad:
                (UInt128 Quotient, UInt128 Remainder) bigDivisionResult = UInt128.DivRem(new(virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().RDX, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX), new(0, toDivideBy));
                
                if (bigDivisionResult.Quotient > ulong.MaxValue)
                {
                    throw new Exception($"Result is larger than what a {Size.Quad} can handle");
                }

                virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = (ulong)bigDivisionResult.Quotient;
                virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().RDX = (ulong)bigDivisionResult.Quotient;

                break;
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
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "DIV";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}