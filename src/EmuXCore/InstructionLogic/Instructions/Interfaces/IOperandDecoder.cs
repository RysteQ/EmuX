using EmuXCore.Common.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.InstructionLogic.Instructions.Interfaces;

/// <summary>
/// The purpose of the IOperandDecoder is for runtime decoding of the IOperand interface for the IInstruction.Execute method to emulate the proper behaviour of instructions
/// </summary>
public interface IOperandDecoder
{
    /// <summary>
    /// Gets the value of the operand regardless of size, it does automatic casting
    /// </summary>
    /// <param name="virtualMachine">The virtual machine to get the value of the operand from</param>
    /// <param name="operand">The operand to get the value of</param>
    /// <returns>The value of the operand</returns>
    public ulong GetOperandValue(IVirtualMachine virtualMachine, IOperand operand);

    /// <summary>
    /// Gets the value of the operand
    /// </summary>
    /// <param name="virtualMachine">The virtual machine to get the value of the operand from</param>
    /// <param name="operand">The operand to get the value of</param>
    /// <returns>The value of the operand</returns>
    public ulong GetOperandQuad(IVirtualMachine virtualMachine, IOperand operand);

    /// <summary>
    /// Gets the value of the operand
    /// </summary>
    /// <param name="virtualMachine">The virtual machine to get the value of the operand from</param>
    /// <param name="operand">The operand to get the value of</param>
    /// <returns>The value of the operand</returns>
    public uint GetOperandDouble(IVirtualMachine virtualMachine, IOperand operand);

    /// <summary>
    /// Gets the value of the operand
    /// </summary>
    /// <param name="virtualMachine">The virtual machine to get the value of the operand from</param>
    /// <param name="operand">The operand to get the value of</param>
    /// <returns>The value of the operand</returns>
    public ushort GetOperandWord(IVirtualMachine virtualMachine, IOperand operand);

    /// <summary>
    /// Gets the value of the operand
    /// </summary>
    /// <param name="virtualMachine">The virtual machine to get the value of the operand from</param>
    /// <param name="operand">The operand to get the value of</param>
    /// <returns>The value of the operand</returns>
    public byte GetOperandByte(IVirtualMachine virtualMachine, IOperand operand);

    /// <summary>
    /// Gets the 32bit address the operand is pointing at
    /// </summary>
    /// <param name="operand">The pointer operand</param>
    /// <returns>The address the operand is pointing at</returns>
    public int GetPointerMemoryAddress(IVirtualMachine virtualMachine, IOperand operand);

    /// <summary>
    /// Gets the instruction line the label is pointing at
    /// </summary>
    /// <param name="operand">The label operand</param>
    /// <returns>The instruction line the operand is pointing at</returns>
    public int GetInstructionMemoryAddress(IVirtualMemory memory, IOperand operand);
}