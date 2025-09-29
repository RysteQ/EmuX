using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Actions;

namespace EmuXCore.VM.Actions;

public class VmAction : IVmAction
{
    public VmAction(VmActionCategory action, Size size, byte[] previousValue, byte[] newValue, string? registerName = null, int? memoryPointer = null, int? deviceId = null, byte? diskId = null)
    {
        Action = action;
        Size = size;
        RegisterName = registerName;
        MemoryPointer = memoryPointer;
        DeviceId = deviceId;
        DiskId = diskId;
        PreviousValue = previousValue;
        NewValue = newValue;
    }

    public void Undo(IVirtualMachine virtualMachineInstance)
    {
        // TODO
    }

    public void Redo(IVirtualMachine virtualMachineInstance)
    {
        // TODO
    }

    public VmActionCategory Action { get; init; }
    public Size Size { get; init; }
    public string? RegisterName { get; init; }
    public int? MemoryPointer { get; init; }
    public int? DeviceId { get; init; }
    public byte? DiskId { get; init; }
    public byte[] PreviousValue { get; init; }
    public byte[] NewValue { get; init; }
}