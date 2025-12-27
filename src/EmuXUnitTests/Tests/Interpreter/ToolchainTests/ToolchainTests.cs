using EmuXCore;
using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter;
using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.Interpreter.LexicalAnalysis.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCore.VM.Interfaces;
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
}