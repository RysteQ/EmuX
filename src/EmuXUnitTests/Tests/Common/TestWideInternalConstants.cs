using EmuXCore;
using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.InstructionLogic.Interfaces;
using EmuXCore.Interpreter.Encoder.Interfaces.Logic;
using EmuXCore.Interpreter.Encoder.Logic;
using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.Interpreter.LexicalAnalysis.Interfaces;
using EmuXCore.Interpreter.LexicalSyntax;
using EmuXCore.Interpreter.Models;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCore.VM;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.BIOS;
using EmuXCore.VM.Interfaces.Components.BIOS.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.BIOS;
using EmuXCore.VM.Internal.CPU;
using EmuXCore.VM.Internal.Device.USBDrives;
using EmuXCore.VM.Internal.Disk;
using EmuXCore.VM.Internal.GPUs;
using EmuXCore.VM.Internal.Memory;
using EmuXCore.VM.Internal.RTC;

namespace EmuXCoreUnitTests.Tests.Common;

public class TestWideInternalConstants
{
    protected IOperand GenerateOperand(string fullOperand, OperandVariant variant, Size operandSize, IMemoryOffset[] offsets) => DIFactory.GenerateIOperand(fullOperand, variant, operandSize, offsets);
    protected IOperandDecoder GenerateOperandDecoder() => DIFactory.GenerateIOperandDecoder();
    protected IFlagStateProcessor GenerateFlagStateProcessor() => DIFactory.GenerateIFlagStateProcessor();
    protected IInstructionEncoder GenerateIInstructionEncoder() => DIFactory.GenerateIInstructionEncoder(GenerateVirtualMachine(), GenerateOperandDecoder());
    protected KeyValuePair<string, IMemoryLabel> GenerateMemoryLabel(string label, int address, int line) => new(label, DIFactory.GenerateIMemoryLabel(label, address, line));
    protected IMemoryOffset GenerateMemoryOffset(MemoryOffsetType type, MemoryOffsetOperand operand, string fullOperand) => DIFactory.GenerateIMemoryOffset(type, operand, fullOperand);
    protected IInterpreter GenerateInterpreter() => DIFactory.GenerateIInterpreter();

    protected IToken GenerateToken(TokenType tokenType, string fullSourceCode) => DIFactory.GenerateIToken(tokenType, fullSourceCode);
    protected ILexer GenerateLexer() => DIFactory.GenerateILexer(GenerateVirtualCPU(), GenerateInstructionLookup(), GeneratePrefixLookup());
    protected IParser GenerateParser() => DIFactory.GenerateIParser(GenerateVirtualMachine(), GenerateInstructionLookup(), GeneratePrefixLookup());
    protected IInstructionLookup GenerateInstructionLookup() => DIFactory.GenerateIInstructionLookup();
    protected IPrefixLookup GeneratePrefixLookup() => DIFactory.GenerateIPrefixLookup();
    protected IInstructionEncoder GenerateInstructionEncoder() => DIFactory.GenerateIInstructionEncoder(GenerateVirtualMachine(), GenerateOperandDecoder());
    protected IInstruction GenerateInstruction<T>(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor, ulong bytes = 5) where T : IInstruction => (T)Activator.CreateInstance(typeof(T), new object[] { variant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor, bytes });

    protected IVirtualCPU GenerateVirtualCPU() => DIFactory.GenerateIVirtualCPU();
    protected IVirtualMemory GenerateVirtualMemory() => DIFactory.GenerateIVirtualMemory(65_536, 1_048_576);
    protected IVirtualBIOS GenerateVirtualBIOS() => DIFactory.GenerateIVirtualBIOS(GenerateDiskInterruptHandler(), GenerateRTCInterruptHandler(), GenerateVideoInterruptHandler(), GenerateDeviceInterruptHandler());
    protected IVirtualDisk GenerateVirtualDisk(byte diskNumber, byte platters = 1, ushort tracks = 16, byte sectorPerTrack = 16) => DIFactory.GenerateIVirtualDisk(diskNumber, platters, tracks, sectorPerTrack);
    protected IVirtualRTC GenerateVirtualRTC() => DIFactory.GenerateIVirtualRTC();
    protected IVirtualDevice GenerateVirtualDevice<T>(ushort deviceId = 0) where T : IVirtualDevice => (T)Activator.CreateInstance(typeof(T), new object[] { deviceId, null });
    protected IVirtualMachineBuilder GenerateVirtualMachineBuilder() => DIFactory.GenerateIVirtualMachineBuilder();
    protected IVirtualGPU GenerateVirtualGPU() => DIFactory.GenerateIVirtualGPU();

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
            .SetGPU(GenerateVirtualGPU())
            .AddVirtualDevice(GenerateVirtualDevice<UsbDrive64Kb>(1));

        return virtualMachineBuilder.Build();
    }

    protected IDiskInterruptHandler GenerateDiskInterruptHandler() => DIFactory.GenerateIDiskInterruptHandler();
    protected IRTCInterruptHandler GenerateRTCInterruptHandler() => DIFactory.GenerateIRTCInterruptHandler();
    protected IVideoInterruptHandler GenerateVideoInterruptHandler() => DIFactory.GenerateIVideoInterruptHandler();
    protected IDeviceInterruptHandler GenerateDeviceInterruptHandler() => DIFactory.GenerateIDeviceInterruptHandler();
}