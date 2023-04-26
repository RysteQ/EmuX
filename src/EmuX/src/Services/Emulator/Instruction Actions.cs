using EmuX.src.Models;

namespace EmuX.src.Services.Emulator
{
    internal class Instruction_Actions
    {
        public (ushort, uint) AAA(ushort ax_register_value, uint flags)
        {
            if ((ax_register_value & 0x000F) > 9 || (flags & 0x00000010) == 1)
            {
                ax_register_value = (ushort)((ax_register_value & 0xFF00) + 1 + (ax_register_value & 0x00FF) + 6);
                flags = flags | 0x00000010;
                flags = flags | 0x00000001;

                return (ax_register_value, flags);
            }

            flags = flags & 0xFFFFFFFC;

            return (ax_register_value, flags);
        }

        public ushort AAD(ushort ax_register_value)
        {
            return (ushort)((ax_register_value & 0xFF00) + (ax_register_value & 0x00FF));
        }

        public ushort AAM(ushort ax_register_value)
        {
            return (ushort)(((ax_register_value & 0xFF00) / 10 << 8) + (ax_register_value & 0xFF00) % 10);
        }

        public VirtualSystem AAS(VirtualSystem virtual_system)
        {
            int low_nibble_al = virtual_system.GetRegisterByte(Registers.Registers_ENUM.RAX, false) & 0x0F;
            bool af_status_flag = (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[2]) == 1;
            uint af_cf_status_flag_mask = virtual_system.GetEFLAGSMasks()[2] + virtual_system.GetEFLAGSMasks()[0];

            if (low_nibble_al > 9 || af_status_flag)
            {
                virtual_system.SetRegisterByte(Registers.Registers_ENUM.RAX, (byte)(low_nibble_al - 6), false);
                virtual_system.SetRegisterByte(Registers.Registers_ENUM.RAX, (byte)(virtual_system.GetRegisterByte(Registers.Registers_ENUM.RAX, true) - 1), true);
                virtual_system.EFLAGS = virtual_system.EFLAGS | af_cf_status_flag_mask;
            }
            else
            {
                virtual_system.EFLAGS = virtual_system.EFLAGS ^ af_cf_status_flag_mask;
            }

            return virtual_system;
        }

        public ulong ADC(ulong destination, ulong source, uint flags)
        {
            ulong toReturn = destination + source;

            if (flags % 2 == 1)
                toReturn++;

            return toReturn;
        }

        public ulong ADD(ulong destination, ulong source)
        {
            return destination + source;
        }

        public ulong AND(ulong destination, ulong source)
        {
            ulong toReturn = destination;
            toReturn &= source;

            return toReturn;
        }

        public (bool, int) CALL(List<Models.Label> labels, string label_to_find)
        {
            foreach (Models.Label label in labels)
                if (label.name == label_to_find)
                    return (true, label.line);

            return (false, -1);
        }

        public byte CBW(byte al_register_value)
        {
            if (al_register_value < 128)
                return 0;

            return byte.MaxValue;
        }

        public uint CLC(uint flags)
        {
            if (flags % 2 == 1)
                flags--;

            return flags;
        }

        public uint CLD(uint flags)
        {
            return flags & 0xFFFFFBFF;
        }

        public uint CLI(uint flags)
        {
            return flags & 0xFFFFFDFF;
        }

        public uint CMC(uint flags)
        {
            if (flags % 2 == 0)
                return flags + 1;
            else
                return flags - 1;
        }

        public uint CMP(ulong operand_one, ulong operand_two, uint flags)
        {
            uint toReturn = 0;

            // CF
            if (operand_one < operand_two)
                toReturn += 0x00000001;

            // RESERVED ALWAYS 1
            toReturn += 0x00000002;

            // PF
            if ((operand_one + operand_two) % 2 == 1)
                toReturn += 0x00000004;

            // AF
            if ((operand_one & 0x0000000F) + (operand_two & 0x0000000F) > 15)
                toReturn += 0x00000010;

            // ZF
            if (operand_one == operand_two)
                toReturn += 0x00000040;

            // SF
            if (operand_one - operand_two >> 63 == 1)
                toReturn += 0x00000080;

            // OF
            if (operand_one >> 63 != operand_two >> 63)
                toReturn += 0x00000800;

            return toReturn;
        }

        public ushort CWD(ushort ax_register_value)
        {
            if (ax_register_value < 32768)
                return 0;

            return ushort.MaxValue;
        }

        public byte DAA(byte value, uint flags)
        {
            if ((value & 0x0F) > 9 || (flags & 0x00000010) == 1)
                return (byte)(value + 6);
            else if (value > 0x9F || (flags & 0x00000001) == 1)
                return (byte)(value + 96);

            return value;
        }

        public byte DAS(byte value, uint flags)
        {
            if ((value & 0x0F) > 9 || (flags & 0x00000010) == 1)
                return (byte)(value - 6);
            else if (value > 0x9F || (flags & 0x00000001) == 1)
                return (byte)(value - 96);

            return value;
        }

        public ulong DEC(ulong value, SIZE.Size_ENUM bit_mode)
        {
            switch (bit_mode)
            {
                case SIZE.Size_ENUM._8_BIT:
                    if ((value & 0x00000000000000FF) == 0)
                        return byte.MaxValue;

                    break;

                case SIZE.Size_ENUM._16_BIT:
                    if ((value & 0x000000000000FFFF) == 0)
                        return ushort.MaxValue;

                    break;

                case SIZE.Size_ENUM._32_BIT:
                    if ((value & 0x00000000FFFFFFFF) == 0)
                        return uint.MaxValue;

                    break;

                case SIZE.Size_ENUM._64_BIT:
                    if (value == 0)
                        return ulong.MaxValue;

                    break;
            }

            return value - 1;
        }

        public VirtualSystem DIV(VirtualSystem virtual_system, SIZE.Size_ENUM bit_mode, ulong destination_value)
        {
            ulong quotient = 0;
            ulong remainder = 0;
            ulong divident = 0;

            switch (bit_mode)
            {
                case SIZE.Size_ENUM._8_BIT:
                    divident = virtual_system.GetRegisterQuad(Registers.Registers_ENUM.RAX);
                    quotient = divident / destination_value;
                    remainder = divident % destination_value;

                    virtual_system.SetRegisterByte(Registers.Registers_ENUM.RAX, (byte)quotient, false);
                    virtual_system.SetRegisterByte(Registers.Registers_ENUM.RAX, (byte)remainder, true);

                    break;

                case SIZE.Size_ENUM._16_BIT:
                    divident = (ulong)((virtual_system.GetRegisterWord(Registers.Registers_ENUM.RDX) << 16) + virtual_system.GetRegisterWord(Registers.Registers_ENUM.RAX));

                    quotient = divident / destination_value;
                    remainder = divident % destination_value;

                    virtual_system.SetRegisterWord(Registers.Registers_ENUM.RAX, (ushort)quotient);
                    virtual_system.SetRegisterWord(Registers.Registers_ENUM.RDX, (ushort)remainder);

                    break;

                case SIZE.Size_ENUM._32_BIT:
                    divident = (virtual_system.GetRegisterDouble(Registers.Registers_ENUM.RDX) << 32) + virtual_system.GetRegisterDouble(Registers.Registers_ENUM.RAX);

                    quotient = divident / destination_value;
                    remainder = divident % destination_value;

                    virtual_system.SetRegisterDouble(Registers.Registers_ENUM.RAX, (uint)quotient);
                    virtual_system.SetRegisterDouble(Registers.Registers_ENUM.RDX, (uint)remainder);

                    break;
            }

            return virtual_system;
        }

        public ulong INC(ulong value_to_increment)
        {
            return value_to_increment + 1;
        }

        public int JA(List<Models.Label> labels, string label_to_jump_to, bool cf_flag_status, bool zf_flag_status)
        {
            if ((cf_flag_status || zf_flag_status) == false)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JAE(List<Models.Label> labels, string label_to_jump_to, bool cf_flag_status)
        {
            if (cf_flag_status == false)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JB(List<Models.Label> labels, string label_to_jump_to, bool cf_flag_status)
        {
            if (cf_flag_status)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JBE(List<Models.Label> labels, string label_to_jump_to, bool cf_flag_status, bool zf_flag_status)
        {
            if (cf_flag_status&& zf_flag_status)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JC(List<Models.Label> labels, string label_to_jump_to, bool cf_flag_status)
        {
            if (cf_flag_status)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JE(List<Models.Label> labels, string label_to_jump_to, bool zf_flag_status)
        {
            if (zf_flag_status)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JG(List<Models.Label> labels, string label_to_jump_to, bool zf_flag_status, bool sf_flag_status, bool of_status_flag)
        {
            if (zf_flag_status == false || sf_flag_status == of_status_flag)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JGE(List<Models.Label> labels, string label_to_jump_to, bool sf_flag_status, bool of_flag_status)
        {
            if (sf_flag_status == of_flag_status)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JL(List<Models.Label> labels, string label_to_jump_to, bool sf_flag_status, bool of_flag_status)
        {
            if (sf_flag_status != of_flag_status)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JNA(List<Models.Label> labels, string label_to_jump_to, bool cf_flag_status, bool zf_flag_status)
        {
            if (cf_flag_status && zf_flag_status)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JNAE(List<Models.Label> labels, string label_to_jump_to, bool cf_flag_status)
        {
            if (cf_flag_status)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JNB(List<Models.Label> labels, string label_to_jump_to, bool cf_flag_status)
        {
            if (cf_flag_status == false)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JNBE(List<Models.Label> labels, string label_to_jump_to, bool cf_flag_status, bool zf_flag_status)
        {
            if ((cf_flag_status || zf_flag_status) == false)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JNC(List<Models.Label> labels, string label_to_jump_to, bool cf_flag_status)
        {
            if (cf_flag_status == false)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JNE(List<Models.Label> labels, string label_to_jump_to, bool zf_flag_status)
        {
            if (zf_flag_status == false)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JNG(List<Models.Label> labels, string label_to_jump_to, bool zf_flag_status, bool sf_flag_status, bool of_flag_status)
        {
            if (zf_flag_status && sf_flag_status != of_flag_status)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JNGE(List<Models.Label> labels, string label_to_jump_to, bool sf_flag_status, bool of_flag_status)
        {
            if (sf_flag_status != of_flag_status)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JNL(List<Models.Label> labels, string label_to_jump_to, bool sf_flag_status, bool of_flag_status)
        {
            if (sf_flag_status == of_flag_status)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JNLE(List<Models.Label> labels, string label_to_jump_to, bool zf_status_flag, bool sf_flag_status, bool of_flag_status)
        {
            if (zf_status_flag == false || sf_flag_status == of_flag_status)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JNO(List<Models.Label> labels, string label_to_jump_to, bool of_flag_status)
        {
            if (of_flag_status == false)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JNP(List<Models.Label> labels, string label_to_jump_to, bool pf_flag_status)
        {
            if (pf_flag_status == false)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JNS(List<Models.Label> labels, string label_to_jump_to, bool sf_flag_status)
        {
            if (sf_flag_status == false)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JNZ(List<Models.Label> labels, string label_to_jump_to, bool zf_flag_status)
        {
            if (zf_flag_status == false)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JO(List<Models.Label> labels, string label_to_jump_to, bool of_flag_status)
        {
            if (of_flag_status)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JP(List<Models.Label> labels, string label_to_jump_to, bool pf_flag_status)
        {
            if (pf_flag_status)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JPE(List<Models.Label> labels, string label_to_jump_to, bool pf_flag_status)
        {
            if (pf_flag_status)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JPO(List<Models.Label> labels, string label_to_jump_to, bool pf_flag_status)
        {
            if (pf_flag_status == false)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JS(List<Models.Label> labels, string label_to_jump_to, bool sf_flag_status)
        {
            if (sf_flag_status)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JZ(List<Models.Label> labels, string label_to_jump_to, bool zf_flag_status)
        {
            if (zf_flag_status)
                foreach (Models.Label label in labels)
                    if (label.name == label_to_jump_to)
                        return label.line;

            return -1;
        }

        public int JMP(List<Models.Label> labels, string label_to_jump_to)
        {
            foreach (Models.Label label in labels)
                if (label.name == label_to_jump_to)
                    return label.line;

            return -1;
        }

        public byte LAHF(uint EFLAGS_register_value)
        {
            return (byte)(EFLAGS_register_value & 0x000000FF);
        }

        public int LEA(List<StaticData> static_data, string static_data_to_find)
        {
            for (int i = 0; i < static_data.Count; i++)
                if (static_data[i].name == static_data_to_find)
                    return static_data[i].memory_location;

            return 0;
        }

        public ulong MOV(ulong value)
        {
            return value;
        }

        public VirtualSystem MUL(VirtualSystem virtual_system, ulong destination_value, SIZE.Size_ENUM bit_mode)
        {
            if (bit_mode == SIZE.Size_ENUM._8_BIT)
            {
                virtual_system.SetRegisterWord(Registers.Registers_ENUM.RAX, (ushort)(virtual_system.GetRegisterByte(Registers.Registers_ENUM.RAX, false) * destination_value));
            }
            else if (bit_mode == SIZE.Size_ENUM._16_BIT)
            {
                virtual_system.SetRegisterWord(Registers.Registers_ENUM.RDX, (ushort)(virtual_system.GetRegisterWord(Registers.Registers_ENUM.RAX) * destination_value & 0xFFFF0000));
                virtual_system.SetRegisterWord(Registers.Registers_ENUM.RAX, (ushort)(virtual_system.GetRegisterWord(Registers.Registers_ENUM.RAX) * destination_value & 0x0000FFFF));
            }
            else
            {
                virtual_system.SetRegisterDouble(Registers.Registers_ENUM.RDX, (uint)(virtual_system.GetRegisterDouble(Registers.Registers_ENUM.RAX) * destination_value & 0xFFFFFFFF00000000));
                virtual_system.SetRegisterDouble(Registers.Registers_ENUM.RAX, (uint)(virtual_system.GetRegisterDouble(Registers.Registers_ENUM.RAX) * destination_value & 0x00000000FFFFFFFF));
            }

            return virtual_system;
        }

        public ulong NEG(ulong value)
        {
            return ~value + 1;
        }

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
                case SIZE.Size_ENUM._8_BIT:
                    value_to_set = virtual_system.PopByte();
                    break;

                case SIZE.Size_ENUM._16_BIT:
                    value_to_set = virtual_system.PopWord();
                    break;

                case SIZE.Size_ENUM._32_BIT:
                    value_to_set = virtual_system.PopDouble();
                    break;

                case SIZE.Size_ENUM._64_BIT:
                    value_to_set = virtual_system.PopQuad();
                    break;
            }

            // the set value function taken from Emulator.cs
            // check if the value needs to be saved in a register or memory location
            if (instruction.destination_register != Registers.Registers_ENUM.NoN && instruction.destination_pointer == false)
            {
                switch (instruction.bit_mode)
                {
                    case SIZE.Size_ENUM._8_BIT:
                        virtual_system.SetRegisterByte(instruction.destination_register, (byte)value_to_set, instruction.destination_high_or_low);
                        break;

                    case SIZE.Size_ENUM._16_BIT:
                        virtual_system.SetRegisterWord(instruction.destination_register, (ushort)value_to_set);
                        break;

                    case SIZE.Size_ENUM._32_BIT:
                        virtual_system.SetRegisterDouble(instruction.destination_register, (uint)value_to_set);
                        break;

                    case SIZE.Size_ENUM._64_BIT:
                        virtual_system.SetRegisterQuad(instruction.destination_register, value_to_set);
                        break;
                }
            }
            else
            {
                switch (instruction.bit_mode)
                {
                    case SIZE.Size_ENUM._8_BIT:
                        virtual_system.SetByteMemory(memory_index, (byte)value_to_set);
                        break;

                    case SIZE.Size_ENUM._16_BIT:
                        virtual_system.SetWordMemory(memory_index, (ushort)value_to_set);
                        break;

                    case SIZE.Size_ENUM._32_BIT:
                        virtual_system.SetDoubleMemory(memory_index, (uint)value_to_set);
                        break;

                    case SIZE.Size_ENUM._64_BIT:
                        virtual_system.SetQuadMemory(memory_index, value_to_set);
                        break;
                }
            }

            return virtual_system;
        }

        public VirtualSystem POPF(VirtualSystem virtual_system)
        {
            virtual_system.EFLAGS = virtual_system.PopWord();
            return virtual_system;
        }

        public VirtualSystem PUSH(VirtualSystem virtual_system, SIZE.Size_ENUM bit_mode, ulong value)
        {
            switch (bit_mode)
            {
                case SIZE.Size_ENUM._8_BIT:
                    virtual_system.PushByte((byte)value);
                    break;

                case SIZE.Size_ENUM._16_BIT:
                    virtual_system.PushWord((ushort)value);
                    break;

                case SIZE.Size_ENUM._32_BIT:
                    virtual_system.PushDouble((uint)value);
                    break;

                case SIZE.Size_ENUM._64_BIT:
                    virtual_system.PushQuad(value);
                    break;
            }

            return virtual_system;
        }

        public VirtualSystem PUSHF(VirtualSystem virtual_system, uint EFLAGS)
        {
            virtual_system.PushWord((ushort)EFLAGS);
            return virtual_system;
        }

        public VirtualSystem RCL(VirtualSystem virtual_system, Instruction instruction, int memory_index, ulong destination_value, int bits_to_shift)
        {
            byte new_value_byte = (byte)destination_value;
            ushort new_value_ushort = (ushort)destination_value;
            uint new_value_uint = (uint)destination_value;
            ulong new_value_ulong = destination_value;

            ulong value_to_set = 0;

            bool CF_value = false;
            bool CF_new_value = false;

            for (int i = 0; i < bits_to_shift; i++)
            {
                // Get the CF value
                CF_value = (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[0]) == 1;

                // get the new CF value and rotate all the bits by one bit
                switch (instruction.bit_mode)
                {
                    case SIZE.Size_ENUM._8_BIT:
                        CF_new_value = ((byte)destination_value & 0x80) == 1;
                        new_value_byte = (byte)((new_value_byte << 1 | new_value_byte >> 8 - 1) ^ 1);

                        if (CF_value)
                            new_value_byte++;

                        break;

                    case SIZE.Size_ENUM._16_BIT:
                        CF_new_value = ((ushort)destination_value & 0x8000) == 1;
                        new_value_ushort = (ushort)((new_value_ushort << 1 | new_value_ushort >> 16 - 1) ^ 1);

                        if (CF_value)
                            new_value_ushort++;

                        break;

                    case SIZE.Size_ENUM._32_BIT:
                        CF_new_value = ((uint)destination_value & 0x80000000) == 1;
                        new_value_uint = new_value_uint << 1 | new_value_uint >> 32 - 1 ^ 1;

                        if (CF_value)
                            new_value_uint++;

                        break;

                    case SIZE.Size_ENUM._64_BIT:
                        CF_new_value = (destination_value & 0x8000000000000000) == 1;
                        new_value_ulong = new_value_ulong << 1 | new_value_ulong >> 64 - 1 ^ 1;

                        if (CF_value)
                            new_value_ulong++;

                        break;
                }

                if (CF_new_value)
                    virtual_system.EFLAGS = virtual_system.EFLAGS ^ virtual_system.GetEFLAGSMasks()[0] + 1;
                else
                    virtual_system.EFLAGS = virtual_system.EFLAGS ^ virtual_system.GetEFLAGSMasks()[0];
            }

            switch (instruction.bit_mode)
            {
                case SIZE.Size_ENUM._8_BIT:
                    value_to_set = new_value_byte;
                    break;

                case SIZE.Size_ENUM._16_BIT:
                    value_to_set = new_value_ushort;
                    break;

                case SIZE.Size_ENUM._32_BIT:
                    value_to_set = new_value_uint;
                    break;

                case SIZE.Size_ENUM._64_BIT:
                    value_to_set = new_value_ulong;
                    break;
            }

            // the set value function taken from Emulator.cs
            // check if the value needs to be saved in a register or memory location
            if (instruction.destination_register != Registers.Registers_ENUM.NoN && instruction.destination_pointer == false)
            {
                switch (instruction.bit_mode)
                {
                    case SIZE.Size_ENUM._8_BIT:
                        virtual_system.SetRegisterByte(instruction.destination_register, (byte)value_to_set, instruction.destination_high_or_low);
                        break;

                    case SIZE.Size_ENUM._16_BIT:
                        virtual_system.SetRegisterWord(instruction.destination_register, (ushort)value_to_set);
                        break;

                    case SIZE.Size_ENUM._32_BIT:
                        virtual_system.SetRegisterDouble(instruction.destination_register, (uint)value_to_set);
                        break;

                    case SIZE.Size_ENUM._64_BIT:
                        virtual_system.SetRegisterQuad(instruction.destination_register, value_to_set);
                        break;
                }
            }
            else
            {
                switch (instruction.bit_mode)
                {
                    case SIZE.Size_ENUM._8_BIT:
                        virtual_system.SetByteMemory(memory_index, (byte)value_to_set);
                        break;

                    case SIZE.Size_ENUM._16_BIT:
                        virtual_system.SetWordMemory(memory_index, (ushort)value_to_set);
                        break;

                    case SIZE.Size_ENUM._32_BIT:
                        virtual_system.SetDoubleMemory(memory_index, (uint)value_to_set);
                        break;

                    case SIZE.Size_ENUM._64_BIT:
                        virtual_system.SetQuadMemory(memory_index, value_to_set);
                        break;
                }
            }

            return virtual_system;
        }

        public VirtualSystem RCR(VirtualSystem virtual_system, Instruction instruction, int memory_index, ulong destination_value, int bits_to_shift)
        {
            byte new_value_byte = (byte)destination_value;
            ushort new_value_ushort = (ushort)destination_value;
            uint new_value_uint = (uint)destination_value;
            ulong new_value_ulong = destination_value;

            ulong value_to_set = 0;

            bool CF_value = false;
            bool CF_new_value = false;

            for (int i = 0; i < bits_to_shift; i++)
            {
                // Get the CF value
                CF_value = (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[0]) == 1;

                // get the new CF value and rotate all the bits by one bit
                switch (instruction.bit_mode)
                {
                    case SIZE.Size_ENUM._8_BIT:
                        CF_new_value = ((byte)destination_value & 0x80) == 1;
                        new_value_byte = (byte)((new_value_byte >> 1 | new_value_byte << 8 - 1) ^ 1);

                        if (CF_value)
                            new_value_byte++;

                        break;

                    case SIZE.Size_ENUM._16_BIT:
                        CF_new_value = ((ushort)destination_value & 0x8000) == 1;
                        new_value_ushort = (ushort)((new_value_ushort >> 1 | new_value_ushort << 16 - 1) ^ 1);

                        if (CF_value)
                            new_value_ushort++;

                        break;

                    case SIZE.Size_ENUM._32_BIT:
                        CF_new_value = ((uint)destination_value & 0x80000000) == 1;
                        new_value_uint = new_value_uint >> 1 | new_value_uint << 32 - 1 ^ 1;

                        if (CF_value)
                            new_value_uint++;

                        break;

                    case SIZE.Size_ENUM._64_BIT:
                        CF_new_value = (destination_value & 0x8000000000000000) == 1;
                        new_value_ulong = new_value_ulong >> 1 | new_value_ulong << 64 - 1 ^ 1;

                        if (CF_value)
                            new_value_ulong++;

                        break;
                }

                if (CF_new_value)
                    virtual_system.EFLAGS = virtual_system.EFLAGS ^ virtual_system.GetEFLAGSMasks()[0] + 1;
                else
                    virtual_system.EFLAGS = virtual_system.EFLAGS ^ virtual_system.GetEFLAGSMasks()[0];
            }

            switch (instruction.bit_mode)
            {
                case SIZE.Size_ENUM._8_BIT:
                    value_to_set = new_value_byte;
                    break;

                case SIZE.Size_ENUM._16_BIT:
                    value_to_set = new_value_ushort;
                    break;

                case SIZE.Size_ENUM._32_BIT:
                    value_to_set = new_value_uint;
                    break;

                case SIZE.Size_ENUM._64_BIT:
                    value_to_set = new_value_ulong;
                    break;
            }

            // the set value function taken from Emulator.cs
            // check if the value needs to be saved in a register or memory location
            if (instruction.destination_register != Registers.Registers_ENUM.NoN && instruction.destination_pointer == false)
            {
                switch (instruction.bit_mode)
                {
                    case SIZE.Size_ENUM._8_BIT:
                        virtual_system.SetRegisterByte(instruction.destination_register, (byte)value_to_set, instruction.destination_high_or_low);
                        break;

                    case SIZE.Size_ENUM._16_BIT:
                        virtual_system.SetRegisterWord(instruction.destination_register, (ushort)value_to_set);
                        break;

                    case SIZE.Size_ENUM._32_BIT:
                        virtual_system.SetRegisterDouble(instruction.destination_register, (uint)value_to_set);
                        break;

                    case SIZE.Size_ENUM._64_BIT:
                        virtual_system.SetRegisterQuad(instruction.destination_register, value_to_set);
                        break;
                }
            }
            else
            {
                switch (instruction.bit_mode)
                {
                    case SIZE.Size_ENUM._8_BIT:
                        virtual_system.SetByteMemory(memory_index, (byte)value_to_set);
                        break;

                    case SIZE.Size_ENUM._16_BIT:
                        virtual_system.SetWordMemory(memory_index, (ushort)value_to_set);
                        break;

                    case SIZE.Size_ENUM._32_BIT:
                        virtual_system.SetDoubleMemory(memory_index, (uint)value_to_set);
                        break;

                    case SIZE.Size_ENUM._64_BIT:
                        virtual_system.SetQuadMemory(memory_index, value_to_set);
                        break;
                }
            }

            return virtual_system;
        }

        public ulong ROL(SIZE.Size_ENUM bit_mode, ulong value, int bits_to_shift)
        {
            byte new_value_byte = (byte)value;
            ushort new_value_ushort = (ushort)value;
            uint new_value_uint = (uint)value;
            ulong new_value_ulong = value;

            switch (bit_mode)
            {
                case SIZE.Size_ENUM._8_BIT:
                    return (ulong)(new_value_byte << bits_to_shift | new_value_byte >> 8 - bits_to_shift);

                case SIZE.Size_ENUM._16_BIT:
                    return (ulong)(new_value_ushort << bits_to_shift | new_value_ushort >> 16 - bits_to_shift);

                case SIZE.Size_ENUM._32_BIT:
                    return new_value_uint << bits_to_shift | new_value_uint << 32 - bits_to_shift;

                case SIZE.Size_ENUM._64_BIT:
                    return new_value_ulong << bits_to_shift | new_value_ulong >> 64 - bits_to_shift;
            }

            return 0;
        }

        public ulong ROR(SIZE.Size_ENUM bit_mode, ulong value, int bits_to_shift)
        {
            byte new_value_byte = (byte)value;
            ushort new_value_ushort = (ushort)value;
            uint new_value_uint = (uint)value;
            ulong new_value_ulong = value;

            switch (bit_mode)
            {
                case SIZE.Size_ENUM._8_BIT:
                    return (ulong)(new_value_byte >> bits_to_shift | new_value_byte << 8 - bits_to_shift);

                case SIZE.Size_ENUM._16_BIT:
                    return (ulong)(new_value_ushort >> bits_to_shift | new_value_ushort << 16 - bits_to_shift);

                case SIZE.Size_ENUM._32_BIT:
                    return new_value_uint >> bits_to_shift | new_value_uint << 32 - bits_to_shift;

                case SIZE.Size_ENUM._64_BIT:
                    return new_value_ulong >> bits_to_shift | new_value_ulong << 64 - bits_to_shift;
            }

            return 0;
        }

        public byte SAHF(uint EFLAGS, uint[] EFLAGS_masks)
        {
            byte toReturn = 0;

            // get the SF
            toReturn = (byte)((EFLAGS & EFLAGS_masks[4]) << 7);

            // get the ZF
            toReturn += (byte)((EFLAGS & EFLAGS_masks[3]) << 6);

            // get the AF
            toReturn += (byte)((EFLAGS & EFLAGS_masks[2]) << 4);

            // get the PF
            toReturn += (byte)((EFLAGS & EFLAGS_masks[1]) << 2);

            // set 1 to the second lowest bit
            toReturn += 1 << 1;

            // get the CF
            toReturn += (byte)(EFLAGS & EFLAGS_masks[0]);

            return toReturn;
        }

        public ulong SAL(ulong value, int bits_to_shift)
        {
            return value << bits_to_shift;
        }

        public ulong SAR(ulong value, int bits_to_shift)
        {
            return value >> bits_to_shift;
        }

        public ulong SBB(ulong destination_value, ulong source_value, bool CF)
        {
            if (CF)
                return destination_value - (source_value + 1);

            return destination_value - source_value;
        }

        public ulong SHL(ulong value, int bits_to_shift)
        {
            return value << bits_to_shift;
        }

        public ulong SHR(ulong value, int bits_to_shift)
        {
            return value >> bits_to_shift;
        }

        public uint STC(uint EFLAGS, uint CF_mask)
        {
            if ((EFLAGS & CF_mask) == 1)
                return EFLAGS;

            return EFLAGS + CF_mask;
        }

        public uint STD(uint EFLAGS, uint DF_mask)
        {
            if ((EFLAGS & DF_mask) == 1)
                return EFLAGS;

            return EFLAGS + DF_mask;
        }

        public uint STI(uint EFLAGS, uint IF_mask)
        {
            if ((EFLAGS & IF_mask) == 1)
                return EFLAGS;

            return EFLAGS + IF_mask;
        }

        public ulong SUB(ulong destination_value, ulong source_value)
        {
            return destination_value - source_value;
        }

        public ulong XOR(ulong destination_value, ulong source_value)
        {
            return destination_value ^ source_value;
        }
    }
}
