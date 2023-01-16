using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX
{
    class StaticData
    {
        public string name = "";
        public int memory_location;
        public SIZE size_in_bits;
        public ulong value;
        
        public enum SIZE
        {
            _8_BIT,
            _16_BIT,
            _32_BIT,
            _64_BIT
        }
    }
}
