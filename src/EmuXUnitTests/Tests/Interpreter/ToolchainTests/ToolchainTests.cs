using EmuXCore;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.Interpreter.LexicalAnalysis.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.Interpreter.ParserTests;

[TestClass]
public sealed class ToolchainTests : TestWideInternalConstants
{
    [TestMethod]
    public void FibonacciSequenceTest()
    {
        IParserResult parserResult;
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        IParser parser = DIFactory.GenerateIParser(virtualMachine, GenerateInstructionLookup(), GeneratePrefixLookup());
        ILexer lexer = DIFactory.GenerateILexer(GenerateVirtualCPU(), GenerateInstructionLookup(), GeneratePrefixLookup());
        IInterpreter interpreter = GenerateInterpreter();
        string toParse = """
            jmp calculate

            fib_iter:
                cmp ecx, 1
                jb base
                je base

                xor eax, eax
                mov ebx, 1

            loopa:
                add eax, ebx
                xchg eax, ebx
                dec ecx
                jnz loopa

                ret

            base:
                mov eax, ecx
                ret

            calculate:
                mov ecx, 8
                call fib_iter
        """;

        parserResult = parser.Parse(lexer.Tokenize(toParse));

        interpreter.VirtualMachine = virtualMachine;
        interpreter.Labels = parserResult.Labels;
        interpreter.Instructions = parserResult.Instructions;

        interpreter.Execute();

        Assert.AreEqual<uint>(21, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX);
    }

    [TestMethod]
    public void DrawBoxTest()
    {
        IParserResult parserResult;
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        IParser parser = DIFactory.GenerateIParser(virtualMachine, GenerateInstructionLookup(), GeneratePrefixLookup());
        ILexer lexer = DIFactory.GenerateILexer(GenerateVirtualCPU(), GenerateInstructionLookup(), GeneratePrefixLookup());
        IInterpreter interpreter = GenerateInterpreter();
        string toParse = """
            mov ah, 0x51
            xor ecx, ecx
            xor edx, edx
            xor ebx, ebx
            mov ecx, 10
            mov edx, 10
            shl ecx, 16
            shl edx, 16
            not ebx
            int 0x10
        """;

        (int X, int Y)[] coordinatesToCheck = [
            (0, 0), (1, 0), (2, 0), (3, 0), (4, 0), (5, 0), (6, 0), (7, 0), (8, 0), (9, 0), (10, 0),
            (10, 1), (10, 2), (10, 3), (10, 4), (10, 5), (10, 6), (10, 7), (10, 8), (10, 9), (10, 10),
            (0, 1), (0, 2), (0, 3), (0, 4), (0, 5), (0, 6), (0, 7), (0, 8), (0, 9), (0, 10),
            (1, 10), (2, 10), (3, 10), (4, 10), (5, 10), (6, 10), (7, 10), (8, 10), (9, 10), (10, 10),
        ];

        parserResult = parser.Parse(lexer.Tokenize(toParse));

        interpreter.VirtualMachine = virtualMachine;
        interpreter.Labels = parserResult.Labels;
        interpreter.Instructions = parserResult.Instructions;

        try
        {
            interpreter.Execute();
        }
        catch (Exception ex)
        {
            Assert.Fail($"{ex.Message} - {ex.InnerException} - {ex.StackTrace}");
        }

        foreach ((int X, int Y) coordinate in coordinatesToCheck)
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = (uint)coordinate.X;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = (uint)coordinate.Y;
            virtualMachine.CPU.GetRegister<VirtualRegisterRBX>().EBX = 0;
            virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.ReadPixel);

            Assert.AreEqual<byte>(255, (byte)((virtualMachine.CPU.GetRegister<VirtualRegisterRBX>().EBX & 0x_00_ff_00_00) >> 16));
            Assert.AreEqual<byte>(255, (byte)((virtualMachine.CPU.GetRegister<VirtualRegisterRBX>().EBX & 0x_00_00_ff_00) >> 8));
            Assert.AreEqual<byte>(255, (byte)(virtualMachine.CPU.GetRegister<VirtualRegisterRBX>().EBX & 0x_00_00_00_ff));
        }
    }
}