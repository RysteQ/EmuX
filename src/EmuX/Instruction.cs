using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX
{
    internal class Instruction
    {
        public Instruction_ENUM instruction;
        public Instruction_Variant_ENUM variant;
        public Registers_ENUM destination_register;
        public Registers_ENUM source_register;
        public Memory_Type_ENUM destination_memory_type;
        public Memory_Type_ENUM source_memory_type;
        public string destination_memory_name;
        public string source_memory_name;

        // All the possible instructions (I will slowly build upon this)
        public enum Instruction_ENUM
        {

        }

        public enum Instruction_Variant_ENUM
        {
            NON,
            SINGLE_REGISTER,
            SINGLE_VALUE,
            DESTINATION_REGISTER_SOURCE_REGISTER,
            DESTINATION_REGISTER_VALUE,
            DESTINATION_REGISTER_ADDRESS,
            VALUE_SOURCE_REGISTER,
            ADDRESS_SOURCE_REGISTER
        }

        public enum Memory_Type_ENUM
        {
            VALUE,
            ADDRESS
        }

        public enum Registers_ENUM
        {
            RAX,
            RBX,
            RCX,
            RDX,
            RSI,
            RDI,
            RSP,
            RBP,

            LAST    // <----- DO NOT USE THIS
        }
    }
}
