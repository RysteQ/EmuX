using EmuX_Unit_Tests.Tests.InternalConstants;
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
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(1, lexerResult.Instructions.Count);
        Assert.AreEqual(InstructionVariant.OneOperandValue(), lexerResult.Instructions[0].Variant);
        Assert.IsInstanceOfType<InstructionAAD>(lexerResult.Instructions[0]);
        Assert.AreEqual("10h", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual(true, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_SingleInstructionWithOneBinaryOperand_OneInstructionParsedWithTheCorrectOperand()
    {
        string inputString = "aad";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(1, lexerResult.Instructions.Count);
        Assert.AreEqual(InstructionVariant.NoOperands(), lexerResult.Instructions[0].Variant);
        Assert.IsInstanceOfType<InstructionAAD>(lexerResult.Instructions[0]);
        Assert.AreEqual(true, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_SingleInstructionWithOneDirectValueOperand_OneInstructionParsedWithTheCorrectOperand()
    {
        string inputString = "aad 10";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(1, lexerResult.Instructions.Count);
        Assert.AreEqual(InstructionVariant.OneOperandValue(), lexerResult.Instructions[0].Variant);
        Assert.IsInstanceOfType<InstructionAAD>(lexerResult.Instructions[0]);
        Assert.AreEqual("10", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual(true, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_SingleInstructionWithOneCharOperand_OneInstructionParsedWithTheCorrectOperand()
    {
        string inputString = "aad 'a'";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(1, lexerResult.Instructions.Count);
        Assert.AreEqual(InstructionVariant.OneOperandValue(), lexerResult.Instructions[0].Variant);
        Assert.IsInstanceOfType<InstructionAAD>(lexerResult.Instructions[0]);
        Assert.AreEqual("'a'", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual(true, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithDirectValueAfterSomeEmptyLines_OneInstructionParsed()
    {
        string inputString = "\n\naam 'a'";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(1, lexerResult.Instructions.Count);
        Assert.AreEqual(InstructionVariant.OneOperandValue(), lexerResult.Instructions[0].Variant);
        Assert.IsInstanceOfType<InstructionAAM>(lexerResult.Instructions[0]);
        Assert.AreEqual("'a'", lexerResult.Instructions[0].FirstOperand.FullOperand);
        Assert.AreEqual(true, lexerResult.Success);
    }
}