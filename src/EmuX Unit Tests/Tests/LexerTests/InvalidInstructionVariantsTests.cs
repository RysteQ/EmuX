using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Interfaces;

namespace EmuX_Unit_Tests.Tests.LexerTests;

[TestClass]
public sealed class InvalidInstructionVariantsTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SingleIncorrectInstruction_ParseError()
    {
        string inputString = "aaa rax, [rax]";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(0, instructions.Count);
        Assert.AreEqual(false, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithInvalidOperands_ParseError()
    {
        string inputString = "add al, 1364";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(0, instructions.Count);
        Assert.AreEqual(false, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithInvalidOperands_2_ParseError()
    {
        string inputString = "add al, cx";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(0, instructions.Count);
        Assert.AreEqual(false, lexer.Success);
    }
}