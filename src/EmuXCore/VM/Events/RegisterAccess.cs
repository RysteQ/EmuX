using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Events;

namespace EmuXCore.VM.Events;

public class RegisterAccess : EventArgs, IRegisterAccess
{
    public RegisterAccess(string registerName, Size size, bool write, ulong previousValue, ulong newValue)
    {
        RegisterName = registerName;
        Size = size;
        Write = write;
        PreviousValue = previousValue;
        NewValue = newValue;
    }

    public string RegisterName { get; init; }
    public Size Size { get; init; }
    public bool Write { get; set; }
    public ulong PreviousValue { get; init; }
    public ulong NewValue { get; init; }
}