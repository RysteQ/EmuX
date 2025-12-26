using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.Memory;

public class VirtualMemory : IVirtualMemory
{
    public VirtualMemory(uint ioMemory, uint generalPurposeMemory, IVirtualMachine? parentVirtualMachine = null)
    {
        IO_MEMORY = ioMemory;
        GENERAL_PURPOSE_MEMORY = generalPurposeMemory;

        RAM = new byte[IO_MEMORY + GENERAL_PURPOSE_MEMORY];
        LabelMemoryLocations = new Dictionary<string, IMemoryLabel>();

        Random.Shared.NextBytes(RAM);

        ParentVirtualMachine = parentVirtualMachine;
    }

    public byte[] RAM { get; set; }

    public IDictionary<string, IMemoryLabel> LabelMemoryLocations { get; set; }

    public uint IO_MEMORY { get; init; }
    public uint GENERAL_PURPOSE_MEMORY { get; init; }
    public IVirtualMachine? ParentVirtualMachine { get; set; }
}