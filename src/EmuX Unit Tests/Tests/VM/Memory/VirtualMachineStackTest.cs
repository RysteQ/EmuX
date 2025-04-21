using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers;

namespace EmuX_Unit_Tests.Tests.VM.Memory;

[TestClass]
public sealed class VirtualMachineStackTest : TestWideInternalConstants
{
    [TestMethod]
    public void PushAndPopBytes()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.PushByte(0x_00);
        virtualMachine.PushByte(0x_0a);
        virtualMachine.PushByte(0x_18);
        virtualMachine.PushByte(0x_1e);

        Assert.AreEqual<ulong>((ulong)(virtualMachine.Memory.RAM.Length - 5), virtualMachine.CPU.GetRegister<VirtualRegisterRSP>().RSP);
        Assert.AreEqual<byte>(0x_1e, virtualMachine.PopByte());
        Assert.AreEqual<ulong>((ulong)(virtualMachine.Memory.RAM.Length - 4), virtualMachine.CPU.GetRegister<VirtualRegisterRSP>().RSP);
        Assert.AreEqual<byte>(0x_18, virtualMachine.PopByte());
        Assert.AreEqual<ulong>((ulong)(virtualMachine.Memory.RAM.Length - 3), virtualMachine.CPU.GetRegister<VirtualRegisterRSP>().RSP);
        Assert.AreEqual<byte>(0x_0a, virtualMachine.PopByte());
        Assert.AreEqual<ulong>((ulong)(virtualMachine.Memory.RAM.Length - 2), virtualMachine.CPU.GetRegister<VirtualRegisterRSP>().RSP);
        Assert.AreEqual<byte>(0x_00, virtualMachine.PopByte());
        Assert.AreEqual<ulong>((ulong)(virtualMachine.Memory.RAM.Length - 1), virtualMachine.CPU.GetRegister<VirtualRegisterRSP>().RSP);
    }

    [TestMethod]
    public void PushAndPopWords()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.SetWord(0, 0x_0000);
        virtualMachine.SetWord(2, 0x_f00f);
        virtualMachine.SetWord(4, 0x_0ff0);
        virtualMachine.SetWord(6, 0x_ffff);

        Assert.AreEqual<ushort>(0x_0000, virtualMachine.GetWord(0));
        Assert.AreEqual<ushort>(0x_f00f, virtualMachine.GetWord(2));
        Assert.AreEqual<ushort>(0x_0ff0, virtualMachine.GetWord(4));
        Assert.AreEqual<ushort>(0x_ffff, virtualMachine.GetWord(6));
    }

    [TestMethod]
    public void PushAndPopDoubleWords()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.SetDouble(0, 0x_0000_0000);
        virtualMachine.SetDouble(4, 0x_f000_000f);
        virtualMachine.SetDouble(8, 0x_000f_f000);
        virtualMachine.SetDouble(12, 0x_ff0f_0ff0);

        Assert.AreEqual<uint>(0x_0000_0000, virtualMachine.GetDouble(0));
        Assert.AreEqual<uint>(0x_f000_000f, virtualMachine.GetDouble(4));
        Assert.AreEqual<uint>(0x_000f_f000, virtualMachine.GetDouble(8));
        Assert.AreEqual<uint>(0x_ff0f_0ff0, virtualMachine.GetDouble(12));
    }

    [TestMethod]
    public void PushAndPopQuads()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.SetQuad(0, 0x_0000_0000_0000_0000);
        virtualMachine.SetQuad(8, 0x_ff00_0000_0000_00ff);
        virtualMachine.SetQuad(16, 0x_0000_00ff_ff00_0000);
        virtualMachine.SetQuad(24, 0x_1234_ffff_0000_2222);

        Assert.AreEqual<ulong>(0x_0000_0000_0000_0000, virtualMachine.GetQuad(0));
        Assert.AreEqual<ulong>(0x_ff00_0000_0000_00ff, virtualMachine.GetQuad(8));
        Assert.AreEqual<ulong>(0x_0000_00ff_ff00_0000, virtualMachine.GetQuad(16));
        Assert.AreEqual<ulong>(0x_1234_ffff_0000_2222, virtualMachine.GetQuad(24));
    }
}