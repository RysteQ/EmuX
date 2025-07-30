using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.BIOS;

namespace EmuXCore.VM.Interfaces;

public interface IVirtualMachineBuilder
{
    IVirtualMachineBuilder SetCpu(IVirtualCPU virtualCPU);
    IVirtualMachineBuilder SetMemory(IVirtualMemory virtualMemory);
    IVirtualMachineBuilder SetGPU(IVirtualGPU virtualGPU);
    IVirtualMachineBuilder AddDisk(IVirtualDisk virtualDisk);
    IVirtualMachineBuilder SetBios(IVirtualBIOS virtualBios);
    IVirtualMachineBuilder SetRTC(IVirtualRTC virtualRTC);
    IVirtualMachineBuilder AddVirtualDevice(IVirtualDevice virtualDevice);
    IVirtualMachine Build();
}