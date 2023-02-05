using System.Security.Cryptography.X509Certificates;

namespace EmuX
{
    internal class Instruction_Actions
    {
        /// <summary>
        /// The AAA instruction
        /// </summary>
        /// <param name="ax_register_value">The value of the AX register</param>
        /// <param name="flags">The EFLAGS register value</param>
        /// <returns>The adjusted value of the AX register and the adjusted value of the EFLAGS register</returns>
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
        /// <param name="ax_register_value">The current value of the AX register</param>
        /// <returns>The sum of AH and AL</returns>
        public ushort AAD(ushort ax_register_value)
        {
            return (ushort) ((ax_register_value & 0xFF00) + (ax_register_value & 0x00FF));
        }

        /// <summary>
        /// The AAM instruction
        /// </summary>
        /// <param name="ax_register_value">The current value of the AX register</param>
        /// <returns>The sum of AH / 10 plus the remainder of the division</returns>
        public ushort AAM(ushort ax_register_value)
        {
            return (ushort) ((((ax_register_value & 0xFF00) / 10) << 8) + (ax_register_value & 0xFF00) % 10);
        }

        /// <summary>
        /// Because this instruction can modify the EFLAGS register and the accumulator at the same time it will return
        /// a virtual system instead of a value
        /// </summary>
        /// <param name="virtual_system">The current state of the virtiual systyem</param>
        /// <returns>The modified virtual system</returns>
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
        /// <param name="destination">The destination value</param>
        /// <param name="source">The source value</param>
        /// <param name="flags">The EFLAGS register value</param>
        /// <returns>A ulong value of the result of destination + source + CF</returns>
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
        /// <param name="destination">The destination value</param>
        /// <param name="source">The source value</param>
        /// <returns>The ulong value of the destination + source</returns>
        public ulong ADD(ulong destination, ulong source)
        {
            return destination + source;
        }

        /// <summary>
        /// The AND instruction
        /// </summary>
        /// <param name="destination">The destination value</param>
        /// <param name="source">The source value</param>
        /// <returns>The bitwise AND operation of destination & source</returns>
        public ulong AND(ulong destination, ulong source)
        {
            ulong toReturn = destination;
            toReturn &= source;

            return toReturn;
        }

        /// <summary>
        /// The CALL instruction, it jumps to the specified label and then returns back once it finds the RET instruction
        /// </summary>
        /// <param name="labels">The labels (string, int), the string is the label name and the int is the line of the label</param>
        /// <param name="label_to_find">The label to go to</param>
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
        /// <param name="source">The value of the AL register</param>
        /// <returns>The new value of the AH register</returns>
        public byte CBW(byte al_register_value)
        {
            if (al_register_value < 128)
                return 0;

            return byte.MaxValue;
        }

        /// <summary>
        /// The CLC instruction, if the carry flag is true then set it to false
        /// </summary>
        /// <param name="flags">The EFLAGS value</param>
        /// <returns>The new EFLAGS value</returns>
        public uint CLC(uint flags)
        {
            if (flags % 2 == 1)
                flags--;

            return flags;
        }

        /// <summary>
        /// The CLD instruction, if the destination flag is set to true then set it to false
        /// </summary>
        /// <param name="flags">The EFLAGS value</param>
        /// <returns>The new EFLAGS value</returns>
        public uint CLD(uint flags)
        {
            return flags & 0xFFFFFBFF;
        }

        /// <summary>
        /// The CLI instruction, if the interrupt flag is set to true then set it to false
        /// </summary>
        /// <param name="flags">The EFLAGS value</param>
        /// <returns>The new EFLAGS value</returns>
        public uint CLI(uint flags)
        {
            return flags & 0xFFFFFDFF;
        }

        /// <summary>
        /// The CMC instruction, executes a not operation to the carry flag
        /// </summary>
        /// <param name="flags">The EFLAGS value</param>
        /// <returns>The new EFLAGS value</returns>
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
        /// <param name="flags">The current EFLAGS value</param>
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
        /// <param name="ax_register_value">The value of the AX register</param>
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
        /// <param name="flags">The EFLAGS register</param>
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
        /// <param name="flags">The EFLAGS register</param>
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
        /// <param name="value">The value to decrement</param>
        /// <param name="bit_mode">The bitmode of the instruction, 8 / 16 / 32 bit, if a 64bit argument is provided the code will not check for underflow</param>
        /// <returns>The original value - 1</returns>
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

        // TODO
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

        /// <summary>
        /// The NOP instruction, do I need to explain it ?
        /// lol no
        /// </summary>
        public void NOP() { }

        /// <summary>
        /// This seems useless but I want all instructions to be in this file and class
        /// </summary>
        /// <param name="value">The value to return</param>
        /// <returns>The value</returns>
        public ulong MOV(ulong value)
        {
            return value;
        }
    }
}
