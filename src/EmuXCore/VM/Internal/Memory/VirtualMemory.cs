using EmuXCore.VM.Interfaces;

namespace EmuXCore.VM.Internal.Memory;

public class VirtualMemory : IVirtualMemory
{
    public VirtualMemory()
    {
        RAM = new byte[VIDEO_MEMORY + GENERAL_PURPOSE_MEMORY];
        LabelMemoryLocations = [];

        Random.Shared.NextBytes(RAM);
    }

    public byte[] RAM { get; set; }
    public Dictionary<string, IMemoryLabel> LabelMemoryLocations { get; set; }

    public uint VIDEO_MEMORY { get; } = 921_600;
    public uint GENERAL_PURPOSE_MEMORY { get; } = 1_048_576;
}