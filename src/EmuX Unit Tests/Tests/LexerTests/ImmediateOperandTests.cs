using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions;
using EmuXCore.Instructions.Internal;
using EmuXCore.Interpreter.Interfaces;

namespace EmuX_Unit_Tests.Tests.LexerTests;

[TestClass]
public sealed class ImmediateOperandTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SingleInstructionWithOneHexOperand_OneInstructionParsedWithTheCorrectOperand()
    {
        string inputString = "aad 10h";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(1, instructions.Count);
        Assert.AreEqual(InstructionVariant.OneOperandValue(), instructions[0].Variant);
        Assert.IsInstanceOfType<InstructionAAD>(instructions[0]);
        Assert.AreEqual("10h", instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual(true, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_SingleInstructionWithOneBinaryOperand_OneInstructionParsedWithTheCorrectOperand()
    {
        string inputString = "aad";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(1, instructions.Count);
        Assert.AreEqual(InstructionVariant.NoOperands(), instructions[0].Variant);
        Assert.IsInstanceOfType<InstructionAAD>(instructions[0]);
        Assert.AreEqual(true, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_SingleInstructionWithOneDirectValueOperand_OneInstructionParsedWithTheCorrectOperand()
    {
        string inputString = "aad 10";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(1, instructions.Count);
        Assert.AreEqual(InstructionVariant.OneOperandValue(), instructions[0].Variant);
        Assert.IsInstanceOfType<InstructionAAD>(instructions[0]);
        Assert.AreEqual("10", instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual(true, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_SingleInstructionWithOneCharOperand_OneInstructionParsedWithTheCorrectOperand()
    {
        string inputString = "aad 'a'";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(1, instructions.Count);
        Assert.AreEqual(InstructionVariant.OneOperandValue(), instructions[0].Variant);
        Assert.IsInstanceOfType<InstructionAAD>(instructions[0]);
        Assert.AreEqual("'a'", instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual(true, lexer.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithDirectValueAfterSomeEmptyLines_OneInstructionParsed()
    {
        string inputString = "\n\naam 'a'";
        List<IInstruction> instructions = [];
        ILexer lexer = GenerateLexer();

        instructions = lexer.Parse(inputString);

        Assert.AreEqual(1, instructions.Count);
        Assert.AreEqual(InstructionVariant.OneOperandValue(), instructions[0].Variant);
        Assert.IsInstanceOfType<InstructionAAM>(instructions[0]);
        Assert.AreEqual("'a'", instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual(true, lexer.Success);
    }
}