using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuX_Unit_Tests.Tests.VM.BIOS;

[TestClass]
public sealed class RTCInterruptTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestSetGetSystemClock_Success()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CH = 13;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL = 20;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH = 1;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DL = 0x_64;
        virtualMachine.BIOS.HandleRTCInterrupt(RTCInterrupt.SetSystemClock);

        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().RCX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().RDX = 0;
        virtualMachine.BIOS.HandleRTCInterrupt(RTCInterrupt.ReadSystemClock);

        Assert.AreEqual<byte>(13, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CH);
        Assert.AreEqual<byte>(20, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL);
        Assert.AreEqual<byte>(1, virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH);
        Assert.AreEqual<byte>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DL);
    }

    [TestMethod]
    public void TestSetGetRTC_Success()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CH = 13;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL = 20;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH = 1;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DL = 0x_64;
        virtualMachine.BIOS.HandleRTCInterrupt(RTCInterrupt.SetRTC);

        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().RCX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().RDX = 0;
        virtualMachine.BIOS.HandleRTCInterrupt(RTCInterrupt.ReadRTC);

        Assert.AreEqual<byte>(13, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CH);
        Assert.AreEqual<byte>(20, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL);
        Assert.AreEqual<byte>(1, virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH);
        Assert.AreEqual<byte>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DL);
    }
}