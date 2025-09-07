using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Components.Events;

namespace EmuXCore.VM.Events;

public class StackAccess : EventArgs, IStackAccess
{
    public StackAccess(Size size, bool pushOrPop, ulong value)
    {
        Size = size;
        PushOrPop = pushOrPop;
        Value = value;
    }

    public Size Size { get; init; }
    public bool PushOrPop { get; init; }
    public ulong Value { get; init; }
}