using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces.Events;

namespace EmuXCore.VM.Events;

public class FlagAccess : EventArgs, IFlagAccess
{
    public FlagAccess(EFlags flagAccessed, bool readOrWrite, bool previousValue, bool auxiliaryPreviousValue, bool newValue, bool auxiliaryNewValue)
    {
        FlagAccessed = flagAccessed;
        ReadOrWrite = readOrWrite;
        PreviousValue = previousValue;
        AuxiliaryPreviousValue = auxiliaryPreviousValue;
        NewValue = newValue;
        AuxiliaryNewValue = auxiliaryNewValue;
    }

    public EFlags FlagAccessed { get; init; }
    public bool ReadOrWrite { get; init; }
    public bool PreviousValue { get; init; }
    public bool AuxiliaryPreviousValue { get; init; }
    public bool NewValue { get; init; }
    public bool AuxiliaryNewValue { get; init; }
}