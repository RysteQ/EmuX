using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.LexicalAnalysis.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
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
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(1, parserResult.Labels.Count);
        Assert.AreEqual<int>(0, parserResult.Instructions.Count);
        Assert.AreEqual<string>("test_label", parserResult.Labels.First().Name);
        Assert.AreEqual<int>(1, parserResult.Labels.First().Line);
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
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(4, parserResult.Labels.Count);
        Assert.AreEqual<int>(0, parserResult.Instructions.Count);
        Assert.AreEqual<string>("test_label_1", parserResult.Labels[0].Name);
        Assert.AreEqual<int>(1, parserResult.Labels[0].Line);
        Assert.AreEqual<string>("test_label_2", parserResult.Labels[1].Name);
        Assert.AreEqual<int>(2, parserResult.Labels[1].Line);
        Assert.AreEqual<string>("test_label_3", parserResult.Labels[2].Name);
        Assert.AreEqual<int>(3, parserResult.Labels[2].Line);
        Assert.AreEqual<string>("test_label_4", parserResult.Labels[3].Name);
        Assert.AreEqual<int>(4, parserResult.Labels[3].Line);
    }

    [TestMethod]
    public void FailToParseOneLabel()
    {
        List<IToken> tokens = [GenerateToken(TokenType.LABEL, "test_label"), GenerateToken(TokenType.COLON, ":"), GenerateToken(TokenType.COLON, ":"), GenerateToken(TokenType.EOL, "\n"), GenerateToken(TokenType.EOF, string.Empty)];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(0, parserResult.Labels.Count);
        Assert.AreEqual<int>(0, parserResult.Instructions.Count);
        Assert.AreEqual<bool>(true, parserResult.Errors.Any());
    }

    [TestMethod]
    public void ParseOneLabelAndOneJumpInstruction()
    {
        List<IToken> tokens = 
        [
            GenerateToken(TokenType.LABEL, "test_label"), GenerateToken(TokenType.COLON, ":"), GenerateToken(TokenType.EOL, "\n"),
            GenerateToken(TokenType.INSTRUCTION, "jmp"), GenerateToken(TokenType.LABEL, "test_label"), GenerateToken(TokenType.EOL, "\n"),
            GenerateToken(TokenType.EOF, string.Empty)
        ];
        IParser parser = GenerateParser();
        IParserResult parserResult;

        parserResult = parser.Parse(tokens);

        Assert.AreEqual<int>(1, parserResult.Labels.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.Count);
        Assert.AreEqual<int>(1, parserResult.Instructions.First().FirstOperand!.Offsets.Length);
        Assert.AreEqual<bool>(false, parserResult.Errors.Any());
    }
}