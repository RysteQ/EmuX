using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.VM.Registers.MainRegisters;

[TestClass]
public sealed class VirtualRegisterRDXTests : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        VirtualRegisterRDX registerOne = new();
        VirtualRegisterRDX registerTwo = new();

        Assert.AreNotEqual(registerOne.RDX, registerTwo.RDX, "Rerun the test, if the test comes out negative then there is probably something going on with the randomness of the registers");
    }

    [TestMethod]
    public void SetRDX()
    {
        VirtualRegisterRDX register = new();

        register.RDX = 0x_ffff_0000_ff00_00ff;

        Assert.AreEqual<ulong>(0x_ffff_0000_ff00_00ff, register.RDX);
        Assert.AreEqual<uint>(0x_ff00_00ff, register.EDX);
        Assert.AreEqual<ushort>(0x_00ff, register.DX);
        Assert.AreEqual<byte>(0x_00, register.DH);
        Assert.AreEqual<byte>(0x_ff, register.DL);
    }

    [TestMethod]
    public void SetEDX()
    {
        VirtualRegisterRDX register = new();

        register.EDX = 0x_ff00_00ff;

        Assert.AreEqual<uint>(0x_ff00_00ff, register.EDX);
        Assert.AreEqual<ushort>(0x_00ff, register.DX);
        Assert.AreEqual<byte>(0x_00, register.DH);
        Assert.AreEqual<byte>(0x_ff, register.DL);
    }

    [TestMethod]
    public void SetDX()
    {
        VirtualRegisterRDX register = new();

        register.RDX = ulong.MaxValue;
        register.DX = 0x_00ff;

        Assert.AreEqual<ulong>(0x_ff_ff_ff_ff_ff_ff_00_ff, register.RDX);
        Assert.AreEqual<ushort>(0x_00ff, register.DX);
        Assert.AreEqual<byte>(0x_00, register.DH);
        Assert.AreEqual<byte>(0x_ff, register.DL);
    }

    [TestMethod]
    public void SetDH()
    {
        VirtualRegisterRDX register = new();

        register.RDX = ulong.MaxValue;
        register.DH = 0x_00;

        Assert.AreEqual<ulong>(0x_ff_ff_ff_ff_ff_ff_00_ff, register.RDX);
        Assert.AreEqual<byte>(0x_00, register.DH);
    }

    [TestMethod]
    public void SetDL()
    {
        VirtualRegisterRDX register = new();

        register.RDX = ulong.MaxValue - byte.MaxValue;
        register.DL = 0x_ff;

        Assert.AreEqual<ulong>(0x_ff_ff_ff_ff_ff_ff_ff_ff, register.RDX);
        Assert.AreEqual<byte>(0x_ff, register.DL);
    }
}