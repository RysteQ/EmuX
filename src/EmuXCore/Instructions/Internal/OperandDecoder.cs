using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.VM.Interfaces;
using System.Numerics;

namespace EmuXCore.Instructions.Internal;

public class OperandDecoder : IOperandDecoder
{
    public ulong GetOperandQuad(IVirtualMachine virtualMachine, IOperand operand)
    {
        return GetOperandValue(virtualMachine, operand);
    }

    public uint GetOperandDouble(IVirtualMachine virtualMachine, IOperand operand)
    {
        return (uint)GetOperandValue(virtualMachine, operand);
    }

    public ushort GetOperandWord(IVirtualMachine virtualMachine, IOperand operand)
    {
        return (ushort)GetOperandValue(virtualMachine, operand);
    }

    public byte GetOperandByte(IVirtualMachine virtualMachine, IOperand operand)
    {
        return (byte)GetOperandValue(virtualMachine, operand);
    }
    
    private ulong GetOperandValue(IVirtualMachine virtualMachine, IOperand operand)
    {
        return operand.Variant switch
        {
            OperandVariant.Value => GetOperandValueOfTypeValue(operand),
            OperandVariant.Register => GetOperandValueOfTypeRegister(virtualMachine, operand),
            OperandVariant.Memory => GetOperandValueOfTypeMemory(virtualMachine, operand),
        };
    }

    private ulong GetOperandValueOfTypeValue(IOperand operand)
    {
        string operandValue = operand.FullOperand.ToUpper();

        return operandValue.Last() switch
        {
            'H' => Convert.ToUInt64(operandValue[..^1], 16),
            'B' => Convert.ToUInt64(operandValue[..^1], 2),
            '\'' => operandValue[1],
            _ => Convert.ToUInt64(operandValue, 10)
        };
    }

    private ulong GetOperandValueOfTypeRegister(IVirtualMachine virtualMachine, IOperand operand)
    {
        return virtualMachine.CPU.GetRegister(operand.FullOperand).Get();
    }

    private ulong GetOperandValueOfTypeMemory(IVirtualMachine virtualMachine, IOperand operand)
    {
        return operand.OperandSize switch
        {
            Size.Byte => virtualMachine.GetByte(GetPointerMemoryAddress(virtualMachine.Memory, operand)),
            Size.Word => virtualMachine.GetWord(GetPointerMemoryAddress(virtualMachine.Memory, operand)),
            Size.Double => virtualMachine.GetDouble(GetPointerMemoryAddress(virtualMachine.Memory, operand)),
            Size.Quad => virtualMachine.GetQuad(GetPointerMemoryAddress(virtualMachine.Memory, operand)),
        };
    }

    public int GetPointerMemoryAddress(IVirtualMemory memory, IOperand operand)
    {
        return memory.LabelMemoryLocations[operand.MemoryLabel].Address;
    }

    public int GetInstructionMemoryAddress(IVirtualMemory memory, IOperand operand)
    {
        return memory.LabelMemoryLocations[operand.MemoryLabel].Line;
    }
}