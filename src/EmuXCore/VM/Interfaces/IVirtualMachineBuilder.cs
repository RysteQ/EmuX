using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Interfaces;

public interface IVirtualMachineBuilder
{
    IVirtualMachineBuilder SetCpu(IVirtualCPU virtualCPU);
    IVirtualMachineBuilder SetMemory(IVirtualMemory virtualMemory);
    IVirtualMachineBuilder AddDisk(IVirtualDisk virtualDisk);
    IVirtualMachineBuilder SetBios(IVirtualBIOS virtualBios); // TODO - Think of a better way than the programmer just putting whatever junk the want inside of this method as a paremeter because the programmer is dumb as fuck
    IVirtualMachineBuilder SetRTC(IVirtualRTC virtualRTC);
    IVirtualMachineBuilder AddVirtualDevice(IVirtualDevice virtualDevice);
    IVirtualMachine Build();
}