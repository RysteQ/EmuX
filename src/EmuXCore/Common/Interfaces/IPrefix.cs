using EmuXCore.VM.Interfaces;

namespace EmuXCore.Common.Interfaces;

/// <summary>
/// This is used to structure prefixes for all instruction types
/// </summary>
public interface IPrefix
{
    /// <summary>
    /// Used to execute the prefix logic
    /// </summary>
    /// <param name="instruction">The instruction to loop through</param>
    /// <param name="virtualMachine">The virtual machine the prefix is executed under</param>
    void Loop(IInstruction instruction, IVirtualMachine virtualMachine);

    /// <summary>
    /// The type of the prefix, used to check the validity of some instructions since prefixes may or may not be valid with some instructions
    /// </summary>
    public Type Type { get; }

    /// <summary>
    /// The full operand of the prefix as read from the source code
    /// </summary>
    public string Prefix { get; }
}