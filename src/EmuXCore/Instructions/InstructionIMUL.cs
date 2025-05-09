using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.Instructions;

public sealed class InstructionIMUL(InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) : IInstruction
{
    public void Execute(IVirtualMachine virtualMachine)
    {
        ulong firstOperandValue = FirstOperand != null ? OperandDecoder.GetOperandValue(virtualMachine, FirstOperand) : 0;
        ulong secondOperandValue = SecondOperand != null ? OperandDecoder.GetOperandValue(virtualMachine, SecondOperand) : 0;
        ulong thirdOperandValue = ThirdOperand != null ? OperandDecoder.GetOperandValue(virtualMachine, ThirdOperand) : 0;
        IVirtualRegister? register;
        UInt128 valueToSet = 0;
        bool updateFlags = false;

        if (SecondOperand == null)
        {
            switch (FirstOperand!.OperandSize)
            {
                case Size.Byte:
                    valueToSet = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL * firstOperandValue;
                    updateFlags = (byte)(valueToSet >> 8) == (byte)valueToSet;

                    virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = (ushort)valueToSet;

                    break;

                case Size.Word:
                    valueToSet = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX * firstOperandValue;
                    updateFlags = (ushort)(valueToSet >> 16) == (ushort)valueToSet;

                    virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = (ushort)(valueToSet >> 16);
                    virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = (ushort)valueToSet;

                    break;

                case Size.Double:
                    valueToSet = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX * firstOperandValue;
                    updateFlags = (uint)(valueToSet >> 32) == (uint)valueToSet;

                    virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = (uint)(valueToSet >> 32);
                    virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX = (uint)valueToSet;

                    break;

                case Size.Quad:
                    valueToSet = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX * firstOperandValue;
                    updateFlags = (ulong)(valueToSet >> 64) == (ulong)valueToSet;

                    virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().RDX = (uint)(valueToSet >> 64);
                    virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = (uint)valueToSet;

                    break;
            }
        }
        else
        {
            register = virtualMachine.CPU.GetRegister(FirstOperand!.FullOperand) ?? throw new ArgumentNullException($"Couldn't find a register with the name {FirstOperand!.FullOperand}");
            valueToSet = ThirdOperand == null ? firstOperandValue * secondOperandValue : secondOperandValue * thirdOperandValue;
            updateFlags = ((ulong)valueToSet >> 64) == valueToSet;

            register!.Set((ulong)valueToSet);
        }

        virtualMachine.SetFlag(EFlags.CF, !updateFlags);
        virtualMachine.SetFlag(EFlags.OF, !updateFlags);
    }

    public bool IsValid()
    {
        InstructionVariant[] allowedVariants =
        [
            InstructionVariant.OneOperandRegister(),
            InstructionVariant.OneOperandMemory(),
            InstructionVariant.TwoOperandsRegisterRegister(),
            InstructionVariant.TwoOperandsRegisterMemory(),
            InstructionVariant.ThreeOperandsRegisterRegisterValue(),
            InstructionVariant.ThreeOperandsRegisterMemoryValue(),
        ];

        // r - r/m
        if (Variant.FirstOperand == OperandVariant.Register && Variant.FirstOperand == FirstOperand?.Variant
            && (Variant.SecondOperand == OperandVariant.Register || Variant.SecondOperand == OperandVariant.Memory) && Variant.SecondOperand == SecondOperand?.Variant)
        {
            if (FirstOperand?.OperandSize != SecondOperand?.OperandSize || FirstOperand?.OperandSize == Size.Byte)
            {
                return false;
            }

            if (!SecondOperand!.AreMemoryOffsetValid())
            {
                return false;
            }
        }

        // r - r/m - i
        if (Variant.FirstOperand == OperandVariant.Register && Variant.FirstOperand == FirstOperand?.Variant
            && (Variant.SecondOperand == OperandVariant.Register || Variant.SecondOperand == OperandVariant.Memory) && Variant.SecondOperand == SecondOperand?.Variant
            && Variant.ThirdOperand == OperandVariant.Value && Variant.ThirdOperand == ThirdOperand?.Variant)
        {
            if (FirstOperand?.OperandSize != SecondOperand?.OperandSize || FirstOperand?.OperandSize == Size.Byte)
            {
                return false;
            }

            if (SecondOperand?.OperandSize < ThirdOperand?.OperandSize)
            {
                return false;
            }

            if (!SecondOperand!.AreMemoryOffsetValid())
            {
                return false;
            }
        }

        return allowedVariants.Any(allowedVariant => allowedVariant.Id == Variant.Id) && FirstOperand?.Variant == Variant.FirstOperand && SecondOperand?.Variant == Variant.SecondOperand && ThirdOperand?.Variant == Variant.ThirdOperand;
    }

    public IOperandDecoder OperandDecoder { get; init; } = operandDecoder;
    public IFlagStateProcessor FlagStateProcessor { get; init; } = flagStateProcessor;

    public string Opcode => "IMUL";

    public InstructionVariant Variant { get; init; } = variant;
    public IOperand? FirstOperand { get; init; } = firstOperand;
    public IOperand? SecondOperand { get; init; } = secondOperand;
    public IOperand? ThirdOperand { get; init; } = thirdOperand;
}