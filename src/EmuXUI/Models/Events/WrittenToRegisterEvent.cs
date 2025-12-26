using System;

namespace EmuXUI.Models.Events;

public sealed class WrittenToRegisterEvent : EventArgs
{
    public WrittenToRegisterEvent(string registerName, ulong previousValue, ulong newValue)
    {
        RegisterName = registerName;
        PreviousValue = previousValue;
        NewValue = newValue;
    }

    public string RegisterName { get; init; }
    public ulong PreviousValue { get; init; }
    public ulong NewValue { get; init; }
}