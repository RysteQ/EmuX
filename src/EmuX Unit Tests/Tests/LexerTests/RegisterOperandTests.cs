using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Interfaces;

namespace EmuX_Unit_Tests.Tests.LexerTests;

[TestClass]
public sealed class RegisterOperandTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterAccess_OneInstructionParsed()
    {
        string inputString = "dec rax";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(1, instructions.Count);
        Assert.AreEqual(true, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterAccess_NoInstructionParsed()
    {
        string inputString = "dec rrax";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(0, instructions.Count);
        Assert.AreEqual(false, lexer.Success);
    }
}