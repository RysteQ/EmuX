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

    /// <summary>
    /// Returns all of the memory
    /// </summary>
    /// <returns>A byte array of all the memory</returns>
    public byte[] GetAllMemory()
    {
        return this.memory;
    }

    /// <summary>
    /// Returns a single byte of memory
    /// </summary>
    /// <param name="index">The index to get the value from memory</param>
    /// <returns>The byte value</returns>
    public byte GetByteMemory(int index)
    {
        return this.memory[index];
    }

    /// <summary>
    /// Returns two bytes of memory
    /// </summary>
    /// <param name="index">The index to get the value from memory</param>
    /// <returns>An unsigned short value</returns>
    public ushort GetShortMemory(int index)
    {
        ushort toReturn = (ushort) ((GetByteMemory(index) << 8) + GetByteMemory(index + 1));
        return toReturn;
    }

    /// <summary>
    /// Returns four bytes of memory
    /// </summary>
    /// <param name="index">The index to get the value from memory</param>
    /// <returns>An unsigned integer value</returns>
    public uint GetDoubleMemory(int index)
    {
        uint toReturn = (uint) ((GetShortMemory(index) << 16) + GetShortMemory(index + 2));
        return toReturn;
    }

    /// <summary>
    /// Returns eight bytes of memory
    /// </summary>
    /// <param name="index">The index to get the value from memory</param>
    /// <returns>An unsigned long value</returns>
    public ulong GetQuadMemory(int index)
    {
        ulong toReturn = (ulong) ((GetDoubleMemory(index) << 32) + GetDoubleMemory(index + 4));
        return toReturn;
    }

    // memory setters

    /// <summary>
    /// Sets all of the memory values to the given array values
    /// </summary>
    /// <param name="memory">The values to set the memory to, it must be the same size at the memory array</param>
    public void SetAllMemory(byte[] memory)
    {
        this.memory = memory;
    }

    /// <summary>
    /// Sets a single byte in memory
    /// </summary>
    /// <param name="index">The index of said byte</param>
    /// <param name="value">The new value of said byte</param>
    public void SetByteMemory(int index, byte value)
    {
        this.memory[index] = value;
    }

    /// <summary>
    /// Sets two bytes in memory
    /// </summary>
    /// <param name="index">The index of said bytes</param>
    /// <param name="value">The new value of said bytes</param>
    public void SetShortMemory(int index, ushort value)
    {
        SetByteMemory(index, (byte) (value >> 8));
        SetByteMemory(index + 1, (byte) value);
    }

    /// <summary>
    /// Sets four bytes in memory
    /// </summary>
    /// <param name="index">The index of said bytes</param>
    /// <param name="value">The new value of said bytes</param>
    public void SetDoubleMemory(int index, uint value)
    {
        SetShortMemory(index, (ushort) (value >> 16));
        SetShortMemory(index + 2, (ushort) value);
    }

    /// <summary>
    /// Sets eight bytes in memory
    /// </summary>
    /// <param name="index">The index of said bytes</param>
    /// <param name="value">The new value of said bytes</param>
    public void SetQuadMemory(int index, ulong value)
    {
        SetDoubleMemory(index, (uint) (value >> 32));
        SetDoubleMemory(index + 4, (uint) value);
    }

    // stack getters

    /// <summary>
    /// Pushes a single byte into the stack
    /// </summary>
    /// <param name="value_to_push">The value to push into the stack</param>
    public void PushByte(byte value_to_push)
    {
        memory[registers[(int) Instruction_Data.Registers_ENUM.RSP]] = value_to_push;
        registers[(int) Instruction_Data.Registers_ENUM.RSP]++;
    }

    /// <summary>
    /// Pushes two bytes into the stack
    /// </summary>
    /// <param name="value_to_push">The value to push into the stack</param>
    public void PushShort(ushort value_to_push)
    {
        PushByte((byte) (value_to_push >> 8));
        PushByte((byte) value_to_push);
    }

    /// <summary>
    /// Pushes four bytes into the stack
    /// </summary>
    /// <param name="value_to_push">The value to push into the stack</param>
    public void PushDouble(uint value_to_push)
    {
        PushShort((ushort) (value_to_push >> 16));
        PushShort((ushort) value_to_push);
    }

    /// <summary>
    /// Pushes eight bytes into the stack
    /// </summary>
    /// <param name="value_to_push">The value to push into the stack</param>
    public void PushQuad(ulong value_to_push)
    {
        PushDouble((uint) (value_to_push >> 32));
        PushDouble((uint) value_to_push);
    }

    // stack setters

    /// <summary>
    /// Pops a single byte from the stack
    /// </summary>
    /// <returns>The popped byte</returns>
    public byte PopByte()
    {
        registers[(int) Instruction_Data.Registers_ENUM.RSP]--;
        return this.memory[registers[(int) Instruction_Data.Registers_ENUM.RSP]];
    }

    /// <summary>
    /// Pops two bytes from the stack
    /// </summary>
    /// <returns>The popped bytes</returns>
    public ushort PopShort()
    {
        return (ushort) ((PopByte() << 8) + PopByte());
    }

    /// <summary>
    /// Pops four bytes from the stack
    /// </summary>
    /// <returns>The popped bytes</returns>
    public uint PopDouble()
    {
        return (uint) ((PopShort() << 16) + PopShort());
    }

    /// <summary>
    /// Pops eight bytes from the stack
    /// </summary>
    /// <returns>The popped bytes</returns>
    public ulong PopQuad()
    {
        return (ulong) ((PopDouble() << 32) + PopDouble());
    }

    // register getters

    /// <summary>
    /// Gets a single byte from said register, must provide the 64bit variant of the register, not the 8bit one
    /// </summary>
    /// <param name="register_to_get">The register enum value to get the value from</param>
    /// <param name="high_or_low">The high or low byte to get the value from</param>
    /// <returns>The said register value</returns>
    public byte GetRegisterByte(Instruction_Data.Registers_ENUM register_to_get, bool high_or_low)
    {
        if (high_or_low)
            return (byte) ((ushort) registers[(int)register_to_get] >> 8);

        return (byte) registers[(int) register_to_get];
    }

    /// <summary>
    /// Gets two bytes from said register, must provide the 64bit variant of the register, not the 16bit one
    /// </summary>
    /// <param name="register_to_get">The register enum value to get the value from</param>
    /// <returns>The said register value</returns>
    public ushort GetRegisterShort(Instruction_Data.Registers_ENUM register_to_get)
    {
        return (ushort) registers[(int) register_to_get];
    }

    /// <summary>
    /// Gets four bytes from said register, must provide the 64bit variant of the register, not the 32bit one
    /// </summary>
    /// <param name="register_to_get">The register enum value to get the value from</param>
    /// <returns>The said register value</returns>
    public uint GetRegisterDouble(Instruction_Data.Registers_ENUM register_to_get)
    {
        return (uint) registers[(int) register_to_get];
    }

    /// <summary>
    /// Gets eight bytes from said register
    /// </summary>
    /// <param name="register_to_get">The register enum value to get the value from</param>
    /// <returns>The said register value</returns>
    public ulong GetRegisterQuad(Instruction_Data.Registers_ENUM register_to_get)
    {
        return (ulong) registers[(int) register_to_get];
    }

    /// <summary>
    /// Returns all of the register value
    /// </summary>
    /// <returns>A unsigned long array of all the register values</returns>
    public ulong[] GetAllRegisterValues()
    {
        return this.registers;
    }

    // register setters

    /// <summary>
    /// Sets a registed to the specified value
    /// </summary>
    /// <param name="register_to_get">The register tou want to change the value of, must provide the 64bit variant of the register</param>
    /// <param name="value">The unsigned long value to set the register at</param>
    public void SetRegisterValue(Instruction_Data.Registers_ENUM register_to_get, ulong value)
    {
        registers[(int) register_to_get] = value;
    }

    /// <summary>
    /// Sets all the registers to the specified values, look at the source code to see the order at
    /// </summary>
    /// <param name="values">The unsigned long array of the value, must be the same length as the total amount of registers</param>
    public void SetAllRegisterValues(ulong[] values)
    {
        for (int i = 0; i < values.Length - 2; i++)
            registers[i] = values[i];
    }

    // EFLAGS getters

    /// <summary>
    /// Gets the EFLAGS register
    /// </summary>
    /// <returns>The unsigned integer value of the EFLAGS register</returns>
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

    /// <summary>
    /// Sets the EFLAGS register
    /// </summary>
    /// <param name="flags">The value to set the EFLAGS register at</param>
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