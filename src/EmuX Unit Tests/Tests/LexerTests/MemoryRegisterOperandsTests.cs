using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Interfaces;

namespace EmuX_Unit_Tests.Tests.LexerTests;

[TestClass]
public sealed class MemoryRegisterOperandsTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SignleInstructionWithMemoryRegisterAccess_OneInstructionParsed()
    {
        string inputString = "add byte ptr [rbx], al";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(1, instructions.Count);
        Assert.AreEqual(true, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithMemoryRegisterAccess_NoInstructionParsed()
    {
        string inputString = "dec byte ptr [rpl], rax";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(0, instructions.Count);
        Assert.AreEqual(false, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithLabelMemoryRegisterAccess_OneInstructionParsed()
    {
        string inputString = "add byte ptr [test_label], al";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(1, instructions.Count);
        Assert.AreEqual(true, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithLabelMemoryRegisterAccess_NoInstructionParsed()
    {
        string inputString = "dec byte ptr test_label], rax";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(0, instructions.Count);
        Assert.AreEqual(false, lexer.Success);
    }
}