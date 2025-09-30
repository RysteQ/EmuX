using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.Memory;

public class VirtualMemory : IVirtualMemory
{
    public VirtualMemory(IVirtualMachine? parentVirtualMachine = null)
    {
        _ram = new byte[IO_MEMORY + VIDEO_MEMORY + GENERAL_PURPOSE_MEMORY];
        LabelMemoryLocations = new Dictionary<string, IMemoryLabel>();

        Random.Shared.NextBytes(RAM);

        ParentVirtualMachine = parentVirtualMachine;
    }

    public byte[] RAM
    {
        get => _ram;
        set
        {
            List<(int Index, byte Item)> modificationIndexes = _ram.Index().Where(selectedByte => selectedByte.Item != value[selectedByte.Index]).ToList();

            ParentVirtualMachine?.RegisterAction(
                VmActionCategory.ModifiedMemory,
                Size.Byte,
                _ram.Skip(modificationIndexes.First().Index).Take(modificationIndexes.Last().Index + 1).ToArray(),
                value.Skip(modificationIndexes.First().Index).Take(modificationIndexes.Last().Index + 1).ToArray(),
                memoryPointer: modificationIndexes.First().Index);

            _ram = value;
        }
    }

    public IDictionary<string, IMemoryLabel> LabelMemoryLocations { get; set; }

    public uint IO_MEMORY { get; } = 65_536;
    public uint VIDEO_MEMORY { get; } = 921_600;
    public uint GENERAL_PURPOSE_MEMORY { get; } = 1_048_576;
    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private byte[] _ram;
}