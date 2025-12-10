using EmuXCore.VM.Internal.CPU.Registers.SegmentRegisters;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.VM.Registers.SegmentRegisters;

[TestClass]
public sealed class VirtualRegisterESTests : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        VirtualRegisterES registerOne = new();
        VirtualRegisterES registerTwo = new();

        Assert.AreNotEqual(registerOne.ES, registerTwo.ES, "Rerun the test, if the test comes out negative then there is probably something going on with the randomness of the registers");
    }
}