using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM;

public class VirtualMachineBuilder : IVirtualMachineBuilder
{
    public IVirtualMachineBuilder SetCpu(IVirtualCPU virtualCPU)
    {
        _virtualCPU = virtualCPU;

        return this;
    }

    public IVirtualMachineBuilder SetMemory(IVirtualMemory virtualMemory)
    {
        _virtualMemory = virtualMemory;

        return this;
    }

    public IVirtualMachineBuilder AddDisk(IVirtualDisk virtualDisk)
    {
        if (_virtualDisks == null)
        {
            _virtualDisks = [];
        }

        _virtualDisks.Add(virtualDisk);

        return this;
    }

    // TODO - Think of a better way than the programmer just putting whatever junk the want inside of this method as a paremeter because the programmer is dumb as fuck
    public IVirtualMachineBuilder SetBios(IVirtualBIOS virtualBios)
    {
        _virtualBios = virtualBios;

        return this;
    }

    public IVirtualMachineBuilder SetRTC(IVirtualRTC virtualRTC)
    {
        _virtualRTC = virtualRTC;

        return this;
    }

    public IVirtualMachineBuilder AddVirtualDevice(IVirtualDevice virtualDevice)
    {
        _virtualDevices.Add(virtualDevice);

        return this;
    }

    public IVirtualMachine Build()
    {
        IVirtualMachine virtualMachine;

        if (_virtualCPU == null)
        {
            throw new ArgumentNullException("Please add a virtual CPU module before building");
        }

        if (_virtualMemory == null)
        {
            throw new ArgumentNullException("Please add a virtual memory module before building");
        }

        if (_virtualDisks == null)
        {
            throw new ArgumentNullException("Please add a single virtual disk module before building");
        }

        if (_virtualBios == null)
        {
            throw new ArgumentNullException("Please add a virtual BIOS module before building");
        }

        if (_virtualRTC == null)
        {
            throw new ArgumentNullException("Please add a virtual BIOS module before building");
        }

        virtualMachine = new VirtualMachine(_virtualCPU, _virtualMemory, [.. _virtualDisks], _virtualBios, _virtualRTC, [.. _virtualDevices]);
        virtualMachine.CPU.ParentVirtualMachine = virtualMachine;
        virtualMachine.Memory.ParentVirtualMachine = virtualMachine;
        virtualMachine.BIOS.ParentVirtualMachine = virtualMachine;
        virtualMachine.RTC.ParentVirtualMachine = virtualMachine;

        foreach (IVirtualDisk virtualDisk in virtualMachine.Disks)
        {
            virtualDisk.ParentVirtualMachine = virtualMachine;
        }

        foreach (IVirtualDevice virtualDevice in virtualMachine.Devices)
        {
            virtualDevice.ParentVirtualMachine = virtualMachine;
        }

        return virtualMachine;
    }

    private IVirtualCPU? _virtualCPU = null;
    private IVirtualMemory? _virtualMemory = null;
    private IList<IVirtualDisk>? _virtualDisks = null;
    private IList<IVirtualDevice> _virtualDevices = []; // A virtual machine might have zero devices attached, this is allowed
    private IVirtualBIOS? _virtualBios = null;
    private IVirtualRTC? _virtualRTC = null;
}