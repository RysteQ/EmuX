using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.VM.Registers.MainRegisters;

[TestClass]
public sealed class VirtualRegisterRCXTests : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        VirtualRegisterRCX registerOne = new();
        VirtualRegisterRCX registerTwo = new();

        Assert.AreNotEqual(registerOne.RCX, registerTwo.RCX, "Rerun the test, if the test comes out negative then there is probably something going on with the randomness of the registers");
    }

    [TestMethod]
    public void SetRCX()
    {
        VirtualRegisterRCX register = new();

        register.RCX = 0x_ffff_0000_ff00_00ff;

        Assert.AreEqual(0x_ffff_0000_ff00_00ff, register.RCX);
        Assert.AreEqual(0x_ff00_00ff, register.ECX);
        Assert.AreEqual<ushort>(0x_00ff, register.CX);
        Assert.AreEqual<byte>(0x_00, register.CH);
        Assert.AreEqual<byte>(0x_ff, register.CL);
    }

    [TestMethod]
    public void SetECX()
    {
        VirtualRegisterRCX register = new();

        register.ECX = 0x_ff00_00ff;

        Assert.AreEqual(0x_ff00_00ff, register.ECX);
        Assert.AreEqual<ushort>(0x_00ff, register.CX);
        Assert.AreEqual<byte>(0x_00, register.CH);
        Assert.AreEqual<byte>(0x_ff, register.CL);
    }

    [TestMethod]
    public void SetCX()
    {
        VirtualRegisterRCX register = new();

        register.CX = 0x_00ff;

        Assert.AreEqual<ushort>(0x_00ff, register.CX);
        Assert.AreEqual<byte>(0x_00, register.CH);
        Assert.AreEqual<byte>(0x_ff, register.CL);
    }

    [TestMethod]
    public void SetCH()
    {
        VirtualRegisterRCX register = new();

        register.CH = 0x_00;

        Assert.AreEqual<byte>(0x_00, register.CH);
    }

    [TestMethod]
    public void SetCL()
    {
        VirtualRegisterRCX register = new();

        register.CL = 0x_ff;

        Assert.AreEqual<byte>(0x_ff, register.CL);
    }
}