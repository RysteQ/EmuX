using EmuX;
using System;

class VirtualSystem
{
    public VirtualSystem()
    {
        for (int i = 0; i < this.registers.Length; i++)
            this.registers[i] = 0;
    }

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

    public ulong[] GetAllRegisterValues()
    {
        return this.registers;
    }

    // register setters
    public void SetRegisterValue(Instruction_Data.Registers_ENUM register_to_get, object value)
    {
        registers[(int) register_to_get] = (ulong) value;
    }

    public void SetAllRegisterValues(ulong[] values)
    {
        for (int i = 0; i < values.Length - 2; i++)
            registers[i] = values[i];
    }

    // EFLAGS getters
    public uint GetEFLAGS()
    {
        return this.EFLAGS;
    }

    public uint[] GetEFLAGSMasks()
    {
        uint[] masks = new uint[]
        {
                0x00000001, // CF
                0x00000004, // PF
                0x00000010, // AF
                0x00000040, // ZF
                0x00000080, // SF
                0x00000100, // TF
                0x00000200, // IF
                0x00000400, // DF
                0x00000800, // OF
                0x00003000, // IOPL
                0x00004000, // NT
                0x00010000, // RF
                0x00020000, // VM
                0x00040000, // AC
                0x00080000, // VIF
                0x00100000, // VIP
                0x00200000, // ID
        };

        return masks;
    }

    // EFLAGS setters
    public void SetEflags(uint flags)
    {
        this.EFLAGS = flags;
    }

    // REGISTERS
    private ulong[] registers = new ulong[(int) Instruction_Data.Registers_ENUM.LAST - 2];

    // EFLAGS
    private uint EFLAGS = 0;

    // MEMORY
    // first kilobyte is the stack
    private byte[] memory = new byte[8192];
    private List<int> call_stack = new List<int>();
}