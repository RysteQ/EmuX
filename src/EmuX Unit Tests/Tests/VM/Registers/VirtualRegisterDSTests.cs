using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuX_Unit_Tests.Tests.VM.Registers;

[TestClass]
public sealed class VirtualRegisterDSTests : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        VirtualRegisterDS registerOne = new();
        VirtualRegisterDS registerTwo = new();

        Assert.AreNotEqual<ushort>(registerOne.DS, registerTwo.DS, "Rerun the test, if the test comes out negative then there is probably something going on with the randomness of the registers");
    }
}