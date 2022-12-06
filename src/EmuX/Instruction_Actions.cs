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

        public long ADC(long destination, long source, bool carry_flag)
        {
            long toReturn = toReturn = destination + source;

            if (carry_flag)
                toReturn++;

            return toReturn;
        }

        public long ADD(long destination, long source)
        {
            return destination + source;
        }

        public long AND(long destination, long source)
        {
            long toReturn = destination;
            toReturn &= source;

            return toReturn;
        }

        public long CBW(long source)
        {
            long toReturn = (byte) source;
            toReturn = (toReturn << 8) + toReturn;

            return toReturn;
        }

        public void NOP() { }
    }
}
