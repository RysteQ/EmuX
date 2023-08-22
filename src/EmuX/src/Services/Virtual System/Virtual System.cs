using EmuX.src.Enums.VM;

public class VirtualSystem
{
    public VirtualSystem()
    {
        for (int i = 0; i < this.registers.Length; i++)
            this.registers[i] = 0;
    }

    public void SetVirtualSystem(VirtualSystem virtual_system)
    {
        this.registers = virtual_system.GetAllRegisterValues();
        this.memory = virtual_system.GetAllMemory();
        this.call_stack = virtual_system.GetCallStack();
        this.EFLAGS = virtual_system.EFLAGS;
    }

    public void Reset()
    {
        for (int i = 0; i < this.registers.Length; i++)
            this.registers[i] = 0;

        for (int i = 0; i < this.memory.Length; i++)
            this.memory[i] = 0;

        this.call_stack = new List<int> { 0 };
        this.EFLAGS = 0;
    }

    public List<int> GetCallStack()
    {
        return this.call_stack;
    }

    public byte[] GetAllMemory()
    {
        return this.memory;
    }

    public byte GetByteMemory(int index)
    {
        return this.memory[index];
    }

    public ushort GetWordMemory(int index)
    {
        return (ushort)((GetByteMemory(index) << 8) + GetByteMemory(index + 1));
    }

    public uint GetDoubleMemory(int index)
    {
        return (uint)((GetWordMemory(index) << 16) + GetWordMemory(index + 2));
    }

    public ulong GetQuadMemory(int index)
    {
        return (GetDoubleMemory(index) << 32) + GetDoubleMemory(index + 4);
    }

    public void SetAllMemory(byte[] memory)
    {
        this.memory = memory;
    }

    public void SetByteMemory(int index, byte value)
    {
        this.memory[index] = value;
    }

    public void SetWordMemory(int index, ushort value)
    {
        SetByteMemory(index + 1, (byte)value);
        SetByteMemory(index, (byte)(value >> 8));
    }

    public void SetDoubleMemory(int index, uint value)
    {
        SetWordMemory(index + 2, (ushort)value);
        SetWordMemory(index, (ushort)(value >> 16));
    }

    public void SetQuadMemory(int index, ulong value)
    {
        SetDoubleMemory(index + 4, (uint)value);
        SetDoubleMemory(index, (uint)(value >> 32));
    }

    public void PushByte(byte value_to_push)
    {
        this.memory[this.registers[(int)Registers.RSP]] = value_to_push;
        this.registers[(int)Registers.RSP]++;
    }

    public void PushWord(ushort value_to_push)
    {
        PushByte((byte)(value_to_push & 0x00FF));
        PushByte((byte)((value_to_push & 0xFF00) >> 8));
    }

    public void PushDouble(uint value_to_push)
    {
        PushWord((ushort)(value_to_push & 0x0000FFFF));
        PushWord((ushort)((value_to_push & 0xFFFF0000) >> 16));
    }

    public void PushQuad(ulong value_to_push)
    {
        PushDouble((uint)(value_to_push & 0x00000000FFFFFFFF));
        PushDouble((uint)((value_to_push & 0xFFFFFFFF00000000) >> 32));
    }

    public byte PopByte()
    {
        registers[(int)Registers.RSP]--;
        return this.memory[this.registers[(int)Registers.RSP]];
    }

    public ushort PopWord()
    {
        return (ushort)((PopByte() << 8) + PopByte());
    }

    public uint PopDouble()
    {
        return (uint)((PopWord() << 16) + PopWord());
    }

    public ulong PopQuad()
    {
        return (PopDouble() << 32) + PopDouble();
    }

    public byte GetRegisterByte(Registers register_to_get, bool high_or_low)
    {
        if (high_or_low)
            return (byte)((ushort)registers[(int)register_to_get] >> 8);

        return (byte)this.registers[(int)register_to_get];
    }

    public ushort GetRegisterWord(Registers register_to_get)
    {
        return (ushort)this.registers[(int)register_to_get];
    }

    public uint GetRegisterDouble(Registers register_to_get)
    {
        return (uint)this.registers[(int)register_to_get];
    }

    public ulong GetRegisterQuad(Registers register_to_get)
    {
        return this.registers[(int)register_to_get];
    }

    public ulong[] GetAllRegisterValues()
    {
        return this.registers;
    }

    public void SetRegisterByte(Registers register_to_get, byte value, bool high_or_low)
    {
        ulong value_to_set;

        if (high_or_low)
        {
            value_to_set = this.registers[(int)register_to_get] & 0xFFFFFFFFFFFF00FF;
            value_to_set += (ushort)(value << 8);
        }
        else
        {
            value_to_set = this.registers[(int)register_to_get] & 0xFFFFFFFFFFFFFF00;
            value_to_set += value;
        }

        this.registers[(int)register_to_get] = value_to_set;
    }

    public void SetRegisterWord(Registers register_to_get, ushort value)
    {
        ulong value_to_set = this.registers[(int)register_to_get] & 0xFFFFFFFFFFFF0000;
        this.registers[(int)register_to_get] = value_to_set + value;
    }

    public void SetRegisterDouble(Registers register_to_get, uint value)
    {
        ulong value_to_set = this.registers[(int)register_to_get] & 0xFFFFFFFF00000000;
        this.registers[(int)register_to_get] = value_to_set + value;
    }

    public void SetRegisterQuad(Registers register_to_get, ulong value)
    {
        this.registers[(int)register_to_get] = value;
    }

    public void SetAllRegisterValues(ulong[] values)
    {
        for (int i = 0; i < values.Length - 2; i++)
            this.registers[i] = values[i];
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

    public void PushCall(int index)
    {
        this.call_stack.Add(index);
    }

    public int PopCall()
    {
        // check if there are any elements
        if (this.call_stack.Count == 0)
            return -1;

        int toReturn = this.call_stack[this.call_stack.Count - 1];
        this.call_stack.RemoveAt(this.call_stack.Count - 1);

        return toReturn;
    }

    // REGISTERS
    private ulong[] registers = new ulong[(int)Registers.LAST - 2];

    // EFLAGS
    public uint EFLAGS;

    // MEMORY
    // first kilobyte is the stack
    private byte[] memory = new byte[8192 + 806400];
    private List<int> call_stack = new List<int>();
}