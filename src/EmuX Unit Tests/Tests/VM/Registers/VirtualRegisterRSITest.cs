using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuX_Unit_Tests.Tests.VM.Registers;

[TestClass]
public class VirtualRegisterRSITest : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        VirtualRegisterRSI registerOne = new();
        VirtualRegisterRSI registerTwo = new();

        Assert.AreNotEqual<ulong>(registerOne.RSI, registerTwo.RSI, "Rerun the test, if the test comes out negative then there is probably something going on with the randomness of the registers");
    }

    [TestMethod]
    public void SetRSI()
    {
        VirtualRegisterRSI register = new();

        register.RSI = 0x_ffff_0000_ff00_00ff;

        Assert.AreEqual<ulong>(0x_ffff_0000_ff00_00ff, register.RSI);
        Assert.AreEqual<uint>(0x_ff00_00ff, register.ESI);
        Assert.AreEqual<ushort>(0x_00ff, register.SI);
        Assert.AreEqual<byte>(0x_ff, register.SIL);
    }

    [TestMethod]
    public void SetESI()
    {
        VirtualRegisterRSI register = new();

        register.ESI = 0x_ff00_00ff;

        Assert.AreEqual<uint>(0x_ff00_00ff, register.ESI);
        Assert.AreEqual<ushort>(0x_00ff, register.SI);
        Assert.AreEqual<byte>(0x_ff, register.SIL);
    }

    [TestMethod]
    public void SetSI()
    {
        VirtualRegisterRSI register = new();

        register.SI = 0x_00ff;

        Assert.AreEqual<ushort>(0x_00ff, register.SI);
        Assert.AreEqual<byte>(0x_ff, register.SIL);
    }

    [TestMethod]
    public void SetSIL()
    {
        VirtualRegisterRSI register = new();

        register.SIL = 0x_ff;

        Assert.AreEqual<byte>(0x_ff, register.SIL);
    }
}