using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.VM.Registers.SubRegisters;

[TestClass]
public sealed class VirtualRegisterSSTests : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        VirtualRegisterSS registerOne = new();
        VirtualRegisterSS registerTwo = new();

        Assert.AreNotEqual(registerOne.SS, registerTwo.SS, "Rerun the test, if the test comes out negative then there is probably something going on with the randomness of the registers");
    }
}