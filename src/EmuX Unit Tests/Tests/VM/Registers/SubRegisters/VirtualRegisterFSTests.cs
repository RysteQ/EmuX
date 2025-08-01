using EmuXCoreUnitTests.Tests.InternalConstants;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuXCoreUnitTests.Tests.VM.Registers.SubRegisters;

[TestClass]
public sealed class VirtualRegisterFSTests : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        VirtualRegisterFS registerOne = new();
        VirtualRegisterFS registerTwo = new();

        Assert.AreNotEqual(registerOne.FS, registerTwo.FS, "Rerun the test, if the test comes out negative then there is probably something going on with the randomness of the registers");
    }
}