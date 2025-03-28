using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU;
using EmuXCore.VM.Internal.Memory;
using EmuXCore.VM;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.Interpreter;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.Common.Interfaces;
using EmuXCore.Common.Enums;

namespace EmuX_Unit_Tests.Tests.InternalConstants;

public class TestWideInternalConstants
{
    public IOperand GenerateOperand(string fullOperand, OperandVariant variant, Size operandSize, int[] offsets, string memoryLabel) => new Operand(fullOperand, variant, operandSize, offsets, memoryLabel);
    public IOperandDecoder GenerateOperandDecoder() => new OperandDecoder();
    public ILexer GenerateLexer() => new Lexer(GenerateVirtualCPU());
    protected IVirtualCPU GenerateVirtualCPU() => new VirtualCPU();
    protected IVirtualMemory GenerateVirtualMemory() => new VirtualMemory();
    protected IVirtualMachine GenerateVirtualMachine() => new VirtualMachine(GenerateVirtualCPU(), GenerateVirtualMemory());
}