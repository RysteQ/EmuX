using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.VM.Registers.SubRegisters;

[TestClass]
public sealed class VirtualRegisterDSTests : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        VirtualRegisterDS registerOne = new();
        VirtualRegisterDS registerTwo = new();

        Assert.AreNotEqual(registerOne.DS, registerTwo.DS, "Rerun the test, if the test comes out negative then there is probably something going on with the randomness of the registers");
    }
}