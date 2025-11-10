using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.Interpreter.Encoder.Interfaces.Logic;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.InstructionLogic.Instructions;

public sealed class InstructionDIV : IInstruction
{
    public InstructionDIV(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor, IInstructionEncoder instructionEncoder)
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
        ulong toDivideBy = OperandDecoder.GetOperandValue(virtualMachine, FirstOperand!);
        (ulong Quotient, ulong Remainder) divisionResult = (0, 0);

        if (toDivideBy == 0)
        {
            throw new Exception("Cannot divide by zero");
        }

        switch (FirstOperand!.OperandSize)
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
                divisionResult = ulong.DivRem((ulong)virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX << 16 | virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX, toDivideBy);

                if (divisionResult.Quotient > ushort.MaxValue)
                {
                    throw new Exception($"Result is larger than what a {Size.Word} can handle");
                }

                virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = (ushort)divisionResult.Quotient;
                virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = (ushort)divisionResult.Remainder;

                break;

            case Size.Dword:
                divisionResult = ulong.DivRem((ulong)virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX << 32 | virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX, toDivideBy);

                if (divisionResult.Quotient > uint.MaxValue)
                {
                    throw new Exception($"Result is larger than what a {Size.Dword} can handle");
                }

                virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX = (uint)divisionResult.Quotient;
                virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = (uint)divisionResult.Remainder;

                break;

            case Size.Qword:
                (UInt128 Quotient, UInt128 Remainder) bigDivisionResult = UInt128.DivRem(new(virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().RDX, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX), new(0, toDivideBy));

                if (bigDivisionResult.Quotient > ulong.MaxValue)
                {
                    throw new Exception($"Result is larger than what a {Size.Qword} can handle");
                }

                virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = (ulong)bigDivisionResult.Quotient;
                virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().RDX = (ulong)bigDivisionResult.Quotient;

                break;
        }

        virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP += Bytes;
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

        if (FirstOperand != null)
        {
            if (!FirstOperand!.AreMemoryOffsetValid())
            {
                return false;
            }
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand == null && ThirdOperand == null;
    }

    public string Opcode => "DIV";
    public ulong Bytes { get; private set; }

    public IOperandDecoder OperandDecoder { get; init; }
    public IFlagStateProcessor FlagStateProcessor { get; init; }
    public InstructionVariant Variant { get; init; }
    public IPrefix? Prefix { get; init; }
    public IOperand? FirstOperand { get; init; }
    public IOperand? SecondOperand { get; init; }
    public IOperand? ThirdOperand { get; init; }
}