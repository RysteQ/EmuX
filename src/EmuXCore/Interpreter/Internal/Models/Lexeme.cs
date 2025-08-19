using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.InstructionLogic.Prefixes;
using EmuXCore.Interpreter.Interfaces.Logic;
using EmuXCore.Interpreter.Interfaces.Models;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU;
using System.Diagnostics;

namespace EmuXCore.Interpreter.Internal.Models;

public class Lexeme : ILexeme
{
    public Lexeme(IVirtualCPU cpuToTranslateFor, ISourceCodeLine sourceCodeLine, string prefix, string opcode, string firstOperand, string secondOperand, string thirdOperand)
    {
        _cpu = cpuToTranslateFor;
        SourceCodeLine = sourceCodeLine;
        Prefix = prefix;
        Opcode = opcode;
        FirstOperand = firstOperand;
        SecondOperand = secondOperand;
        ThirdOperand = thirdOperand;
    }

    public IInstruction ToIInstruction()
    {
        InstructionVariant instructionVariant = GetInstructionVariant();
        IOperandDecoder operandDecoder = DIFactory.GenerateIOperandDecoder();
        IFlagStateProcessor flagStateProcessor = DIFactory.GenerateIFlagStateProcessor();
        IPrefix? prefix = null;
        IOperand? firstOperand = null;
        IOperand? secondOperand = null;
        IOperand? thirdOperand = null;
        IInstruction instruction;

        if (!string.IsNullOrEmpty(Prefix))
        {
            prefix = ParsePrefix(Prefix);
        }

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

        instruction = Opcode.ToUpper() switch
        {
            "AAA" => DIFactory.GenerateIInstruction<InstructionAAA>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor), // *
            "AAD" => DIFactory.GenerateIInstruction<InstructionAAD>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor), // *
            "AAM" => DIFactory.GenerateIInstruction<InstructionAAM>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor), // *
            "AAS" => DIFactory.GenerateIInstruction<InstructionAAS>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor), // *
            "ADC" => DIFactory.GenerateIInstruction<InstructionADC>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "ADD" => DIFactory.GenerateIInstruction<InstructionADD>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "AND" => DIFactory.GenerateIInstruction<InstructionAND>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CALL" => DIFactory.GenerateIInstruction<InstructionCALL>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CBW" => DIFactory.GenerateIInstruction<InstructionCBW>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CDQE" => DIFactory.GenerateIInstruction<InstructionCDQE>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CWDE" => DIFactory.GenerateIInstruction<InstructionCWDE>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CLC" => DIFactory.GenerateIInstruction<InstructionCLC>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CLD" => DIFactory.GenerateIInstruction<InstructionCLD>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CLI" => DIFactory.GenerateIInstruction<InstructionCLI>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CMC" => DIFactory.GenerateIInstruction<InstructionCMC>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CMP" => DIFactory.GenerateIInstruction<InstructionCMC>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CMPSB" => DIFactory.GenerateIInstruction<InstructionCMPSB>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CMPSW" => DIFactory.GenerateIInstruction<InstructionCMPSW>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CWD" => DIFactory.GenerateIInstruction<InstructionCWD>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "CDQ" => DIFactory.GenerateIInstruction<InstructionCDQ>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "DAA" => DIFactory.GenerateIInstruction<InstructionDAA>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor), // *
            "DAS" => DIFactory.GenerateIInstruction<InstructionDAS>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor), // *
            "DEC" => DIFactory.GenerateIInstruction<InstructionDEC>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "DIV" => DIFactory.GenerateIInstruction<InstructionDIV>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "HLT" => DIFactory.GenerateIInstruction<InstructionHLT>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "IDIV" => DIFactory.GenerateIInstruction<InstructionIDIV>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "IMUL" => DIFactory.GenerateIInstruction<InstructionIMUL>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "IN" => DIFactory.GenerateIInstruction<InstructionIN>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "INC" => DIFactory.GenerateIInstruction<InstructionINC>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "INT" => DIFactory.GenerateIInstruction<InstructionINT>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "INTO" => DIFactory.GenerateIInstruction<InstructionINTO>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JA" => DIFactory.GenerateIInstruction<InstructionJA>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JAE" => DIFactory.GenerateIInstruction<InstructionJAE>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JB" => DIFactory.GenerateIInstruction<InstructionJB>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JBE" => DIFactory.GenerateIInstruction<InstructionJBE>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JC" => DIFactory.GenerateIInstruction<InstructionJC>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JE" => DIFactory.GenerateIInstruction<InstructionJE>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JG" => DIFactory.GenerateIInstruction<InstructionJG>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JGE" => DIFactory.GenerateIInstruction<InstructionJGE>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JL" => DIFactory.GenerateIInstruction<InstructionJL>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JLE" => DIFactory.GenerateIInstruction<InstructionJLE>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JMP" => DIFactory.GenerateIInstruction<InstructionJMP>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JNA" => DIFactory.GenerateIInstruction<InstructionJNA>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JNAE" => DIFactory.GenerateIInstruction<InstructionJNAE>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JNB" => DIFactory.GenerateIInstruction<InstructionJNB>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JNBE" => DIFactory.GenerateIInstruction<InstructionJNBE>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JNC" => DIFactory.GenerateIInstruction<InstructionJNC>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JNE" => DIFactory.GenerateIInstruction<InstructionJNE>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JNG" => DIFactory.GenerateIInstruction<InstructionJNG>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JNGE" => DIFactory.GenerateIInstruction<InstructionJNGE>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JNL" => DIFactory.GenerateIInstruction<InstructionJNL>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JNLE" => DIFactory.GenerateIInstruction<InstructionJNLE>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JNO" => DIFactory.GenerateIInstruction<InstructionJNO>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JNP" => DIFactory.GenerateIInstruction<InstructionJNP>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JNS" => DIFactory.GenerateIInstruction<InstructionJNS>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JNZ" => DIFactory.GenerateIInstruction<InstructionJNZ>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JO" => DIFactory.GenerateIInstruction<InstructionJO>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JP" => DIFactory.GenerateIInstruction<InstructionJP>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JPE" => DIFactory.GenerateIInstruction<InstructionJPE>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JPO" => DIFactory.GenerateIInstruction<InstructionJPO>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JS" => DIFactory.GenerateIInstruction<InstructionJS>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "JZ" => DIFactory.GenerateIInstruction<InstructionJZ>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "LAHF" => DIFactory.GenerateIInstruction<InstructionLAHF>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "LDS" => DIFactory.GenerateIInstruction<InstructionLDS>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "LEA" => DIFactory.GenerateIInstruction<InstructionLEA>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "LES" => DIFactory.GenerateIInstruction<InstructionLES>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "LODSB" => DIFactory.GenerateIInstruction<InstructionLODSB>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "LODSW" => DIFactory.GenerateIInstruction<InstructionLODSW>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "LOOP" => DIFactory.GenerateIInstruction<InstructionLOOP>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "MOV" => DIFactory.GenerateIInstruction<InstructionMOV>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "MOVSB" => DIFactory.GenerateIInstruction<InstructionMOVSB>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "MOVSW" => DIFactory.GenerateIInstruction<InstructionMOVSW>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "MUL" => DIFactory.GenerateIInstruction<InstructionMUL>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "NEG" => DIFactory.GenerateIInstruction<InstructionNEG>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "NOT" => DIFactory.GenerateIInstruction<InstructionNOT>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "OR" => DIFactory.GenerateIInstruction<InstructionOR>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "OUT" => DIFactory.GenerateIInstruction<InstructionOUT>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "POP" => DIFactory.GenerateIInstruction<InstructionPOP>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "POPF" => DIFactory.GenerateIInstruction<InstructionPOPF>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "POPFD" => DIFactory.GenerateIInstruction<InstructionPOPFD>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor), // *
            "POPFQ" => DIFactory.GenerateIInstruction<InstructionPOPFQ>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor), // *
            "PUSH" => DIFactory.GenerateIInstruction<InstructionPUSH>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "PUSHF" => DIFactory.GenerateIInstruction<InstructionPUSHF>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "PUSHFD" => DIFactory.GenerateIInstruction<InstructionPUSHFD>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor), // *
            "PUSHFQ" => DIFactory.GenerateIInstruction<InstructionPUSHFQ>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor), // *
            "RCL" => DIFactory.GenerateIInstruction<InstructionRCL>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "RCR" => DIFactory.GenerateIInstruction<InstructionRCR>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "RET" => DIFactory.GenerateIInstruction<InstructionRET>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "ROL" => DIFactory.GenerateIInstruction<InstructionROL>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "ROR" => DIFactory.GenerateIInstruction<InstructionROR>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "SAHF" => DIFactory.GenerateIInstruction<InstructionSAHF>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "SAL" => DIFactory.GenerateIInstruction<InstructionSAL>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "SAR" => DIFactory.GenerateIInstruction<InstructionSAR>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "SBB" => DIFactory.GenerateIInstruction<InstructionSBB>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "SCASB" => DIFactory.GenerateIInstruction<InstructionSCASB>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "SCASW" => DIFactory.GenerateIInstruction<InstructionSCASW>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "SHL" => DIFactory.GenerateIInstruction<InstructionSHL>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "SHR" => DIFactory.GenerateIInstruction<InstructionSHR>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "STC" => DIFactory.GenerateIInstruction<InstructionSTC>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "STD" => DIFactory.GenerateIInstruction<InstructionSTD>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "STI" => DIFactory.GenerateIInstruction<InstructionSTI>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "STOSB" => DIFactory.GenerateIInstruction<InstructionSTOSB>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "STOSW" => DIFactory.GenerateIInstruction<InstructionSTOSW>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "SUB" => DIFactory.GenerateIInstruction<InstructionSUB>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "TEST" => DIFactory.GenerateIInstruction<InstructionTEST>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "XCHG" => DIFactory.GenerateIInstruction<InstructionXCHG>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "XLAT" => DIFactory.GenerateIInstruction<InstructionXLAT>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            "XOR" => DIFactory.GenerateIInstruction<InstructionXOR>(instructionVariant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor),
            _ => throw new Exception($"Unknown opcode \"{SourceCodeLine.SourceCode}\" : {SourceCodeLine.Line}"),
        };

        return instruction;
    }

    public ILabel ToILabel()
    {
        return DIFactory.GenerateILabel(Opcode.TrimEnd(':'), SourceCodeLine.Line);
    }

    public bool AreInstructionOperandsValid()
    {
        IOperand firstOperand;
        IOperand secondOperand;
        IOperand thirdOperand;

        if (Opcode.Last() == ':')
        {
            return false;
        }

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

    public bool IsLabelValid()
    {
        if (string.IsNullOrEmpty(Opcode))
        {
            return false;
        }

        if (Opcode.Length == 1)
        {
            return false;
        }

        if (!string.IsNullOrEmpty(FirstOperand) || !string.IsNullOrEmpty(SecondOperand) || !string.IsNullOrEmpty(ThirdOperand))
        {
            return false;
        }

        return Opcode.Last() == ':' && !string.IsNullOrEmpty(Opcode) && !Opcode.Any(selectedChar => Char.IsAscii(selectedChar) == false && Char.IsAsciiDigit(selectedChar) == false);
    }

    private InstructionVariant GetInstructionVariant()
    {
        if (string.IsNullOrEmpty(FirstOperand) && string.IsNullOrEmpty(SecondOperand) && string.IsNullOrEmpty(ThirdOperand))
        {
            return InstructionVariant.NoOperands();
        }

        if (!string.IsNullOrEmpty(FirstOperand) && string.IsNullOrEmpty(SecondOperand) && string.IsNullOrEmpty(ThirdOperand))
        {
            switch (GetTypeOfOperand(FirstOperand))
            {
                case OperandVariant.Value: return InstructionVariant.OneOperandValue();
                case OperandVariant.Memory: return InstructionVariant.OneOperandMemory();
                case OperandVariant.Register: return InstructionVariant.OneOperandRegister();
                case OperandVariant.NaN: return InstructionVariant.NaN();
            }
            ;
        }

        if (!string.IsNullOrEmpty(FirstOperand) && !string.IsNullOrEmpty(SecondOperand) && string.IsNullOrEmpty(ThirdOperand))
        {
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

        if (!string.IsNullOrEmpty(FirstOperand) && !string.IsNullOrEmpty(SecondOperand) && !string.IsNullOrEmpty(ThirdOperand))
        {
            if (GetTypeOfOperand(FirstOperand) == OperandVariant.Register || GetTypeOfOperand(ThirdOperand) == OperandVariant.Value)
            {
                if (GetTypeOfOperand(SecondOperand) == OperandVariant.Register)
                {
                    return InstructionVariant.ThreeOperandsRegisterRegisterValue();
                }
                else if (GetTypeOfOperand(SecondOperand) == OperandVariant.Memory)
                {
                    return InstructionVariant.ThreeOperandsRegisterMemoryValue();
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
        if (operand.Split(' ').Count() == 2)
        {
            List<string> acceptableSizeOperands = ["byte", "word", "dword", "qword"];
            
            if (!acceptableSizeOperands.Contains(operand))
            {
                return false;
            }

            operand = operand.Split(' ').Last();
        }

        return (operand.EndsWith('B')
            && !operand[..^1].Where(selectedChar => selectedChar != '0' && selectedChar != '1').Any())
            || (operand.EndsWith('H')
            && !operand[..^1].Where(selectedChar => int.TryParse(selectedChar.ToString(), out _) == false && selectedChar < 65 && selectedChar > 70).Any())
            || (operand.StartsWith("0B")
            && !operand[2..].Where(selectedChar => selectedChar != '0' && selectedChar != '1').Any())
            || (operand.StartsWith("0X")
            && !operand[2..].Where(selectedChar => int.TryParse(selectedChar.ToString(), out _) == false && selectedChar < 65 && selectedChar > 70).Any())
            || (operand.Length == 3 && operand[0] == '\'' && operand[2] == '\''
            || int.TryParse(operand, out _));
    }

    private bool IsMemoryType(string operand)
    {
        string[] memorySizeOperands = [nameof(Size.Byte).ToUpper(), nameof(Size.Word).ToUpper(), nameof(Size.Dword).ToUpper(), nameof(Size.Qword).ToUpper()];
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
        IVirtualCPU virtualCPU = DIFactory.GenerateIVirtualCPU();

        foreach (IVirtualRegister virtualRegister in virtualCPU.Registers)
        {
            if (virtualRegister.RegisterNamesAndSizes.ContainsKey(operand))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsLabelType(string operand)
    {
        if (string.IsNullOrEmpty(operand))
        {
            return false;
        }

        if (operand.Any(selectedCharacter => !char.IsAscii(selectedCharacter)))
        {
            return false;
        }

        return true;
    }

    private IVirtualRegister GetRegister(string operand)
    {
        IVirtualCPU virtualCPU = DIFactory.GenerateIVirtualCPU();

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

    private IPrefix ParsePrefix(string prefixToParse)
    {
        return prefixToParse.ToUpper() switch
        {
            "REP" => DIFactory.GenerateIPrefix<PrefixREP>(),
            "REPE" => DIFactory.GenerateIPrefix<PrefixREPE>(),
            "REPNE" => DIFactory.GenerateIPrefix<PrefixREPNE>(),
            "REPNZ" => DIFactory.GenerateIPrefix<PrefixREPNZ>(),
            "REPZ" => DIFactory.GenerateIPrefix<PrefixREPZ>(),
            _ => throw new Exception("Invalid prefix")
        };
    }

    private IOperand GetValueOperand(string operandToParse)
    {
        ulong immediateValue = 0;
        Size operandSize = Size.Qword;

        if (operandToParse.StartsWith("0x"))
        {
            immediateValue = Convert.ToUInt64(operandToParse[2..], 16);
        }
        else if (operandToParse.StartsWith("0b"))
        {
            immediateValue = Convert.ToUInt64(operandToParse[2..], 2);
        }
        else
        {
            immediateValue = operandToParse.ToUpper().Last() switch
            {
                'H' => Convert.ToUInt64(operandToParse[..^1], 16),
                'B' => Convert.ToUInt64(operandToParse[..^1], 2),
                '\'' => (byte)operandSize,
                _ => ulong.Parse(operandToParse)
            };
        }

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
            operandSize = Size.Dword;
        }

        return new Operand(operandToParse, OperandVariant.Value, operandSize, []);
    }

    private IOperand GetMemoryOperand(string operandToParse)
    {
        string modifiedOperandToParse = string.Empty;
        string operandSize = string.Empty;
        string[] memoryPointerOperands = [];
        int memoryPointerOperandsIndex = 0;
        string memoryLabel = string.Empty;
        List<IVirtualRegister> registers = [];
        List<IMemoryOffset> offsets = [];
        MemoryOffsetOperand memoryOffsetOperand;
        MemoryOffsetType memoryOffsetType;
        Dictionary<string, Size> memoryPointerSizes = new()
        {
            { nameof(Size.Byte).ToUpper(), Size.Byte },
            { nameof(Size.Word).ToUpper(), Size.Word },
            { nameof(Size.Dword).ToUpper(), Size.Dword },
            { nameof(Size.Qword).ToUpper(), Size.Qword }
        };

        operandSize = operandToParse.Split(' ')[0].ToUpper();
        modifiedOperandToParse = string.Join(' ', operandToParse.Split(' ').Skip(2));
        modifiedOperandToParse = modifiedOperandToParse.Replace(" ", string.Empty).Trim('[', ']');
        memoryPointerOperands = modifiedOperandToParse.Split('+', '-', '*');

        foreach (string memoryPointerOperand in memoryPointerOperands)
        {
            if (int.TryParse(memoryPointerOperand, out int detectedOffset) || memoryPointerOperand.StartsWith("0b") || memoryPointerOperand.StartsWith("0x"))
            {
                memoryOffsetType = MemoryOffsetType.Integer;
            }
            else if (_cpu.Registers.Any(selectedRegister => selectedRegister.RegisterNamesAndSizes.ContainsKey(memoryPointerOperand.ToUpper())))
            {
                memoryOffsetType = MemoryOffsetType.Register;
                registers.Add(GetRegister(memoryPointerOperand.ToUpper()));
            }
            else
            {
                memoryOffsetType = MemoryOffsetType.Label;
            }

            memoryOffsetOperand = modifiedOperandToParse[memoryPointerOperandsIndex != 0 ? memoryPointerOperandsIndex - 1 : 0] switch
            {
                '+' => MemoryOffsetOperand.Addition,
                '-' => MemoryOffsetOperand.Subtraction,
                '*' => MemoryOffsetOperand.Multiplication,
                _ => MemoryOffsetOperand.NaN
            };

            if (memoryOffsetOperand == MemoryOffsetOperand.Multiplication && memoryOffsetType != MemoryOffsetType.Integer)
            {
                throw new InvalidCastException("Cannot have a scale index with a non integer type");
            }

            offsets.Add(GenerateMemoryOffset(memoryOffsetType, memoryOffsetOperand, memoryPointerOperand));
            memoryPointerOperandsIndex += memoryPointerOperand.Length + 1;
        }

        return DIFactory.GenerateIOperand(operandToParse, OperandVariant.Memory, memoryPointerSizes[operandSize], offsets.ToArray());
    }

    private IOperand GetRegisterOperand(string expression)
    {
        IVirtualRegister virtualRegister = GetRegister(expression);

        return DIFactory.GenerateIOperand(expression, OperandVariant.Register, virtualRegister.RegisterNamesAndSizes[expression.ToUpper()], []);
    }

    private IMemoryOffset GenerateMemoryOffset(MemoryOffsetType type, MemoryOffsetOperand operand, string fullOperand)
    {
        return DIFactory.GenerateIMemoryOffset(type, operand, fullOperand);
    }

    public ISourceCodeLine SourceCodeLine { get; init; }
    public string Prefix { get; init; }
    public string Opcode { get; init; }
    public string FirstOperand { get; init; }
    public string SecondOperand { get; init; }
    public string ThirdOperand { get; init; }

    private readonly IVirtualCPU _cpu;
}