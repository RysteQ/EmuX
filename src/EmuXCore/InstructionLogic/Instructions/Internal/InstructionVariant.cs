using EmuXCore.Common.Enums;

namespace EmuXCore.InstructionLogic.Instructions.Internal;

public class InstructionVariant : IComparable
{
    public InstructionVariant(OperandVariant? firstOperand = null, OperandVariant? secondOperand = null, OperandVariant? thirdOperand = null)
    {
        FirstOperand = firstOperand;
        SecondOperand = secondOperand;
        ThirdOperand = thirdOperand;
    
        firstOperand = firstOperand == null ? OperandVariant.NaN : firstOperand;
        secondOperand = secondOperand == null ? OperandVariant.NaN : secondOperand;
        thirdOperand = thirdOperand == null ? OperandVariant.NaN : thirdOperand;

        Id = (int)firstOperand * 16 + (int)secondOperand * 4 + (int)thirdOperand;
    }

    public static InstructionVariant NoOperands() => new();

    public static InstructionVariant OneOperandValue() => new(OperandVariant.Value);
    public static InstructionVariant OneOperandRegister() => new(OperandVariant.Register);
    public static InstructionVariant OneOperandMemory() => new(OperandVariant.Memory);

    public static InstructionVariant TwoOperandsRegisterValue() => new(OperandVariant.Register, OperandVariant.Value);
    public static InstructionVariant TwoOperandsRegisterRegister() => new(OperandVariant.Register, OperandVariant.Register);
    public static InstructionVariant TwoOperandsRegisterMemory() => new(OperandVariant.Register, OperandVariant.Memory);
    public static InstructionVariant TwoOperandsMemoryValue() => new(OperandVariant.Memory, OperandVariant.Value);
    public static InstructionVariant TwoOperandsMemoryRegister() => new(OperandVariant.Memory, OperandVariant.Register);
    public static InstructionVariant TwoOperandsValueRegister() => new(OperandVariant.Value, OperandVariant.Register);

    public static InstructionVariant ThreeOperandsRegisterRegisterValue() => new(OperandVariant.Register, OperandVariant.Register, OperandVariant.Value);
    public static InstructionVariant ThreeOperandsRegisterMemoryValue() => new(OperandVariant.Register, OperandVariant.Memory, OperandVariant.Value);

    public static InstructionVariant NaN() => new() { Id = -1 };

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