using EmuXCore.VM.Internal.CPU.Registers;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;
using EmuXCoreUnitTests.Tests.InternalConstants;

namespace EmuXCoreUnitTests.Tests.VM.Registers.MainRegisters;

[TestClass]
public sealed class VirtualRegisterRSPTests : TestWideInternalConstants
{
    [TestMethod]
    public void SetRBP()
    {
        VirtualRegisterRSP register = new();

        register.RSP = 0x_ffff_0000_ff00_00ff;

        Assert.AreEqual(0x_ffff_0000_ff00_00ff, register.RSP);
        Assert.AreEqual(0x_ff00_00ff, register.ESP);
        Assert.AreEqual<ushort>(0x_00ff, register.SP);
        Assert.AreEqual<byte>(0x_ff, register.SPL);
    }

    [TestMethod]
    public void SetEBP()
    {
        VirtualRegisterRSP register = new();

        register.ESP = 0x_ff00_00ff;

        Assert.AreEqual(0x_ff00_00ff, register.ESP);
        Assert.AreEqual<ushort>(0x_00ff, register.SP);
        Assert.AreEqual<byte>(0x_ff, register.SPL);
    }

    [TestMethod]
    public void SetBP()
    {
        VirtualRegisterRSP register = new();

        register.SP = 0x_00ff;

        Assert.AreEqual<ushort>(0x_00ff, register.SP);
        Assert.AreEqual<byte>(0x_ff, register.SPL);
    }

    [TestMethod]
    public void SetBPL()
    {
        VirtualRegisterRSP register = new();

        register.SPL = 0x_ff;

        Assert.AreEqual<byte>(0x_ff, register.SPL);
    }
}