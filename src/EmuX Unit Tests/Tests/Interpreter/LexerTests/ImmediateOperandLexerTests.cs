using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.Interfaces.Logic;
using EmuXCore.Interpreter.Interfaces.Models;
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

        Assert.AreEqual(4, tokens.Count);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual(TokenType.VALUE, tokens[1].Type);
        Assert.AreEqual(TokenType.EOL, tokens[2].Type);
        Assert.AreEqual(TokenType.EOF, tokens[3].Type);
    }

    [TestMethod]
    public void TestParseMethod_SingleInstructionWithOneLeadingHexOperand()
    {
        string inputString = "aad 0x10";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(4, tokens.Count);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual(TokenType.VALUE, tokens[1].Type);
        Assert.AreEqual(TokenType.EOL, tokens[2].Type);
        Assert.AreEqual(TokenType.EOF, tokens[3].Type);
    }

    [TestMethod]
    public void TestParseMethod_SingleInstructionWithNoOperand()
    {
        string inputString = "aad";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(3, tokens.Count);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual(TokenType.EOL, tokens[1].Type);
        Assert.AreEqual(TokenType.EOF, tokens[2].Type);
    }

    [TestMethod]
    public void TestParseMethod_SingleInstructionWithOneDirectValueOperand()
    {
        string inputString = "aad 10";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(4, tokens.Count);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual(TokenType.VALUE, tokens[1].Type);
        Assert.AreEqual(TokenType.EOL, tokens[2].Type);
        Assert.AreEqual(TokenType.EOF, tokens[3].Type);
    }

    [TestMethod]
    public void TestParseMethod_SingleInstructionWithOneCharOperand()
    {
        string inputString = "aad 'a'";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(4, tokens.Count);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual(TokenType.VALUE, tokens[1].Type);
        Assert.AreEqual(TokenType.EOL, tokens[2].Type);
        Assert.AreEqual(TokenType.EOF, tokens[3].Type);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithDirectValueAfterSomeEmptyLines()
    {
        string inputString = "\n\naam 'a'";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(4, tokens.Count);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual(TokenType.VALUE, tokens[1].Type);
        Assert.AreEqual(TokenType.EOL, tokens[2].Type);
        Assert.AreEqual(TokenType.EOF, tokens[3].Type);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterAndDirectValueOperands()
    {
        string inputString = "add rax, qword 10";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(7, tokens.Count);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual(TokenType.REGISTER, tokens[1].Type);
        Assert.AreEqual(TokenType.COMMA, tokens[2].Type);
        Assert.AreEqual(TokenType.SIZE, tokens[3].Type);
        Assert.AreEqual(TokenType.VALUE, tokens[4].Type);
        Assert.AreEqual(TokenType.EOL, tokens[5].Type);
        Assert.AreEqual(TokenType.EOF, tokens[6].Type);
    }
}