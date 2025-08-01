﻿using EmuXCore.Common.Enums;

namespace EmuXCore.InstructionLogic.Instructions.Internal;

public class InstructionVariant : IComparable
{
    public InstructionVariant(int id, OperandVariant? firstOperand = null, OperandVariant? secondOperand = null, OperandVariant? thirdOperand = null)
    {
        Id = id;
        FirstOperand = firstOperand;
        SecondOperand = secondOperand;
        ThirdOperand = thirdOperand;
    }

    public static InstructionVariant NoOperands() => new(1);

    public static InstructionVariant OneOperandValue() => new(2, OperandVariant.Value);
    public static InstructionVariant OneOperandRegister() => new(3, OperandVariant.Register);
    public static InstructionVariant OneOperandMemory() => new(4, OperandVariant.Memory);

    public static InstructionVariant TwoOperandsRegisterValue() => new(5, OperandVariant.Register, OperandVariant.Value);
    public static InstructionVariant TwoOperandsRegisterRegister() => new(6, OperandVariant.Register, OperandVariant.Register);
    public static InstructionVariant TwoOperandsRegisterMemory() => new(7, OperandVariant.Register, OperandVariant.Memory);
    public static InstructionVariant TwoOperandsMemoryValue() => new(8, OperandVariant.Memory, OperandVariant.Value);
    public static InstructionVariant TwoOperandsMemoryRegister() => new(9, OperandVariant.Memory, OperandVariant.Register);
    public static InstructionVariant TwoOperandsValueRegister() => new(10, OperandVariant.Value, OperandVariant.Register);

    public static InstructionVariant ThreeOperandsRegisterRegisterValue() => new(11, OperandVariant.Register, OperandVariant.Register, OperandVariant.Value);
    public static InstructionVariant ThreeOperandsRegisterMemoryValue() => new(12, OperandVariant.Register, OperandVariant.Memory, OperandVariant.Value);

    public static InstructionVariant NaN() => new(-1);

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    public int CompareTo(object? obj) => Id.CompareTo(((InstructionVariant)obj).Id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

    public override bool Equals(object? obj)
    {
        if (obj is not InstructionVariant)
        {
            return false;
        }

        return ((InstructionVariant)obj).Id == Id;
    }


    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static bool operator ==(InstructionVariant left, InstructionVariant right)
    {
        return left.Id == right.Id;
    }

    public static bool operator !=(InstructionVariant left, InstructionVariant right)
    {
        return left.Id != right.Id;
    }

    public int AmountOfOperands { get => FirstOperand == null ? 0 : 1 + SecondOperand == null ? 0 : 1 + ThirdOperand == null ? 0 : 1; }

    public int Id { get; init; }
    public OperandVariant? FirstOperand { get; init; }
    public OperandVariant? SecondOperand { get; init; }
    public OperandVariant? ThirdOperand { get; init; }
}