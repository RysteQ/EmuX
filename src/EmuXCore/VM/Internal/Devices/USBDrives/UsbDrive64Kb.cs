using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;

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
    }

    public void Execute() { }

    public IVirtualMachine? ParentVirtualMachine { get; set; }
    public ushort DeviceId { get; init; }
    public DeviceStatus Status { get; set; } = DeviceStatus.NonOperational;
    public byte[] Data
    {
        get => _data;
        set
        {
            List<(int Index, byte Item)> modificationIndexes = _data.Index().Where(selectedByte => selectedByte.Item != value[selectedByte.Index]).ToList();

            ParentVirtualMachine?.RegisterAction(
                VmActionCategory.ModifiedDevice,
                Size.Byte, 
                _data.Skip(modificationIndexes.First().Index).Take(modificationIndexes.Last().Index + 1).ToArray(), 
                value.Skip(modificationIndexes.First().Index).Take(modificationIndexes.Last().Index + 1).ToArray(), 
                memoryPointer: modificationIndexes.First().Index,
                deviceId: DeviceId);
            
            _data = value;
        }
    }

    private byte[] _data = new byte[ushort.MaxValue];
}