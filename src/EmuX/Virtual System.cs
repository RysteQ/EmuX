using EmuX;

class VirtualSystem
{
    public VirtualSystem()
    {
        for (int i = 0; i < this.registers.Length; i++)
            this.registers[i] = 0;
    }

    /// <summary>
    /// Setter - Sets all the virtual system values with the new virtual system provided
    /// </summary>
    public void SetVirtualSystem(VirtualSystem virtual_system)
    {
        this.registers = virtual_system.GetAllRegisterValues();
        this.memory = virtual_system.GetAllMemory();
        this.call_stack = virtual_system.GetCallStack();
        this.EFLAGS = virtual_system.GetEFLAGS();
    }

    public void ResetVirtualSystem()
    {
        for (int i = 0; i < this.registers.Length; i++)
            this.registers[i] = 0;

        for (int i = 0; i < this.memory.Length; i++)
            this.memory[i] = 0;

        this.call_stack.Clear();
        this.EFLAGS = 0;
    }

    /// <summary>
    /// Getter - Gets the call stack
    /// </summary>
    /// <returns>A List<int> with all the indexes on where to return after a call (FIFO)</returns>
    public List<int> GetCallStack()
    {
        return this.call_stack;
    }

    // memory getters

    /// <summary>
    /// Returns all of the memory
    /// </summary>
    public byte[] GetAllMemory()
    {
        return this.memory;
    }

    /// <summary>
    /// Returns a single byte of memory
    /// </summary>
    public byte GetByteMemory(int index)
    {
        return this.memory[index];
    }

    /// <summary>
    /// Returns two bytes of memory
    /// </summary>
    public ushort GetWordMemory(int index)
    {
        return (ushort) ((this.GetByteMemory(index) << 8) + this.GetByteMemory(index + 1));
    }

    /// <summary>
    /// Returns four bytes of memory
    /// </summary>
    public uint GetDoubleMemory(int index)
    {
        return (uint) ((this.GetWordMemory(index) << 16) + this.GetWordMemory(index + 2));
    }

    /// <summary>
    /// Returns eight bytes of memory
    /// </summary>
    public ulong GetQuadMemory(int index)
    {
        return (ulong) ((this.GetDoubleMemory(index) << 32) + this.GetDoubleMemory(index + 4));
    }

    // memory setters

    /// <summary>
    /// Sets all of the memory values to the given array values
    /// </summary>
    public void SetAllMemory(byte[] memory)
    {
        this.memory = memory;
    }

    /// <summary>
    /// Sets a single byte in memory
    /// </summary>
    public void SetByteMemory(int index, byte value)
    {
        this.memory[index] = value;
    }

    /// <summary>
    /// Sets two bytes in memory
    /// </summary>
    public void SetWordMemory(int index, ushort value)
    {
        this.SetByteMemory(index + 1, (byte) value);
        this.SetByteMemory(index, (byte) (value >> 8));
    }

    /// <summary>
    /// Sets four bytes in memory
    /// </summary>
    public void SetDoubleMemory(int index, uint value)
    {
        this.SetWordMemory(index + 2, (ushort) value);
        this.SetWordMemory(index, (ushort) (value >> 16));
    }

    /// <summary>
    /// Sets eight bytes in memory
    /// </summary>
    public void SetQuadMemory(int index, ulong value)
    {
        this.SetDoubleMemory(index + 4, (uint) value);
        this.SetDoubleMemory(index, (uint) (value >> 32));
    }

    // stack getters

    /// <summary>
    /// Pushes a single byte into the stack
    /// </summary>
    public void PushByte(byte value_to_push)
    {
        this.memory[this.registers[(int) Instruction_Data.Registers_ENUM.RSP]] = value_to_push;
        this.registers[(int) Instruction_Data.Registers_ENUM.RSP]++;
    }

    /// <summary>
    /// Pushes two bytes into the stack
    /// </summary>
    public void PushWord(ushort value_to_push)
    {
        this.PushByte((byte) (value_to_push & 0x00FF));
        this.PushByte((byte) ((value_to_push & 0xFF00) >> 8));
    }

    /// <summary>
    /// Pushes four bytes into the stack
    /// </summary>
    public void PushDouble(uint value_to_push)
    {
        this.PushWord((ushort) (value_to_push & 0x0000FFFF));
        this.PushWord((ushort) ((value_to_push & 0xFFFF0000) >> 16));
    }

    /// <summary>
    /// Pushes eight bytes into the stack
    /// </summary>
    public void PushQuad(ulong value_to_push)
    {
        this.PushDouble((uint) (value_to_push & 0x00000000FFFFFFFF));
        this.PushDouble((uint) ((value_to_push & 0xFFFFFFFF00000000) >> 32));
    }

    // stack setters

    /// <summary>
    /// Pops a single byte from the stack
    /// </summary>
    public byte PopByte()
    {
        registers[(int) Instruction_Data.Registers_ENUM.RSP]--;
        return this.memory[this.registers[(int) Instruction_Data.Registers_ENUM.RSP]];
    }

    /// <summary>
    /// Pops two bytes from the stack
    /// </summary>
    public ushort PopWord()
    {
        return (ushort) ((this.PopByte() << 8) + this.PopByte());
    }

    /// <summary>
    /// Pops four bytes from the stack
    /// </summary>
    public uint PopDouble()
    {
        return (uint) ((this.PopWord() << 16) + this.PopWord());
    }

    /// <summary>
    /// Pops eight bytes from the stack
    /// </summary>
    public ulong PopQuad()
    {
        return (ulong) ((this.PopDouble() << 32) + this.PopDouble());
    }

    // register getters

    /// <summary>
    /// Gets a single byte from said register, must provide the 64bit variant of the register, not the 8bit one
    /// </summary>
    /// <param name="high_or_low">True for the high register and false for the low register</param>
    public byte GetRegisterByte(Instruction_Data.Registers_ENUM register_to_get, bool high_or_low)
    {
        if (high_or_low)
            return (byte) ((ushort) registers[(int)register_to_get] >> 8);

        return (byte) this.registers[(int) register_to_get];
    }

    /// <summary>
    /// Gets two bytes from said register, must provide the 64bit variant of the register, not the 16bit one
    /// </summary>
    public ushort GetRegisterWord(Instruction_Data.Registers_ENUM register_to_get)
    {
        return (ushort) this.registers[(int) register_to_get];
    }

    /// <summary>
    /// Gets four bytes from said register, must provide the 64bit variant of the register, not the 32bit one
    /// </summary>
    public uint GetRegisterDouble(Instruction_Data.Registers_ENUM register_to_get)
    {
        return (uint) this.registers[(int) register_to_get];
    }

    /// <summary>
    /// Gets eight bytes from said register
    /// </summary>
    public ulong GetRegisterQuad(Instruction_Data.Registers_ENUM register_to_get)
    {
        return (ulong) this.registers[(int) register_to_get];
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
    /// Sets the new value for the said register, either high or low
    /// </summary>
    /// <param name="high_or_low">True for high and false for low</param>
    public void SetRegisterByte(Instruction_Data.Registers_ENUM register_to_get, byte value, bool high_or_low)
    {
        ulong value_to_set;

        if (high_or_low == new Instruction_Data().HIGH)
        {
            value_to_set = this.registers[(int)register_to_get] & 0xFFFFFFFFFFFF00FF;
            value_to_set += (ushort) (value << 8);
        }
        else
        {
            value_to_set = this.registers[(int)register_to_get] & 0xFFFFFFFFFFFFFF00;
            value_to_set += value;
        }

        this.registers[(int) register_to_get] = value_to_set;
    }

    /// <summary>
    /// Sets a registed to the specified value
    /// </summary>
    public void SetRegisterWord(Instruction_Data.Registers_ENUM register_to_get, ushort value)
    {
        ulong value_to_set = this.registers[(int) register_to_get] & 0xFFFFFFFFFFFF0000;
        this.registers[(int) register_to_get] = value_to_set + value;
    }

    /// <summary>
    /// Sets a registed to the specified value
    /// </summary>
    public void SetRegisterDouble(Instruction_Data.Registers_ENUM register_to_get, uint value)
    {
        ulong value_to_set = this.registers[(int) register_to_get] & 0xFFFFFFFF00000000;
        this.registers[(int) register_to_get] = value_to_set + value;
    }

    /// <summary>
    /// Sets a registed to the specified value
    /// </summary>
    public void SetRegisterQuad(Instruction_Data.Registers_ENUM register_to_get, ulong value)
    {
        this.registers[(int)register_to_get] = value;
    }

    /// <summary>
    /// Sets all the registers to the specified values, look at the source code to see the order at
    /// </summary>
    public void SetAllRegisterValues(ulong[] values)
    {
        for (int i = 0; i < values.Length - 2; i++)
            this.registers[i] = values[i];
    }

    // EFLAGS getters

    /// <summary>
    /// Gets the EFLAGS register
    /// </summary>
    public uint GetEFLAGS()
    {
        return this.EFLAGS;
    }

    public uint[] GetEFLAGSMasks()
    {
        uint[] masks = new uint[]
        {
                0x00000001, // 0) CF
                0x00000004, // 1) PF
                0x00000010, // 2) AF
                0x00000040, // 3) ZF
                0x00000080, // 4) SF
                0x00000100, // 5) TF
                0x00000200, // 6) IF
                0x00000400, // 7) DF
                0x00000800, // 8) OF
                0x00003000, // 9) IOPL
                0x00004000, // 10) NT
                0x00010000, // 11) RF
                0x00020000, // 12) VM
                0x00040000, // 13) AC
                0x00080000, // 14) VIF
                0x00100000, // 15) VIP
                0x00200000, // 16) ID
        };

        return masks;
    }

    /// <summary>
    /// Pushes the index value to the call stack
    /// </summary>
    public void PushCall(int index)
    {
        this.call_stack.Add(index);
    }

    /// <summary>
    /// Pops the last value of the call stack if there is something in the call stack
    /// </summary>
    public int PopCall()
    {
        // check if there are any elements
        if (this.call_stack.Count == 0)
            return -1;

        int toReturn = this.call_stack[this.call_stack.Count - 1];
        this.call_stack.RemoveAt(this.call_stack.Count - 1);

        return toReturn;
    }

    // EFLAGS setters

    /// <summary>
    /// Sets the EFLAGS register
    /// </summary>
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