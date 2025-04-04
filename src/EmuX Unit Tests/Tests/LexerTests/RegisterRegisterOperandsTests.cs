using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Interfaces;

namespace EmuX_Unit_Tests.Tests.LexerTests;

[TestClass]
public sealed class RegisterRegisterOperands : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterRegisterAccess_OneInstructionParsed()
    {
        string inputString = "adc rax, rax";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(1, instructions.Count);
        Assert.AreEqual(true, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterRegisterAccess_NoInstructionParsed()
    {
        string inputString = "adc rax, rrax";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(0, instructions.Count);
        Assert.AreEqual(false, lexer.Success);
    }
}