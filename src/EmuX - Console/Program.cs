using EmuX_Console.Libraries;
using EmuX_Console.Libraries.Enums;
using EmuX_Console.Libraries.Interfaces;
using EmuXCore.VM;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.BIOS;
using EmuXCore.VM.Internal.BIOS.Internal;
using EmuXCore.VM.Internal.CPU;
using EmuXCore.VM.Internal.Device.USBDrives;
using EmuXCore.VM.Internal.Disk;
using EmuXCore.VM.Internal.GPUs;
using EmuXCore.VM.Internal.Memory;
using EmuXCore.VM.Internal.RTC;

string input = string.Empty;
ITerminalIOHandler terminalIOHandler = new TerminalIOHandler("> ", ConsoleKey.UpArrow, ConsoleKey.DownArrow);
IVirtualMachineBuilder virtualMachineBuilder = new VirtualMachineBuilder();
IVirtualMachine virtualMachine = virtualMachineBuilder
            .SetCpu(new VirtualCPU())
            .SetMemory(new VirtualMemory())
            .SetBios(new VirtualBIOS(new DiskInterruptHandler(), new RTCInterruptHandler(), new VideoInterruptHandler(), new DeviceInterruptHandler()))
            .SetRTC(new VirtualRTC())
            .AddDisk(new VirtualDisk(1, 4, 16, 64))
            .AddDisk(new VirtualDisk(2, 4, 12, 24))
            .SetGPU(new VirtualGPU())
            .AddVirtualDevice(new UsbDrive64Kb(1))
            .Build();

Console.Clear();

terminalIOHandler.Output(virtualMachine);

while (true)
{
    input = terminalIOHandler.GetUserInput();

    terminalIOHandler.Output("Input -> " + input, OutputSeverity.Normal);
    terminalIOHandler.Output("Input -> " + input, OutputSeverity.Error);
}