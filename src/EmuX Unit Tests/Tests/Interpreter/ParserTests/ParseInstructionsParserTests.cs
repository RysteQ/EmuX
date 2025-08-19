using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.InstructionLogic.Prefixes;
using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.Interfaces.Logic;
using EmuXCore.Interpreter.Interfaces.Models;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.Interpreter.ParserTests;

[TestClass]
public class ParseInstructionsParserTests : TestWideInternalConstants
{
    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixNoOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CLI"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCLI), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.NoOperands(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].FirstOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixNoOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CMPSB"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMPSB), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.NoOperands(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].FirstOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixNoOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CMPSW"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMPSW), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.NoOperands(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].FirstOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixNoOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CWD"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCWD), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.NoOperands(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].FirstOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneValueOperand_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "AAD"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionAAD), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("127", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneValueOperand_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "PUSH"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionPUSH), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("127", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneValueOperand_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "INT"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINT), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("127", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneValueOperand_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "INTO"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINTO), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("127", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneRegisterOperand_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "POP"), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionPOP), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("AL", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneRegisterOperand_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "INC"), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINC), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("AL", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneRegisterOperand_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "DEC"), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDEC), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("AL", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneRegisterOperand_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "DIV"), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDIV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("AL", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryOperand_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CALL"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "40"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.LABEL, "my_label"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCALL), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Label, lexerResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryOperand_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "NEG"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionNEG), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryOperand_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "NOT"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionNOT), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryOperand_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "SCASB"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SUBTRACTION, "-"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSCASB), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Subtraction, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterValueOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "ff4500h"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Dword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("ff4500h", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterValueOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10101010b"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10101010b", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterValueOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "0xff4500"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Dword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("0xff4500", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterValueOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "0b10101010"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("0b10101010", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterRegisterOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "RCX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("RCX", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterRegisterOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "RDX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("RDX", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterRegisterOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "RBX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("RBX", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterRegisterOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "SS"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("SS", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterMemoryOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "14"), GenerateToken(TokenType.SUBTRACTION, "-"), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Subtraction, lexerResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterMemoryOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, lexerResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterMemoryOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterMemoryOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.LABEL, "my_label"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Label, lexerResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryValueOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.VALUE, "4"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryValueOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MUL"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "8"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMUL), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryValueOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "DIV"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDIV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryValueOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "OR"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "10h"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionOR), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryRegisterOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "AND"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.VALUE, "4"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionAND), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("AH", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryRegisterOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "SBB"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "8"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSBB), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("CX", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryRegisterOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "SAR"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSAR), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("AL", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryRegisterOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "SAL"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "10h"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "CL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSAL), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("CL", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoValueRegisterOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "OUT"), GenerateToken(TokenType.VALUE, "ffffh"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionOUT), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsValueRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("ffffh", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("AH", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneValueOperandWithSize_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "AAD"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionAAD), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("WORD 127", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneValueOperandWithSize_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "PUSH"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionPUSH), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("WORD 127", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneValueOperandWithSize_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "INT"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINT), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("WORD 127", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneValueOperandWithSize_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "INTO"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINTO), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("WORD 127", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneRegisterOperandWithSize_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "POP"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionPOP), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("WORD AX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneRegisterOperandWithSize_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "INC"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINC), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("WORD AX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneRegisterOperandWithSize_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "DEC"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDEC), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("WORD AX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneRegisterOperandWithSize_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "DIV"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDIV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("WORD AX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryOperandWithSize_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CALL"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "40"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.LABEL, "my_label"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCALL), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Label, lexerResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryOperandWithSize_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "NEG"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionNEG), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryOperandWithSize_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "NOT"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionNOT), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryOperandWithSize_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "SCASB"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SUBTRACTION, "-"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSCASB), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Subtraction, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterValueOperandsWithSize_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.VALUE, "ff4500h"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("QWORD ff4500h", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterValueOperandsWithSize_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.VALUE, "10101010b"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("QWORD 10101010b", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterValueOperandsWithSize_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.VALUE, "0xff4500"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("QWORD 0xff4500", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterValueOperandsWithSize_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.VALUE, "0b10101010"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("QWORD 0b10101010", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterRegisterOperandsWithSize_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.REGISTER, "ECX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("QWORD ECX", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterRegisterOperandsWithSize_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.REGISTER, "EDX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("QWORD EDX", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterRegisterOperandsWithSize_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.REGISTER, "EBX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("QWORD EBX", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterRegisterOperandsWithSize_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.REGISTER, "SS"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("QWORD SS", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterMemoryOperandsWithSize_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "14"), GenerateToken(TokenType.SUBTRACTION, "-"), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Subtraction, lexerResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterMemoryOperandsWithSize_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, lexerResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterMemoryOperandsWithSize_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterMemoryOperandsWithSize_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.LABEL, "my_label"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Label, lexerResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryValueOperandsWithSize_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.SIZE, "BYTE"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.VALUE, "4"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryValueOperandsWithSize_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MUL"), GenerateToken(TokenType.SIZE, "BYTE"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "8"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMUL), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryValueOperandsWithSize_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "DIV"), GenerateToken(TokenType.SIZE, "BYTE"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDIV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryValueOperandsWithSize_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "OR"), GenerateToken(TokenType.SIZE, "BYTE"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "10h"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionOR), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryRegisterOperandsWithSize_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "AND"), GenerateToken(TokenType.SIZE, "BYTE"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.VALUE, "4"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionAND), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("AH", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryRegisterOperandsWithSize_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "SBB"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "8"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSBB), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("CX", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryRegisterOperandsWithSize_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "SAR"), GenerateToken(TokenType.SIZE, "BYTE"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSAR), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("AL", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryRegisterOperandsWithSize_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "SAL"), GenerateToken(TokenType.SIZE, "BYTE"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "10h"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "CL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSAL), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, lexerResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("CL", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixNoOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "CLI"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCLI), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.NoOperands(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].FirstOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixNoOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "CMPSB"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMPSB), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.NoOperands(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].FirstOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixNoOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "CMPSW"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMPSW), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.NoOperands(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].FirstOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixNoOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "CWD"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCWD), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.NoOperands(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].FirstOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixOneValueOperand_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "AAD"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionAAD), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("127", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixOneValueOperand_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "PUSH"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionPUSH), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("127", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixOneValueOperand_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "INT"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINT), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("127", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixOneValueOperand_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "INTO"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINTO), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("127", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixOneRegisterOperand_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "POP"), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionPOP), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("AL", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixOneRegisterOperand_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "INC"), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINC), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("AL", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixOneRegisterOperand_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "DEC"), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDEC), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("AL", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixOneRegisterOperand_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "DIV"), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDIV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("AL", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixOneMemoryOperand_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "CALL"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "40"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.LABEL, "my_label"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCALL), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Label, lexerResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixOneMemoryOperand_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "NEG"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionNEG), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixOneMemoryOperand_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "NOT"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionNOT), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixOneMemoryOperand_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "SCASB"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SUBTRACTION, "-"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSCASB), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Subtraction, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixTwoRegisterValueOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "ff4500h"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Dword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("ff4500h", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixTwoRegisterValueOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10101010b"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10101010b", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixTwoRegisterValueOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "0xff4500"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Dword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("0xff4500", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixTwoRegisterValueOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "0b10101010"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("0b10101010", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixTwoRegisterRegisterOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "RCX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("RCX", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixTwoRegisterRegisterOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "RDX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("RDX", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixTwoRegisterRegisterOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "RBX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("RBX", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixTwoRegisterRegisterOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "SS"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("SS", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixTwoRegisterMemoryOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "14"), GenerateToken(TokenType.SUBTRACTION, "-"), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Subtraction, lexerResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixTwoRegisterMemoryOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, lexerResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixTwoRegisterMemoryOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixTwoRegisterMemoryOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.LABEL, "my_label"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Label, lexerResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixTwoMemoryValueOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.VALUE, "4"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixTwoMemoryValueOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "MUL"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "8"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMUL), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixTwoMemoryValueOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "DIV"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDIV), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixTwoMemoryValueOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "OR"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "10h"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionOR), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixTwoMemoryRegisterOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "AND"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.VALUE, "4"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionAND), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("AH", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, lexerResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixTwoMemoryRegisterOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "SBB"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "8"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSBB), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("CX", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, lexerResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixTwoMemoryRegisterOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "SAR"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSAR), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("AL", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixTwoMemoryRegisterOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "SAL"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "10h"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "CL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSAL), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("CL", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, lexerResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixTwoValueRegisterOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "OUT"), GenerateToken(TokenType.VALUE, "ffffh"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(1, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(false, lexerResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionOUT), lexerResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsValueRegister(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), lexerResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, lexerResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, lexerResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("ffffh", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, lexerResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, lexerResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("AH", lexerResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, lexerResult.Instructions[0].ThirdOperand);
    }
}