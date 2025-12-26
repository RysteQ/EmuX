using EmuXUI.Models.Events;
using EmuXUI.Models.Internal;
using System;

namespace EmuXUI.Models.Observable;

public sealed class MemoryByte : ObservableObject
{
    public MemoryByte(int index, byte value)
    {
        Index = index;
        Value = value;
    }

    public event EventHandler? ValueChanged;

    public int Index { get; init; }
    public byte Value
    {
        get => field;
        set
        {
            ValueChanged?.Invoke(this, new WrittenMemoryByteEvent(Index, field, value));

            OnPropertyChanged(ref field, value);
        }
    }
}