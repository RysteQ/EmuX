using EmuXCoreUnitTests.Tests.InternalConstants;
using EmuXCore.Interpreter.Interfaces;

namespace EmuXCoreUnitTests.Tests.LexerTests;

[TestClass]
public sealed class RegisterOperandTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterAccess_OneInstructionParsed()
    {
        string inputString = "dec rax";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(1, lexerResult.Instructions.Count);
        Assert.AreEqual(true, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterAccess_NoInstructionParsed()
    {
        string inputString = "dec rrax";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(0, lexerResult.Instructions.Count);
        Assert.AreEqual(false, lexerResult.Success);
    }
}