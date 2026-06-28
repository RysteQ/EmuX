using System.Collections.Generic;
using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Exceptions;

namespace EmuXCore.VM.Interfaces.Components.Internal;

/// <summary>
/// This is used to implement any register for the <see cref="IVirtualCPU"/> module.
/// </summary>
public interface IVirtualRegister : IVirtualComponent
{
    /// <summary>
    /// Generic getter method.
    /// </summary>
    /// <returns>The the largest (in terms of binary digits) available register property value.</returns>
    ulong Get();

    /// <summary>
    /// Generic setter method.
    /// </summary>
    /// <param name="value">The value to set the register at.</param>
    /// <param name="register">The name of the register to set the value at, it is case insensitive.</param>
    /// <exception cref="VirtualRegisterNotFoundException" />
    void Set(string register, ulong value);

    /// <summary>
    /// The name of the register.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Must contain all of the register names, example given is the RAX register, in that case this must contain the RAX, EAX, AX, AH, and AL register names <br/>
    /// The first element is the largest size and the last element is of the smallest size
    /// </summary>
    IDictionary<string, Size> RegisterNamesAndSizes { get; }
}