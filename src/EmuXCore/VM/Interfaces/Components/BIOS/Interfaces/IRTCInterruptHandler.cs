using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Interfaces.Components.BIOS.Interfaces;

/// <summary>
/// The RTC interrupt handler is used to handle all the RTC the sub-interrupt code function calls
/// </summary>
public interface IRTCInterruptHandler
{
    /// <summary>
    /// Reads the system clock <br />
    /// Hour: CH <br />
    /// Minute: CL <br />
    /// Second: DH <br />
    /// Warning: DL is set to 0 <br />
    /// </summary>
    /// <param name="virtualCPU">The IVirtualCPU implementation to save the system clock data to</param>
    /// <param name="virtualRTC">The IVirtualRTC implementation to get the system clock data from</param>
    void ReadSystemClock(IVirtualCPU virtualCPU, IVirtualRTC virtualRTC);

    /// <summary>
    /// Sets the system clock <br />
    /// Hour: CH <br />
    /// Minute: CL <br />
    /// Second: DH <br />
    /// Warning: DL is set to 0
    /// </summary>
    /// <param name="virtualCPU">The IVirtualCPU implementation to get the new system clock data from</param>
    /// <param name="virtualRTC">The IVirtualRTC implementation to set the RTC data</param>
    void SetSystemClock(IVirtualCPU virtualCPU, IVirtualRTC virtualRTC);

    /// <summary>
    /// Reads the RTC <br />
    /// Hour: CH <br />
    /// Minute: CL <br />
    /// Second: DH <br />
    /// Warning: DL is set to 0 <br />
    /// </summary>
    /// <param name="virtualCPU">The IVirtualCPU implementation to save the RTC data to</param>
    /// <param name="virtualRTC">The IVirtualRTC implementation to get the RTC data from</param>
    void ReadRTC(IVirtualCPU virtualCPU, IVirtualRTC virtualRTC);

    /// <summary>
    /// Sets the RTC <br />
    /// Hour: CH <br />
    /// Minute: CL <br />
    /// Second: DH <br />
    /// Warning: DL is set to 0 <br />
    /// </summary>
    /// <param name="virtualCPU">The IVirtualCPU implementation to get the new RTC data from</param>
    /// <param name="virtualRTC">The IVirtualRTC implementation to set the system clock data</param>
    void SetRTC(IVirtualCPU virtualCPU, IVirtualRTC virtualRTC);
}