using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Internal.BIOS.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuXCore.VM.Internal.BIOS.Internal;

public class DiskInterruptHandler : IDiskInterruptHandler
{
    public void ReadTrack(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks)
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

        for (int i = 0; i < totalAmountOfSectors; i++)
        {
            buffer = selectedDisk.ReadSector(selectedPlatter, selectedTrack, selectedSector);

            for (int j = 0; j < buffer.Length; j++)
            {
                virtualMemory.RAM[i * selectedDisk.BytesPerSector + j + addressReference] = buffer[j];
            }
        }
    }

    public void WriteTrack(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks)
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

        for (int i = 0; i < totalAmountOfSectors; i++)
        {
            for (int j = 0; j < buffer.Length; j++)
            {
                buffer[j] = virtualMemory.RAM[i * selectedDisk.BytesPerSector + j + addressReference];
            }

            selectedDisk.WriteSector(selectedPlatter, selectedTrack, selectedSector, buffer);
        }
    }
}