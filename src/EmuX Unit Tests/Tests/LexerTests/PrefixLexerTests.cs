using EmuXCoreUnitTests.Tests.InternalConstants;
using EmuXCore.Interpreter.Interfaces;

namespace EmuXCoreUnitTests.Tests.LexerTests;

[TestClass]
public sealed class PrefixLexerTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_PrefixREP_Parsed()
    {
        string inputString = "rep movsb";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(1, lexerResult.Instructions.Count);
        Assert.AreEqual(true, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_PrefixREPE_NoInstructionParsed()
    {
        string inputString = "repe movsb";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(0, lexerResult.Instructions.Count);
        Assert.AreEqual(false, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_PrefixREPNE_NoInstructionParsed()
    {
        string inputString = "repne movsb";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(0, lexerResult.Instructions.Count);
        Assert.AreEqual(false, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_PrefixREPNZ_NoInstructionParsed()
    {
        string inputString = "repnz movsb";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(0, lexerResult.Instructions.Count);
        Assert.AreEqual(false, lexerResult.Success);
    }

    [TestMethod]
    public void TestParseMethod_PrefixREPZ_NoInstructionParsed()
    {
        string inputString = "repz movsb";
        ILexer lexer = GenerateLexer();
        ILexerResult lexerResult;

        lexerResult = lexer.Parse(inputString);

        Assert.AreEqual(0, lexerResult.Instructions.Count);
        Assert.AreEqual(false, lexerResult.Success);
    }
}