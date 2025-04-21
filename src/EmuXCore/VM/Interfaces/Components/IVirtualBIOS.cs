using EmuXCore.VM.Internal.BIOS.Enums.SubInterrupts;

namespace EmuXCore.VM.Interfaces.Components;

public interface IVirtualBIOS : IVirtualComponent
{
    void HandleDiskInterrupt(DiskInterrupt interruptCode);
    void HandleRTCInterrupt(RTCInterrupt interruptCode);
}