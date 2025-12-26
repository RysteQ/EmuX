using EmuXCore.VM.Internal.CPU.Registers;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.VM.Registers.MainRegisters;

[TestClass]
public sealed class VirtualRegisterRSPTests : TestWideInternalConstants
{
    [TestMethod]
    public void SetRBP()
    {
        VirtualRegisterRSP register = new();

        register.RSP = 0x_ffff_0000_ff00_00ff;

        Assert.AreEqual<ulong>(0x_ffff_0000_ff00_00ff, register.RSP);
        Assert.AreEqual<uint>(0x_ff00_00ff, register.ESP);
        Assert.AreEqual<ushort>(0x_00ff, register.SP);
        Assert.AreEqual<byte>(0x_ff, register.SPL);
    }

    [TestMethod]
    public void SetEBP()
    {
        VirtualRegisterRSP register = new();

        register.RSP = ulong.MaxValue;
        register.ESP = 0x_ff00_00ff;

        Assert.AreEqual<uint>(0x_00_00_00_00_ff_00_00_ff, register.ESP);
        Assert.AreEqual<uint>(0x_ff00_00ff, register.ESP);
        Assert.AreEqual<ushort>(0x_00ff, register.SP);
        Assert.AreEqual<byte>(0x_ff, register.SPL);
    }

    [TestMethod]
    public void SetBP()
    {
        VirtualRegisterRSP register = new();

        register.RSP = ulong.MaxValue;
        register.SP = 0x_00ff;

        Assert.AreEqual<ulong>(0x_ff_ff_ff_ff_ff_ff_00_ff, register.RSP);
        Assert.AreEqual<ushort>(0x_00ff, register.SP);
        Assert.AreEqual<byte>(0x_ff, register.SPL);
    }

    [TestMethod]
    public void SetBPL()
    {
        VirtualRegisterRSP register = new();

        register.RSP = ulong.MaxValue - byte.MaxValue;
        register.SPL = 0x_ff;

        Assert.AreEqual<ulong>(0x_ff_ff_ff_ff_ff_ff_ff_ff, register.RSP);
        Assert.AreEqual<byte>(0x_ff, register.SPL);
    }
}