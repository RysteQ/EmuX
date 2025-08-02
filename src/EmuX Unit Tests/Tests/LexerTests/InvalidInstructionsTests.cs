using EmuXCore.Interpreter.Interfaces;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.LexerTests;

[TestClass]
public sealed class InvalidInstructionsTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SingleIncorrectInstruction_ParseError()
    {
        string inputString = "bbb";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(0, lexerResult.Instructions.Count);
        Assert.AreEqual(false, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_InvalidInstruction_ParseError()
    {
        string inputString = "3333";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(0, lexerResult.Instructions.Count);
        Assert.AreEqual(false, lexerResult.Success);
    }
}