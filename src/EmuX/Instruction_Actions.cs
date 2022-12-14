using System;
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
        /// The NOP instruction, do I need to explain it ?
        /// lol no
        /// </summary>
        public void NOP() { }
    }
}
