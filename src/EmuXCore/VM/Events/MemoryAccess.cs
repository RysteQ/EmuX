using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Components.Events;

namespace EmuXCore.VM.Events;

public class MemoryAccess : EventArgs, IMemoryAccess
{
    public MemoryAccess(int memoryAddress, Size size, bool readOrWrite, ulong previousValue, ulong newValue)
    {
        MemoryAddress = memoryAddress;
        Size = size;
        ReadOrWrite = readOrWrite;
        PreviousValue = previousValue;
        NewValue = newValue;
    }

    public int MemoryAddress { get; init; }
    public Size Size { get; init; }
    public bool ReadOrWrite { get; init; }
    public ulong PreviousValue { get; init; }
    public ulong NewValue { get; init; }
}