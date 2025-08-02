using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.Interpreter.Interfaces;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.LexerTests;

[TestClass]
public sealed class NoOperandsTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SingleInstruction_OneInstructionParsed()
    {
        string inputString = "aaa";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(1, lexerResult.Instructions.Count);
        Assert.AreEqual(InstructionVariant.NoOperands(), lexerResult.Instructions[0].Variant);
        Assert.IsInstanceOfType<InstructionAAA>(lexerResult.Instructions[0]);
        Assert.AreEqual(true, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_SingleInstructionWithAComment_OneInstructionParsed()
    {
        string inputString = "aaa ; test";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(1, lexerResult.Instructions.Count);
        Assert.AreEqual(InstructionVariant.NoOperands(), lexerResult.Instructions[0].Variant);
        Assert.IsInstanceOfType<InstructionAAA>(lexerResult.Instructions[0]);
        Assert.AreEqual(true, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_TwoInstructionsWithAComment_TwoInstructionsParsed()
    {
        string inputString = "aaa ; test\n" +
            "aaa";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(2, lexerResult.Instructions.Count);
        Assert.AreEqual(InstructionVariant.NoOperands(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual(InstructionVariant.NoOperands(), lexerResult.Instructions[1].Variant);
        Assert.IsInstanceOfType<InstructionAAA>(lexerResult.Instructions[0]);
        Assert.IsInstanceOfType<InstructionAAA>(lexerResult.Instructions[1]);
        Assert.AreEqual(true, lexerResult.Success);
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
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(3, lexerResult.Instructions.Count);
        Assert.AreEqual(InstructionVariant.NoOperands(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual(InstructionVariant.NoOperands(), lexerResult.Instructions[1].Variant);
        Assert.AreEqual(InstructionVariant.NoOperands(), lexerResult.Instructions[2].Variant);
        Assert.IsInstanceOfType<InstructionAAA>(lexerResult.Instructions[0]);
        Assert.IsInstanceOfType<InstructionAAA>(lexerResult.Instructions[1]);
        Assert.IsInstanceOfType<InstructionAAA>(lexerResult.Instructions[2]);
        Assert.AreEqual(true, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_TwoDifferentInstructions_TwoInstructionsParsed()
    {
        string inputString = "aaa\n" +
            "aad";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(2, lexerResult.Instructions.Count);
        Assert.AreEqual(InstructionVariant.NoOperands(), lexerResult.Instructions[0].Variant);
        Assert.AreEqual(InstructionVariant.NoOperands(), lexerResult.Instructions[1].Variant);
        Assert.IsInstanceOfType<InstructionAAA>(lexerResult.Instructions[0]);
        Assert.IsInstanceOfType<InstructionAAD>(lexerResult.Instructions[1]);
        Assert.AreEqual(true, lexerResult.Success);
    }
}