using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.BIOS.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.VM.Internal.BIOS.Internal;

public class RTCInterruptHandler : IRTCInterruptHandler
{
    public void ReadSystemClock(IVirtualCPU virtualCPU, IVirtualRTC virtualRTC)
    {
        DateTime currentDateTime = virtualRTC.SystemClock;

        virtualCPU.GetRegister<VirtualRegisterRCX>().CH = (byte)currentDateTime.Hour;
        virtualCPU.GetRegister<VirtualRegisterRCX>().CL = (byte)currentDateTime.Minute;
        virtualCPU.GetRegister<VirtualRegisterRDX>().DH = (byte)currentDateTime.Second;
        virtualCPU.GetRegister<VirtualRegisterRDX>().DL = 0;
    }

    public void SetSystemClock(IVirtualCPU virtualCPU, IVirtualRTC virtualRTC)
    {
        virtualRTC.SetSystemClock(virtualCPU.GetRegister<VirtualRegisterRCX>().CH, virtualCPU.GetRegister<VirtualRegisterRCX>().CL, virtualCPU.GetRegister<VirtualRegisterRDX>().DH);
    }

    public void ReadRTC(IVirtualCPU virtualCPU, IVirtualRTC virtualRTC)
    {
        DateTime currentDateTime = virtualRTC.RTC;

        virtualCPU.GetRegister<VirtualRegisterRCX>().CH = (byte)currentDateTime.Hour;
        virtualCPU.GetRegister<VirtualRegisterRCX>().CL = (byte)currentDateTime.Minute;
        virtualCPU.GetRegister<VirtualRegisterRDX>().DH = (byte)currentDateTime.Second;
        virtualCPU.GetRegister<VirtualRegisterRDX>().DL = 0;
    }

    public void SetRTC(IVirtualCPU virtualCPU, IVirtualRTC virtualRTC)
    {
        virtualRTC.SetRTC(virtualCPU.GetRegister<VirtualRegisterRCX>().CH, virtualCPU.GetRegister<VirtualRegisterRCX>().CL, virtualCPU.GetRegister<VirtualRegisterRDX>().DH);
    }
}