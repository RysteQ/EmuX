using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Interfaces;

namespace EmuX_Unit_Tests.Tests.LexerTests;

[TestClass]
public sealed class RegisterMemoryOperandsTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterMemoryAccess_OneInstructionParsed()
    {
        string inputString = "dec byte ptr [test_label]";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(1, instructions.Count);
        Assert.AreEqual(true, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithRegisterMemoryAccess_NoInstructionParsed()
    {
        string inputString = "dec woord ptr [test_label]";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(0, instructions.Count);
        Assert.AreEqual(false, lexer.Success);
    }
}