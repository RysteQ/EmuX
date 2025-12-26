using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.VM.Registers.MainRegisters;

[TestClass]
public sealed class VirtualRegisterRAXTests : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        VirtualRegisterRAX registerOne = new();
        VirtualRegisterRAX registerTwo = new();

        Assert.AreNotEqual(registerOne.RAX, registerTwo.RAX, "Rerun the test, if the test comes out negative then there is probably something going on with the randomness of the registers");
    }

    [TestMethod]
    public void SetRAX()
    {
        VirtualRegisterRAX register = new();

        register.RAX = ulong.MaxValue;
        register.RAX = 0x_ffff_0000_ff00_00ff;

        Assert.AreEqual<ulong>(0x_ffff_0000_ff00_00ff, register.RAX);
        Assert.AreEqual<uint>(0x_ff00_00ff, register.EAX);
        Assert.AreEqual<ushort>(0x_00ff, register.AX);
        Assert.AreEqual<byte>(0x_00, register.AH);
        Assert.AreEqual<byte>(0x_ff, register.AL);
    }

    [TestMethod]
    public void SetEAX()
    {
        VirtualRegisterRAX register = new();

        register.RAX = ulong.MaxValue;
        register.EAX = 0x_ff00_00ff;

        Assert.AreEqual<ulong>(0x_00_00_00_00_ff_00_00_ff, register.RAX);
        Assert.AreEqual<uint>(0x_ff00_00ff, register.EAX);
        Assert.AreEqual<ushort>(0x_00ff, register.AX);
        Assert.AreEqual<byte>(0x_00, register.AH);
        Assert.AreEqual<byte>(0x_ff, register.AL);
    }

    [TestMethod]
    public void SetAX()
    {
        VirtualRegisterRAX register = new();

        register.RAX = ulong.MaxValue;
        register.AX = 0x_00ff;

        Assert.AreEqual<ulong>(0x_ff_ff_ff_ff_ff_ff_00_ff, register.RAX);
        Assert.AreEqual<ushort>(0x_00ff, register.AX);
        Assert.AreEqual<byte>(0x_00, register.AH);
        Assert.AreEqual<byte>(0x_ff, register.AL);
    }

    [TestMethod]
    public void SetAH()
    {
        VirtualRegisterRAX register = new();

        register.RAX = ulong.MaxValue;
        register.AH = 0x_00;

        Assert.AreEqual<ulong>(0x_ff_ff_ff_ff_ff_ff_00_ff, register.RAX);
        Assert.AreEqual<byte>(0x_00, register.AH);
    }

    [TestMethod]
    public void SetAL()
    {
        VirtualRegisterRAX register = new();

        register.RAX = ulong.MaxValue - 0x_00_00_00_00_00_00_ff_ff;
        register.AL = 0x_ff;

        Assert.AreEqual<ulong>(0x_ff_ff_ff_ff_ff_ff_00_ff, register.RAX);
        Assert.AreEqual<byte>(0x_ff, register.AL);
    }
}