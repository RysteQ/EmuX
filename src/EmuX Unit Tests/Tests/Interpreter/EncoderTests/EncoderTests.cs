using EmuXCore.Interpreter.Encoder.Interfaces.Logic;
using EmuXCore.Interpreter.LexicalAnalysis.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.Interpreter.EncoderTests;

[TestClass]
public class EncoderTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestEncoder_TestNumber_1_Success()
    {
        IInstructionEncoder instructionEncoder = GeenerateInstructionEncoder();
        ILexer lexer = GenerateLexer();
        IParser parser = GenerateParser();
        IList<IToken> tokens = [];
        IParserResult parserResult;
        IInstructionEncoderResult instructionEncoderResult;

        string sourceCode = @"
                                neg eax
                                neg dword ptr [eax]
                                neg dword ptr [esp]
                                neg dword ptr [ebp]
                                neg dword ptr [eax + esp]
                                neg dword ptr [esp + ebp]
                                neg dword ptr [ebx + ebp]
                                neg dword ptr [ebp * 2]
                                neg dword ptr [ebp * 2 + 5]
                                neg dword ptr [esp + eax * 4]
                                neg dword ptr [ebx + ebx * 4]
                                neg dword ptr [eax + 5]
                                neg dword ptr [esp + 5]
                                neg dword ptr [esp + ebx + 5]
                                neg dword ptr [esp + ebx * 2 + 7]";

        byte[] expectedOutput =
        [
            0xF7, 0xD8, 0xF7, 0x18, 0xF7, 0x1C, 0x24, 0xF7,
            0x5D, 0x00, 0xF7, 0x1C, 0x04, 0xF7, 0x1C, 0x2C,
            0xF7, 0x5C, 0x1D, 0x00, 0xF7, 0x1C, 0x6D, 0x00,
            0x00, 0x00, 0x00, 0xF7, 0x1C, 0x6D, 0x05, 0x00,
            0x00, 0x00, 0xF7, 0x1C, 0x84, 0xF7, 0x1C, 0x9B,
            0xF7, 0x98, 0x05, 0x00, 0x00, 0x00, 0xF7, 0x9C,
            0x24, 0x05, 0x00, 0x00, 0x00, 0xF7, 0x9C, 0x1C,
            0x05, 0x00, 0x00, 0x00, 0xF7, 0x9C, 0x5C, 0x07,
            0x00, 0x00, 0x00
        ];

        tokens = lexer.Tokenize(sourceCode);
        parserResult = parser.Parse(tokens);
        instructionEncoderResult = instructionEncoder.Parse(parserResult.Instructions);

        Assert.IsTrue(instructionEncoderResult.Success);
        Assert.AreEqual<int>(expectedOutput.Length, instructionEncoderResult.Bytes.Length);

        for (int i = 0; i < expectedOutput.Length; i++)
        {
            Assert.AreEqual<byte>(expectedOutput[i], instructionEncoderResult.Bytes[i], $"Invalid byte at position [start + {i}]");
        }
    }
}