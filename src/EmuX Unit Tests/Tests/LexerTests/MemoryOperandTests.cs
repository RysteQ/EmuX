using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Interfaces;

namespace EmuX_Unit_Tests.Tests.LexerTests;

[TestClass]
public sealed class MemoryOperandTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SignleInstructionWithMemoryAccess_OneInstructionParsed()
    {
        string inputString = "dec byte ptr [rbx]";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(1, instructions.Count);
        Assert.AreEqual(true, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithMemoryAccess_NoInstructionParsed()
    {
        string inputString = "dec byte ptr [rbx], 10";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(0, instructions.Count);
        Assert.AreEqual(false, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithLabelMemoryAccess_OneInstructionParsed()
    {
        string inputString = "dec byte ptr [test_label]";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(1, instructions.Count);
        Assert.AreEqual(true, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithLabelMemoryAccess_NoInstructionParsed()
    {
        string inputString = "dec byte ptr [test_label], 10";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(0, instructions.Count);
        Assert.AreEqual(false, lexer.Success);
    }
}