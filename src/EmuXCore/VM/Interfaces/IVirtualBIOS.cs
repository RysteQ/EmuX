using EmuXCore.VM.Internal.BIOS.Enums.SubInterrupts;

namespace EmuXCore.VM.Interfaces;

public interface IVirtualBIOS
{
    void HandleKeyboardInterrupt(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks, KeyboardInterrupt interruptCode);
    void HandleVideoInterrupt(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks, VideoInterrupt interruptCode);
    void HandleDiskInterrupt(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks, DiskInterrupt interruptCode);
    void HandleSerialInterrupt(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks, SerialInterrupt interruptCode);
    void HandleRTCInterrupt(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks, RTCInterrupt interruptCode);

    Dictionary<KeyboardInterrupt, EventHandler> KeyboardEvents { get; init; }
    Dictionary<VideoInterrupt, EventHandler> VideoEvents { get; init; }
    Dictionary<DiskInterrupt, EventHandler> DiskEvents { get; init; }
    Dictionary<SerialInterrupt, EventHandler> SerialEvents { get; init; }
    Dictionary<RTCInterrupt, EventHandler> RTCEvents { get; init; }
}