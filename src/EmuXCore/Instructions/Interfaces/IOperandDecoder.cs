﻿using EmuXCore.Common.Interfaces;
using EmuXCore.VM.Interfaces;

namespace EmuXCore.Instructions.Interfaces;

public interface IOperandDecoder
{
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
    public int GetPointerMemoryAddress(IVirtualMemory memory, IOperand operand);

    /// <summary>
    /// Gets the instruction line the label is pointing at
    /// </summary>
    /// <param name="operand">The label operand</param>
    /// <returns>The instruction line the operand is pointing at</returns>
    public int GetInstructionMemoryAddress(IVirtualMemory memory, IOperand operand);
}