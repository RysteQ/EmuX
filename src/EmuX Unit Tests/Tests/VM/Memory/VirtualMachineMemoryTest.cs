using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.VM.Interfaces;

namespace EmuX_Unit_Tests.Tests.VM.Memory;

[TestClass]
public class VirtualMachineMemoryTest : TestWideInternalConstants
{
    [TestMethod]
    public void Randomness()
    {
        IVirtualMachine firstVirtualMachine = GenerateVirtualMachine();
        IVirtualMachine secondVirtualMachine = GenerateVirtualMachine();

        Assert.AreNotEqual<byte[]>(firstVirtualMachine.Memory.RAM, secondVirtualMachine.Memory.RAM, $"Both arrays are equal, they should be randomised, the probability of both of those array being the same for all {firstVirtualMachine.Memory.RAM.Length} elements is basically zero, rerun this test case specifically to make sure that there is an issue with the randomiser");
    }

    [TestMethod]
    public void SetAndGetBytes()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.SetByte(0, 0x_00);
        virtualMachine.SetByte(1, 0x_0a);
        virtualMachine.SetByte(2, 0x_18);
        virtualMachine.SetByte(3, 0x_1e);

        Assert.AreEqual<byte>(0x_00, virtualMachine.GetByte(0));
        Assert.AreEqual<byte>(0x_0a, virtualMachine.GetByte(1));
        Assert.AreEqual<byte>(0x_18, virtualMachine.GetByte(2));
        Assert.AreEqual<byte>(0x_1e, virtualMachine.GetByte(3));
    }

    [TestMethod]
    public void SetAndGetWords()
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
    public void SetAndGetDoubleWords()
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
    public void SetAndGetQuads()
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