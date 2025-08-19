using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.InstructionLogic.Interfaces;
using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.Interfaces.Logic;
using EmuXCore.Interpreter.Interfaces.Models;
using EmuXCore.Interpreter.Internal.Models;
using EmuXCore.VM.Interfaces.Components;
using System.Reflection;

namespace EmuXCore.Interpreter.LexicalSyntax;

public class Lexer : ILexer
{
    public Lexer(IVirtualCPU cpu, IInstructionLookup instructionLookup, IPrefixLookup prefixLookup)
    {
        _cpu = cpu;
        _instructionLookup = instructionLookup;
        _prefixLookup = prefixLookup;
    }

    public IList<IToken> Tokenize(string sourceCode)
    {
        List<ISourceCodeLine> lines = [];
        List<IToken> tokens = [];

        lines = GetLines(sourceCode);
        lines = RemoveEmptyLines(lines);
        lines = RemoveComments(lines);
        lines = NormaliseCharacter(lines, ',');
        lines = NormaliseCharacter(lines, ':');
        lines = NormaliseCharacter(lines, '[');
        lines = NormaliseCharacter(lines, ']');

        foreach (ISourceCodeLine line in lines)
        {
            tokens.AddRange(TokenizeLine(line));
        }

        tokens.Add(DIFactory.GenerateIToken(TokenType.EOF, string.Empty));

        return tokens;
    }

    private List<ISourceCodeLine> GetLines(string stringToProcess)
    {
        List<ISourceCodeLine> processedLines = [];
        string[] lines = stringToProcess.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            processedLines.Add(DIFactory.GenerateISourceCodeLine(lines[i].Trim(), i + 1));
        }

        return processedLines;
    }

    private List<ISourceCodeLine> RemoveEmptyLines(List<ISourceCodeLine> linesToProcess)
    {
        List<ISourceCodeLine> processedLines = [];

        processedLines = linesToProcess;
        processedLines = linesToProcess.Where(line => line.SourceCode.Trim().Length != 0).ToList();

        return processedLines;
    }

    private List<ISourceCodeLine> RemoveComments(List<ISourceCodeLine> linesToProcess)
    {
        List<ISourceCodeLine> processedLines = [];
        string processedLine = string.Empty;
        bool foundStartOfString = false;

        for (int i = 0; i < linesToProcess.Count; i++)
        {
            processedLine = linesToProcess[i].SourceCode;
            foundStartOfString = false;

            for (int j = linesToProcess[i].SourceCode.Length - 1; j >= 0; j--)
            {
                if (j != 0)
                {
                    foundStartOfString = (linesToProcess[i].SourceCode[j] == '\''
                                        || linesToProcess[i].SourceCode[j] == '\"')
                                        && linesToProcess[i].SourceCode[j - 1] != '\\'
                                        && foundStartOfString == false;
                }

                if (foundStartOfString)
                {
                    continue;
                }

                if (linesToProcess[i].SourceCode[j] == ';')
                {
                    processedLine = processedLine.Substring(0, j).Trim();
                    break;
                }
            }

            if (!string.IsNullOrEmpty(processedLine))
            {
                processedLines.Add(DIFactory.GenerateISourceCodeLine(processedLine, linesToProcess[i].Line));
            }
        }

        return processedLines;
    }

    private List<ISourceCodeLine> NormaliseCharacter(List<ISourceCodeLine> linesToProcess, char characterToNormalise)
    {
        List<ISourceCodeLine> processedLines = [];
        int characterIndex = 0;
        string processedSourceCodeLine = string.Empty;

        foreach (ISourceCodeLine line in linesToProcess)
        {
            characterIndex = line.SourceCode.IndexOf(characterToNormalise);

            if (characterIndex <= 0)
            {
                processedLines.Add(line);
                continue;
            }

            if (line.SourceCode[characterIndex - 1] == '\'')
            {
                processedLines.Add(line);
                continue;
            }

            if (characterIndex < line.SourceCode.Length && characterIndex != 0)
            {
                processedSourceCodeLine = string.Concat(line.SourceCode[..characterIndex], $" {characterToNormalise} ", line.SourceCode[(characterIndex + 1)..]);
            }
            else
            {
                processedSourceCodeLine = line.SourceCode;
            }

            processedLines.Add(DIFactory.GenerateISourceCodeLine(processedSourceCodeLine, line.Line));
        }

        return processedLines;
    }

    private List<IToken> TokenizeLine(ISourceCodeLine line)
    {
        string[] lineTokens = line.SourceCode.Split(' ').Where(selectedToken => !string.IsNullOrEmpty(selectedToken)).ToArray();
        List<IToken> tokens = [];
        TokenType tokenType = TokenType.NaN;

        foreach (string token in lineTokens)
        {
            tokenType = GetTokenType(token.ToUpper());

            if (tokenType == TokenType.NaN)
            {
                throw new ArgumentException($"Invalid token '{token}'");
            }

            tokens.Add(DIFactory.GenerateIToken(tokenType, token));
        }

        tokens.Add(DIFactory.GenerateIToken(TokenType.EOL, string.Empty));

        return tokens;
    }

    private TokenType GetTokenType(string token)
    {
        if (IsTokenOfPrefixType(token))
        {
            return TokenType.PREFIX;
        }

        if (IsTokenOfInstructionType(token))
        {
            return TokenType.INSTRUCTION;
        }

        if (IsTokenOfRegisterType(token))
        {
            return TokenType.REGISTER;
        }

        if (IsTokenOfValueType(token))
        {
            return TokenType.VALUE;
        }

        if (IsTokenOfPointerType(token))
        {
            return TokenType.POINTER;
        }

        if (IsTokenOfOpenBracketType(token))
        {
            return TokenType.OPEN_BRACKET;
        }

        if (IsTokenOfAdditionType(token))
        {
            return TokenType.ADDITION;
        }

        if (IsTokenOfSubtractionType(token))
        {
            return TokenType.SUBTRACTION;
        }

        if (IsTokenOfScaleType(token))
        {
            return TokenType.SCALE;
        }

        if (IsTokenOfCloseBracketType(token))
        {
            return TokenType.CLOSE_BRACKET;
        }

        if (IsTokenOfSizeType(token))
        {
            return TokenType.SIZE;
        }

        if (IsTokenOfCommaType(token))
        {
            return TokenType.COMMA;
        }

        if (IsTokenOfSemicolonType(token))
        {
            return TokenType.COLON;
        }

        if (IsTokenOfLabelType(token))
        {
            return TokenType.LABEL;
        }

        return TokenType.NaN;
    }

    private bool IsTokenOfPrefixType(string token)
    {
        return _prefixLookup.DoesPrefixExist(token);
    }

    private bool IsTokenOfInstructionType(string token)
    {
        return _instructionLookup.DoesInstructionExist(token);
    }

    private bool IsTokenOfRegisterType(string token)
    {
        return _cpu.Registers.Any(selectedRegister => selectedRegister.RegisterNamesAndSizes.ContainsKey(token.ToUpper()));
    }

    private bool IsTokenOfValueType(string token)
    {
        return (token.EndsWith('B')
            && !token[..^1].Where(selectedChar => selectedChar != '0' && selectedChar != '1').Any())
            || (token.EndsWith('H')
            && !token[..^1].Where(selectedChar => int.TryParse(selectedChar.ToString(), out _) == false && selectedChar < 65 && selectedChar > 70).Any())
            || (token.StartsWith("0B")
            && !token[2..].Where(selectedChar => selectedChar != '0' && selectedChar != '1').Any())
            || (token.StartsWith("0X")
            && !token[2..].Where(selectedChar => int.TryParse(selectedChar.ToString(), out _) == false && selectedChar < 65 && selectedChar > 70).Any())
            || (token.Length == 3 && token[0] == '\'' && token[2] == '\''
            || int.TryParse(token, out _));
    }

    private bool IsTokenOfLabelType(string token)
    {
        return token.All(selectedCharacter => (selectedCharacter <= 'Z' && selectedCharacter >= 'A') || (selectedCharacter <= 'z' && selectedCharacter >= 'a') || selectedCharacter == '_');
    }

    private bool IsTokenOfPointerType(string token)
    {
        return token.ToUpper() == "PTR";
    }

    private bool IsTokenOfOpenBracketType(string token)
    {
        return token == "[";
    }

    private bool IsTokenOfAdditionType(string token)
    {
        return token == "+";
    }

    private bool IsTokenOfSubtractionType(string token)
    {
        return token == "-";
    }

    private bool IsTokenOfScaleType(string token)
    {
        return token == "*";
    }

    private bool IsTokenOfCloseBracketType(string token)
    {
        return token == "]";
    }

    private bool IsTokenOfSizeType(string token)
    {
        string[] sizeKeywords = ["BYTE", "WORD", "DWORD", "QWORD"];

        return sizeKeywords.Contains(token);
    }

    private bool IsTokenOfCommaType(string token)
    {
        return token == ",";
    }

    private bool IsTokenOfSemicolonType(string token)
    {
        return token == ":";
    }

    private readonly IVirtualCPU _cpu;
    private readonly IInstructionLookup _instructionLookup;
    private readonly IPrefixLookup _prefixLookup;
}