using EmuXCoreUnitTests.Tests.InternalConstants;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuXCoreUnitTests.Tests.VM.Registers.SubRegisters;

[TestClass]
public sealed class VirtualRegisterCSTests : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        VirtualRegisterCS registerOne = new();
        VirtualRegisterCS registerTwo = new();

        Assert.AreNotEqual(registerOne.CS, registerTwo.CS, "Rerun the test, if the test comes out negative then there is probably something going on with the randomness of the registers");
    }
}