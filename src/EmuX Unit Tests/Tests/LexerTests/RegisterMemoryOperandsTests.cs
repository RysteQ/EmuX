using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.Interpreter.Internal.Models;

namespace EmuX_Unit_Tests.Tests.LexerTests;

[TestClass]
public sealed class RegisterMemoryOperandsTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterMemoryAccess_OneInstructionParsed()
    {
        string inputString = "dec byte ptr [test_label]";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(1, lexerResult.Instructions.Count);
        Assert.AreEqual(true, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterMemoryAccess_NoInstructionParsed()
    {
        string inputString = "dec woord ptr [test_label]";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(0, lexerResult.Instructions.Count);
        Assert.AreEqual(false, lexerResult.Success);
    }
}