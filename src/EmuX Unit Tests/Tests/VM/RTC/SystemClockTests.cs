using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.VM.Interfaces.Components;

namespace EmuX_Unit_Tests.Tests.VM.RTC;

[TestClass]
public sealed class SystemClockTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestReadSystemClock_SameTimeAsNow()
    {
        IVirtualRTC virtualRTC = GenerateVirtualRTC();

        Assert.AreEqual<string>(DateTime.MinValue.ToLongTimeString(), virtualRTC.SystemClock.ToLongTimeString());
    }

    [TestMethod]
    public void TestSystemClock_Changed()
    {
        IVirtualRTC virtualRTC = GenerateVirtualRTC();

        virtualRTC.SetSystemClock(0, 0, 10);

        Assert.AreEqual<string>(DateTime.MinValue.AddSeconds(10).ToLongTimeString(), virtualRTC.SystemClock.ToLongTimeString());
    }
}