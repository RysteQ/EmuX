using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.BIOS.Enums.SubInterrupts;
using EmuXCore.VM.Internal.BIOS.Internal;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuXCore.VM.Internal.BIOS;

public class VirtualBIOS : IVirtualBIOS
{
    public VirtualBIOS()
    {
        _diskInterruptHandler = new();
        _keyboardInterruptHandler = new();
        _rtcInterruptHandler = new();
        _serialInterruptHandler = new();
        _videoInterruptHandler = new();
    }

    public void HandleDiskInterrupt(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks, DiskInterrupt interruptCode)
    {
        byte totalAmountOfSectors = virtualCPU.GetRegister<VirtualRegisterRAX>().AL;
        byte selectedPlatter = virtualCPU.GetRegister<VirtualRegisterRDX>().DH;
        byte selectedTrack = virtualCPU.GetRegister<VirtualRegisterRCX>().CH;
        byte selectedSector = virtualCPU.GetRegister<VirtualRegisterRCX>().CL;
        byte disk = virtualCPU.GetRegister<VirtualRegisterRDX>().DL;
        uint addressReference = (uint)(virtualCPU.GetRegister<VirtualRegisterES>().ES << 16 | virtualCPU.GetRegister<VirtualRegisterRBX>().BX);
        IVirtualDisk? selectedDisk = virtualDisks.Where(selectedDisk => selectedDisk.DiskNumber == disk).FirstOrDefault();
        byte[] buffer;

        if (selectedDisk == null)
        {
            throw new DriveNotFoundException($"Drive with drive number {disk} not found");
        }

        buffer = new byte[selectedDisk.BytesPerSector];

        switch (interruptCode)
        {
            case DiskInterrupt.ReadTrack:
                for (int i = 0; i < totalAmountOfSectors; i++)
                {
                    buffer = selectedDisk.ReadSector(selectedPlatter, selectedTrack, selectedSector);

                    for (int j = 0; j < buffer.Length; j++)
                    {
                        virtualMemory.RAM[i * selectedDisk.BytesPerSector + j + addressReference] = buffer[j];
                    }
                }

                break;

            case DiskInterrupt.WriteTrack:
                for (int i = 0; i < totalAmountOfSectors; i++)
                {
                    for (int j = 0; j < buffer.Length; j++)
                    {
                        buffer[j] = virtualMemory.RAM[i * selectedDisk.BytesPerSector + j + addressReference];
                    }

                    selectedDisk.WriteSector(selectedPlatter, selectedTrack, selectedSector, buffer);
                }

                break;
        }
    }

    public void HandleKeyboardInterrupt(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks, KeyboardInterrupt interruptCode)
    {
        throw new NotImplementedException();
    }

    public void HandleRTCInterrupt(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks, RTCInterrupt interruptCode)
    {
        throw new NotImplementedException();
    }

    public void HandleSerialInterrupt(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks, SerialInterrupt interruptCode)
    {
        throw new NotImplementedException();
    }

    public void HandleVideoInterrupt(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks, VideoInterrupt interruptCode)
    {
        throw new NotImplementedException();
    }

    private DiskInterruptHandler _diskInterruptHandler;
    private KeyboardInterruptHandler _keyboardInterruptHandler;
    private RTCInterruptHandler _rtcInterruptHandler;
    private SerialInterruptHandler _serialInterruptHandler;
    private VideoInterruptHandler _videoInterruptHandler;
}