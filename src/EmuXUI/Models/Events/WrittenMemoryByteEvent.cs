using System;

namespace EmuXUI.Models.Events;

public sealed class WrittenMemoryByteEvent : EventArgs
{
    public WrittenMemoryByteEvent(int index, byte previousValue, byte newValue)
    {
        Index = index;
        PreviousValue = previousValue;
        NewValue = newValue;
    }

    public int Index { get; init; }
    public byte PreviousValue { get; init; }
    public byte NewValue { get; init; }
}