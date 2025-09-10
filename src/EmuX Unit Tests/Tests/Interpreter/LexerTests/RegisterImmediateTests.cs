using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.LexicalAnalysis.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.Interpreter.LexerTests;

[TestClass]
public sealed class RegisterLexerImmediate : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SignleInstructionWithInvalidOperands_OneInstructionParsed()
    {
        string inputString = "add al, 255";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(6, tokens.Count);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual(TokenType.REGISTER, tokens[1].Type);
        Assert.AreEqual(TokenType.COMMA, tokens[2].Type);
        Assert.AreEqual(TokenType.VALUE, tokens[3].Type);
        Assert.AreEqual(TokenType.EOL, tokens[4].Type);
        Assert.AreEqual(TokenType.EOF, tokens[5].Type);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithInvalidOperands_OneInstructionParsed_2()
    {
        string inputString = "add al, 0xff";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(6, tokens.Count);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual(TokenType.REGISTER, tokens[1].Type);
        Assert.AreEqual(TokenType.COMMA, tokens[2].Type);
        Assert.AreEqual(TokenType.VALUE, tokens[3].Type);
        Assert.AreEqual(TokenType.EOL, tokens[4].Type);
        Assert.AreEqual(TokenType.EOF, tokens[5].Type);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithInvalidOperands_OneInstructionParsed_3()
    {
        string inputString = "add al, 0b11111111";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(6, tokens.Count);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual(TokenType.REGISTER, tokens[1].Type);
        Assert.AreEqual(TokenType.COMMA, tokens[2].Type);
        Assert.AreEqual(TokenType.VALUE, tokens[3].Type);
        Assert.AreEqual(TokenType.EOL, tokens[4].Type);
        Assert.AreEqual(TokenType.EOF, tokens[5].Type);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithInvalidOperands_ParseError()
    {
        string inputString = "add al, 1364";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(6, tokens.Count);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual(TokenType.REGISTER, tokens[1].Type);
        Assert.AreEqual(TokenType.COMMA, tokens[2].Type);
        Assert.AreEqual(TokenType.VALUE, tokens[3].Type);
        Assert.AreEqual(TokenType.EOL, tokens[4].Type);
        Assert.AreEqual(TokenType.EOF, tokens[5].Type);
    }
}