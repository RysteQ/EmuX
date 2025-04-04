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
using EmuXCore.VM.Internal.BIOS;
using EmuXCore.VM.Internal.Disk;

namespace EmuX_Unit_Tests.Tests.InternalConstants;

public class TestWideInternalConstants
{
    public IOperand GenerateOperand(string fullOperand, OperandVariant variant, Size operandSize, int[] offsets, string memoryLabel) => new Operand(fullOperand, variant, operandSize, offsets, memoryLabel);
    public IOperandDecoder GenerateOperandDecoder() => new OperandDecoder();
    protected IFlagStateProcessor GenerateFlagStateProcessor() => new FlagStateProcessor();
    public ILexer GenerateLexer() => new Lexer(GenerateVirtualCPU());
    protected IVirtualCPU GenerateVirtualCPU() => new VirtualCPU();
    protected IVirtualMemory GenerateVirtualMemory() => new VirtualMemory();
    protected IVirtualBIOS GenerateVirtualBIOS() => new VirtualBIOS();
    protected IVirtualDisk GenerateVirtualDisk(byte diskNumber, byte platters = 1, ushort tracks = 16, byte sectorPerTrack = 16) => new VirtualDisk(diskNumber, platters, tracks, sectorPerTrack);
    protected IVirtualMachine GenerateVirtualMachine() => new VirtualMachine(GenerateVirtualCPU(), GenerateVirtualMemory(), [GenerateVirtualDisk(1)], GenerateVirtualBIOS());
}