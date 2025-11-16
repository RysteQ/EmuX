# Adding new virtual devices

To add a new virtual device first navigate to the `EmuXCore\VM\Internal\Devices` folder and look if any subfolder fits the criteria of the device you want to implement, if yes then create it under that folder, if not then create a new folder for it.

The code template for the new virtual device is the following.

```csharp
public class VirtualDevice : IVirtualDevice
{
    public VirtualDevice(ushort deviceId, IVirtualMachine? parentVirtualMachine = null)
    {
        DeviceId = deviceId;
        ParentVirtualMachine = parentVirtualMachine;
        Data = new byte[...];
    }

    public void Execute()
    {
    }

    public IVirtualMachine? ParentVirtualMachine { get; set; }
    public ushort DeviceId { get; init; }
    public DeviceStatus Status { get; set; } = DeviceStatus.NonOperational;
    public byte[] Data { get; set; }
}
```

Change the name of the class to the name of the virtual device, then allocate enough memory to the `Data` buffer for the device, and lastly implement the logic behind the `Execute` method.