﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX
{
    internal class Instruction_Actions
    {
        /// <summary>
        /// NOT IMPLEMENTED
        /// </summary>
        public void AAD()
        {
            // I do not understand what this does and / or I cannot find what it does online
        }

        /// <summary>
        /// NOT IMPLEMENTED
        /// </summary>
        public void AAM()
        {
            // I do not understand what this does and / or I cannot find what it does online
        }

        /// <summary>
        /// The ADC (ADd with Carry) instruction
        /// </summary>
        /// <param name="destination">The destination value</param>
        /// <param name="source">The source value</param>
        /// <param name="carry_flag">The carry flag value (bool)</param>
        /// <returns>A ulong value of the result of destination + source + carry_flag</returns>
        public ulong ADC(ulong destination, ulong source, bool carry_flag)
        {
            ulong toReturn = destination + source;

            if (carry_flag)
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
        /// <param name="source">The source value</param>
        /// <returns>The shifter left value of source + source (return -> (source << 8) + source)</returns>
        public ulong CBW(ulong source)
        {
            ulong toReturn = (byte) source;
            toReturn = (toReturn << 8) + toReturn;

            return toReturn;
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
        /// The CMPSB instruction, it compares two bytes in memory and sets the EFLAGS register accordingly
        /// </summary>
        /// <param name="operand_one">The first byte to compare</param>
        /// <param name="operand_two">The second byte to compare against</param>
        /// <param name="flags">The current EFLAGS register value</param>
        /// <returns>The new EFLAGS register value</returns>
        public uint CMPSB(byte operand_one, byte operand_two, uint flags)
        {
            if (operand_one < operand_two)
            {
                // set the C flag to one if its not already set to one
                if (flags % 2 == 0)
                    flags++;

                // set the Z flag to zero
                flags = flags & 0xFFFFFFBF;
            }
            else if (operand_one > operand_two)
            {
                // set the C flag to zero if its not already set to zero
                flags = flags & 0xFFFFFFFE;

                // set the Z flag to zero
                flags = flags & 0xFFFFFFBF;
            }
            else
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
        /// The CMPSW instruction, it compares two words (word = 2 bytes) in memory and sets the EFLAGS register accordingly
        /// </summary>
        /// <param name="operand_one">The first word to compare</param>
        /// <param name="operand_two">The second word to compare against</param>
        /// <param name="flags">The current EFLAGS register value</param>
        /// <returns>The new EFLAGS register value</returns>
        public uint CMPSW(ushort operand_one, ushort operand_two, uint flags)
        {
            if (operand_one < operand_two)
            {
                // set the C flag to one if its not already set to one
                if (flags % 2 == 0)
                    flags++;

                // set the Z flag to zero
                flags = flags & 0xFFFFFFBF;
            }
            else if (operand_one > operand_two)
            {
                // set the C flag to zero if its not already set to zero
                flags = flags & 0xFFFFFFFE;

                // set the Z flag to zero
                flags = flags & 0xFFFFFFBF;
            }
            else
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
        /// <param name="value">The RDX value</param>
        /// <returns>The upmost 48bit of the register</returns>
        public ulong CWD(ulong value)
        {
            return value & 0xFFFFFFFFFFFF0000;
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
        /// The NOP instruction, do I need to explain it ?
        /// lol no
        /// </summary>
        public void NOP() { }
    }
}
