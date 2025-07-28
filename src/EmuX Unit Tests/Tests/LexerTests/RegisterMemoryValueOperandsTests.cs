using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.Interpreter.Interfaces;

namespace EmuX_Unit_Tests.Tests.LexerTests;

[TestClass]
public sealed class RegisterMemoryValueOperands : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterRegisterValueAccess_OneInstructionParsed()
    {
        string inputString = "imul ax, word ptr [cx], 8";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(1, lexerResult.Instructions.Count);
        Assert.AreEqual(true, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterRegisterValueAccess_NoInstructionParsed()
    {
        string inputString = "imul ax, byte ptr [cx], 8";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(0, lexerResult.Instructions.Count);
        Assert.AreEqual(false, lexerResult.Success);
    }
}