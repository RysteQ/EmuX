using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.Memory;

public class VirtualMemory : IVirtualMemory
{
    public VirtualMemory(IVirtualMachine? parentVirtualMachine = null)
    {
        RAM = new byte[IO_MEMORY + VIDEO_MEMORY + GENERAL_PURPOSE_MEMORY];
        LabelMemoryLocations = new Dictionary<string, IMemoryLabel>();

        Random.Shared.NextBytes(RAM);

        ParentVirtualMachine = parentVirtualMachine;
    }

    public byte[] RAM { get; set; }

    public IDictionary<string, IMemoryLabel> LabelMemoryLocations { get; set; }

    public uint IO_MEMORY { get; } = 65_536;
    public uint VIDEO_MEMORY { get; } = 921_600;
    public uint GENERAL_PURPOSE_MEMORY { get; } = 1_048_576;
    public IVirtualMachine? ParentVirtualMachine { get; set; }
}