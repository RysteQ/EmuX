using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.InstructionLogic.Interfaces;
using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.LexicalAnalysis.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.Interpreter.LexicalSyntax;

public class Parser : IParser
{
    public Parser(IVirtualCPU cpuToParseFor, IInstructionLookup instructionLookup, IPrefixLookup prefixLookup)
    {
        _CpuToParseFor = cpuToParseFor;
        _instructionLookup = instructionLookup;
        _prefixLookup = prefixLookup;
    }

    public IParserResult Parse(IList<IToken> tokens)
    {
        IBytecode? bytecode;
        int line = 1;

        _tokens = tokens;
        _instructions.Clear();
        _labels.Clear();
        _errors.Clear();

        do
        {
            bytecode = ParseTokensIntoBytecode(line);

            if (bytecode?.Type == typeof(IInstruction) && bytecode.Instruction != null)
            {
                _instructions.Add(bytecode.Instruction!);
            }
            else if (bytecode?.Type == typeof(ILabel) && bytecode.Label != null)
            {
                _labels.Add(bytecode.Label);
            }

            if (Accept(TokenType.EOL) || Peek().Type == TokenType.EOF)
            {
                line++;
            }
            else
            {
                _errors.Add($"Expected {TokenType.EOL}, got {Peek().Type} at line {line}");

                do
                {
                    Accept(Peek().Type);
                } while (Peek().Type != TokenType.EOL);
            }
        } while (!Accept(TokenType.EOF));

        return DIFactory.GenerateILexerResult(_instructions, _labels, _errors);
    }

    private IBytecode? ParseTokensIntoBytecode(int line)
    {
        IInstruction? instruction = null;
        ILabel? label = null;

        if (Peek().Type != TokenType.PREFIX && Peek().Type != TokenType.INSTRUCTION && Peek().Type != TokenType.LABEL)
        {
            _errors.Add($"Invalid first token for line {line}, first token must be of type {nameof(TokenType.PREFIX)} or {nameof(TokenType.INSTRUCTION)} or {nameof(TokenType.LABEL)}, but it is of type {Peek().Type}");

            return null;
        }

        if (Peek().Type == TokenType.LABEL)
        {
            label = HandleLabelTokenLine(line);
        }
        else
        {
            instruction = HandleInstructionTokenLine(line);
        }

        if (instruction == null && label == null)
        {
            return null;
        }

        return DIFactory.GenerateIBytecode(instruction, label);
    }

    private ILabel? HandleLabelTokenLine(int line)
    {
        IToken labelTokenMaybe = Peek();

        if (!Accept(TokenType.LABEL))
        {
            _errors.Add($"Expected {TokenType.LABEL}, got {Peek().Type} at line {line}");

            return null;
        }

        if (!Accept(TokenType.COLON))
        {
            _errors.Add($"Expected {TokenType.COLON}, got {Peek().Type} at line {line}");

            return null;
        }

        if (Peek().Type != TokenType.EOL)
        {
            _errors.Add($"Expected {TokenType.EOL}, got {Peek().Type} at line {line}");

            return null;
        }

        return DIFactory.GenerateILabel(labelTokenMaybe.SourceCode, line);
    }

    private IInstruction? HandleInstructionTokenLine(int line)
    {
        List<IOperand?> operands = [];
        string instructionOpcode = string.Empty;
        string instructionPrefix = string.Empty;
        Dictionary<int, InstructionVariant> variantLookup = new()
        {
            { -1, InstructionVariant.NaN() },
            { CalculateInstructionVariantId(), InstructionVariant.NoOperands() },
            { CalculateInstructionVariantId(OperandVariant.Value), InstructionVariant.OneOperandValue() },
            { CalculateInstructionVariantId(OperandVariant.Register), InstructionVariant.OneOperandRegister() },
            { CalculateInstructionVariantId(OperandVariant.Memory), InstructionVariant.OneOperandMemory() },
            { CalculateInstructionVariantId(OperandVariant.Label), InstructionVariant.OneOperandLabel() },
            { CalculateInstructionVariantId(OperandVariant.Register, OperandVariant.Value), InstructionVariant.TwoOperandsRegisterValue() },
            { CalculateInstructionVariantId(OperandVariant.Register, OperandVariant.Register), InstructionVariant.TwoOperandsRegisterRegister() },
            { CalculateInstructionVariantId(OperandVariant.Register, OperandVariant.Memory), InstructionVariant.TwoOperandsRegisterMemory() },
            { CalculateInstructionVariantId(OperandVariant.Memory, OperandVariant.Value), InstructionVariant.TwoOperandsMemoryValue() },
            { CalculateInstructionVariantId(OperandVariant.Memory, OperandVariant.Register), InstructionVariant.TwoOperandsMemoryRegister() },
            { CalculateInstructionVariantId(OperandVariant.Value, OperandVariant.Register), InstructionVariant.TwoOperandsValueRegister() },
            { CalculateInstructionVariantId(OperandVariant.Register, OperandVariant.Register, OperandVariant.Value), InstructionVariant.ThreeOperandsRegisterRegisterValue() },
            { CalculateInstructionVariantId(OperandVariant.Register, OperandVariant.Memory, OperandVariant.Value), InstructionVariant.ThreeOperandsRegisterMemoryValue() },
        };

        if (Accept(TokenType.PREFIX))
        {
            instructionPrefix = _previousToken!.SourceCode;
        }

        if (!Accept(TokenType.INSTRUCTION))
        {
            _errors.Add($"Expected token of type {TokenType.INSTRUCTION}, got {Peek().Type} instead");

            return null;
        }

        instructionOpcode = _previousToken!.SourceCode;

        while (Peek().Type != TokenType.EOL)
        {
            operands.Add(ParseOperand(line));

            if (!Accept(TokenType.COMMA) && Peek().Type != TokenType.EOL)
            {
                _errors.Add($"Expected token of type {TokenType.COMMA}, got {Peek().Type} instead");
                Accept(Peek().Type);
            }
        }

        if (operands.Count > 3)
        {
            _errors.Add($"Cannot have more than three operands at line {line}");

            return null;
        }

        operands.AddRange(Enumerable.Repeat<IOperand?>(null, 3 - operands.Count));

        if (!string.IsNullOrEmpty(instructionPrefix))
        {
            return DIFactory.GenerateIInstruction(_instructionLookup.GetInstructionType(instructionOpcode), _prefixLookup.GetPrefixType(instructionPrefix), variantLookup[CalculateInstructionVariantId(operands[0]?.Variant, operands[1]?.Variant, operands[2]?.Variant)], operands[0], operands[1], operands[2], DIFactory.GenerateIOperandDecoder(), DIFactory.GenerateIFlagStateProcessor());
        }

        return DIFactory.GenerateIInstruction(_instructionLookup.GetInstructionType(instructionOpcode), variantLookup[CalculateInstructionVariantId(operands[0]?.Variant, operands[1]?.Variant, operands[2]?.Variant)], null, operands[0], operands[1], operands[2], DIFactory.GenerateIOperandDecoder(), DIFactory.GenerateIFlagStateProcessor());
    }

    private IOperand? ParseOperand(int line)
    {
        Dictionary<string, Size> sizeLookup = new() { { "BYTE", Size.Byte }, { "WORD", Size.Word }, { "DWORD", Size.Dword }, { "QWORD", Size.Qword } };
        string fullOperand = string.Empty;
        OperandVariant variant = OperandVariant.NaN;
        Size size = Size.NaN;
        List<IMemoryOffset> offsets = [];

        if (Accept(TokenType.SIZE))
        {
            size = sizeLookup[_previousToken!.SourceCode.ToUpper()];
            fullOperand += $"{_previousToken!.SourceCode} ";
        }

        if (Accept(TokenType.VALUE))
        {
            fullOperand += _previousToken!.SourceCode;
            variant = OperandVariant.Value;
        }
        else if (Accept(TokenType.REGISTER))
        {
            fullOperand += _previousToken!.SourceCode;
            variant = OperandVariant.Register;
        }
        else if (Accept(TokenType.OPEN_BRACKET) || Accept(TokenType.POINTER))
        {
            Accept(TokenType.OPEN_BRACKET);

            (string fullOperand, List<IMemoryOffset> offsets) memoryOffsetsResult = CalculateMemoryOffsets(line);

            fullOperand += $"[{memoryOffsetsResult.fullOperand.Trim()}]";
            offsets = memoryOffsetsResult.offsets;
            variant = OperandVariant.Memory;
        }
        else if (Accept(TokenType.LABEL))
        {
            fullOperand += _previousToken!.SourceCode;
            variant = OperandVariant.Label;
        }
        else
        {
            _errors.Add($"Expected {TokenType.VALUE} or {TokenType.REGISTER} or {TokenType.OPEN_BRACKET} or {TokenType.OPEN_BRACKET}, got {Peek().Type} at line {line}");

            return null;
        }

        if (size == Size.NaN && (variant == OperandVariant.Value || variant == OperandVariant.Register))
        {
            size = CalculateOperandSize(variant, _previousToken!.SourceCode);
        }

        return DIFactory.GenerateIOperand(fullOperand, variant, size, [.. offsets]);
    }

    private (string fullOperand, List<IMemoryOffset> offsets) CalculateMemoryOffsets(int line)
    {
        (string fullOperand, List<IMemoryOffset> offsets) memoryOffsetsResult = (string.Empty, []);
        string memoryTokenSourceCode = string.Empty;
        MemoryOffsetType memoryOffsetType = MemoryOffsetType.NaN;
        MemoryOffsetOperand memoryOffsetOperand = MemoryOffsetOperand.NaN;

        Dictionary<TokenType, MemoryOffsetType> memoryOffsetTypesLookup = new()
        {
            { TokenType.LABEL, MemoryOffsetType.Label },
            { TokenType.REGISTER, MemoryOffsetType.Register },
            { TokenType.VALUE, MemoryOffsetType.Integer }
        };

        Dictionary<TokenType, MemoryOffsetOperand> memoryOffsetOperandsLookup = new()
        {
            { TokenType.ADDITION, MemoryOffsetOperand.Addition },
            { TokenType.SUBTRACTION, MemoryOffsetOperand.Subtraction },
            { TokenType.SCALE, MemoryOffsetOperand.Multiplication }
        };

        while ((Peek().Type == TokenType.VALUE
                || Peek().Type == TokenType.REGISTER
                || Peek().Type == TokenType.LABEL
                || Peek().Type == TokenType.ADDITION
                || Peek().Type == TokenType.SUBTRACTION
                || Peek().Type == TokenType.SCALE
                || Peek().Type == TokenType.CLOSE_BRACKET
            ) && !Accept(TokenType.CLOSE_BRACKET))
        {
            memoryTokenSourceCode = string.Empty;

            if (!Accept(TokenType.ADDITION, TokenType.SUBTRACTION, TokenType.SCALE) && memoryOffsetsResult.offsets.Any())
            {
                _errors.Add($"Expected {TokenType.ADDITION} or {TokenType.SUBTRACTION} or {TokenType.SCALE}, got {Peek().Type} at line {line}");

                return (string.Empty, []);
            }
            else if (_previousToken?.Type == TokenType.ADDITION || _previousToken?.Type == TokenType.SUBTRACTION || _previousToken?.Type == TokenType.SCALE)
            {
                memoryOffsetOperand = memoryOffsetOperandsLookup[_previousToken!.Type];
                memoryTokenSourceCode += _previousToken!.SourceCode;
            }

            if (!Accept(TokenType.LABEL, TokenType.REGISTER, TokenType.VALUE))
            {
                _errors.Add($"Expected {TokenType.LABEL} or {TokenType.REGISTER} or {TokenType.VALUE}, got {Peek().Type} at line {line}");

                return (string.Empty, []);
            }

            memoryOffsetType = memoryOffsetTypesLookup[_previousToken!.Type];
            memoryTokenSourceCode += string.IsNullOrEmpty(memoryTokenSourceCode) ? $"{_previousToken!.SourceCode} " : $" {_previousToken!.SourceCode} ";

            memoryOffsetsResult.fullOperand += memoryTokenSourceCode;
            memoryOffsetsResult.offsets.Add(DIFactory.GenerateIMemoryOffset(memoryOffsetType, memoryOffsetOperand, memoryTokenSourceCode));
        }

        return memoryOffsetsResult;
    }

    private Size CalculateOperandSize(OperandVariant variant, string operand)
    {
        IVirtualRegister virtualRegister;

        if (variant == OperandVariant.Value)
        {
            ulong operandValue = ConvertStringValueToUlong(operand);

            if (operandValue <= byte.MaxValue)
            {
                return Size.Byte;
            }
            else if (operandValue <= ushort.MaxValue)
            {
                return Size.Word;
            }
            else if (operandValue <= uint.MaxValue)
            {
                return Size.Dword;
            }

            return Size.Qword;
        }

        try
        {
            virtualRegister = _CpuToParseFor.GetRegister(operand);
        }
        catch (Exception ex)
        {
            _errors.Add(ex.Message);

            return Size.NaN;
        }

        return virtualRegister.RegisterNamesAndSizes[operand.ToUpper()];
    }

    private ulong ConvertStringValueToUlong(string valueToConvert)
    {
        valueToConvert = valueToConvert.Trim().ToUpper();

        if (valueToConvert.StartsWith("0X"))
        {
            return Convert.ToUInt64(valueToConvert[2..], 16);
        }
        else if (valueToConvert.EndsWith("H"))
        {
            return Convert.ToUInt64(valueToConvert[..^1], 16);
        }

        if (valueToConvert.StartsWith("0B"))
        {
            return Convert.ToUInt64(valueToConvert[2..].ToString(), 2);
        }
        else if (valueToConvert.EndsWith("B"))
        {
            return Convert.ToUInt64(valueToConvert[..^1].ToString(), 2);
        }

        return valueToConvert.Last() switch
        {
            'H' => Convert.ToUInt64(valueToConvert[..^1], 16),
            'B' => Convert.ToUInt64(valueToConvert[..^1], 2),
            '\'' => valueToConvert[1],
            _ => Convert.ToUInt64(valueToConvert, 10)
        };
    }

    private int CalculateInstructionVariantId(OperandVariant? firstOperand = null, OperandVariant? secondOperand = null, OperandVariant? thirdOperand = null)
    {
        firstOperand = firstOperand == null ? OperandVariant.NaN : firstOperand;
        secondOperand = secondOperand == null ? OperandVariant.NaN : secondOperand;
        thirdOperand = thirdOperand == null ? OperandVariant.NaN : thirdOperand;

        return (int)firstOperand * 16 + (int)secondOperand * 4 + (int)thirdOperand;
    }

    private IToken Peek()
    {
        return _tokens.First();
    }

    private bool Accept(params TokenType[] tokenType)
    {
        if (!_tokens.Any())
        {
            return false;
        }

        if (tokenType.Any(selectedTokenType => Peek().Type == selectedTokenType))
        {
            _previousToken = _tokens.FirstOrDefault();
            _tokens.RemoveAt(0);

            return true;
        }

        return false;
    }

    private bool Expect(params TokenType[] allowedTokenTypes)
    {
        return allowedTokenTypes.Any(selectedTokenType => selectedTokenType == Peek().Type);
    }

    private IToken? _previousToken;
    private IList<IToken> _tokens = [];
    private List<IInstruction> _instructions = [];
    private List<ILabel> _labels = [];
    private List<string> _errors = [];

    private readonly IInstructionLookup _instructionLookup;
    private readonly IPrefixLookup _prefixLookup;
    private readonly IVirtualCPU _CpuToParseFor;
}