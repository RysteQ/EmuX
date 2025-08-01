using EmuXCore.VM.Internal.CPU.Registers;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;
using EmuXCoreUnitTests.Tests.InternalConstants;

namespace EmuXCoreUnitTests.Tests.VM.Registers.MainRegisters;

[TestClass]
public sealed class VirtualRegisterRIPTests : TestWideInternalConstants
{
    [TestMethod]
    public void SetRIP()
    {
        VirtualRegisterRIP register = new();

        register.RIP = 0x_ffff_0000_ff00_00ff;

        Assert.AreEqual(0x_ffff_0000_ff00_00ff, register.RIP);
        Assert.AreEqual(0x_ff00_00ff, register.EIP);
        Assert.AreEqual<ushort>(0x_00ff, register.IP);
    }

    [TestMethod]
    public void SetEIP()
    {
        VirtualRegisterRIP register = new();

        register.EIP = 0x_ff00_00ff;

        Assert.AreEqual(0x_ff00_00ff, register.EIP);
        Assert.AreEqual<ushort>(0x_00ff, register.IP);
    }

    [TestMethod]
    public void SetIP()
    {
        VirtualRegisterRIP register = new();

        register.IP = 0x_00ff;

        Assert.AreEqual<ushort>(0x_00ff, register.IP);
    }
}