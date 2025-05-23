using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuX_Unit_Tests.Tests.VM.Registers;

[TestClass]
public sealed class FlagsIOPLTests : TestWideInternalConstants
{
    [TestMethod]
    public void SetIOPLToZero()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.SetIOPL(false, false);

        Assert.AreEqual<byte>(0, virtualMachine.GetIOPL());
    }

    [TestMethod]
    public void SetIOPLToOne()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.SetIOPL(false, true);

        Assert.AreEqual<byte>(1, virtualMachine.GetIOPL());
    }

    [TestMethod]
    public void SetIOPLToTwo()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.SetIOPL(true, false);

        Assert.AreEqual<byte>(2, virtualMachine.GetIOPL());
    }

    [TestMethod]
    public void SetIOPLToThree()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.SetIOPL(true, true);

        Assert.AreEqual<byte>(3, virtualMachine.GetIOPL());
    }
}