namespace EmuXCore.VM.Interfaces.Components;

public interface IVirtualRTC : IVirtualComponent
{
    void SetSystemClock(byte hours, byte minutes, byte seconds);
    void SetRTC(byte hours, byte minutes, byte seconds);

    DateTime SystemClock { get; }
    DateTime RTC { get; }
}