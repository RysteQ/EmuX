using EmuXCore.Common.Enums;

namespace EmuXCore.VM.Interfaces;

public interface IVirtualRegister
{
    /// <summary>
    /// Generic getter method, must return the value of the largest (in terms of binary digits) available register property
    /// </summary>
    /// <returns>The value of the register</returns>
    ulong Get();

    /// <summary>
    /// Generic setter method, must set the value of the largest (in terms of binary digits) available register property
    /// </summary>
    /// <param name="value">The value to set the register at</param>
    void Set(ulong value);

    /// <summary>
    /// Must contain all of the register names, example given is the RAX register, in that case this must contain the RAX, EAX, AX, AH, and AL register names
    /// </summary>
    Dictionary<string, Size> RegisterNamesAndSizes { get; }
}