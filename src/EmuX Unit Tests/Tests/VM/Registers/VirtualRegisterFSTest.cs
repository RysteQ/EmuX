using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuX_Unit_Tests.Tests.VM.Registers;

[TestClass]
public class VirtualRegisterFSTest : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        VirtualRegisterFS registerOne = new();
        VirtualRegisterFS registerTwo = new();

        Assert.AreNotEqual<ushort>(registerOne.FS, registerTwo.FS, "Rerun the test, if the test comes out negative then there is probably something going on with the randomness of the registers");
    }
}