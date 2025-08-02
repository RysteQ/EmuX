using EmuXCore.Interpreter.Interfaces;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.LexerTests;

[TestClass]
public sealed class LabelTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SingleLabel_Success()
    {
        string inputString = "test:";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(0, lexerResult.Instructions.Count);
        Assert.AreEqual(1, lexerResult.Labels.Count);
        Assert.AreEqual(true, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_SingleLabel_InvalidLabelName()
    {
        string inputString = "test quack:";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(0, lexerResult.Instructions.Count);
        Assert.AreEqual(0, lexerResult.Labels.Count);
        Assert.AreEqual(false, lexerResult.Success);
    }
}