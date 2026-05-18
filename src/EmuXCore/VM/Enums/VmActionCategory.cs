using EmuXCore.VM.Interfaces;

namespace EmuXCore.VM.Enums;

/// <summary>
/// Indicates the action taken in the <see cref="IVirtualMachine"/> to modify its current state.
/// </summary>
public enum VmActionCategory : byte
{
    ModifiedRegister,
    ModifiedMemory,
    ModifiedDisk,
    ModifiedDevice,
    ModifiedGpu,

    NaN
}