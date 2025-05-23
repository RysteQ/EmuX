using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuX_Unit_Tests.Tests.VM.Registers;

[TestClass]
public sealed class VirtualRegisterGSTests : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        VirtualRegisterGS registerOne = new();
        VirtualRegisterGS registerTwo = new();

        Assert.AreNotEqual<ushort>(registerOne.GS, registerTwo.GS, "Rerun the test, if the test comes out negative then there is probably something going on with the randomness of the registers");
    }
}