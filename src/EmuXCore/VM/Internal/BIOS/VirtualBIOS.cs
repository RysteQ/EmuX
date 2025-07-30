using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.BIOS;
using EmuXCore.VM.Interfaces.Components.BIOS.Interfaces;
using EmuXCore.VM.Interfaces.Components.Enums.SubInterrupts;
using EmuXCore.VM.Internal.BIOS.Enums.SubInterrupts;
using EmuXCore.VM.Internal.BIOS.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;

namespace EmuXCore.VM.Internal.BIOS;

public class VirtualBIOS(IDiskInterruptHandler diskInterruptHandler, IRTCInterruptHandler rtcInterruptHandler, IVideoInterruptHandler videoInterruptHandler, IVirtualMachine? parentVirtualMachine = null) : IVirtualBIOS
{
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

    public IVirtualMachine? ParentVirtualMachine { get; set; } = parentVirtualMachine;

    private IDiskInterruptHandler _diskInterruptHandler = diskInterruptHandler;
    private IRTCInterruptHandler _rtcInterruptHandler = rtcInterruptHandler;
    private IVideoInterruptHandler _videoInterruptHandler = videoInterruptHandler;
}