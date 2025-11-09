using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Actions;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;

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
        switch (Action)
        {
            case VmActionCategory.ModifiedRegister: UndoRegisterAction(virtualMachineInstance); break;
            case VmActionCategory.ModifiedMemory: UndoMemoryAction(virtualMachineInstance); break;
            case VmActionCategory.ModifiedDisk: UndoDiskAction(virtualMachineInstance); break;
            case VmActionCategory.ModifiedDevice: UndoDeviceAction(virtualMachineInstance); break;
            case VmActionCategory.ModifiedGpu: UndoGpuAction(virtualMachineInstance); break;
            default: throw new ArgumentException("Invalid action");
        }
    }

    public void Redo(IVirtualMachine virtualMachineInstance)
    {
        switch (Action)
        {
            case VmActionCategory.ModifiedRegister: RedoRegisterAction(virtualMachineInstance); break;
            case VmActionCategory.ModifiedMemory: RedoMemoryAction(virtualMachineInstance); break;
            case VmActionCategory.ModifiedDisk: RedoDiskAction(virtualMachineInstance); break;
            case VmActionCategory.ModifiedDevice: RedoDeviceAction(virtualMachineInstance); break;
            case VmActionCategory.ModifiedGpu: RedoGpuAction(virtualMachineInstance); break;
            default: throw new ArgumentException("Invalid action");
        }
    }

    private void UndoRegisterAction(IVirtualMachine virtualMachineInstance)
    {
        IVirtualRegister? virtualRegister = null;
        ulong currentRegisterValue = 0;

        if (string.IsNullOrEmpty(RegisterName))
        {
            throw new ArgumentNullException($"Value {nameof(RegisterName)} is null or empty");
        }

        virtualRegister = virtualMachineInstance.CPU.GetRegister(RegisterName);
        currentRegisterValue = virtualRegister.Get();

        switch (Size)
        {
            case Size.Byte: virtualRegister.Set(RegisterName, (currentRegisterValue & 0x_ff_ff_ff_ff_ff_ff_ff_00) + ConvertBytesToUlong(PreviousValue)); break;
            case Size.Word: virtualRegister.Set(RegisterName, (currentRegisterValue & 0x_ff_ff_ff_ff_ff_ff_00_00) + ConvertBytesToUlong(PreviousValue)); break;
            case Size.Dword: virtualRegister.Set(RegisterName, (currentRegisterValue & 0x_ff_ff_ff_ff_00_00_00_00) + ConvertBytesToUlong(PreviousValue)); break;
            case Size.Qword: virtualRegister.Set(RegisterName, ConvertBytesToUlong(PreviousValue)); break;
            default: throw new ArgumentException("Invalid size");
        }
    }

    private void UndoMemoryAction(IVirtualMachine virtualMachineInstance)
    {
        if (MemoryPointer == null)
        {
            throw new ArgumentNullException($"Value {nameof(MemoryPointer)} is null or empty");
        }

        for (int i = 0; i < PreviousValue.Length; i++)
        {
            virtualMachineInstance.SetByte((int)(MemoryPointer + i), PreviousValue[i]);
        }
    }

    private void UndoDiskAction(IVirtualMachine virtualMachineInstance)
    {
        IVirtualDisk? virtualDisk = virtualMachineInstance.Disks.FirstOrDefault(selectedDisk => selectedDisk.DiskNumber == DiskId);

        if (MemoryPointer == null)
        {
            throw new ArgumentNullException($"Value {nameof(MemoryPointer)} is null or empty");
        }

        if (virtualDisk == null)
        {
            throw new ArgumentNullException($"Cannot find the disk with the ID of {DiskId}");
        }

        for (int i = 0; i < PreviousValue.Length; i++)
        {
            virtualDisk.WriteSector((byte)((MemoryPointer & 0x_ff_00_00_00) >> 24), (ushort)((MemoryPointer & 0x_00_ff_ff_00) >> 8), (byte)(MemoryPointer & 0x_00_00_00_ff), PreviousValue);
        }
    }

    private void UndoDeviceAction(IVirtualMachine virtualMachineInstance)
    {
        IVirtualDevice? virtualDevice = virtualMachineInstance.Devices.FirstOrDefault(selectedDevice => selectedDevice.DeviceId == DeviceId);

        if (MemoryPointer == null)
        {
            throw new ArgumentNullException($"Value {nameof(MemoryPointer)} is null or empty");
        }

        if (virtualDevice == null)
        {
            throw new ArgumentNullException($"Cannot find the device with the ID of {DeviceId}");
        }

        for (int i = 0; i < PreviousValue.Length; i++)
        {
            virtualDevice.Data[(int)(MemoryPointer + i)] = PreviousValue[i];
        }
    }

    private void UndoGpuAction(IVirtualMachine virtualMachineInstance)
    {
        IVirtualGPU virtualGPU = virtualMachineInstance.GPU;

        if (MemoryPointer == null)
        {
            throw new ArgumentNullException($"Value {nameof(MemoryPointer)} is null or empty");
        }

        for (int i = 0; i < PreviousValue.Length; i++)
        {
            virtualGPU.Data[(int)(MemoryPointer + i)] = PreviousValue[i];
        }
    }

    private void RedoRegisterAction(IVirtualMachine virtualMachineInstance)
    {
        IVirtualRegister? virtualRegister = null;
        ulong currentRegisterValue = 0;

        if (string.IsNullOrEmpty(RegisterName)) 
        {
            throw new ArgumentNullException($"Value {nameof(RegisterName)} is null or empty");
        }

        virtualRegister = virtualMachineInstance.CPU.GetRegister(RegisterName);
        currentRegisterValue = virtualRegister.Get();

        switch (Size)
        {
            case Size.Byte: virtualRegister.Set(RegisterName, (currentRegisterValue & 0x_ff_ff_ff_ff_ff_ff_ff_00) + ConvertBytesToUlong(NewValue)); break;
            case Size.Word: virtualRegister.Set(RegisterName, (currentRegisterValue & 0x_ff_ff_ff_ff_ff_ff_00_00) + ConvertBytesToUlong(NewValue)); break;
            case Size.Dword: virtualRegister.Set(RegisterName, (currentRegisterValue & 0x_ff_ff_ff_ff_00_00_00_00) + ConvertBytesToUlong(NewValue)); break;
            case Size.Qword: virtualRegister.Set(RegisterName, ConvertBytesToUlong(NewValue)); break;
            default: throw new ArgumentException("Invalid size");
        }
    }

    private void RedoMemoryAction(IVirtualMachine virtualMachineInstance)
    {
        if (MemoryPointer == null)
        {
            throw new ArgumentNullException($"Value {nameof(MemoryPointer)} is null or empty");
        }

        for (int i = 0; i < PreviousValue.Length; i++)
        {
            virtualMachineInstance.SetByte((int)(MemoryPointer + i), NewValue[i]);
        }
    }

    private void RedoDiskAction(IVirtualMachine virtualMachineInstance)
    {
        IVirtualDisk? virtualDisk = virtualMachineInstance.Disks.FirstOrDefault(selectedDisk => selectedDisk.DiskNumber == DiskId);

        if (MemoryPointer == null)
        {
            throw new ArgumentNullException($"Value {nameof(MemoryPointer)} is null or empty");
        }

        if (virtualDisk == null)
        {
            throw new ArgumentNullException($"Cannot find the disk with the ID of {DiskId}");
        }

        for (int i = 0; i < NewValue.Length; i++)
        {
            virtualDisk.WriteSector((byte)((MemoryPointer & 0x_ff_00_00_00) >> 24), (ushort)((MemoryPointer & 0x_00_ff_ff_00) >> 8), (byte)(MemoryPointer & 0x_00_00_00_ff), NewValue);
        }
    }

    private void RedoDeviceAction(IVirtualMachine virtualMachineInstance)
    {
        IVirtualDevice? virtualDevice = virtualMachineInstance.Devices.FirstOrDefault(selectedDevice => selectedDevice.DeviceId == DeviceId);

        if (MemoryPointer == null)
        {
            throw new ArgumentNullException($"Value {nameof(MemoryPointer)} is null or empty");
        }

        if (virtualDevice == null)
        {
            throw new ArgumentNullException($"Cannot find the device with the ID of {DeviceId}");
        }

        for (int i = 0; i < NewValue.Length; i++)
        {
            virtualDevice.Data[(int)(MemoryPointer + i)] = NewValue[i];
        }
    }

    private void RedoGpuAction(IVirtualMachine virtualMachineInstance)
    {
        IVirtualGPU virtualGPU = virtualMachineInstance.GPU;

        if (MemoryPointer == null)
        {
            throw new ArgumentNullException($"Value {nameof(MemoryPointer)} is null or empty");
        }

        for (int i = 0; i < NewValue.Length; i++)
        {
            virtualGPU.Data[(int)(MemoryPointer + i)] = NewValue[i];
        }
    }

    private ulong ConvertBytesToUlong(byte[] bytesToConvert)
    {
        switch (Size)
        {
            case Size.Byte: return bytesToConvert[0];
            case Size.Word: return ((ulong)bytesToConvert[1] << 8) + (ulong)bytesToConvert[0];
            case Size.Dword: return ((ulong)bytesToConvert[3] << 24) + ((ulong)bytesToConvert[2] << 16) + ((ulong)bytesToConvert[1] << 8) + (ulong)bytesToConvert[0];
            case Size.Qword: return ((ulong)bytesToConvert[7] << 56) + ((ulong)bytesToConvert[6] << 48) + ((ulong)bytesToConvert[5] << 40) + ((ulong)bytesToConvert[4] << 32) + ((ulong)bytesToConvert[3] << 24) + ((ulong)bytesToConvert[2] << 16) + ((ulong)bytesToConvert[1] << 8) + (ulong)bytesToConvert[0];
            default: throw new ArgumentException($"Cannot convert property {nameof(PreviousValue)} from invalid size {Size}");
        }
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