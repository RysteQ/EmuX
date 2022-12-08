using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX
{
    internal class Instruction_Actions
    {
        public void AAD()
        {
            // I do not understand what this does and / or I cannot find what it does online
        }

        public void AAM()
        {
            // I do not understand what this does and / or I cannot find what it does online
        }

        public ulong ADC(ulong destination, ulong source, bool carry_flag)
        {
            ulong toReturn = destination + source;

            if (carry_flag)
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

        public ulong CBW(ulong source)
        {
            ulong toReturn = (byte) source;
            toReturn = (toReturn << 8) + toReturn;

            return toReturn;
        }

        public void NOP() { }
    }
}
