using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.LexicalAnalysis.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.Interpreter.LexerTests;

[TestClass]
public sealed class LabelLexerTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SingleLabel_Success()
    {
        string inputString = "test_label:";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(4, tokens.Count);
        Assert.AreEqual(TokenType.LABEL, tokens[0].Type);
        Assert.AreEqual(TokenType.COLON, tokens[1].Type);
        Assert.AreEqual(TokenType.EOL, tokens[2].Type);
        Assert.AreEqual(TokenType.EOF, tokens[3].Type);
    }

    [TestMethod]
    public void TestParseMethod_SingleLabelJmpInstruction_Success()
    {
        string inputString = "test_label:\njmp test_label";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(7, tokens.Count);
        Assert.AreEqual(TokenType.LABEL, tokens[0].Type);
        Assert.AreEqual(TokenType.COLON, tokens[1].Type);
        Assert.AreEqual(TokenType.EOL, tokens[2].Type);
        Assert.AreEqual(TokenType.INSTRUCTION, tokens[3].Type);
        Assert.AreEqual(TokenType.LABEL, tokens[4].Type);
        Assert.AreEqual(TokenType.EOL, tokens[5].Type);
        Assert.AreEqual(TokenType.EOF, tokens[6].Type);
    }
}