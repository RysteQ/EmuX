using EmuXCoreUnitTests.Tests.InternalConstants;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCoreUnitTests.Tests.VM.Registers.MainRegisters;

[TestClass]
public sealed class VirtualRegisterRDITests : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        VirtualRegisterRDI registerOne = new();
        VirtualRegisterRDI registerTwo = new();

        Assert.AreNotEqual(registerOne.RDI, registerTwo.RDI, "Rerun the test, if the test comes out negative then there is probably something going on with the randomness of the registers");
    }

    [TestMethod]
    public void SetRDI()
    {
        VirtualRegisterRDI register = new();

        register.RDI = 0x_ffff_0000_ff00_00ff;

        Assert.AreEqual(0x_ffff_0000_ff00_00ff, register.RDI);
        Assert.AreEqual(0x_ff00_00ff, register.EDI);
        Assert.AreEqual<ushort>(0x_00ff, register.DI);
        Assert.AreEqual<byte>(0x_ff, register.DIL);
    }

    [TestMethod]
    public void SetEDI()
    {
        VirtualRegisterRDI register = new();

        register.EDI = 0x_ff00_00ff;

        Assert.AreEqual(0x_ff00_00ff, register.EDI);
        Assert.AreEqual<ushort>(0x_00ff, register.DI);
        Assert.AreEqual<byte>(0x_ff, register.DIL);
    }

    [TestMethod]
    public void SetDI()
    {
        VirtualRegisterRDI register = new();

        register.DI = 0x_00ff;

        Assert.AreEqual<ushort>(0x_00ff, register.DI);
        Assert.AreEqual<byte>(0x_ff, register.DIL);
    }

    [TestMethod]
    public void SetDIL()
    {
        VirtualRegisterRDI register = new();

        register.DIL = 0x_ff;

        Assert.AreEqual<byte>(0x_ff, register.DIL);
    }
}