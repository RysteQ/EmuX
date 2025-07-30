using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Interfaces.Components.BIOS.Interfaces;

public interface IRTCInterruptHandler
{
    void ReadSystemClock(IVirtualCPU virtualCPU, IVirtualRTC virtualRTC);
    void SetSystemClock(IVirtualCPU virtualCPU, IVirtualRTC virtualRTC);
    void ReadRTC(IVirtualCPU virtualCPU, IVirtualRTC virtualRTC);
    void SetRTC(IVirtualCPU virtualCPU, IVirtualRTC virtualRTC);
}