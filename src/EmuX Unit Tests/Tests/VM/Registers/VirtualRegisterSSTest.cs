using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuX_Unit_Tests.Tests.VM.Registers;

[TestClass]
public sealed class VirtualRegisterSSTest : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        VirtualRegisterSS registerOne = new();
        VirtualRegisterSS registerTwo = new();

        Assert.AreNotEqual<ushort>(registerOne.SS, registerTwo.SS, "Rerun the test, if the test comes out negative then there is probably something going on with the randomness of the registers");
    }
}