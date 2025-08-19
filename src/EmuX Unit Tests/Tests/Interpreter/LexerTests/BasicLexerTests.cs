using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.Interfaces.Logic;
using EmuXCore.Interpreter.Interfaces.Models;
using EmuXCore.Interpreter.LexicalSyntax;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.Interpreter.LexerTests;

[TestClass]
public sealed class BasicLexerTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_Empty()
    {
        string inputString = string.Empty;
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(1, tokens.Count);
        Assert.AreEqual(TokenType.EOF, tokens[0].Type);
    }

    [TestMethod]
    public void TestParseMethod_Spaces()
    {
        string inputString = "   ";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(1, tokens.Count);
        Assert.AreEqual(TokenType.EOF, tokens[0].Type);
    }

    [TestMethod]
    public void TestParseMethod_NewLines()
    {
        string inputString = "\n\n\n";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(1, tokens.Count);
        Assert.AreEqual(TokenType.EOF, tokens[0].Type);
    }

    [TestMethod]
    public void TestParseMethod_Comment()
    {
        string inputString = " ; test";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual(1, tokens.Count);
        Assert.AreEqual(TokenType.EOF, tokens[0].Type);
    }
}