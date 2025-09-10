using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.BIOS.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.VM.Internal.BIOS.Internal;

public class DeviceInterruptHandler : IDeviceInterruptHandler
{
    public void ExecuteLogic(IVirtualCPU virtualCPU, IVirtualDevice[] virtualDevices)
    {
        IVirtualDevice? deviceToExecute = virtualDevices.SingleOrDefault(selectedVirtualDevice => selectedVirtualDevice.DeviceId == virtualCPU.GetRegister<VirtualRegisterRAX>().AL);

        if (deviceToExecute == null)
        {
            throw new ArgumentNullException($"The device with the ID of {virtualCPU.GetRegister<VirtualRegisterRAX>().AL} cannot be found");
        }

        deviceToExecute.Execute();
    }
}