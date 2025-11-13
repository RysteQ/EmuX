using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.LexicalAnalysis.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.Interpreter.LexerTests;

[TestClass]
public sealed class PrefixLexerTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_PrefixREP()
    {
        string inputString = "rep movsb";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(4, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.PREFIX, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[3].Type);
    }

    [TestMethod]
    public void TestParseMethod_PrefixREPE()
    {
        string inputString = "repe movsb";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(4, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.PREFIX, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[3].Type);
    }

    [TestMethod]
    public void TestParseMethod_PrefixREPNE()
    {
        string inputString = "repne movsb";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(4, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.PREFIX, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[3].Type);
    }

    [TestMethod]
    public void TestParseMethod_PrefixREPNZ()
    {
        string inputString = "repnz movsb";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(4, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.PREFIX, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[3].Type);
    }

    [TestMethod]
    public void TestParseMethod_PrefixREPZ()
    {
        string inputString = "repz movsb";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(4, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.PREFIX, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[3].Type);
    }
}