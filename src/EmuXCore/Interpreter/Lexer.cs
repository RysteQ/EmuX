﻿using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.Interpreter.Internal.Models;
using EmuXCore.VM.Interfaces.Components;
using System.Reflection;

namespace EmuXCore.Interpreter;

public class Lexer : ILexer
{
    public Lexer(IVirtualCPU cpu)
    {
        _cpu = cpu;
    }

    public ILexerResult Parse(string codeToParse)
    {
        List<ISourceCodeLine> lines = [];
        List<ILexeme> lexemes = [];
        List<IBytecode> bytecode = [];

        lines = GetLines(codeToParse);
        lines = RemoveEmptyLines(lines);
        lines = RemoveComments(lines);

        _errorLog.Clear();

        lexemes = ParseSourceCode(lines);
        bytecode = ParseLexemes(lexemes);

        return NewLexerResult(bytecode.Where(selectedBytecode => selectedBytecode.Type == typeof(IInstruction)).Select(selectedBytecode => selectedBytecode.Instruction).ToList(), bytecode.Where(selectedBytecode => selectedBytecode.Type == typeof(ILabel)).Select(selectedBytecode => selectedBytecode.Label).ToList(), _errorLog);
    }

    private List<ISourceCodeLine> GetLines(string stringToProcess)
    {
        List<ISourceCodeLine> processedLines = [];
        string[] lines = stringToProcess.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            processedLines.Add(NewISourceCodeLine(lines[i].Trim(), i + 1));
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
                processedLines.Add(NewISourceCodeLine(processedLine, linesToProcess[i].Line));
            }
        }

        return processedLines;
    }

    private List<ILexeme> ParseSourceCode(List<ISourceCodeLine> lines)
    {
        List<IPrefix> prefixLookup = [];
        List<ILexeme> lexemes = [];
        string prefix;
        string opcode;
        string operandOne;
        string operandTwo;
        string operandThree;
        int selector;

        foreach (Type prefixImplementationClass in Assembly.GetExecutingAssembly().GetTypes().Where(selectedType => selectedType.GetInterfaces().Contains(typeof(IPrefix))))
        {
            prefixLookup.Add((IPrefix)Activator.CreateInstance(prefixImplementationClass));
        }

        foreach (ISourceCodeLine line in lines)
        {
            prefix = string.Empty;
            opcode = string.Empty;
            operandOne = string.Empty;
            operandTwo = string.Empty;
            operandThree = string.Empty;
            selector = 1;

            if (line.SourceCode.Split(' ').Count() >= 2)
            {
                if (prefixLookup.Any(selectedPrefix => selectedPrefix.Prefix == line.SourceCode.Split(' ').First().ToUpper()))
                {
                    selector--;
                }
            }

            for (int i = 0; i < line.SourceCode.Length; i++)
            {
                if (line.SourceCode[i] == ' ' && selector == 0)
                {
                    selector++;
                    continue;
                }

                if (line.SourceCode[i] == ' ' && selector == 1)
                {
                    selector++;
                    continue;
                }

                if (line.SourceCode[i] == ',' && selector == 2)
                {
                    selector++;
                    continue;
                }

                if (line.SourceCode[i] == ',' && selector == 3)
                {
                    selector++;
                    continue;
                }

                switch (selector)
                {
                    case 0: prefix += line.SourceCode[i]; break;
                    case 1: opcode += line.SourceCode[i]; break;
                    case 2: operandOne += line.SourceCode[i]; break;
                    case 3: operandTwo += line.SourceCode[i]; break;
                    case 4: operandThree += line.SourceCode[i]; break;
                }
            }

            lexemes.Add(NewLexeme(line, prefix, opcode, operandOne.Trim(), operandTwo.Trim(), operandThree.Trim()));
        }

        return lexemes;
    }

    private List<IBytecode> ParseLexemes(List<ILexeme> lexemes)
    {
        List<IBytecode> bytecode = [];
        IInstruction instruction;

        foreach (ILexeme lexeme in lexemes)
        {
            if (lexeme.AreInstructionOperandsValid())
            {
                try
                {
                    instruction = lexeme.ToIInstruction();

                    if (instruction.IsValid())
                    {
                        bytecode.Add(NewBytecode(instruction, null));
                    }
                    else
                    {
                        _errorLog.Add($"Invalid instruction at \"{lexeme.SourceCodeLine.SourceCode}\" : {lexeme.SourceCodeLine.Line}");
                    }
                }
                catch (Exception ex)
                {
                    _errorLog.Add(ex.Message);

                    continue;
                }
            }
            else if (lexeme.IsLabelValid())
            {
                try
                {
                    if (lexeme.IsLabelValid())
                    {
                        bytecode.Add(NewBytecode(null, lexeme.ToILabel()));
                    }
                    else
                    {
                        _errorLog.Add($"Invalid label at \"{lexeme.SourceCodeLine.SourceCode}\" : {lexeme.SourceCodeLine.Line}");
                    }
                }
                catch (Exception ex)
                {
                    _errorLog.Add(ex.Message);

                    continue;
                }
            }
            else
            {
                _errorLog.Add($"Invalid line at \"{lexeme.SourceCodeLine.SourceCode}\" : {lexeme.SourceCodeLine.Line}");
            }
        }

        return bytecode;
    }

    private ISourceCodeLine NewISourceCodeLine(string sourceCode, int line)
    {
        return new SourceCodeLine(sourceCode, line);
    }

    private ILexeme NewLexeme(ISourceCodeLine sourceCodeLine, string prefix, string opcode, string firstOperand, string secondOperand, string thirdOperand)
    {
        return new Lexeme(_cpu, sourceCodeLine, prefix, opcode, firstOperand, secondOperand, thirdOperand);
    }

    private IBytecode NewBytecode(IInstruction? instruction = null, ILabel? label = null)
    {
        return new Bytecode(instruction, label);
    }

    private ILexerResult NewLexerResult(IList<IInstruction> instructions, IList<ILabel> labels, IList<string> errors)
    {
        return new LexerResult(instructions, labels, errors);
    }

    private List<string> _errorLog = [];

    private readonly IVirtualCPU _cpu;
}