using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.VM.Registers.SubRegisters;

[TestClass]
public sealed class VirtualRegisterGSTests : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        VirtualRegisterGS registerOne = new();
        VirtualRegisterGS registerTwo = new();

        Assert.AreNotEqual(registerOne.GS, registerTwo.GS, "Rerun the test, if the test comes out negative then there is probably something going on with the randomness of the registers");
    }
}