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
using EmuXCore.VM.Internal.BIOS.Interfaces;
using EmuXCore.VM.Internal.BIOS.Internal;
using EmuXCore.VM.Internal.RTC;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Internal.Device;

namespace EmuX_Unit_Tests.Tests.InternalConstants;

public class TestWideInternalConstants
{
    protected IOperand GenerateOperand(string fullOperand, OperandVariant variant, Size operandSize, int[] offsets, string memoryLabel) => new Operand(fullOperand, variant, operandSize, offsets, memoryLabel);
    public IOperandDecoder GenerateOperandDecoder() => new OperandDecoder();
    protected IFlagStateProcessor GenerateFlagStateProcessor() => new FlagStateProcessor();
    
    public ILexer GenerateLexer() => new Lexer(GenerateVirtualCPU());
    
    protected IVirtualCPU GenerateVirtualCPU() => new VirtualCPU();
    protected IVirtualMemory GenerateVirtualMemory() => new VirtualMemory();
    protected IVirtualBIOS GenerateVirtualBIOS() => new VirtualBIOS(GenerateDiskInterruptHandler(), GenerateRTCInterruptHandler());
    protected IVirtualDisk GenerateVirtualDisk(byte diskNumber, byte platters = 1, ushort tracks = 16, byte sectorPerTrack = 16) => new VirtualDisk(diskNumber, platters, tracks, sectorPerTrack);
    protected IVirtualRTC GenerateVirtualRTC() => new VirtualRTC();
    protected IVirtualDevice GenerateVirtualDevice() => new VirtualDevice();
    protected IVirtualMachineBuilder GenerateVirtualMachineBuilder() => new VirtualMachineBuilder();

    protected IVirtualMachine GenerateVirtualMachine()
    {
        IVirtualMachineBuilder virtualMachineBuilder = GenerateVirtualMachineBuilder();

        virtualMachineBuilder
            .SetCpu(GenerateVirtualCPU())
            .SetMemory(GenerateVirtualMemory())
            .SetBios(GenerateVirtualBIOS())
            .SetRTC(GenerateVirtualRTC())
            .AddDisk(GenerateVirtualDisk(1))
            .AddDisk(GenerateVirtualDisk(2))
            .AddVirtualDevice(GenerateVirtualDevice());

        return virtualMachineBuilder.Build();
    }
    
    protected IDiskInterruptHandler GenerateDiskInterruptHandler() => new DiskInterruptHandler();
    protected IRTCInterruptHandler GenerateRTCInterruptHandler() => new RTCInterruptHandler();
}