using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers;

namespace EmuX_Unit_Tests.Tests.VM.Memory;

[TestClass]
public sealed class VirtualMachineStackTests : TestWideInternalConstants
{
    [TestMethod]
    public void PushAndPopByte()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.PushByte(0x_00);
        virtualMachine.PushByte(0x_0f);
        virtualMachine.PushByte(0x_f0);
        virtualMachine.PushByte(0x_ff);

        Assert.AreEqual<byte>(0x_ff, virtualMachine.PopByte());
        Assert.AreEqual<byte>(0x_f0, virtualMachine.PopByte());
        Assert.AreEqual<byte>(0x_0f, virtualMachine.PopByte());
        Assert.AreEqual<byte>(0x_00, virtualMachine.PopByte());
    }

    [TestMethod]
    public void PushAndPopWords()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.PushWord(0x_0000);
        virtualMachine.PushWord(0x_f00f);
        virtualMachine.PushWord(0x_0ff0);
        virtualMachine.PushWord(0x_ffff);

        Assert.AreEqual<ushort>(0x_ffff, virtualMachine.PopWord());
        Assert.AreEqual<ushort>(0x_0ff0, virtualMachine.PopWord());
        Assert.AreEqual<ushort>(0x_f00f, virtualMachine.PopWord());
        Assert.AreEqual<ushort>(0x_0000, virtualMachine.PopWord());
    }

    [TestMethod]
    public void PushAndPopDoubleWords()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.PushDouble(0x_0000_0000);
        virtualMachine.PushDouble(0x_f000_000f);
        virtualMachine.PushDouble(0x_000f_f000);
        virtualMachine.PushDouble(0x_ff0f_0ff0);

        Assert.AreEqual<uint>(0x_ff0f_0ff0, virtualMachine.PopDouble());
        Assert.AreEqual<uint>(0x_000f_f000, virtualMachine.PopDouble());
        Assert.AreEqual<uint>(0x_f000_000f, virtualMachine.PopDouble());
        Assert.AreEqual<uint>(0x_0000_0000, virtualMachine.PopDouble());
    }

    [TestMethod]
    public void PushAndPopQuads()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.PushQuad(0x_0000_0000_0000_0000);
        virtualMachine.PushQuad(0x_ff00_0000_0000_00ff);
        virtualMachine.PushQuad(0x_0000_00ff_ff00_0000);
        virtualMachine.PushQuad(0x_1234_ffff_0000_2222);

        Assert.AreEqual<ulong>(0x_1234_ffff_0000_2222, virtualMachine.PopQuad());
        Assert.AreEqual<ulong>(0x_0000_00ff_ff00_0000, virtualMachine.PopQuad());
        Assert.AreEqual<ulong>(0x_ff00_0000_0000_00ff, virtualMachine.PopQuad());
        Assert.AreEqual<ulong>(0x_0000_0000_0000_0000, virtualMachine.PopQuad());
    }
}