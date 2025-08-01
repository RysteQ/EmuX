using EmuXCoreUnitTests.Tests.InternalConstants;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCoreUnitTests.Tests.VM.Registers.MainRegisters;

[TestClass]
public sealed class VirtualRegisterEFLAGSTests : TestWideInternalConstants
{
    [TestMethod]
    public void SetRFLAGS()
    {
        VirtualRegisterEFLAGS register = new();

        register.RFLAGS = 0x_ff_45_00_60_72_84_18_a8;

        Assert.AreEqual<ulong>(0x_ff_45_00_60_72_84_18_a8, register.RFLAGS);
        Assert.AreEqual<uint>(0x_72_84_18_a8, register.EFLAGS);
        Assert.AreEqual<ushort>(0x_18_a8, register.FLAGS);
    }

    [TestMethod]
    public void SetEFLAGS()
    {
        VirtualRegisterEFLAGS register = new();

        register.EFLAGS = 0x_72_84_18_a8;

        Assert.AreEqual<uint>(0x_72_84_18_a8, register.EFLAGS);
        Assert.AreEqual<ushort>(0x_18_a8, register.FLAGS);
    }

    [TestMethod]
    public void SetFLAGS()
    {
        VirtualRegisterEFLAGS register = new();

        register.FLAGS = 0x_18_a8;

        Assert.AreEqual<ushort>(0x_18_a8, register.FLAGS);
    }
}