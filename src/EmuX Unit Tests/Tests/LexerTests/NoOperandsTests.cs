using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions;
using EmuXCore.Instructions.Internal;
using EmuXCore.Interpreter.Interfaces;

namespace EmuX_Unit_Tests.Tests.LexerTests;

[TestClass]
public sealed class NoOperandsTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SingleInstruction_OneInstructionParsed()
    {
        string inputString = "aaa";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(1, instructions.Count);
        Assert.AreEqual(InstructionVariant.NoOperands(), instructions[0].Variant);
        Assert.IsInstanceOfType<InstructionAAA>(instructions[0]);
        Assert.AreEqual(true, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_SingleInstructionWithAComment_OneInstructionParsed()
    {
        string inputString = "aaa ; test";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(1, instructions.Count);
        Assert.AreEqual(InstructionVariant.NoOperands(), instructions[0].Variant);
        Assert.IsInstanceOfType<InstructionAAA>(instructions[0]);
        Assert.AreEqual(true, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_TwoInstructionsWithAComment_TwoInstructionsParsed()
    {
        string inputString = "aaa ; test\n" +
            "aaa";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(2, instructions.Count);
        Assert.AreEqual(InstructionVariant.NoOperands(), instructions[0].Variant);
        Assert.AreEqual(InstructionVariant.NoOperands(), instructions[1].Variant);
        Assert.IsInstanceOfType<InstructionAAA>(instructions[0]);
        Assert.IsInstanceOfType<InstructionAAA>(instructions[1]);
        Assert.AreEqual(true, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_ThreeInstructionsWithCommendsNewLinesAndEmptySpaces_ThreeInstructionsParsed()
    {
        string inputString = "aaa ; test\n" +
            "aaa\n" +
            "\n" +
            "\n" +
            "   ;  332321\n" +
            "aaa\n" +
            "   ";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(3, instructions.Count);
        Assert.AreEqual(InstructionVariant.NoOperands(), instructions[0].Variant);
        Assert.AreEqual(InstructionVariant.NoOperands(), instructions[1].Variant);
        Assert.AreEqual(InstructionVariant.NoOperands(), instructions[2].Variant);
        Assert.IsInstanceOfType<InstructionAAA>(instructions[0]);
        Assert.IsInstanceOfType<InstructionAAA>(instructions[1]);
        Assert.IsInstanceOfType<InstructionAAA>(instructions[2]);
        Assert.AreEqual(true, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_TwoDifferentInstructions_TwoInstructionsParsed()
    {
        string inputString = "aaa\n" +
            "aad";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(2, instructions.Count);
        Assert.AreEqual(InstructionVariant.NoOperands(), instructions[0].Variant);
        Assert.AreEqual(InstructionVariant.NoOperands(), instructions[1].Variant);
        Assert.IsInstanceOfType<InstructionAAA>(instructions[0]);
        Assert.IsInstanceOfType<InstructionAAD>(instructions[1]);
        Assert.AreEqual(true, lexer.Success);
    }
}