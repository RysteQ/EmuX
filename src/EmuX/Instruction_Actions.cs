using System.Security.Cryptography.X509Certificates;

namespace EmuX
{
    internal class Instruction_Actions
    {
        /// <summary>
        /// The AAA instruction
        /// </summary>
        public (ushort, uint) AAA(ushort ax_register_value, uint flags)
        {
            if (((ax_register_value) & 0x000F) > 9 || (flags & 0x00000010) == 1)
            {
                ax_register_value = (ushort) (((ax_register_value & 0xFF00) + 1) + ((ax_register_value & 0x00FF) + 6));
                flags = flags | 0x00000010;
                flags = flags | 0x00000001;

                return (ax_register_value, flags);
            }

            flags = flags & 0xFFFFFFFC;

            return (ax_register_value, flags);
        }

        /// <summary>
        /// The AAD instruction
        /// </summary>
        public ushort AAD(ushort ax_register_value)
        {
            return (ushort) ((ax_register_value & 0xFF00) + (ax_register_value & 0x00FF));
        }

        /// <summary>
        /// The AAM instruction
        /// </summary>
        public ushort AAM(ushort ax_register_value)
        {
            return (ushort) ((((ax_register_value & 0xFF00) / 10) << 8) + (ax_register_value & 0xFF00) % 10);
        }

        /// <summary>
        /// Because this instruction can modify the EFLAGS register and the accumulator at the same time it will return
        /// a virtual system instead of just a single value
        /// </summary>
        public VirtualSystem AAS(VirtualSystem virtual_system)
        {
            int low_nibble_al = virtual_system.GetRegisterByte(Instruction_Data.Registers_ENUM.RAX, false) & 0x0F;
            bool af_status_flag = (virtual_system.GetEFLAGS() & virtual_system.GetEFLAGSMasks()[2]) == 1;
            uint af_cf_status_flag_mask = virtual_system.GetEFLAGSMasks()[2] + virtual_system.GetEFLAGSMasks()[0];

            if (low_nibble_al > 9 || af_status_flag)
            {
                virtual_system.SetRegisterByte(Instruction_Data.Registers_ENUM.RAX, (byte) (low_nibble_al - 6), false);
                virtual_system.SetRegisterByte(Instruction_Data.Registers_ENUM.RAX, (byte) (virtual_system.GetRegisterByte(Instruction_Data.Registers_ENUM.RAX, true) - 1), true);
                virtual_system.SetEflags(virtual_system.GetEFLAGS() | af_cf_status_flag_mask);
            } else
            {
                virtual_system.SetEflags(virtual_system.GetEFLAGS() ^ af_cf_status_flag_mask);
            }

            return virtual_system;
        }

        /// <summary>
        /// The ADC (ADd with Carry) instruction
        /// </summary>
        public ulong ADC(ulong destination, ulong source, uint flags)
        {
            ulong toReturn = destination + source;

            if (flags % 2 == 1)
                toReturn++;

            return toReturn;
        }

        /// <summary>
        /// The ADD instruction
        /// </summary>
        public ulong ADD(ulong destination, ulong source)
        {
            return destination + source;
        }

        /// <summary>
        /// The AND instruction
        /// </summary>
        public ulong AND(ulong destination, ulong source)
        {
            ulong toReturn = destination;
            toReturn &= source;

            return toReturn;
        }

        /// <summary>
        /// The CALL instruction, it jumps to the specified label and then returns back once it finds the RET instruction
        /// </summary>
        /// <returns>A touple (bool, int) that specified if the label was found and the line the label was found</returns>
        public (bool, int) CALL(List<(string, int)> labels, string label_to_find)
        {
            (bool, int) toReturn = (false, -1);

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_find)
                    toReturn = (true, labels[i].Item2);

            return toReturn;
        }

        /// <summary>
        /// The CBW (Convert Byte to Word) instruction
        /// </summary>
        public byte CBW(byte al_register_value)
        {
            if (al_register_value < 128)
                return 0;

            return byte.MaxValue;
        }

        /// <summary>
        /// The CLC instruction, if the carry flag is true then set it to false
        /// </summary>
        public uint CLC(uint flags)
        {
            if (flags % 2 == 1)
                flags--;

            return flags;
        }

        /// <summary>
        /// The CLD instruction, if the destination flag is set to true then set it to false
        /// </summary>
        public uint CLD(uint flags)
        {
            return flags & 0xFFFFFBFF;
        }

        /// <summary>
        /// The CLI instruction, if the interrupt flag is set to true then set it to false
        /// </summary>
        public uint CLI(uint flags)
        {
            return flags & 0xFFFFFDFF;
        }

        /// <summary>
        /// The CMC instruction, executes a not operation to the carry flag
        /// </summary>
        public uint CMC(uint flags)
        {
            if (flags % 2 == 0)
                return flags + 1;
            else
                return flags - 1;
        }

        /// <summary>
        /// The CMP instruction, it compares to values and then modifies the EFLAGS register
        /// </summary>
        /// <returns>The new EFLAGS value</returns>
        public uint CMP(ulong operand_one, ulong operand_two, uint flags)
        {
            if (operand_one < operand_two)
            {
                // set the C flag to one if its not already set to one
                if (flags % 2 == 0)
                    flags++;

                // set the Z flag to zero
                flags = flags & 0xFFFFFFBF;
            } else if (operand_one > operand_two)
            {
                // set the C flag to zero if its not already set to zero
                flags = flags & 0xFFFFFFFE;

                // set the Z flag to zero
                flags = flags & 0xFFFFFFBF;
            } else
            {
                // set the C flag to zero if its not already set to zero
                flags = flags & 0xFFFFFFFE;

                // set the Z flag to one if its not already se to one
                if ((flags & 0x0040) == 0)
                    flags += 0x00000040;
            }

            return flags;
        }

        /// <summary>
        /// The CWD instruction
        /// </summary>
        /// <returns>The new value for the DX register</returns>
        public ushort CWD(ushort ax_register_value)
        {
            if (ax_register_value < 32768)
                return 0;

            return ushort.MaxValue;
        }

        /// <summary>
        /// The DAA instruction, just look at the wikidev what it does, that's how I found out
        /// </summary>
        /// <param name="value">The value of the AL register</param>
        /// <returns>The adjusted value if applicabble</returns>
        public byte DAA(byte value, uint flags)
        {
            if ((value & 0x0F) > 9 || (flags & 0x00000010) == 1)
                return (byte) (value + 6);
            else if (value > 0x9F || (flags & 0x00000001) == 1)
                return (byte) (value + 96);

            return value;
        }

        /// <summary>
        /// The DAS instruction, same as the DAA instruction but it does subtraction instead of addition to the value
        /// </summary>
        /// <param name="value">The value of the AL register</param>
        /// <returns>The adjusted value if applicabble</returns>
        public byte DAS(byte value, uint flags)
        {
            if ((value & 0x0F) > 9 || (flags & 0x00000010) == 1)
                return (byte) (value - 6);
            else if (value > 0x9F || (flags & 0x00000001) == 1)
                return (byte) (value - 96);

            return value;
        }

        /// <summary>
        /// The DEC instruction, it does not protect from underflow
        /// </summary>
        /// <param name="bit_mode">The bitmode of the instruction, 8 / 16 / 32 bit, if a 64bit argument is provided the code will not check for underflow</param>
        public ulong DEC(ulong value, Instruction_Data.Bit_Mode_ENUM bit_mode)
        {
            switch (bit_mode)
            {
                case Instruction_Data.Bit_Mode_ENUM._8_BIT:
                    if ((value & 0x00000000000000FF) == 0)
                        return byte.MaxValue;
                    
                    break;

                case Instruction_Data.Bit_Mode_ENUM._16_BIT:
                    if ((value & 0x000000000000FFFF) == 0)
                        return ushort.MaxValue;

                    break;

                case Instruction_Data.Bit_Mode_ENUM._32_BIT:
                    if ((value & 0x00000000FFFFFFFF) == 0)
                        return uint.MaxValue;

                    break;

                case Instruction_Data.Bit_Mode_ENUM._64_BIT:
                    if (value == 0)
                        return ulong.MaxValue;

                    break;
            }

            return value - 1;
        }

        /// <summary>
        /// The DIV instruction, because it return more than one value I just made it that it does all the work and then returns
        /// the virtual_system back after it modifies some values
        /// </summary>
        public VirtualSystem DIV(VirtualSystem virtual_system, Instruction_Data.Bit_Mode_ENUM bit_mode, ulong destination_value)
        {
            ulong quotient = 0;
            ulong remainder = 0;
            ulong divident = 0;

            switch (bit_mode)
            {
                case Instruction_Data.Bit_Mode_ENUM._8_BIT:
                    divident = virtual_system.GetRegisterQuad(Instruction_Data.Registers_ENUM.RAX);
                    quotient = divident / destination_value;
                    remainder = divident % destination_value;

                    virtual_system.SetRegisterByte(Instruction_Data.Registers_ENUM.RAX, (byte) quotient, false);
                    virtual_system.SetRegisterByte(Instruction_Data.Registers_ENUM.RAX, (byte) remainder, true);

                    break;

                case Instruction_Data.Bit_Mode_ENUM._16_BIT:
                    divident = (ulong) ((virtual_system.GetRegisterWord(Instruction_Data.Registers_ENUM.RDX) << 16) + virtual_system.GetRegisterWord(Instruction_Data.Registers_ENUM.RAX));

                    quotient = divident / destination_value;
                    remainder = divident % destination_value;

                    virtual_system.SetRegisterWord(Instruction_Data.Registers_ENUM.RAX, (ushort) quotient);
                    virtual_system.SetRegisterWord(Instruction_Data.Registers_ENUM.RDX, (ushort) remainder);

                    break;

                case Instruction_Data.Bit_Mode_ENUM._32_BIT:
                    divident = (ulong) ((virtual_system.GetRegisterDouble(Instruction_Data.Registers_ENUM.RDX) << 32) + virtual_system.GetRegisterDouble(Instruction_Data.Registers_ENUM.RAX));

                    quotient = divident / destination_value;
                    remainder = divident % destination_value;

                    virtual_system.SetRegisterDouble(Instruction_Data.Registers_ENUM.RAX, (uint) quotient);
                    virtual_system.SetRegisterDouble(Instruction_Data.Registers_ENUM.RDX, (uint) remainder);

                    break;
            }

            return virtual_system;
        }

        public ulong INC(ulong value_to_increment)
        {
            return value_to_increment + 1;
        }

        public int JA(List<(string, int)> labels, string label_to_jump_to, bool cf_flag_status, bool zf_flag_status)
        {
            if (cf_flag_status || zf_flag_status)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }
        
        public int JAE(List<(string, int)> labels, string label_to_jump_to, bool cf_flag_status)
        {
            if (cf_flag_status)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JB(List<(string, int)> labels, string label_to_jump_to, bool cf_flag_status)
        {
            if (cf_flag_status == false)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JBE(List<(string, int)> labels, string label_to_jump_to, bool cf_flag_status, bool zf_flag_status)
        {
            if (cf_flag_status == false && zf_flag_status == false)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JC(List<(string, int)> labels, string label_to_jump_to, bool cf_flag_status)
        {
            if (cf_flag_status == false)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JE(List<(string, int)> labels, string label_to_jump_to, bool zf_flag_status)
        {
            if (zf_flag_status == false)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }
        
        public int JG(List<(string, int)> labels, string label_to_jump_to, bool zf_flag_status, bool sf_flag_status, bool of_status_flag)
        {
            if (zf_flag_status || (sf_flag_status != of_status_flag))
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JGE(List<(string, int)> labels, string label_to_jump_to, bool sf_flag_status, bool of_flag_status)
        {
            if (sf_flag_status != of_flag_status)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JL(List<(string, int)> labels, string label_to_jump_to, bool sf_flag_status, bool of_flag_status)
        {
            if (sf_flag_status == of_flag_status)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JNA(List<(string, int)> labels, string label_to_jump_to, bool cf_flag_status, bool zf_flag_status)
        {
            if (cf_flag_status == false && zf_flag_status == false)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JNAE(List<(string, int)> labels, string label_to_jump_to, bool cf_flag_status)
        {
            if (cf_flag_status == false)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JNB(List<(string, int)> labels, string label_to_jump_to, bool cf_flag_status)
        {
            if (cf_flag_status)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JNBE(List<(string, int)> labels, string label_to_jump_to, bool cf_flag_status, bool zf_flag_status)
        {
            if (cf_flag_status || zf_flag_status)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JNC(List<(string, int)> labels, string label_to_jump_to, bool cf_flag_status)
        {
            if (cf_flag_status)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JNE(List<(string, int)> labels, string label_to_jump_to, bool zf_flag_status)
        {
            if (zf_flag_status)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JNG(List<(string, int)> labels, string label_to_jump_to, bool zf_flag_status, bool sf_flag_status, bool of_flag_status)
        {
            if (zf_flag_status == false && (sf_flag_status == of_flag_status))
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JNGE(List<(string, int)> labels, string label_to_jump_to, bool sf_flag_status, bool of_flag_status)
        {
            if (sf_flag_status == of_flag_status)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JNL(List<(string, int)> labels, string label_to_jump_to, bool sf_flag_status, bool of_flag_status)
        {
            if (sf_flag_status != of_flag_status)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JNLE(List<(string, int)> labels, string label_to_jump_to, bool zf_status_flag, bool sf_flag_status, bool of_flag_status)
        {
            if (zf_status_flag || (sf_flag_status != of_flag_status))
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JNO(List<(string, int)> labels, string label_to_jump_to, bool of_flag_status)
        {
            if (of_flag_status)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JNP(List<(string, int)> labels, string label_to_jump_to, bool pf_flag_status)
        {
            if (pf_flag_status)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JNS(List<(string, int)> labels, string label_to_jump_to, bool sf_flag_status)
        {
            if (sf_flag_status)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JNZ(List<(string, int)> labels, string label_to_jump_to, bool zf_flag_status)
        {
            if (zf_flag_status)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JO(List<(string, int)> labels, string label_to_jump_to, bool of_flag_status)
        {
            if (of_flag_status == false)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JP(List<(string, int)> labels, string label_to_jump_to, bool pf_flag_status)
        {
            if (pf_flag_status == false)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JPE(List<(string, int)> labels, string label_to_jump_to, bool pf_flag_status)
        {
            if (pf_flag_status == false)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JPO(List<(string, int)> labels, string label_to_jump_to, bool pf_flag_status)
        {
            if (pf_flag_status)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JS(List<(string, int)> labels, string label_to_jump_to, bool sf_flag_status)
        {
            if (sf_flag_status == false)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JZ(List<(string, int)> labels, string label_to_jump_to, bool zf_flag_status)
        {
            if (zf_flag_status == false)
                return -1;

            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public int JMP(List<(string, int)> labels, string label_to_jump_to)
        {
            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label_to_jump_to)
                    return labels[i].Item2;

            return -1;
        }

        public byte LAHF(uint EFLAGS_register_value)
        {
            return (byte) (EFLAGS_register_value & 0x000000FF);
        }

        public int LEA(List<StaticData> static_data, string static_data_to_find)
        {
            for (int i = 0; i < static_data.Count; i++)
                if (static_data[i].name == static_data_to_find)
                    return static_data[i].memory_location;

            return 0;
        }

        /// <summary>
        /// This seems useless but I want all instructions to be in this file and class
        /// </summary>
        public ulong MOV(ulong value)
        {
            return value;
        }

        public VirtualSystem MUL(VirtualSystem virtual_system, ulong destination_value, Instruction_Data.Bit_Mode_ENUM bit_mode)
        {
            if (bit_mode == Instruction_Data.Bit_Mode_ENUM._8_BIT)
            {
                virtual_system.SetRegisterWord(Instruction_Data.Registers_ENUM.RAX, (ushort) (virtual_system.GetRegisterByte(Instruction_Data.Registers_ENUM.RAX, false) * destination_value));
            } else if (bit_mode == Instruction_Data.Bit_Mode_ENUM._16_BIT)
            {
                virtual_system.SetRegisterWord(Instruction_Data.Registers_ENUM.RDX, (ushort) ((virtual_system.GetRegisterWord(Instruction_Data.Registers_ENUM.RAX) * destination_value) & 0xFFFF0000));
                virtual_system.SetRegisterWord(Instruction_Data.Registers_ENUM.RAX, (ushort) ((virtual_system.GetRegisterWord(Instruction_Data.Registers_ENUM.RAX) * destination_value) & 0x0000FFFF));
            } else
            {
                virtual_system.SetRegisterDouble(Instruction_Data.Registers_ENUM.RDX, (uint) ((virtual_system.GetRegisterDouble(Instruction_Data.Registers_ENUM.RAX) * destination_value) & 0xFFFFFFFF00000000));
                virtual_system.SetRegisterDouble(Instruction_Data.Registers_ENUM.RAX, (uint) ((virtual_system.GetRegisterDouble(Instruction_Data.Registers_ENUM.RAX) * destination_value) & 0x00000000FFFFFFFF));
            }

            return virtual_system;
        }

        public ulong NEG(ulong value)
        {
            return (~value) + 1;
        }

        /// <summary>
        /// The NOP instruction, do I need to explain it ?
        /// lol no
        /// </summary>
        public void NOP() { }

        public ulong NOT(ulong value)
        {
            return ~value;
        }

        public ulong OR(ulong destination_value, ulong source_value)
        {
            return destination_value | source_value;
        }

        public VirtualSystem POP(VirtualSystem virtual_system, Instruction instruction, int memory_index)
        {
            ulong value_to_set = 0;
            
            // pop the right amount of bytes from the stack
            switch (instruction.bit_mode)
            {
                case Instruction_Data.Bit_Mode_ENUM._8_BIT:
                    value_to_set = virtual_system.PopByte();
                    break;

                case Instruction_Data.Bit_Mode_ENUM._16_BIT:
                    value_to_set = virtual_system.PopWord();
                    break;

                case Instruction_Data.Bit_Mode_ENUM._32_BIT:
                    value_to_set = virtual_system.PopDouble();
                    break;

                case Instruction_Data.Bit_Mode_ENUM._64_BIT:
                    value_to_set = virtual_system.PopQuad();
                    break;
            }

            // the set value function taken from Emulator.cs
            // check if the value needs to be saved in a register or memory location
            if (instruction.destination_register != Instruction_Data.Registers_ENUM.NoN && instruction.destination_register_pointer == false)
            {
                switch (instruction.bit_mode)
                {
                    case Instruction_Data.Bit_Mode_ENUM._8_BIT:
                        virtual_system.SetRegisterByte(instruction.destination_register, (byte) value_to_set, instruction.high_or_low);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._16_BIT:
                        virtual_system.SetRegisterWord(instruction.destination_register, (ushort) value_to_set);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._32_BIT:
                        virtual_system.SetRegisterDouble(instruction.destination_register, (uint) value_to_set);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._64_BIT:
                        virtual_system.SetRegisterQuad(instruction.destination_register, value_to_set);
                        break;
                }
            }
            else
            {
                switch (instruction.bit_mode)
                {
                    case Instruction_Data.Bit_Mode_ENUM._8_BIT:
                        virtual_system.SetByteMemory(memory_index, (byte) value_to_set);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._16_BIT:
                        virtual_system.SetWordMemory(memory_index, (ushort) value_to_set);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._32_BIT:
                        virtual_system.SetDoubleMemory(memory_index, (uint) value_to_set);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._64_BIT:
                        virtual_system.SetQuadMemory(memory_index, value_to_set);
                        break;
                }
            }

            return virtual_system;
        }

        public VirtualSystem POPF(VirtualSystem virtual_system)
        {
            virtual_system.SetEflags(virtual_system.PopWord());
            return virtual_system;
        }

        public VirtualSystem PUSH(VirtualSystem virtual_system, Instruction_Data.Bit_Mode_ENUM bit_mode, ulong value)
        {
            switch (bit_mode)
            {
                case Instruction_Data.Bit_Mode_ENUM._8_BIT:
                    virtual_system.PushByte((byte) value);
                    break;

                case Instruction_Data.Bit_Mode_ENUM._16_BIT:
                    virtual_system.PushWord((ushort) value);
                    break;

                case Instruction_Data.Bit_Mode_ENUM._32_BIT:
                    virtual_system.PushDouble((uint) value);
                    break;

                case Instruction_Data.Bit_Mode_ENUM._64_BIT:
                    virtual_system.PushQuad(value);
                    break;
            }

            return virtual_system;
        }

        public VirtualSystem PUSHF(VirtualSystem virtual_system, uint EFLAGS)
        {
            virtual_system.PushWord((ushort) EFLAGS);
            return virtual_system;
        }

        public VirtualSystem RCL(VirtualSystem virtual_system, Instruction instruction, int memory_index, ulong destination_value, int bits_to_shift)
        {
            byte new_value_byte = (byte) destination_value;
            ushort new_value_ushort = (ushort) destination_value;
            uint new_value_uint = (uint) destination_value;
            ulong new_value_ulong = destination_value;

            ulong value_to_set = 0;

            bool CF_value = false;
            bool CF_new_value = false;

            for (int i = 0; i < bits_to_shift; i++)
            {
                // Get the CF value
                CF_value = (virtual_system.GetEFLAGS() & virtual_system.GetEFLAGSMasks()[0]) == 1;

                // get the new CF value and rotate all the bits by one bit
                switch (instruction.bit_mode)
                {
                    case Instruction_Data.Bit_Mode_ENUM._8_BIT:
                        CF_new_value = (((byte) destination_value) & 0x80) == 1;
                        new_value_byte = (byte) (((new_value_byte << 1) | (new_value_byte >> (8 - 1))) ^ 1);

                        if (CF_value)
                            new_value_byte++;

                        break;

                    case Instruction_Data.Bit_Mode_ENUM._16_BIT:
                        CF_new_value = (((ushort) destination_value) & 0x8000) == 1;
                        new_value_ushort = (ushort) (((new_value_ushort << 1) | (new_value_ushort >> (16 - 1))) ^ 1);

                        if (CF_value)
                            new_value_ushort++;

                        break;

                    case Instruction_Data.Bit_Mode_ENUM._32_BIT:
                        CF_new_value = (((uint) destination_value) & 0x80000000) == 1;
                        new_value_uint = (new_value_uint << 1) | (new_value_uint >> (32 - 1)) ^ 1;

                        if (CF_value)
                            new_value_uint++;

                        break;

                    case Instruction_Data.Bit_Mode_ENUM._64_BIT:
                        CF_new_value = (destination_value & 0x8000000000000000) == 1;
                        new_value_ulong = (new_value_ulong << 1) | (new_value_ulong >> (64 - 1)) ^ 1;

                        if (CF_value)
                            new_value_ulong++;

                        break;
                }

                if (CF_new_value)
                    virtual_system.SetEflags(virtual_system.GetEFLAGS() ^ virtual_system.GetEFLAGSMasks()[0] + 1);
                else
                    virtual_system.SetEflags(virtual_system.GetEFLAGS() ^ virtual_system.GetEFLAGSMasks()[0]);
            }

            switch (instruction.bit_mode)
            {
                case Instruction_Data.Bit_Mode_ENUM._8_BIT:
                    value_to_set = new_value_byte;
                    break;

                case Instruction_Data.Bit_Mode_ENUM._16_BIT:
                    value_to_set = new_value_ushort;
                    break;

                case Instruction_Data.Bit_Mode_ENUM._32_BIT:
                    value_to_set = new_value_uint;
                    break;

                case Instruction_Data.Bit_Mode_ENUM._64_BIT:
                    value_to_set = new_value_ulong;
                    break;
            }

            // the set value function taken from Emulator.cs
            // check if the value needs to be saved in a register or memory location
            if (instruction.destination_register != Instruction_Data.Registers_ENUM.NoN && instruction.destination_register_pointer == false)
            {
                switch (instruction.bit_mode)
                {
                    case Instruction_Data.Bit_Mode_ENUM._8_BIT:
                        virtual_system.SetRegisterByte(instruction.destination_register, (byte) value_to_set, instruction.high_or_low);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._16_BIT:
                        virtual_system.SetRegisterWord(instruction.destination_register, (ushort) value_to_set);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._32_BIT:
                        virtual_system.SetRegisterDouble(instruction.destination_register, (uint) value_to_set);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._64_BIT:
                        virtual_system.SetRegisterQuad(instruction.destination_register, value_to_set);
                        break;
                }
            }
            else
            {
                switch (instruction.bit_mode)
                {
                    case Instruction_Data.Bit_Mode_ENUM._8_BIT:
                        virtual_system.SetByteMemory(memory_index, (byte) value_to_set);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._16_BIT:
                        virtual_system.SetWordMemory(memory_index, (ushort) value_to_set);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._32_BIT:
                        virtual_system.SetDoubleMemory(memory_index, (uint) value_to_set);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._64_BIT:
                        virtual_system.SetQuadMemory(memory_index, value_to_set);
                        break;
                }
            }

            return virtual_system;
        }

        public VirtualSystem RCR(VirtualSystem virtual_system, Instruction instruction, int memory_index, ulong destination_value, int bits_to_shift)
        {
            byte new_value_byte = (byte) destination_value;
            ushort new_value_ushort = (ushort) destination_value;
            uint new_value_uint = (uint) destination_value;
            ulong new_value_ulong = destination_value;

            ulong value_to_set = 0;

            bool CF_value = false;
            bool CF_new_value = false;

            for (int i = 0; i < bits_to_shift; i++)
            {
                // Get the CF value
                CF_value = (virtual_system.GetEFLAGS() & virtual_system.GetEFLAGSMasks()[0]) == 1;

                // get the new CF value and rotate all the bits by one bit
                switch (instruction.bit_mode)
                {
                    case Instruction_Data.Bit_Mode_ENUM._8_BIT:
                        CF_new_value = (((byte) destination_value) & 0x80) == 1;
                        new_value_byte = (byte) (((new_value_byte >> 1) | (new_value_byte << (8 - 1))) ^ 1);

                        if (CF_value)
                            new_value_byte++;

                        break;

                    case Instruction_Data.Bit_Mode_ENUM._16_BIT:
                        CF_new_value = (((ushort) destination_value) & 0x8000) == 1;
                        new_value_ushort = (ushort) (((new_value_ushort >> 1) | (new_value_ushort << (16 - 1))) ^ 1);

                        if (CF_value)
                            new_value_ushort++;

                        break;

                    case Instruction_Data.Bit_Mode_ENUM._32_BIT:
                        CF_new_value = (((uint) destination_value) & 0x80000000) == 1;
                        new_value_uint = (new_value_uint >> 1) | (new_value_uint << (32 - 1)) ^ 1;

                        if (CF_value)
                            new_value_uint++;

                        break;

                    case Instruction_Data.Bit_Mode_ENUM._64_BIT:
                        CF_new_value = (destination_value & 0x8000000000000000) == 1;
                        new_value_ulong = (new_value_ulong >> 1) | (new_value_ulong << (64 - 1)) ^ 1;

                        if (CF_value)
                            new_value_ulong++;

                        break;
                }

                if (CF_new_value)
                    virtual_system.SetEflags(virtual_system.GetEFLAGS() ^ virtual_system.GetEFLAGSMasks()[0] + 1);
                else
                    virtual_system.SetEflags(virtual_system.GetEFLAGS() ^ virtual_system.GetEFLAGSMasks()[0]);
            }

            switch (instruction.bit_mode)
            {
                case Instruction_Data.Bit_Mode_ENUM._8_BIT:
                    value_to_set = new_value_byte;
                    break;

                case Instruction_Data.Bit_Mode_ENUM._16_BIT:
                    value_to_set = new_value_ushort;
                    break;

                case Instruction_Data.Bit_Mode_ENUM._32_BIT:
                    value_to_set = new_value_uint;
                    break;

                case Instruction_Data.Bit_Mode_ENUM._64_BIT:
                    value_to_set = new_value_ulong;
                    break;
            }

            // the set value function taken from Emulator.cs
            // check if the value needs to be saved in a register or memory location
            if (instruction.destination_register != Instruction_Data.Registers_ENUM.NoN && instruction.destination_register_pointer == false)
            {
                switch (instruction.bit_mode)
                {
                    case Instruction_Data.Bit_Mode_ENUM._8_BIT:
                        virtual_system.SetRegisterByte(instruction.destination_register, (byte) value_to_set, instruction.high_or_low);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._16_BIT:
                        virtual_system.SetRegisterWord(instruction.destination_register, (ushort) value_to_set);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._32_BIT:
                        virtual_system.SetRegisterDouble(instruction.destination_register, (uint) value_to_set);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._64_BIT:
                        virtual_system.SetRegisterQuad(instruction.destination_register, value_to_set);
                        break;
                }
            }
            else
            {
                switch (instruction.bit_mode)
                {
                    case Instruction_Data.Bit_Mode_ENUM._8_BIT:
                        virtual_system.SetByteMemory(memory_index, (byte) value_to_set);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._16_BIT:
                        virtual_system.SetWordMemory(memory_index, (ushort) value_to_set);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._32_BIT:
                        virtual_system.SetDoubleMemory(memory_index, (uint) value_to_set);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._64_BIT:
                        virtual_system.SetQuadMemory(memory_index, value_to_set);
                        break;
                }
            }

            return virtual_system;
        }

        public ulong ROL(Instruction_Data.Bit_Mode_ENUM bit_mode, ulong value, int bits_to_shift)
        {
            byte new_value_byte = (byte) value;
            ushort new_value_ushort = (ushort) value;
            uint new_value_uint = (uint) value;
            ulong new_value_ulong = value;

            switch (bit_mode)
            {
                case Instruction_Data.Bit_Mode_ENUM._8_BIT:
                    return (ulong) ((new_value_byte << bits_to_shift) | (new_value_byte >> (8 - bits_to_shift)));

                case Instruction_Data.Bit_Mode_ENUM._16_BIT:
                    return (ulong) ((new_value_ushort << bits_to_shift) | (new_value_ushort >> (16 - bits_to_shift)));

                case Instruction_Data.Bit_Mode_ENUM._32_BIT:
                    return (new_value_uint << bits_to_shift) | (new_value_uint << (32 - bits_to_shift));

                case Instruction_Data.Bit_Mode_ENUM._64_BIT:
                    return (new_value_ulong << bits_to_shift) | (new_value_ulong >> (64 - bits_to_shift));
            }

            return 0;
        }

        public ulong ROR(Instruction_Data.Bit_Mode_ENUM bit_mode, ulong value, int bits_to_shift)
        {
            byte new_value_byte = (byte)value;
            ushort new_value_ushort = (ushort)value;
            uint new_value_uint = (uint)value;
            ulong new_value_ulong = value;

            switch (bit_mode)
            {
                case Instruction_Data.Bit_Mode_ENUM._8_BIT:
                    return (ulong) ((new_value_byte >> bits_to_shift) | (new_value_byte << (8 - bits_to_shift)));

                case Instruction_Data.Bit_Mode_ENUM._16_BIT:
                    return (ulong) ((new_value_ushort >> bits_to_shift) | (new_value_ushort << (16 - bits_to_shift)));

                case Instruction_Data.Bit_Mode_ENUM._32_BIT:
                    return (new_value_uint >> bits_to_shift) | (new_value_uint << (32 - bits_to_shift));

                case Instruction_Data.Bit_Mode_ENUM._64_BIT:
                    return (new_value_ulong >> bits_to_shift) | (new_value_ulong << (64 - bits_to_shift));
            }

            return 0;
        }
    }
}
