using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;
using System.Collections.Generic;

namespace EmuXCore.Instructions.Internal;

public class OperandDecoder : IOperandDecoder
{
    public ulong GetOperandValue(IVirtualMachine virtualMachine, IOperand operand)
    {
        return operand.OperandSize switch
        {
            Size.Byte => GetOperandByte(virtualMachine, operand),
            Size.Word => GetOperandWord(virtualMachine, operand),
            Size.Double => GetOperandDouble(virtualMachine, operand),
            Size.Quad => GetOperandQuad(virtualMachine, operand),
            _ => throw new NotImplementedException()
        };
    }

    public ulong GetOperandQuad(IVirtualMachine virtualMachine, IOperand operand)
    {
        return GetOperandValueInternal(virtualMachine, operand);
    }

    public uint GetOperandDouble(IVirtualMachine virtualMachine, IOperand operand)
    {
        return (uint)GetOperandValueInternal(virtualMachine, operand);
    }

    public ushort GetOperandWord(IVirtualMachine virtualMachine, IOperand operand)
    {
        return (ushort)GetOperandValueInternal(virtualMachine, operand);
    }

    public byte GetOperandByte(IVirtualMachine virtualMachine, IOperand operand)
    {
        return (byte)GetOperandValueInternal(virtualMachine, operand);
    }

    private ulong GetOperandValueInternal(IVirtualMachine virtualMachine, IOperand operand)
    {
        return operand.Variant switch
        {
            OperandVariant.Value => GetOperandValueOfTypeValue(operand.FullOperand),
            OperandVariant.Register => GetOperandValueOfTypeRegister(virtualMachine, operand),
            OperandVariant.Memory => GetOperandValueOfTypeMemory(virtualMachine, operand),
        };
    }

    private ulong GetOperandValueOfTypeValue(string operand)
    {
        string operandValue = operand.Trim().ToUpper();

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
        IVirtualRegister? register = virtualMachine.CPU.GetRegister(operand.FullOperand);

        if (register == null)
        {
            throw new ArgumentNullException($"Couldn't find a register with the name {operand!.FullOperand}");
        }

        return register!.Get();
    }

    private ulong GetOperandValueOfTypeMemory(IVirtualMachine virtualMachine, IOperand operand)
    {
        return operand.OperandSize switch
        {
            Size.Byte => virtualMachine.GetByte(GetPointerMemoryAddress(virtualMachine, operand)),
            Size.Word => virtualMachine.GetWord(GetPointerMemoryAddress(virtualMachine, operand)),
            Size.Double => virtualMachine.GetDouble(GetPointerMemoryAddress(virtualMachine, operand)),
            Size.Quad => virtualMachine.GetQuad(GetPointerMemoryAddress(virtualMachine, operand)),
        };
    }

    public int GetPointerMemoryAddress(IVirtualMachine virtualMachine, IOperand operand)
    {
        List<IMemoryOffset> offsetsToProcess = [];
        IMemoryOffset scaleOffset;
        int totalMemoryOffset = 0;
        int tempMemoryOffset = 0;

        foreach (IMemoryOffset offset in operand.Offsets)
        {
            offsetsToProcess.Add(offset);
        }

        if (offsetsToProcess.Any(selectedOffsetToProcess => selectedOffsetToProcess.Type == MemoryOffsetType.Scale))
        {
            scaleOffset = offsetsToProcess.Single(selectedOffsetToProcess => selectedOffsetToProcess.Type == MemoryOffsetType.Scale);

            tempMemoryOffset = (int)GetOperandValueOfTypeValue(scaleOffset.FullOperand);
            tempMemoryOffset *= offsetsToProcess[offsetsToProcess.IndexOf(scaleOffset) - 1].Type switch
            {
                MemoryOffsetType.Label => virtualMachine.Memory.LabelMemoryLocations[offsetsToProcess[offsetsToProcess.IndexOf(scaleOffset) - 1].FullOperand].Address,
                MemoryOffsetType.Register => (int)virtualMachine.CPU.GetRegister(offsetsToProcess[offsetsToProcess.IndexOf(scaleOffset) - 1].FullOperand).Get(),
                MemoryOffsetType.Integer => (int)GetOperandValueOfTypeValue(offsetsToProcess[offsetsToProcess.IndexOf(scaleOffset) - 1].FullOperand),
                MemoryOffsetType.Scale => throw new InvalidDataException("Cannot have two or more scale values in the memory offset of an operand"),
                _ => (int)GetOperandValueOfTypeValue(offsetsToProcess[offsetsToProcess.IndexOf(scaleOffset) - 1].FullOperand),
            };

            totalMemoryOffset = tempMemoryOffset;
            tempMemoryOffset = 0;

            offsetsToProcess.RemoveAt(offsetsToProcess.IndexOf(scaleOffset) - 1);
            offsetsToProcess.RemoveAt(offsetsToProcess.IndexOf(scaleOffset));
        }

        foreach (IMemoryOffset offset in offsetsToProcess)
        {
            tempMemoryOffset = offset.Type switch
            {
                MemoryOffsetType.Label => virtualMachine.Memory.LabelMemoryLocations[offset.FullOperand].Address,
                MemoryOffsetType.Register => (int)virtualMachine.CPU.GetRegister(offset.FullOperand).Get(),
                MemoryOffsetType.Integer => (int)GetOperandValueOfTypeValue(offset.FullOperand),
                MemoryOffsetType.Scale => throw new InvalidDataException("Cannot have two or more scale values in the memory offset of an operand"),
                _ => (int)GetOperandValueOfTypeValue(offset.FullOperand),
            };

            tempMemoryOffset = operand.OperandSize switch
            {
                Size.Byte => (byte)tempMemoryOffset,
                Size.Word => (ushort)tempMemoryOffset,
                Size.Double => tempMemoryOffset,
                Size.Quad => tempMemoryOffset,
                _ => throw new NotImplementedException("Invalid operand size")
            };

            totalMemoryOffset = offset.Operand switch
            {
                MemoryOffsetOperand.Addition => totalMemoryOffset + tempMemoryOffset,
                MemoryOffsetOperand.Subtraction => totalMemoryOffset - tempMemoryOffset,
                MemoryOffsetOperand.Multiplication => totalMemoryOffset * tempMemoryOffset,
                _ => totalMemoryOffset + tempMemoryOffset
            };
        }

        return totalMemoryOffset;
    }

    public int GetInstructionMemoryAddress(IVirtualMemory memory, IOperand operand)
    {
        return memory.LabelMemoryLocations[operand.Offsets.First().FullOperand].Line;
    }
}