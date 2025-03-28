using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuX_Unit_Tests.Tests.VM.Registers;

[TestClass]
public class VirtualRegisterESTest : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        VirtualRegisterES registerOne = new();
        VirtualRegisterES registerTwo = new();

        Assert.AreNotEqual<ushort>(registerOne.ES, registerTwo.ES, "Rerun the test, if the test comes out negative then there is probably something going on with the randomness of the registers");
    }
}