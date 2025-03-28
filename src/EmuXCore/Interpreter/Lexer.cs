using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.Interpreter.Internal.Models;
using EmuXCore.VM.Interfaces;

namespace EmuXCore.Interpreter;

public class Lexer(IVirtualCPU cpu) : ILexer
{
    public List<IInstruction> Parse(string codeToParse)
    {
        List<ISourceCodeLine> lines = [];
        List<ILexeme> lexemes = [];
        List<IInstruction> bytecode = [];

        lines = GetLines(codeToParse);
        lines = RemoveEmptyLines(lines);
        lines = RemoveComments(lines);

        _success = true;
        _errorLog.Clear();

        lexemes = ParseSourceCode(lines);
        bytecode = ParseLexemes(lexemes);

        return bytecode;
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
        List<ILexeme> lexemes = [];
        string opcode;
        string operandOne;
        string operandTwo;
        int selector;

        foreach (ISourceCodeLine line in lines)
        {
            opcode = string.Empty;
            operandOne = string.Empty;
            operandTwo = string.Empty;
            selector = 0;

            for (int i = 0; i < line.SourceCode.Length; i++)
            {
                if (line.SourceCode[i] == ' ' && selector == 0)
                {
                    selector++;
                    continue;
                }

                if (line.SourceCode[i] == ',' && selector == 1)
                {
                    selector++;
                    continue;
                }

                switch (selector)
                {
                    case 0: opcode += line.SourceCode[i]; break;
                    case 1: operandOne += line.SourceCode[i]; break;
                    case 2: operandTwo += line.SourceCode[i]; break;
                }
            }

            lexemes.Add(NewLexeme(line, opcode.ToUpper(), operandOne.Trim(), operandTwo.Trim()));
        }

        return lexemes;
    }

    private List<IInstruction> ParseLexemes(List<ILexeme> lexemes)
    {
        List<IInstruction> instructions = [];
        IInstruction toAdd;

        foreach (ILexeme lexeme in lexemes)
        {
            if (lexeme.AreOperandsValid())
            {
                try
                {
                    toAdd = lexeme.ToIInstruction();

                    if (toAdd.IsValid())
                    {
                        instructions.Add(toAdd);
                    }
                    else
                    {
                        _success = false;
                        _errorLog.Add($"Invalid instruction at \"{lexeme.SourceCodeLine.SourceCode}\" : {lexeme.SourceCodeLine.Line}");
                    }
                }
                catch (Exception ex)
                {
                    _success = false;
                    _errorLog.Add(ex.Message);

                    continue;
                }
            }
            else
            {
                _success = false;
                _errorLog.Add($"Invalid operands at \"{lexeme.SourceCodeLine.SourceCode}\" : {lexeme.SourceCodeLine.Line}");
            }
        }

        return instructions;
    }

    private ISourceCodeLine NewISourceCodeLine(string sourceCode, int line)
    {
        return new SourceCodeLine(sourceCode, line);
    }

    private ILexeme NewLexeme(ISourceCodeLine sourceCodeLine, string opcode, string firstOperand, string secondOperand)
    {
        return new Lexeme(_cpu, sourceCodeLine, opcode, firstOperand, secondOperand);
    }

    public bool Success { get => _success; }
    public List<string> ErrorLog { get => _errorLog; }

    private bool _success = true;
    private List<string> _errorLog = [];

    private readonly IVirtualCPU _cpu = cpu;
}