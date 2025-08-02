using EmuXCore.Interpreter.Interfaces;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.LexerTests;

[TestClass]
public sealed class InvalidInstructionVariantsTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SingleIncorrectInstruction_ParseError()
    {
        string inputString = "aaa rax, [rax]";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(0, lexerResult.Instructions.Count);
        Assert.AreEqual(false, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithInvalidOperands_ParseError()
    {
        string inputString = "add al, 1364";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(0, lexerResult.Instructions.Count);
        Assert.AreEqual(false, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithInvalidOperands_2_ParseError()
    {
        string inputString = "add al, cx";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(0, lexerResult.Instructions.Count);
        Assert.AreEqual(false, lexerResult.Success);
    }
}