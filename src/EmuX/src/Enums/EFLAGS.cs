using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX.src.Enums
{
    public class EFLAGS
    {
        public enum EFLAGS_ENUM
        {
            CF = 0x00000001,
            PF = 0x00000004,
            AF = 0x00000010,
            ZF = 0x00000040,
            SF = 0x00000080,
            TF = 0x00000100,
            IF = 0x00000200,
            DF = 0x00000400,
            OF = 0x00000800,
            IOPL = 0x00003000,
            NT = 0x00004000,
            RF = 0x00010000,
            VM = 0x00020000,
            AC = 0x00040000,
            VIF = 0x00080000,
            VIP = 0x00100000,
            ID = 0x00200000
        }
    }
}
