using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.VM.Registers.MainRegisters;

[TestClass]
public sealed class VirtualRegisterRBXTests : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        VirtualRegisterRBX registerOne = new();
        VirtualRegisterRBX registerTwo = new();

        Assert.AreNotEqual(registerOne.RBX, registerTwo.RBX, "Rerun the test, if the test comes out negative then there is probably something going on with the randomness of the registers");
    }

    [TestMethod]
    public void SetRBX()
    {
        VirtualRegisterRBX register = new();

        register.RBX = 0x_ffff_0000_ff00_00ff;

        Assert.AreEqual(0x_ffff_0000_ff00_00ff, register.RBX);
        Assert.AreEqual(0x_ff00_00ff, register.EBX);
        Assert.AreEqual<ushort>(0x_00ff, register.BX);
        Assert.AreEqual<byte>(0x_00, register.BH);
        Assert.AreEqual<byte>(0x_ff, register.BL);
    }

    [TestMethod]
    public void SetEBX()
    {
        VirtualRegisterRBX register = new();

        register.EBX = 0x_ff00_00ff;

        Assert.AreEqual(0x_ff00_00ff, register.EBX);
        Assert.AreEqual<ushort>(0x_00ff, register.BX);
        Assert.AreEqual<byte>(0x_00, register.BH);
        Assert.AreEqual<byte>(0x_ff, register.BL);
    }

    [TestMethod]
    public void SetBX()
    {
        VirtualRegisterRBX register = new();

        register.BX = 0x_00ff;

        Assert.AreEqual<ushort>(0x_00ff, register.BX);
        Assert.AreEqual<byte>(0x_00, register.BH);
        Assert.AreEqual<byte>(0x_ff, register.BL);
    }

    [TestMethod]
    public void SetBH()
    {
        VirtualRegisterRBX register = new();

        register.BH = 0x_00;

        Assert.AreEqual<byte>(0x_00, register.BH);
    }

    [TestMethod]
    public void SetBL()
    {
        VirtualRegisterRBX register = new();

        register.BL = 0x_ff;

        Assert.AreEqual<byte>(0x_ff, register.BL);
    }
}