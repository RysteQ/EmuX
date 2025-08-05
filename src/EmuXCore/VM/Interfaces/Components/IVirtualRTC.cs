namespace EmuXCore.VM.Interfaces.Components;

/// <summary>
/// The IVirtualRTC is meant to emulate the RTC of a real system
/// </summary>
public interface IVirtualRTC : IVirtualComponent
{
    /// <summary>
    /// This method updates the value of the SystemClock
    /// </summary>
    /// <param name="hours">The new hours value</param>
    /// <param name="minutes">The new minutes value</param>
    /// <param name="seconds">The new seconds value</param>
    void SetSystemClock(byte hours, byte minutes, byte seconds);

    /// <summary>
    /// This method updates the value of the RTC
    /// </summary>
    /// <param name="hours">The new hours value</param>
    /// <param name="minutes">The new minutes value</param>
    /// <param name="seconds">The new seconds value</param>
    void SetRTC(byte hours, byte minutes, byte seconds);

    /// <summary>
    /// The system clock. This is a read only property, if you want to modify its value call the SetSystemClock method
    /// </summary>
    DateTime SystemClock { get; }

    /// <summary>
    /// The RTC. This is a read only property, if you want to modify its value call the SetRTC method
    /// </summary>
    DateTime RTC { get; }
}