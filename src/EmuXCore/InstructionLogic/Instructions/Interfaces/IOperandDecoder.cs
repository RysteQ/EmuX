using EmuXCore.Common.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.InstructionLogic.Instructions.Interfaces;

/// <summary>
/// The purpose of the IOperandDecoder is for runtime decoding of the <see cref="IOperand" /> interface for the <see cref="IInstruction.Execute"/> method to emulate the proper behaviour of instructions.
/// </summary>
public interface IOperandDecoder
{
    /// <summary>
    /// Gets the value of the <see cref="IOperand"/> regardless of size, it does automatic casting.
    /// </summary>
    /// <param name="virtualMachine">The <see cref="IVirtualMachine"/> implementation to get the value of the <see cref="IOperand"/> from.</param>
    /// <param name="operand">The <see cref="IOperand"/> to get the value of.</param>
    /// <returns>The value of the <see cref="IOperand"/>.</returns>
    public ulong GetOperandValue(IVirtualMachine virtualMachine, IOperand operand);

    /// <summary>
    /// Gets the value of the <see cref="IOperand"/>.
    /// </summary>
    /// <param name="virtualMachine">The <see cref="IVirtualMachine"/> implementation to get the value of the <see cref="IOperand"/> from.</param>
    /// <param name="operand">The <see cref="IOperand"/> to get the value of.</param>
    /// <returns>The value of the <see cref="IOperand"/>.</returns>
    public ulong GetOperandQuad(IVirtualMachine virtualMachine, IOperand operand);

    /// <summary>
    /// Gets the value of the <see cref="IOperand"/>.
    /// </summary>
    /// <param name="virtualMachine">The <see cref="IVirtualMachine"/> implementation to get the value of the <see cref="IOperand"/> from.</param>
    /// <param name="operand">The <see cref="IOperand"/> to get the value of.</param>
    /// <returns>The value of the <see cref="IOperand"/>.</returns>
    public uint GetOperandDouble(IVirtualMachine virtualMachine, IOperand operand);

    /// <summary>
    /// Gets the value of the <see cref="IOperand"/>.
    /// </summary>
    /// <param name="virtualMachine">The <see cref="IVirtualMachine"/> implementation to get the value of the <see cref="IOperand"/> from.</param>
    /// <param name="operand">The <see cref="IOperand"/> to get the value of.</param>
    /// <returns>The value of the <see cref="IOperand"/>.</returns>
    public ushort GetOperandWord(IVirtualMachine virtualMachine, IOperand operand);

    /// <summary>
    /// Gets the value of the <see cref="IOperand"/>.
    /// </summary>
    /// <param name="virtualMachine">The <see cref="IVirtualMachine"/> implementation to get the value of the <see cref="IOperand"/> from.</param>
    /// <param name="operand">The <see cref="IOperand"/> to get the value of.</param>
    /// <returns>The value of the <see cref="IOperand"/>.</returns>
    public byte GetOperandByte(IVirtualMachine virtualMachine, IOperand operand);

    /// <summary>
    /// Gets the 32bit address the <see cref="IOperand"/> is pointing at.
    /// </summary>
    /// <param name="operand">The pointer inside the <see cref="IOperand"/> implementation.</param>
    /// <returns>The address the <see cref="IOperand"/> implementation is pointing at.</returns>
    public int GetPointerMemoryAddress(IVirtualMachine virtualMachine, IOperand operand);

    /// <summary>
    /// Gets the <see cref="IInstruction"/> line the label is pointing at.
    /// </summary>
    /// <param name="operand">The label inside the <see cref="IOperand"/> implementation.</param>
    /// <returns>The instruction line the <see cref="IOperand"/> implementation is pointing at.</returns>
    public int GetInstructionMemoryAddress(IVirtualMemory memory, IOperand operand);
}