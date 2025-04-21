using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Internal.RTC;

public class VirtualRTC(IVirtualMachine? parentVirtualMachine = null) : IVirtualRTC
{
    public void SetSystemClock(byte hours, byte minutes, byte seconds)
    {
        SystemClock = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hours, minutes, seconds);
    }

    public void SetRTC(byte hours, byte minutes, byte seconds)
    {
        RTC = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hours, minutes, seconds);
    }

    public DateTime SystemClock { get; private set; } = DateTime.MinValue;
    public DateTime RTC { get; private set; } = DateTime.Now;
    public IVirtualMachine? ParentVirtualMachine { get; set; } = parentVirtualMachine;
}