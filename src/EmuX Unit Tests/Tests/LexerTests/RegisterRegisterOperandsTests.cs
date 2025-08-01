using EmuXCoreUnitTests.Tests.InternalConstants;
using EmuXCore.Interpreter.Interfaces;

namespace EmuXCoreUnitTests.Tests.LexerTests;

[TestClass]
public sealed class RegisterRegisterOperands : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterRegisterAccess_OneInstructionParsed()
    {
        string inputString = "adc rax, rax";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(1, lexerResult.Instructions.Count);
        Assert.AreEqual(true, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterRegisterAccess_NoInstructionParsed()
    {
        string inputString = "adc rax, rrax";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(0, lexerResult.Instructions.Count);
        Assert.AreEqual(false, lexerResult.Success);
    }
}