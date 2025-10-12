using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.VM.Internal.Device.USBDrives;

/// <summary>
/// This is an example of an IVirtualDevice for future contributions
/// </summary>
public class UsbDrive64Kb : IVirtualDevice
{
    public UsbDrive64Kb(ushort deviceId, IVirtualMachine? parentVirtualMachine = null)
    {
        DeviceId = deviceId;
        ParentVirtualMachine = parentVirtualMachine;
        Data = new byte[ushort.MaxValue];
    }

    public void Execute()
    {
        ushort memoryAddress = 0;
        byte valueToWrite = 0;

        if (ParentVirtualMachine == null)
        {
            throw new ArgumentNullException($"Property {nameof(ParentVirtualMachine)} cannot be null when executing the {nameof(Execute)} method");
        }

        memoryAddress = ParentVirtualMachine!.CPU.GetRegister<VirtualRegisterRCX>().CX;
        valueToWrite = ParentVirtualMachine!.CPU.GetRegister<VirtualRegisterRDX>().DL;

        ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedDevice, Size.Byte, [Data[memoryAddress]], [valueToWrite], memoryPointer: memoryAddress, deviceId: DeviceId);

        Data[memoryAddress] = valueToWrite;
    }

    public IVirtualMachine? ParentVirtualMachine { get; set; }
    public ushort DeviceId { get; init; }
    public DeviceStatus Status { get; set; } = DeviceStatus.NonOperational;
    public byte[] Data { get; set; }
}