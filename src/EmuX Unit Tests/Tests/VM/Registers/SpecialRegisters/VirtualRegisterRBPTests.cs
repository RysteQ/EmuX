using EmuXCore.VM.Internal.CPU.Registers;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.VM.Registers.MainRegisters;

[TestClass]
public sealed class VirtualRegisterRBPTests : TestWideInternalConstants
{
    [TestMethod]
    public void SetRBP()
    {
        VirtualRegisterRBP register = new();

        register.RBP = 0x_ffff_0000_ff00_00ff;

        Assert.AreEqual<ulong>(0x_ffff_0000_ff00_00ff, register.RBP);
        Assert.AreEqual<uint>(0x_ff00_00ff, register.EBP);
        Assert.AreEqual<ushort>(0x_00ff, register.BP);
        Assert.AreEqual<byte>(0x_ff, register.BPL);
    }

    [TestMethod]
    public void SetEBP()
    {
        VirtualRegisterRBP register = new();

        register.EBP = 0x_ff00_00ff;

        Assert.AreEqual<uint>(0x_ff00_00ff, register.EBP);
        Assert.AreEqual<ushort>(0x_00ff, register.BP);
        Assert.AreEqual<byte>(0x_ff, register.BPL);
    }

    [TestMethod]
    public void SetBP()
    {
        VirtualRegisterRBP register = new();

        register.BP = 0x_00ff;

        Assert.AreEqual<ushort>(0x_00ff, register.BP);
        Assert.AreEqual<byte>(0x_ff, register.BPL);
    }

    [TestMethod]
    public void SetBPL()
    {
        VirtualRegisterRBP register = new();

        register.BPL = 0x_ff;

        Assert.AreEqual<byte>(0x_ff, register.BPL);
    }
}