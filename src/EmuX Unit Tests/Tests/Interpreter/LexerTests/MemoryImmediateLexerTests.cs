using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.LexicalAnalysis.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.Interpreter.LexerTests;

[TestClass]
public sealed class MemoryImmediateLexerTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestParseMethod_SignleInstructionWithMemoryAccess()
    {
        string inputString = "add byte ptr [rbx], 10";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(10, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.SIZE, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.POINTER, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.OPEN_BRACKET, tokens[3].Type);
        Assert.AreEqual<TokenType>(TokenType.REGISTER, tokens[4].Type);
        Assert.AreEqual<TokenType>(TokenType.CLOSE_BRACKET, tokens[5].Type);
        Assert.AreEqual<TokenType>(TokenType.COMMA, tokens[6].Type);
        Assert.AreEqual<TokenType>(TokenType.VALUE, tokens[7].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[8].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[9].Type);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithMemoryAccess_2()
    {
        string inputString = "add byte ptr [rbx + 10], 10";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(12, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.SIZE, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.POINTER, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.OPEN_BRACKET, tokens[3].Type);
        Assert.AreEqual<TokenType>(TokenType.REGISTER, tokens[4].Type);
        Assert.AreEqual<TokenType>(TokenType.ADDITION, tokens[5].Type);
        Assert.AreEqual<TokenType>(TokenType.VALUE, tokens[6].Type);
        Assert.AreEqual<TokenType>(TokenType.CLOSE_BRACKET, tokens[7].Type);
        Assert.AreEqual<TokenType>(TokenType.COMMA, tokens[8].Type);
        Assert.AreEqual<TokenType>(TokenType.VALUE, tokens[9].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[10].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[11].Type);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithMemoryAccess_3()
    {
        string inputString = "add byte ptr [rbx - 20], 10";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(12, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.SIZE, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.POINTER, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.OPEN_BRACKET, tokens[3].Type);
        Assert.AreEqual<TokenType>(TokenType.REGISTER, tokens[4].Type);
        Assert.AreEqual<TokenType>(TokenType.SUBTRACTION, tokens[5].Type);
        Assert.AreEqual<TokenType>(TokenType.VALUE, tokens[6].Type);
        Assert.AreEqual<TokenType>(TokenType.CLOSE_BRACKET, tokens[7].Type);
        Assert.AreEqual<TokenType>(TokenType.COMMA, tokens[8].Type);
        Assert.AreEqual<TokenType>(TokenType.VALUE, tokens[9].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[10].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[11].Type);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithMemoryAccess_4()
    {
        string inputString = "add byte ptr [10h], 10";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(10, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.SIZE, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.POINTER, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.OPEN_BRACKET, tokens[3].Type);
        Assert.AreEqual<TokenType>(TokenType.VALUE, tokens[4].Type);
        Assert.AreEqual<TokenType>(TokenType.CLOSE_BRACKET, tokens[5].Type);
        Assert.AreEqual<TokenType>(TokenType.COMMA, tokens[6].Type);
        Assert.AreEqual<TokenType>(TokenType.VALUE, tokens[7].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[8].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[9].Type);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithMemoryAccess_5()
    {
        string inputString = "add byte ptr [rbx + rcx], 10";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(12, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.SIZE, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.POINTER, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.OPEN_BRACKET, tokens[3].Type);
        Assert.AreEqual<TokenType>(TokenType.REGISTER, tokens[4].Type);
        Assert.AreEqual<TokenType>(TokenType.ADDITION, tokens[5].Type);
        Assert.AreEqual<TokenType>(TokenType.REGISTER, tokens[6].Type);
        Assert.AreEqual<TokenType>(TokenType.CLOSE_BRACKET, tokens[7].Type);
        Assert.AreEqual<TokenType>(TokenType.COMMA, tokens[8].Type);
        Assert.AreEqual<TokenType>(TokenType.VALUE, tokens[9].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[10].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[11].Type);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithMemoryAccess_6()
    {
        string inputString = "add byte ptr [rbx - rcx], 10";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(12, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.SIZE, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.POINTER, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.OPEN_BRACKET, tokens[3].Type);
        Assert.AreEqual<TokenType>(TokenType.REGISTER, tokens[4].Type);
        Assert.AreEqual<TokenType>(TokenType.SUBTRACTION, tokens[5].Type);
        Assert.AreEqual<TokenType>(TokenType.REGISTER, tokens[6].Type);
        Assert.AreEqual<TokenType>(TokenType.CLOSE_BRACKET, tokens[7].Type);
        Assert.AreEqual<TokenType>(TokenType.COMMA, tokens[8].Type);
        Assert.AreEqual<TokenType>(TokenType.VALUE, tokens[9].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[10].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[11].Type);
    }

    [TestMethod]
    public void TestParseMethod_SignleInstructionWithMemoryAccess_7()
    {
        string inputString = "add dword ptr [0x10], 10";
        ILexer lexer = GenerateLexer();
        IList<IToken> tokens;

        tokens = lexer.Tokenize(inputString);

        Assert.AreEqual<int>(10, tokens.Count);
        Assert.AreEqual<TokenType>(TokenType.INSTRUCTION, tokens[0].Type);
        Assert.AreEqual<TokenType>(TokenType.SIZE, tokens[1].Type);
        Assert.AreEqual<TokenType>(TokenType.POINTER, tokens[2].Type);
        Assert.AreEqual<TokenType>(TokenType.OPEN_BRACKET, tokens[3].Type);
        Assert.AreEqual<TokenType>(TokenType.VALUE, tokens[4].Type);
        Assert.AreEqual<TokenType>(TokenType.CLOSE_BRACKET, tokens[5].Type);
        Assert.AreEqual<TokenType>(TokenType.COMMA, tokens[6].Type);
        Assert.AreEqual<TokenType>(TokenType.VALUE, tokens[7].Type);
        Assert.AreEqual<TokenType>(TokenType.EOL, tokens[8].Type);
        Assert.AreEqual<TokenType>(TokenType.EOF, tokens[9].Type);
    }
}