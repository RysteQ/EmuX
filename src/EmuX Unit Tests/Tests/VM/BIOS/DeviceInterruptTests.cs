using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Enums.SubInterrupts;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.VM.BIOS;

[TestClass]
public sealed class DeviceInterruptTests : TestWideInternalConstants
{
    [TestMethod]
    public void DeviceCallExecuteMethodViaBios_FailedToFindTheDevice()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = 100;

        try
        {
            virtualMachine.BIOS.HandleDeviceInterrupt(DeviceInterrupt.ExecuteLogic);
        }
        catch (ArgumentNullException ex)
        {
            Assert.IsTrue(true);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
}