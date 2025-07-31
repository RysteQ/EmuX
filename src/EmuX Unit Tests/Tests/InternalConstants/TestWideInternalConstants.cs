using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.Interpreter;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.VM;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.BIOS;
using EmuXCore.VM.Interfaces.Components.BIOS.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.BIOS;
using EmuXCore.VM.Internal.BIOS.Interfaces;
using EmuXCore.VM.Internal.BIOS.Internal;
using EmuXCore.VM.Internal.CPU;
using EmuXCore.VM.Internal.Device.USBDrives;
using EmuXCore.VM.Internal.Disk;
using EmuXCore.VM.Internal.GPUs;
using EmuXCore.VM.Internal.Memory;
using EmuXCore.VM.Internal.RTC;

namespace EmuX_Unit_Tests.Tests.InternalConstants;

public class TestWideInternalConstants
{
    protected IOperand GenerateOperand(string fullOperand, OperandVariant variant, Size operandSize, IMemoryOffset[] offsets) => new Operand(fullOperand, variant, operandSize, offsets);
    protected IOperandDecoder GenerateOperandDecoder() => new OperandDecoder();
    protected IFlagStateProcessor GenerateFlagStateProcessor() => new FlagStateProcessor();
    protected KeyValuePair<string, IMemoryLabel> GenerateMemoryLabel(string label, int address, int line) => new(label, new MemoryLabel(label, address, line));
    protected IMemoryOffset GenerateMemoryOffset(MemoryOffsetType type, MemoryOffsetOperand operand, string fullOperand) => new MemoryOffset(type, operand, fullOperand);

    public ILexer GenerateLexer() => new Lexer(GenerateVirtualCPU());

    protected IVirtualCPU GenerateVirtualCPU() => new VirtualCPU();
    protected IVirtualMemory GenerateVirtualMemory() => new VirtualMemory();
    protected IVirtualBIOS GenerateVirtualBIOS() => new VirtualBIOS(GenerateDiskInterruptHandler(), GenerateRTCInterruptHandler(), GenerateVideoInterruptHandler(), GenerateDeviceInterruptHandler());
    protected IVirtualDisk GenerateVirtualDisk(byte diskNumber, byte platters = 1, ushort tracks = 16, byte sectorPerTrack = 16) => new VirtualDisk(diskNumber, platters, tracks, sectorPerTrack);
    protected IVirtualRTC GenerateVirtualRTC() => new VirtualRTC();
    protected IVirtualDevice GenerateVirtualDevice<T>(ushort deviceId = 0) where T : IVirtualDevice => (T)Activator.CreateInstance(typeof(T), new object[] { deviceId, null });
    protected IVirtualMachineBuilder GenerateVirtualMachineBuilder() => new VirtualMachineBuilder();
    protected IVirtualGPU GenerateVirtualGPU() => new VirtualGPU();

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

    protected IDiskInterruptHandler GenerateDiskInterruptHandler() => new DiskInterruptHandler();
    protected IRTCInterruptHandler GenerateRTCInterruptHandler() => new RTCInterruptHandler();
    protected IVideoInterruptHandler GenerateVideoInterruptHandler() => new VideoInterruptHandler();
    protected IDeviceInterruptHandler GenerateDeviceInterruptHandler() => new DeviceInterruptHandler();
}