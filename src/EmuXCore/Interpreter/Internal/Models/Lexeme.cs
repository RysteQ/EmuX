using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU;
using System.Diagnostics;

namespace EmuXCore.Interpreter.Internal.Models;

public class Lexeme(IVirtualCPU cpuToTranslateFor, ISourceCodeLine sourceCodeLine, string opcode, string firstOperand, string secondOperand, string thirdOperand) : ILexeme
{
    public IInstruction ToIInstruction()
    {
        InstructionVariant instructionVariant = GetInstructionVariant();
        IOperandDecoder operandDecoder = GenerateOperandDecoder();
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();
        IOperand? firstOperand = null;
        IOperand? secondOperand = null;
        IOperand? thirdOperand = null;
        IInstruction instruction;

        if (!string.IsNullOrEmpty(FirstOperand))
        {
            firstOperand = ParseOperand(FirstOperand);
        }

        if (!string.IsNullOrEmpty(SecondOperand))
        {
            secondOperand = ParseOperand(SecondOperand);
        }

        if (!string.IsNullOrEmpty(ThirdOperand))
        {
            thirdOperand = ParseOperand(ThirdOperand);
        }

        // 81 (85) instructions total, 20 completed, 25% completed
        instruction = Opcode switch
        {
            "AAA" => new InstructionAAA(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "AAD" => new InstructionAAD(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "AAM" => new InstructionAAM(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "AAS" => new InstructionAAS(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "ADC" => new InstructionADC(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "ADD" => new InstructionADD(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "AND" => new InstructionAND(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CALL" => new InstructionCALL(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CBW" => new InstructionCBW(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CDQE" => new InstructionCDQE(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor), // *
            "CWDE" => new InstructionCWDE(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor), // *
            "CLC" => new InstructionCLC(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CLD" => new InstructionCLD(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CLI" => new InstructionCLI(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CMC" => new InstructionCMC(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CMP" => new InstructionCMC(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CMPSB" => new InstructionCMPSB(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CMPSW" => new InstructionCMPSW(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CWD" => new InstructionCWD(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CDQ" => new InstructionCDQ(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor), // *
            "CDO" => new InstructionCDO(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor), // *
            "DAA" => new InstructionDAA(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "DAS" => new InstructionDAS(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "DEC" => new InstructionDEC(instructionVariant, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            _ => throw new Exception($"Unknown opcode \"{SourceCodeLine.SourceCode}\" : {SourceCodeLine.Line}"),
        };

        return instruction;
    }

    public bool AreOperandsValid()
    {
        IOperand firstOperand;
        IOperand secondOperand;
        IOperand thirdOperand;
        
        if (string.IsNullOrEmpty(FirstOperand))
        {
            return true;
        }

        try
        {
            if (!string.IsNullOrEmpty(FirstOperand) && string.IsNullOrEmpty(SecondOperand))
            {
                firstOperand = ParseOperand(FirstOperand);

                return firstOperand.Variant != OperandVariant.NaN;
            }

            if (!string.IsNullOrEmpty(FirstOperand) && !string.IsNullOrEmpty(SecondOperand) && string.IsNullOrEmpty(ThirdOperand))
            {
                firstOperand = ParseOperand(FirstOperand);
                secondOperand = ParseOperand(SecondOperand);

                if (firstOperand.OperandSize < secondOperand.OperandSize)
                {
                    return false;
                }

                return firstOperand.Variant != OperandVariant.NaN && secondOperand.Variant != OperandVariant.NaN;
            }

            firstOperand = ParseOperand(FirstOperand);
            secondOperand = ParseOperand(SecondOperand);
            thirdOperand = ParseOperand(ThirdOperand);

            if (!string.IsNullOrEmpty(FirstOperand) && !string.IsNullOrEmpty(SecondOperand) && !string.IsNullOrEmpty(ThirdOperand))
            {
                if (firstOperand.OperandSize < secondOperand.OperandSize && secondOperand.OperandSize < thirdOperand.OperandSize)
                {
                    return false;
                }
            }

            return firstOperand.Variant != OperandVariant.NaN && secondOperand.Variant != OperandVariant.NaN && thirdOperand.Variant != OperandVariant.NaN;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);

            return false;
        }
    }
    
    private InstructionVariant GetInstructionVariant()
    {
        if (string.IsNullOrEmpty(FirstOperand) && string.IsNullOrEmpty(SecondOperand))
        {
            return InstructionVariant.NoOperands();
        }

        if (!string.IsNullOrEmpty(FirstOperand) && string.IsNullOrEmpty(SecondOperand))
        {
            switch (GetTypeOfOperand(FirstOperand))
            {
                case OperandVariant.Value: return InstructionVariant.OneOperandValue();
                case OperandVariant.Memory: return InstructionVariant.OneOperandMemory();
                case OperandVariant.Register: return InstructionVariant.OneOperandRegister();
                case OperandVariant.NaN: return InstructionVariant.NaN();
            };
        }

        if (!string.IsNullOrEmpty(FirstOperand) && !string.IsNullOrEmpty(SecondOperand))
        {
            if (GetTypeOfOperand(FirstOperand) == OperandVariant.Value)
            {
                return InstructionVariant.NaN();
            }

            if (GetTypeOfOperand(FirstOperand) == OperandVariant.Memory)
            {
                switch (GetTypeOfOperand(SecondOperand))
                {
                    case OperandVariant.Value: return InstructionVariant.TwoOperandsMemoryValue();
                    case OperandVariant.Register: return InstructionVariant.TwoOperandsMemoryRegister();
                    default: return InstructionVariant.NaN();
                }
            }
            else if (GetTypeOfOperand(FirstOperand) == OperandVariant.Register)
            {
                switch (GetTypeOfOperand(SecondOperand))
                {
                    case OperandVariant.Value: return InstructionVariant.TwoOperandsRegisterValue();
                    case OperandVariant.Memory: return InstructionVariant.TwoOperandsRegisterMemory();
                    case OperandVariant.Register: return InstructionVariant.TwoOperandsRegisterRegister();
                    default: return InstructionVariant.NaN();
                }
            }
        }

        return InstructionVariant.NaN();
    }

    private OperandVariant GetTypeOfOperand(string operand)
    {

        operand = operand.ToUpper();

        if (string.IsNullOrEmpty(operand))
        {
            return OperandVariant.NaN;
        }

        if (IsImmediateType(operand))
        {
            return OperandVariant.Value;
        }

        if (IsMemoryType(operand))
        {
            return OperandVariant.Memory;
        }

        if (IsRegisterType(operand))
        {
            return OperandVariant.Register;
        }

        return OperandVariant.NaN;
    }

    private bool IsImmediateType(string operand)
    {
        return operand.EndsWith('B')
            && !operand.Substring(0, operand.Length - 1).Where(selectedChar => selectedChar != '0' && selectedChar != '1').Any()
            || operand.EndsWith('H')
            && !operand.Substring(0, operand.Length - 1).Where(selectedChar => int.TryParse(selectedChar.ToString(), out _) == false && selectedChar < 65 && selectedChar > 70).Any()
            || operand.Length == 3 && operand[0] == '\'' && operand[2] == '\''
            || int.TryParse(operand, out _);
    }

    private bool IsMemoryType(string operand)
    {
        string[] memorySizeOperands = ["BYTE", "WORD", "DOUBLE", "QUAD"];
        string memoryPointer = string.Empty;
        string memorySize = string.Empty;
        string operandType = string.Empty;
        int operandAdditions = 0;
        int operandSubtractions = 0;

        if (operand.Where(selectedCharacter => selectedCharacter == ' ').ToList().Count() >= 2)
        {
            memorySize = operand.Split(' ')[0].ToUpper();
            operandType = operand.Split(' ')[1].ToUpper();
            memoryPointer = string.Join(' ', operand.Split(' ').Skip(2));

            if (memorySizeOperands.Contains(memorySize)
                && operandType == "PTR"
                && memoryPointer.StartsWith('[')
                && memoryPointer.EndsWith(']'))
            {
                memoryPointer = memoryPointer.Trim('[', ']').Replace(" ", string.Empty);
                operandAdditions = operand.Where(selectedCharacter => selectedCharacter == '+').ToList().Count();
                operandSubtractions = operand.Where(selectedCharacter => selectedCharacter == '-').ToList().Count();

                return true;
            }
        }

        return false;
    }

    private bool IsRegisterType(string operand)
    {
        IVirtualCPU virtualCPU = new VirtualCPU();

        foreach (IVirtualRegister virtualRegister in virtualCPU.Registers)
        {
            if (virtualRegister.RegisterNamesAndSizes.ContainsKey(operand))
            {
                return true;
            }
        }

        return false;
    }

    private IVirtualRegister GetRegister(string operand)
    {
        IVirtualCPU virtualCPU = new VirtualCPU();

        foreach (IVirtualRegister virtualRegister in virtualCPU.Registers)
        {
            if (virtualRegister.RegisterNamesAndSizes.ContainsKey(operand.ToUpper()))
            {
                return virtualRegister;
            }
        }

        throw new KeyNotFoundException($"Register with name {operand} not found");
    }

    private IOperand ParseOperand(string operandToParse)
    {
        IOperand operand;
        OperandVariant operandVariant = GetTypeOfOperand(operandToParse);

        operand = operandVariant switch
        {
            OperandVariant.Value => GetValueOperand(operandToParse),
            OperandVariant.Memory => GetMemoryOperand(operandToParse),
            OperandVariant.Register => GetRegisterOperand(operandToParse),
            _ => throw new Exception("Invalid operand variant")
        };

        return operand;
    }

    private IOperand GetValueOperand(string operandToParse)
    {
        ulong immediateValue = 0;
        Size operandSize = Size.Quad;

        immediateValue = operandToParse.ToUpper().Last() switch
        {
            'H' => Convert.ToUInt64(operandToParse[..^1], 16),
            'B' => Convert.ToUInt64(operandToParse[..^1], 2),
            '\'' => (byte)operandSize,
            _ => ulong.Parse(operandToParse)
        };

        if (immediateValue <= byte.MaxValue)
        {
            operandSize = Size.Byte;
        }
        else if (immediateValue <= ushort.MaxValue)
        {
            operandSize = Size.Word;
        }
        else if (immediateValue <= uint.MaxValue)
        {
            operandSize = Size.Double;
        }

        return new Operand(operandToParse, OperandVariant.Value, operandSize, [], string.Empty);
    }

    private IOperand GetMemoryOperand(string operandToParse)
    {
        string modifiedOperandToParse = string.Empty;
        string operandSize = string.Empty;
        string[] memoryPointerOperands = [];
        int memoryPointerOperandsIndex = 0;
        string memoryLabel = string.Empty;
        List<IVirtualRegister> registers = [];
        List<int> offsets = [];
        Dictionary<string, Size> memoryPointerSizes = new()
        {
            { nameof(Size.Byte).ToUpper(), Size.Byte },
            { nameof(Size.Word).ToUpper(), Size.Word },
            { nameof(Size.Double).ToUpper(), Size.Double },
            { nameof(Size.Quad).ToUpper(), Size.Quad }
        };

        operandSize = operandToParse.Split(' ')[0].ToUpper();
        modifiedOperandToParse = string.Join(' ', operandToParse.Split(' ').Skip(2));
        modifiedOperandToParse = modifiedOperandToParse.Replace(" ", string.Empty).Trim('[', ']');
        memoryPointerOperands = modifiedOperandToParse.Split('+', '-');

        foreach (string memoryPointerOperand in memoryPointerOperands)
        {
            if (int.TryParse(memoryPointerOperand, out int detectedOffset))
            {
                if (modifiedOperandToParse[memoryPointerOperandsIndex - 1] == '-')
                {
                    detectedOffset = -detectedOffset;
                }

                offsets.Add(detectedOffset);
            }
            else if (_cpu.Registers.Any(selectedRegister => selectedRegister.RegisterNamesAndSizes.ContainsKey(memoryPointerOperand.ToUpper())))
            {
                registers.Add(GetRegister(memoryPointerOperand.ToUpper()));
            }
            else
            {
                memoryLabel = memoryPointerOperand;
            }

            memoryPointerOperandsIndex += memoryPointerOperand.Length + 1;
        }

        return new Operand(operandToParse, OperandVariant.Memory, memoryPointerSizes[operandSize], offsets.ToArray(), memoryLabel);
    }

    private IOperand GetRegisterOperand(string expression)
    {
        IVirtualRegister virtualRegister = GetRegister(expression);

        return new Operand(expression, OperandVariant.Register, virtualRegister.RegisterNamesAndSizes[expression.ToUpper()], [], string.Empty);
    }

    private IOperandDecoder GenerateOperandDecoder()
    {
        return new OperandDecoder();
    }

    private IFlagStateProcessor GenerateFlagStateProcessor()
    {
        return new FlagStateProcessor();
    }

    public ISourceCodeLine SourceCodeLine { get; init; } = sourceCodeLine;
    public string Opcode { get; init; } = opcode;
    public string FirstOperand { get; init; } = firstOperand;
    public string SecondOperand { get; init; } = secondOperand;
    public string ThirdOperand { get; init; } = thirdOperand;

    private readonly IVirtualCPU _cpu = cpuToTranslateFor;
}