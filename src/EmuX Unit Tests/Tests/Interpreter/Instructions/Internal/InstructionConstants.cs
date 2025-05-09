using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Internal;

namespace EmuX_Unit_Tests.Tests.Interpreter.Instructions.Internal;

public class InstructionConstants<T> : TestWideInternalConstants where T : IInstruction
{
    protected T GenerateInstruction(InstructionVariant? variant = null, IOperand? firstOperand = null, IOperand? secondOperand = null, IOperand? thirdOperand = null)
    {
        return (T)Activator.CreateInstance(typeof(T), new object[] { variant ?? InstructionVariant.NoOperands(), firstOperand, secondOperand, thirdOperand, GenerateOperandDecoder(), GenerateFlagStateProcessor() });
    }

    protected IOperand GenerateOperand(string fullOperand, OperandVariant variant, Size size, IMemoryOffset[]? offsets = null)
    {
        offsets = offsets ?? [];

        return new Operand(fullOperand, variant, size, offsets);
    }
}