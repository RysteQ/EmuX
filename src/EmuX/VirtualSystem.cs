﻿using EmuX;
using System;

class VirtualSystem
{
    // memory getters
    public byte[] GetAllMemory()
    {
        return this.memory;
    }

    public byte GetByteMemory(int index)
    {
        return this.memory[index];
    }

    public ushort GetShortMemory(int index)
    {
        ushort toReturn = (ushort) ((GetByteMemory(index) << 8) + GetByteMemory(index + 1));
        return toReturn;
    }

    public uint GetDoubleMemory(int index)
    {
        uint toReturn = (uint) ((GetShortMemory(index) << 16) + GetShortMemory(index + 2));
        return toReturn;
    }

    public ulong GetQuadMemory(int index)
    {
        ulong toReturn = (ulong) ((GetDoubleMemory(index) << 32) + GetDoubleMemory(index + 4));
        return toReturn;
    }

    // memory setters
    public void SetAllMemory(byte[] memory)
    {
        this.memory = memory;
    }

    public void SetByteMemory(int index, byte value)
    {
        this.memory[index] = value;
    }

    public void SetShortMemory(int index, ushort value)
    {
        SetByteMemory(index, (byte) (value >> 8));
        SetByteMemory(index + 1, (byte) value);
    }

    public void SetDoubleMemory(int index, uint value)
    {
        SetShortMemory(index, (ushort) (value >> 16));
        SetShortMemory(index + 2, (ushort) value);
    }

    public void SetQuadMemory(int index, ulong value)
    {
        SetDoubleMemory(index, (uint) (value >> 32));
        SetDoubleMemory(index + 4, (uint) value);
    }

    // stack getters
    public void PushByte(byte value_to_push)
    {
        memory[registers[(int) Instruction_Data.Registers_ENUM.RSP]] = value_to_push;
        registers[(int) Instruction_Data.Registers_ENUM.RSP]++;
    }

    public void PushShort(ushort value_to_push)
    {
        PushByte((byte) (value_to_push >> 8));
        PushByte((byte) value_to_push);
    }

    public void PushDouble(uint value_to_push)
    {
        PushShort((ushort) (value_to_push >> 16));
        PushShort((ushort) value_to_push);
    }

    public void PushQuad(ulong value_to_push)
    {
        PushDouble((uint) (value_to_push >> 32));
        PushDouble((uint) value_to_push);
    }

    // stack setters
    public byte PopByte()
    {
        registers[(int) Instruction_Data.Registers_ENUM.RSP]--;
        return this.memory[registers[(int) Instruction_Data.Registers_ENUM.RSP]];
    }

    public ushort PopShort()
    {
        return (ushort) ((PopByte() << 8) + PopByte());
    }

    public uint PopDouble()
    {
        return (uint) ((PopShort() << 16) + PopShort());
    }

    public ulong PopQuad()
    {
        return (ulong) ((PopDouble() << 32) + PopDouble());
    }

    // register getters
    public byte GetRegisterByte(Instruction_Data.Registers_ENUM register_to_get, bool high_or_low)
    {
        if (high_or_low)
            return (byte) ((ushort) registers[(int)register_to_get] >> 8);

        return (byte) registers[(int) register_to_get];
    }

    public ushort GetRegisterShort(Instruction_Data.Registers_ENUM register_to_get)
    {
        return (ushort) registers[(int) register_to_get];
    }

    public uint GetRegisterDouble(Instruction_Data.Registers_ENUM register_to_get)
    {
        return (uint) registers[(int) register_to_get];
    }

    public ulong GetRegisterQuad(Instruction_Data.Registers_ENUM register_to_get)
    {
        return (ulong) registers[(int) register_to_get];
    }

    // register setters
    public void SetRegisterValue(Instruction_Data.Registers_ENUM register_to_get, object value)
    {
        registers[(int) register_to_get] = (ulong) value;
    }

    // EFLAGS getters
    public uint GetEFLAGS()
    {
        return this.EFLAGS;
    }

    // EFLAGS setters
    public void SetEflags(uint flags)
    {
        this.EFLAGS = flags;
    }

    // REGISTERS
    private ulong[] registers = new ulong[(int) Instruction_Data.Registers_ENUM.LAST - 1];

    // EFLAGS
    private uint EFLAGS = 0;

    // MEMORY
    // first kilobyte is the stack
    private byte[] memory = new byte[8192];
    private List<int> call_stack = new List<int>();
}