using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.LexicalAnalysis.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.Interpreter.LexerTests;

[TestClass]
public sealed class ImmediateOperandLexerTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SingleInstructionWithOneTrailingHexOperand()
    {
        string inputString = "aad 10h";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(4, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.VALUE, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[3].Type);
    }

    [TestMethod]
    public void TestParseMethod_SingleInstructionWithOneLeadingHexOperand()
    {
        string inputString = "aad 0x10";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(4, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.VALUE, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[3].Type);
    }

    [TestMethod]
    public void TestParseMethod_SingleInstructionWithNoOperand()
    {
        string inputString = "aad";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(3, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[2].Type);
    }

    [TestMethod]
    public void TestParseMethod_SingleInstructionWithOneDirectValueOperand()
    {
        string inputString = "aad 10";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(4, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.VALUE, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[3].Type);
    }

    [TestMethod]
    public void TestParseMethod_SingleInstructionWithOneCharOperand()
    {
        string inputString = "aad 'a'";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(4, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.VALUE, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[3].Type);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithDirectValueAfterSomeEmptyLines()
    {
        string inputString = "\n\naam 'a'";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(4, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.VALUE, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[3].Type);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterAndDirectValueOperands()
    {
        string inputString = "add rax, qword 10";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(7, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.REGISTER, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.COMMA, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.SIZE, tokens[3].Type);
        Assert.AreEqual<TokenType>(TokenType.VALUE, tokens[4].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[5].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[6].Type);
    }
}