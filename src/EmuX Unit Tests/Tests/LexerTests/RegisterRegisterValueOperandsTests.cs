using EmuXCoreUnitTests.Tests.InternalConstants;
using EmuXCore.Interpreter.Interfaces;

namespace EmuXCoreUnitTests.Tests.LexerTests;

[TestClass]
public sealed class RegisterRegisterValueOperands : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterRegisterValueAccess_OneInstructionParsed()
    {
        string inputString = "imul ax, cx, 8";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(1, lexerResult.Instructions.Count);
        Assert.AreEqual(true, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterRegisterValueAccess_NoInstructionParsed()
    {
        string inputString = "imul ax, cl, 8";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(0, lexerResult.Instructions.Count);
        Assert.AreEqual(false, lexerResult.Success);
    }
}