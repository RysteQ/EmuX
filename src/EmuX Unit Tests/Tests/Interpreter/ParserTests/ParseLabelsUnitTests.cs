using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.Interfaces.Logic;
using EmuXCore.Interpreter.Interfaces.Models;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.Interpreter.ParserTests;

[TestClass]
public sealed class ParseLabelsUnitTests : TestWideInternalConstants
{
    [TestMethod]
    public void ParseOneLabel()
    {
        List<IToken> tokens = [GenerateToken(TokenType.LABEL, "test_label"), GenerateToken(TokenType.COLON, ":"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(1, lexerResult.Labels.Count);
        Assert.AreEqual<int>(0, lexerResult.Instructions.Count);
        Assert.AreEqual<string>("test_label", lexerResult.Labels.First().Name);
        Assert.AreEqual<int>(1, lexerResult.Labels.First().Line);
    }

    [TestMethod]
    public void ParseMultipleLabels()
    {
        List<IToken> tokens = 
        [
            GenerateToken(TokenType.LABEL, "test_label_1"), GenerateToken(TokenType.COLON, ":"), GenerateToken(TokenType.EOL, "\n"),
            GenerateToken(TokenType.LABEL, "test_label_2"), GenerateToken(TokenType.COLON, ":"), GenerateToken(TokenType.EOL, "\n"),
            GenerateToken(TokenType.LABEL, "test_label_3"), GenerateToken(TokenType.COLON, ":"), GenerateToken(TokenType.EOL, "\n"),
            GenerateToken(TokenType.LABEL, "test_label_4"), GenerateToken(TokenType.COLON, ":"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty),
        ];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(4, lexerResult.Labels.Count);
        Assert.AreEqual<int>(0, lexerResult.Instructions.Count);
        Assert.AreEqual<string>("test_label_1", lexerResult.Labels[0].Name);
        Assert.AreEqual<int>(1, lexerResult.Labels[0].Line);
        Assert.AreEqual<string>("test_label_2", lexerResult.Labels[1].Name);
        Assert.AreEqual<int>(2, lexerResult.Labels[1].Line);
        Assert.AreEqual<string>("test_label_3", lexerResult.Labels[2].Name);
        Assert.AreEqual<int>(3, lexerResult.Labels[2].Line);
        Assert.AreEqual<string>("test_label_4", lexerResult.Labels[3].Name);
        Assert.AreEqual<int>(4, lexerResult.Labels[3].Line);
    }

    [TestMethod]
    public void FailToParseOneLabel()
    {
        List<IToken> tokens = [GenerateToken(TokenType.LABEL, "test_label"), GenerateToken(TokenType.COLON, ":"), GenerateToken(TokenType.COLON, ":"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        ILexerResult lexerResult;

        lexerResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, lexerResult.Labels.Count);
        Assert.AreEqual<int>(0, lexerResult.Instructions.Count);
        Assert.AreEqual<bool>(true, lexerResult.Errors.Any());
    }
}