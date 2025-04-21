using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Internal.Device;

public class VirtualDevice(IVirtualMachine? parentVirtualMachine = null) : IVirtualDevice
{
    public IVirtualMachine? ParentVirtualMachine { get; set; } = parentVirtualMachine;
}