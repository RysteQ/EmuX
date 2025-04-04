using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.Common.Enums;
using EmuXCore.Instructions.Interfaces;

namespace EmuX_Unit_Tests.Tests.Instructions.Internal;

[TestClass]
public sealed class FlagStateProcessorTest : TestWideInternalConstants
{
    [TestMethod]
    public void TestCarryFlag_True()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual<bool>(true, flagStateProcessor.TestCarryFlag(ulong.MaxValue, 1, Size.Quad));
    }

    [TestMethod]
    public void TestCarryFlag_False()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual<bool>(false, flagStateProcessor.TestCarryFlag(ulong.MinValue, 1, Size.Quad));
    }

    [TestMethod]
    public void TestSignFlag_True()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual<bool>(true, flagStateProcessor.TestSignFlag(ulong.MaxValue, Size.Quad));
    }

    [TestMethod]
    public void TestSignFlag_False()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual<bool>(false, flagStateProcessor.TestSignFlag(0, Size.Quad));
    }

    [TestMethod]
    public void TestOverflowFlag_True()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual<bool>(true, flagStateProcessor.TestOverflowFlag(ulong.MaxValue, ulong.MaxValue, ulong.MaxValue - ulong.MaxValue, Size.Quad));
    }

    [TestMethod]
    public void TestOverflowFlag_False()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual<bool>(false, flagStateProcessor.TestOverflowFlag(1, 1, 1 + 1, Size.Quad));
    }

    [TestMethod]
    public void TestZeroFlag_True()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual<bool>(true, flagStateProcessor.TestZeroFlag(0));
    }

    [TestMethod]
    public void TestZeroFlag_False()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual<bool>(false, flagStateProcessor.TestZeroFlag(1));
    }

    [TestMethod]
    public void TestAuxilliaryFlag_True()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual<bool>(true, flagStateProcessor.TestAuxilliaryFlag(15, 1));
    }

    [TestMethod]
    public void TestAuxilliaryFlag_False()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual<bool>(false, flagStateProcessor.TestAuxilliaryFlag(0, 0));
    }

    [TestMethod]
    public void TestParityFlag_True()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();


        Assert.AreEqual<bool>(true, flagStateProcessor.TestParityFlag(2));
    }

    [TestMethod]
    public void TestParityFlag_False()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();


        Assert.AreEqual<bool>(false, flagStateProcessor.TestParityFlag(1));
    }
}