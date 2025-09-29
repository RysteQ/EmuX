namespace EmuXCore.VM.Enums;

/// <summary>
/// Indicates the action taken in the VM to modify its current state
/// </summary>
public enum VmActionCategory : byte
{
    ModifiedRegister,
    ModifiedMemory,
    ModifiedDisk,

    NaN
}