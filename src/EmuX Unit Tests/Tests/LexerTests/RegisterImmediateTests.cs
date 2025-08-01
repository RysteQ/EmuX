using EmuXCoreUnitTests.Tests.InternalConstants;
using EmuXCore.Interpreter.Interfaces;

namespace EmuXCoreUnitTests.Tests.LexerTests;

[TestClass]
public sealed class RegisterImmediate : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SignleInstructionWithInvalidOperands_OneInstructionParsed()
    {
        string inputString = "add al, 255";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(1, lexerResult.Instructions.Count);
        Assert.AreEqual(true, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithInvalidOperands_OneInstructionParsed_2()
    {
        string inputString = "add al, 0xff";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(1, lexerResult.Instructions.Count);
        Assert.AreEqual(true, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithInvalidOperands_OneInstructionParsed_3()
    {
        string inputString = "add al, 0b11111111";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(1, lexerResult.Instructions.Count);
        Assert.AreEqual(true, lexerResult.Success);
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
}