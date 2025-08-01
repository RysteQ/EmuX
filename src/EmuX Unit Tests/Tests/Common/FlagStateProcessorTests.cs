using EmuXCoreUnitTests.Tests.InternalConstants;
using EmuXCore.Common.Enums;
using EmuXCore.InstructionLogic.Instructions.Interfaces;

namespace EmuXCoreUnitTests.Tests.Common;

[TestClass]
public sealed class FlagStateProcessorTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestCarryFlag_True()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual(true, flagStateProcessor.TestCarryFlag(ulong.MaxValue, 1, Size.Qword));
    }

    [TestMethod]
    public void TestCarryFlag_False()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual(false, flagStateProcessor.TestCarryFlag(ulong.MinValue, 1, Size.Qword));
    }

    [TestMethod]
    public void TestSignFlag_True()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual(true, flagStateProcessor.TestSignFlag(ulong.MaxValue, Size.Qword));
    }

    [TestMethod]
    public void TestSignFlag_False()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual(false, flagStateProcessor.TestSignFlag(0, Size.Qword));
    }

    [TestMethod]
    public void TestOverflowFlag_True()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual(true, flagStateProcessor.TestOverflowFlag(ulong.MaxValue, ulong.MaxValue, ulong.MaxValue - ulong.MaxValue, Size.Qword));
    }

    [TestMethod]
    public void TestOverflowFlag_False()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual(false, flagStateProcessor.TestOverflowFlag(1, 1, 1 + 1, Size.Qword));
    }

    [TestMethod]
    public void TestZeroFlag_True()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual(true, flagStateProcessor.TestZeroFlag(0));
    }

    [TestMethod]
    public void TestZeroFlag_False()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual(false, flagStateProcessor.TestZeroFlag(1));
    }

    [TestMethod]
    public void TestAuxilliaryFlagAddition_True()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual(true, flagStateProcessor.TestAuxilliaryFlag(15, 1, true));
    }

    [TestMethod]
    public void TestAuxilliaryFlagAddition_False()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual(false, flagStateProcessor.TestAuxilliaryFlag(0, 0, true));
    }

    [TestMethod]
    public void TestAuxilliaryFlagSubtraction_True()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual(true, flagStateProcessor.TestAuxilliaryFlag(15, 14, true));
    }

    [TestMethod]
    public void TestAuxilliaryFlagSubtraction_False()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();

        Assert.AreEqual(false, flagStateProcessor.TestAuxilliaryFlag(17, 1, false));
    }

    [TestMethod]
    public void TestParityFlag_True()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();


        Assert.AreEqual(true, flagStateProcessor.TestParityFlag(2));
    }

    [TestMethod]
    public void TestParityFlag_False()
    {
        IFlagStateProcessor flagStateProcessor = GenerateFlagStateProcessor();


        Assert.AreEqual(false, flagStateProcessor.TestParityFlag(1));
    }
}