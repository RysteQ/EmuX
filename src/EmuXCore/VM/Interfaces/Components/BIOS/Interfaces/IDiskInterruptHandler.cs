using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Interfaces.Components.BIOS.Interfaces;

public interface IDiskInterruptHandler
{
    void ReadTrack(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks);
    void WriteTrack(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks);
}