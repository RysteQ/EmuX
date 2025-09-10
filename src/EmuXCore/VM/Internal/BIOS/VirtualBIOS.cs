using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.BIOS;
using EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts;
using EmuXCore.VM.Interfaces.Components.BIOS.Interfaces;
using EmuXCore.VM.Interfaces.Components.Enums.SubInterrupts;

namespace EmuXCore.VM.Internal.BIOS;

public class VirtualBIOS : IVirtualBIOS
{
    public VirtualBIOS(IDiskInterruptHandler diskInterruptHandler, IRTCInterruptHandler rtcInterruptHandler, IVideoInterruptHandler videoInterruptHandler, IDeviceInterruptHandler deviceInterruptHandler, IVirtualMachine? parentVirtualMachine = null)
    {
        ParentVirtualMachine = parentVirtualMachine;

        _diskInterruptHandler = diskInterruptHandler;
        _rtcInterruptHandler = rtcInterruptHandler;
        _videoInterruptHandler = videoInterruptHandler;
        _deviceInterruptHandler = deviceInterruptHandler;
    }

    public void HandleDiskInterrupt(DiskInterrupt interruptCode)
    {
        if (ParentVirtualMachine == null)
        {
            throw new ArgumentNullException($"Property {nameof(ParentVirtualMachine)} cannot be null when calling this method, please provide a value for it");
        }

        switch (interruptCode)
        {
            case DiskInterrupt.ReadTrack: _diskInterruptHandler.ReadTrack(ParentVirtualMachine.CPU, ParentVirtualMachine.Memory, ParentVirtualMachine.Disks); break;
            case DiskInterrupt.WriteTrack: _diskInterruptHandler.WriteTrack(ParentVirtualMachine.CPU, ParentVirtualMachine.Memory, ParentVirtualMachine.Disks); break;
        }
    }

    public void HandleRTCInterrupt(RTCInterrupt interruptCode)
    {
        if (ParentVirtualMachine == null)
        {
            throw new ArgumentNullException($"Property {nameof(ParentVirtualMachine)} cannot be null when calling this method, please provide a value for it");
        }

        // It is granted that the virtual machine is going to have an operational virtual RTC unit
        ParentVirtualMachine.SetFlag(EFlags.CF, false);

        switch (interruptCode)
        {
            case RTCInterrupt.ReadSystemClock: _rtcInterruptHandler.ReadSystemClock(ParentVirtualMachine.CPU, ParentVirtualMachine.RTC); break;
            case RTCInterrupt.SetSystemClock: _rtcInterruptHandler.SetSystemClock(ParentVirtualMachine.CPU, ParentVirtualMachine.RTC); break;
            case RTCInterrupt.ReadRTC: _rtcInterruptHandler.ReadRTC(ParentVirtualMachine.CPU, ParentVirtualMachine.RTC); break;
            case RTCInterrupt.SetRTC: _rtcInterruptHandler.SetRTC(ParentVirtualMachine.CPU, ParentVirtualMachine.RTC); break;
        }
    }

    public void HandleVideoInterrupt(VideoInterrupt interruptCode)
    {
        if (ParentVirtualMachine == null)
        {
            throw new ArgumentNullException($"Property {nameof(ParentVirtualMachine)} cannot be null when calling this method, please provide a value for it");
        }

        switch (interruptCode)
        {
            case VideoInterrupt.ReadPixel: _videoInterruptHandler.ReadPixel(ParentVirtualMachine.CPU, ParentVirtualMachine.GPU); break;
            case VideoInterrupt.GetResolution: _videoInterruptHandler.GetResolution(ParentVirtualMachine.CPU, ParentVirtualMachine.GPU); break;
            case VideoInterrupt.DrawPixel: _videoInterruptHandler.DrawPixel(ParentVirtualMachine.CPU, ParentVirtualMachine.GPU); break;
            case VideoInterrupt.DrawLine: _videoInterruptHandler.DrawLine(ParentVirtualMachine.CPU, ParentVirtualMachine.GPU); break;
            case VideoInterrupt.DrawBox: _videoInterruptHandler.DrawBox(ParentVirtualMachine.CPU, ParentVirtualMachine.GPU); break;
        }
    }

    public void HandleDeviceInterrupt(DeviceInterrupt interruptCode)
    {
        if (ParentVirtualMachine == null)
        {
            throw new ArgumentNullException($"Property {nameof(ParentVirtualMachine)} cannot be null when calling this method, please provide a value for it");
        }

        switch (interruptCode)
        {
            case DeviceInterrupt.ExecuteLogic: _deviceInterruptHandler.ExecuteLogic(ParentVirtualMachine.CPU, ParentVirtualMachine.Devices); break;
        }
    }

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private IDiskInterruptHandler _diskInterruptHandler;
    private IRTCInterruptHandler _rtcInterruptHandler;
    private IVideoInterruptHandler _videoInterruptHandler;
    private IDeviceInterruptHandler _deviceInterruptHandler;
}