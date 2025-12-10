using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.InstructionLogic.Prefixes;
using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.LexicalAnalysis.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
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
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCLI), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.NoOperands(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].FirstOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixNoOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CMPSB"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMPSB), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.NoOperands(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].FirstOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixNoOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CMPSW"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMPSW), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.NoOperands(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].FirstOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixNoOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CWD"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCWD), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.NoOperands(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].FirstOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneValueOperand_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "AAD"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionAAD), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("127", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneValueOperand_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "PUSH"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionPUSH), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("127", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneValueOperand_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "INT"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINT), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("127", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneValueOperand_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "INTO"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINTO), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("127", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneRegisterOperand_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "POP"), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionPOP), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("AL", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneRegisterOperand_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "INC"), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINC), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("AL", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneRegisterOperand_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "DEC"), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDEC), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("AL", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneRegisterOperand_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "DIV"), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDIV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("AL", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryOperand_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "INC"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "40"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.LABEL, "my_label"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINC), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Label, parserResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryOperand_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "NEG"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionNEG), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryOperand_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "NOT"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionNOT), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryOperand_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "SCASB"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SUBTRACTION, "-"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSCASB), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Subtraction, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryMemory_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "JE"), GenerateToken(TokenType.LABEL, "my_label"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionJE), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandLabel(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Label, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryMemory_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "JG"), GenerateToken(TokenType.LABEL, "my_label"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionJG), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandLabel(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Label, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryMemory_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "JC"), GenerateToken(TokenType.LABEL, "my_label"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionJC), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandLabel(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Label, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryMemory_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "JAE"), GenerateToken(TokenType.LABEL, "my_label"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionJAE), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandLabel(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Label, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterValueOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "ff4500h"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Dword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("ff4500h", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterValueOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10101010b"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10101010b", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterValueOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "0xff4500"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Dword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("0xff4500", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterValueOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "0b10101010"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("0b10101010", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterRegisterOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "RCX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("RCX", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterRegisterOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "RDX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("RDX", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterRegisterOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "RBX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("RBX", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterRegisterOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "SS"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("SS", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterMemoryOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "14"), GenerateToken(TokenType.SUBTRACTION, "-"), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Subtraction, parserResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterMemoryOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, parserResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterMemoryOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterMemoryOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.LABEL, "my_label"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Label, parserResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryValueOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.VALUE, "4"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryValueOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MUL"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "8"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMUL), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryValueOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "DIV"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDIV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryValueOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "OR"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "10h"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionOR), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryRegisterOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "AND"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.VALUE, "4"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionAND), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("AH", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryRegisterOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "SBB"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "8"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSBB), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("CX", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryRegisterOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "SAR"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSAR), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("AL", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryRegisterOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "SAL"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "10h"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "CL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSAL), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("CL", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoValueRegisterOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "OUT"), GenerateToken(TokenType.VALUE, "ffffh"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionOUT), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsValueRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("ffffh", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("AH", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneValueOperandWithSize_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "AAD"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionAAD), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("WORD 127", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneValueOperandWithSize_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "PUSH"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionPUSH), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("WORD 127", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneValueOperandWithSize_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "INT"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINT), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("WORD 127", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneValueOperandWithSize_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "INTO"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINTO), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("WORD 127", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneRegisterOperandWithSize_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "POP"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionPOP), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("WORD AX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneRegisterOperandWithSize_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "INC"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINC), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("WORD AX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneRegisterOperandWithSize_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "DEC"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDEC), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("WORD AX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneRegisterOperandWithSize_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "DIV"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDIV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("WORD AX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryOperandWithSize_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CALL"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "40"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.LABEL, "my_label"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCALL), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Label, parserResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryOperandWithSize_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "NEG"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionNEG), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryOperandWithSize_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "NOT"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionNOT), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixOneMemoryOperandWithSize_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "SCASB"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SUBTRACTION, "-"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSCASB), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Subtraction, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterValueOperandsWithSize_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.VALUE, "ff4500h"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("QWORD ff4500h", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterValueOperandsWithSize_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.VALUE, "10101010b"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("QWORD 10101010b", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterValueOperandsWithSize_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.VALUE, "0xff4500"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("QWORD 0xff4500", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterValueOperandsWithSize_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.VALUE, "0b10101010"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("QWORD 0b10101010", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterRegisterOperandsWithSize_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.REGISTER, "ECX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("QWORD ECX", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterRegisterOperandsWithSize_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.REGISTER, "EDX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("QWORD EDX", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterRegisterOperandsWithSize_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.REGISTER, "EBX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("QWORD EBX", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterRegisterOperandsWithSize_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.REGISTER, "SS"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("QWORD SS", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterMemoryOperandsWithSize_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "14"), GenerateToken(TokenType.SUBTRACTION, "-"), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Subtraction, parserResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterMemoryOperandsWithSize_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, parserResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterMemoryOperandsWithSize_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoRegisterMemoryOperandsWithSize_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.SIZE, "QWORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.LABEL, "my_label"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Label, parserResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryValueOperandsWithSize_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.SIZE, "BYTE"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.VALUE, "4"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryValueOperandsWithSize_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "MUL"), GenerateToken(TokenType.SIZE, "BYTE"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "8"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMUL), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryValueOperandsWithSize_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "DIV"), GenerateToken(TokenType.SIZE, "BYTE"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDIV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryValueOperandsWithSize_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "OR"), GenerateToken(TokenType.SIZE, "BYTE"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "10h"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionOR), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryRegisterOperandsWithSize_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "AND"), GenerateToken(TokenType.SIZE, "BYTE"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.VALUE, "4"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionAND), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("AH", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryRegisterOperandsWithSize_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "SBB"), GenerateToken(TokenType.SIZE, "WORD"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "8"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSBB), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("CX", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryRegisterOperandsWithSize_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "SAR"), GenerateToken(TokenType.SIZE, "BYTE"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSAR), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("AL", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithNoPrefixTwoMemoryRegisterOperandsWithSize_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.INSTRUCTION, "SAL"), GenerateToken(TokenType.SIZE, "BYTE"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "10h"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "CL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSAL), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<IPrefix>(null, parserResult.Instructions[0].Prefix);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("CL", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixNoOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "CLI"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCLI), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.NoOperands(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].FirstOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixNoOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "CMPSB"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMPSB), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.NoOperands(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].FirstOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixNoOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "CMPSW"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMPSW), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.NoOperands(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].FirstOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixNoOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "CWD"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCWD), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.NoOperands(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].FirstOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixOneValueOperand_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "AAD"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionAAD), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("127", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixOneValueOperand_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "PUSH"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionPUSH), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("127", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixOneValueOperand_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "INT"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINT), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("127", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixOneValueOperand_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "INTO"), GenerateToken(TokenType.VALUE, "127"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINTO), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("127", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixOneRegisterOperand_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "POP"), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionPOP), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("AL", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixOneRegisterOperand_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "INC"), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionINC), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("AL", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixOneRegisterOperand_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "DEC"), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDEC), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("AL", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixOneRegisterOperand_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "DIV"), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDIV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("AL", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixOneMemoryOperand_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "CALL"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "40"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.LABEL, "my_label"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCALL), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Label, parserResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixOneMemoryOperand_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "NEG"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionNEG), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixOneMemoryOperand_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "NOT"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionNOT), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixOneMemoryOperand_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "SCASB"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.SUBTRACTION, "-"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSCASB), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.OneOperandMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].SecondOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Subtraction, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixTwoRegisterValueOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "ff4500h"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Dword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("ff4500h", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixTwoRegisterValueOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10101010b"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10101010b", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixTwoRegisterValueOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "0xff4500"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Dword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("0xff4500", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixTwoRegisterValueOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "0b10101010"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("0b10101010", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixTwoRegisterRegisterOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "RCX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("RCX", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixTwoRegisterRegisterOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "RDX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("RDX", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixTwoRegisterRegisterOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "RBX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("RBX", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixTwoRegisterRegisterOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "SS"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("SS", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixTwoRegisterMemoryOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "14"), GenerateToken(TokenType.SUBTRACTION, "-"), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Subtraction, parserResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixTwoRegisterMemoryOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "CMP"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionCMP), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, parserResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixTwoRegisterMemoryOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "ADC"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADC), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixTwoRegisterMemoryOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "ADD"), GenerateToken(TokenType.REGISTER, "RAX"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.LABEL, "my_label"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionADD), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsRegisterMemory(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Qword, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("RAX", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].SecondOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].SecondOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Label, parserResult.Instructions[0].SecondOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixTwoMemoryValueOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "MOV"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.VALUE, "4"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMOV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixTwoMemoryValueOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "MUL"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "8"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionMUL), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixTwoMemoryValueOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "DIV"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionDIV), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixTwoMemoryValueOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "OR"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "10h"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.VALUE, "10"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionOR), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryValue(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("10", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixTwoMemoryRegisterOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "AND"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.ADDITION, "+"), GenerateToken(TokenType.VALUE, "4"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionAND), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("AH", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Addition, parserResult.Instructions[0].FirstOperand.Offsets[2].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[2].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepePrefixTwoMemoryRegisterOperands_Two_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPE"), GenerateToken(TokenType.INSTRUCTION, "SBB"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.SCALE, "*"), GenerateToken(TokenType.VALUE, "8"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "CX"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSBB), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("CX", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
        Assert.AreEqual<MemoryOffsetOperand>(MemoryOffsetOperand.Multiplication, parserResult.Instructions[0].FirstOperand.Offsets[1].Operand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[1].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnePrefixTwoMemoryRegisterOperands_Three_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNE"), GenerateToken(TokenType.INSTRUCTION, "SAR"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "AL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSAR), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNE), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("AL", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Register, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepnzPrefixTwoMemoryRegisterOperands_Four_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REPNZ"), GenerateToken(TokenType.INSTRUCTION, "SAL"), GenerateToken(TokenType.OPEN_BRACKET, "["), GenerateToken(TokenType.VALUE, "10h"), GenerateToken(TokenType.CLOSE_BRACKET, "]"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "CL"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionSAL), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsMemoryRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREPNZ), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Memory, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.NaN, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("CL", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
        Assert.AreEqual<MemoryOffsetType>(MemoryOffsetType.Integer, parserResult.Instructions[0].FirstOperand.Offsets[0].Type);
    }

    [TestMethod]
    public void ParseSingleInstructionWithRepPrefixTwoValueRegisterOperands_One_Success()
    {
        List<IToken> tokens = [GenerateToken(TokenType.PREFIX, "REP"), GenerateToken(TokenType.INSTRUCTION, "OUT"), GenerateToken(TokenType.VALUE, "ffffh"), GenerateToken(TokenType.COMMA, ","), GenerateToken(TokenType.REGISTER, "AH"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
        Assert.AreEqual<Type>(typeof(InstructionOUT), parserResult.Instructions[0].GetType());
        Assert.AreEqual<InstructionVariant>(InstructionVariant.TwoOperandsValueRegister(), parserResult.Instructions[0].Variant);
        Assert.AreEqual<Type>(typeof(PrefixREP), parserResult.Instructions[0].Prefix.GetType());
        Assert.AreEqual<OperandVariant>(OperandVariant.Value, parserResult.Instructions[0].FirstOperand.Variant);
        Assert.AreEqual<Size>(Size.Word, parserResult.Instructions[0].FirstOperand.OperandSize);
        Assert.AreEqual<string>("ffffh", parserResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual<OperandVariant>(OperandVariant.Register, parserResult.Instructions[0].SecondOperand.Variant);
        Assert.AreEqual<Size>(Size.Byte, parserResult.Instructions[0].SecondOperand.OperandSize);
        Assert.AreEqual<string>("AH", parserResult.Instructions[0].SecondOperand.FullOperand);
        Assert.AreEqual<IOperand>(null, parserResult.Instructions[0].ThirdOperand);
    }
}