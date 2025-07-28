using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.InstructionLogic.Prefixes;

namespace EmuX_Unit_Tests.Tests.Interpreter.Instructions.Internal;

public class InstructionConstants<T> : TestWideInternalConstants where T : IInstruction
{
    protected T GenerateInstruction(InstructionVariant? variant = null, IPrefix? prefix = null, IOperand? firstOperand = null, IOperand? secondOperand = null, IOperand? thirdOperand = null)
    {
        return (T)Activator.CreateInstance(typeof(T), new object[] { variant ?? InstructionVariant.NoOperands(), prefix, firstOperand, secondOperand, thirdOperand, GenerateOperandDecoder(), GenerateFlagStateProcessor() });
    }

    protected IOperand GenerateOperand(string fullOperand, OperandVariant variant, Size size, IMemoryOffset[]? offsets = null)
    {
        offsets = offsets ?? [];

        return new Operand(fullOperand, variant, size, offsets);
    }

    protected IPrefix? GeneratePrefix(string? prefix = null)
    {
        if (prefix == null)
        {
            return null;
        }

        return prefix.ToUpper() switch
        {
            "REP" => new PrefixREP(),
            "REPE" => new PrefixREPE(),
            "REPNE" => new PrefixREPNE(),
            "REPNZ" => new PrefixREPNZ(),
            "REPZ" => new PrefixREPZ(),
            _ => throw new NotImplementedException("Unknown prefix"),
        };
    }

    protected List<IPrefix> AllPrefixes()
    {
        return [new PrefixREP(), new PrefixREPE(), new PrefixREPNE(), new PrefixREPNZ(), new PrefixREPZ()];
    }
}