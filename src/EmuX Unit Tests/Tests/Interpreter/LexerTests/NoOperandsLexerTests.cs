using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.Interfaces.Logic;
using EmuXCore.Interpreter.Interfaces.Models;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.Interpreter.LexerTests;

[TestClass]
public sealed class NoOperandsLexerTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SingleInstruction()
    {
        string inputString = "aaa";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(3, tokens.Count);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual(TokenType.EOL, tokens[1].Type);
        Assert.AreEqual(TokenType.EOF, tokens[2].Type);
    }

    [TestMethod]
    public void TestParseMethod_SingleInstructionWithAComment()
    {
        string inputString = "aaa ; test";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(3, tokens.Count);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual(TokenType.EOL, tokens[1].Type);
        Assert.AreEqual(TokenType.EOF, tokens[2].Type);
    }

    [TestMethod]
    public void TestParseMethod_TwoInstructionsWithAComment()
    {
        string inputString = "aaa ; test\n" +
            "aaa";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(5, tokens.Count);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual(TokenType.EOL, tokens[1].Type);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[2].Type);
        Assert.AreEqual(TokenType.EOL, tokens[3].Type);
        Assert.AreEqual(TokenType.EOF, tokens[4].Type);
    }

    [TestMethod]
    public void TestParseMethod_ThreeInstructionsWithCommendsNewLinesAndEmptySpaces()
    {
        string inputString = "aaa ; test\n" +
            "aaa\n" +
            "\n" +
            "\n" +
            "   ;  332321\n" +
            "aaa\n" +
            "   ";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(7, tokens.Count);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual(TokenType.EOL, tokens[1].Type);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[2].Type);
        Assert.AreEqual(TokenType.EOL, tokens[3].Type);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[4].Type);
        Assert.AreEqual(TokenType.EOL, tokens[5].Type);
        Assert.AreEqual(TokenType.EOF, tokens[6].Type);
    }

    [TestMethod]
    public void TestParseMethod_TwoDifferentInstructions()
    {
        string inputString = "aaa\n" +
            "aad";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(5, tokens.Count);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual(TokenType.EOL, tokens[1].Type);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[2].Type);
        Assert.AreEqual(TokenType.EOL, tokens[3].Type);
        Assert.AreEqual(TokenType.EOF, tokens[4].Type);
    }
}