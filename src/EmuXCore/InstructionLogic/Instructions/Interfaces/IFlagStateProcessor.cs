using EmuXCore.Common.Enums;

namespace EmuXCore.InstructionLogic.Instructions.Interfaces;

public interface IFlagStateProcessor
{
    /// <summary>
    /// Check the carry flag status
    /// </summary>
    /// <param name="firstValue">The first value of the mathematical operation</param>
    /// <param name="secondValue">The second value of the mathematical operation</param>
    /// <param name="size">The size of the operation, doesn't matter if the method requires 64 bit unsigned integers, this value must be provided</param>
    /// <returns>The status of the carry flag</returns>
    bool TestCarryFlag(ulong firstValue, ulong secondValue, Size size);

    /// <summary>
    /// Check the sign flag status
    /// </summary>
    /// <param name="firstValue">The value to check the sign of</param>
    /// <param name="size">The size of the value, doesn't matter if the method requires 64 bit unsigned integers, this value must be provided</param>
    /// <returns>The status of the sign flag</returns>
    bool TestSignFlag(ulong firstValue, Size size);

    /// <summary>
    /// Checks the overflow flag status
    /// </summary>
    /// <param name="firstValue">The first value of the mathematical operation</param>
    /// <param name="secondValue">The second value of the mathematical operation</param>
    /// <param name="newValue">The result of the mathematical operation</param>
    /// <param name="size">The size of the values, doesn't matter if the method requires 64 bit unsigned integers, this value must be provided</param>
    /// <returns>The status of the overflow flag</returns>
    bool TestOverflowFlag(ulong firstValue, ulong secondValue, ulong newValue, Size size);

    /// <summary>
    /// Checks the zero flag status
    /// </summary>
    /// <param name="value">The value to check</param>
    /// <returns>The status of the zero flag</returns>
    bool TestZeroFlag(ulong value);

    /// <summary>
    /// Checks the auxiliarry flag status
    /// </summary>
    /// <param name="firstValue">The first value to check</param>
    /// <param name="secondValue">The second value to check</param>
    /// <param name="addition">True if the operation is an "add" operation, otherwise false for a subtraction operation</param>
    /// <returns>The status of the auxilliary flag</returns>
    bool TestAuxilliaryFlag(ulong firstValue, ulong secondValue, bool addition);

    /// <summary>
    /// Checks the partity flag status
    /// </summary>
    /// <param name="value">The value to check</param>
    /// <returns>The status of the parity flag</returns>
    bool TestParityFlag(ulong value);
}