using EmuXCore.VM.Interfaces.Components;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.VM.RTC;

[TestClass]
public sealed class RTCTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestReadRTC_SameTimeAsNow()
    {
        IVirtualRTC virtualRTC = GenerateVirtualRTC();

        Assert.AreEqual<string>(DateTime.Now.ToLongTimeString(), virtualRTC.RTC.ToLongTimeString());
    }

    [TestMethod]
    public void TestReadRTC_Changed()
    {
        IVirtualRTC virtualRTC = GenerateVirtualRTC();
        DateTime toCompareAgainst = new(virtualRTC.RTC.Year, virtualRTC.RTC.Month, virtualRTC.RTC.Day, 0, 0, 10);

        virtualRTC.SetRTC(0, 0, 10);

        Assert.AreEqual<string>(toCompareAgainst.ToLongTimeString(), virtualRTC.RTC.ToLongTimeString());
    }
}