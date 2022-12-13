using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX
{
    internal class Instruction
    {
        public Instruction_Data.Instruction_ENUM instruction;
        public Instruction_Data.Instruction_Variant_ENUM variant;
        public Instruction_Data.Registers_ENUM destination_register;
        public Instruction_Data.Registers_ENUM source_register;
        public Instruction_Data.Memory_Type_ENUM destination_memory_type;
        public Instruction_Data.Memory_Type_ENUM source_memory_type;
        public Instruction_Data.Bit_Mode_ENUM bit_mode;
        public string destination_memory_name;
        public string source_memory_name;
        public string label;
    }

    internal class Instruction_Data
    {
        // All the possible instructions (I will slowly build upon this)
        public enum Instruction_ENUM
        {
            // AAA, This instructioni is not in x86_64
            AAD,
            AAM,
            // AAS, This instructioni is not in x86_64
            ADC,
            ADD,
            AND,
            CALL,
            CBW,
            CLC,
            CLD,
            CLI,
            CMC,
            CMP,
            CMPSB,
            CMPSW,
            CWD,
            DAA,
            DAS,
            DEC,
            DIV,
            ESC,
            HTL,
            HLT,
            IDIV,
            IMUL,
            INC,
            INT,
            INTO,
            IRET,
            JA, JAE, JB, JBE, JC, JE, JG, JGE, JL, JLE, JNA, JNAE, JNB, JNBE, JNC, JNE, JNG, JNGE, JNL, JNLE, JNO, JNP, JNS, JNZ, JO, JP, JPE, JPO, JS, JZ, // Jcc
            JCXZ,
            JMP,
            LAHF,
            LDS,
            LEA,
            LES,
            // LOCK,
            LODSB,
            LODSW,
            MOV,
            MOVSB,
            MOVSW,
            MUL,
            NEG,
            NOP,
            NOT,
            OR,
            // OUT,
            POP,
            POPF,
            PUSH,
            PUSHF,
            RCL,
            RCR,
            RET,
            // RETN,
            // RETF,
            ROL,
            ROR,
            SAHF,
            SAL,
            SAR,
            SBB,
            SCASB,
            SCASW,
            SHL,
            SHR,
            STC,
            STD,
            STI,
            STOSB,
            STOSW,
            SUB,
            XOR,

            LABEL,
            NoN
        }

        public enum Instruction_Variant_ENUM
        {
            SINGLE,
            SINGLE_REGISTER,
            SINGLE_VALUE,
            SINGLE_ADDRESS_VALUE,
            DESTINATION_REGISTER_SOURCE_REGISTER,
            DESTINATION_REGISTER_SOURCE_VALUE,
            DESTINATION_REGISTER_SOURCE_ADDRESS,
            DESTINATION_ADDRESS_SOURCE_REGISTER,

            LABEL,
            NoN
        }

        public enum Memory_Type_ENUM
        {
            VALUE,
            ADDRESS,

            LABEL,
            NoN
        }

        // Only add 64 bit registers here and pair them with the dictionary named register_lookup
        public enum Registers_ENUM
        {
            RAX, // 64 BIT
            RBX,
            RCX,
            RDX,
            RSI,
            RDI,
            RSP,
            RBP,
            RIP,
            R8,
            R9,
            R10,
            R11,
            R12,
            R13,
            R14,
            R15,
            EFLAGS,

            NoN, // DO NOT USE THIS
            LAST    // <----- OR THIS
        }

        public enum Bit_Mode_ENUM
        {
            _8_BIT,
            _16_BIT,
            _32_BIT,
            _64_BIT,

            NoN
        }

        public readonly string[] _64_bit_registers =
{
            "RAX", "RBX", "RCX", "RDX",
            "RSI", "RDI", "RSP", "RBP",
            "RIP", "R8", "R9", "R10",
            "R11", "R12", "R13", "R14",
            "R15"
        };

        public readonly string[] _32_bit_registers =
        {
            "EAX", "EBX", "ECX", "EDX",
            "ESI", "EDI", "ESP", "EBP",
            "EIP", "R8D", "R9D", "R10D",
            "R11D", "R12D", "R13D", "R14D",
            "R15D"
        };

        public readonly string[] _16_bit_registers =
        {
            "AX", "BX", "CX", "DX",
            "SI", "DI", "SP", "BP",
            "IP", "R8W", "R9W", "R10W",
            "R11W", "R12W", "R13W", "R14W",
            "R15W"
        };

        public readonly string[] _8_bit_registers =
        {
            "AH", "AL", "BH", "BL",
            "CH", "CL", "DH", "DL",
            "SIL", "DIL", "SPL", "BPL",
            "R8B", "R9B", "R10B", "R11B",
            "R12B", "R13B", "R14B", "R15W"
        };

        // Add all possible register names here and then add the 64 bit register enum so it pairs for example the 16 bit version of a register
        // to its 64 bit variant
        public readonly Dictionary<string[], Registers_ENUM> register_lookup = new Dictionary<string[], Registers_ENUM>
        {
            { new string[] { "RAX", "EAX", "AX", "AH", "AL" }, Registers_ENUM.RAX },
            { new string[] { "RBX", "EBX", "BX", "BH", "BL" }, Registers_ENUM.RBX },
            { new string[] { "RCX", "ECX", "CX", "CH", "CL" }, Registers_ENUM.RCX },
            { new string[] { "RDX", "EDX", "DX", "DH", "DL" }, Registers_ENUM.RDX },
            { new string[] { "RSI", "ESI", "SI", "SIL" }, Registers_ENUM.RSI },
            { new string[] { "RDI", "EDI", "DI", "DIL" }, Registers_ENUM.RDI },
            { new string[] { "RSP", "ESP", "SP", "SPL" }, Registers_ENUM.RSP },
            { new string[] { "RBP", "EBP", "BP", "BPL" }, Registers_ENUM.RBP },
            { new string[] { "RIP", "EIP", "IP" }, Registers_ENUM.RIP },
            { new string[] { "R8", "R8D", "R8W", "R8B" }, Registers_ENUM.R8 },
            { new string[] { "R9", "R9D", "R9W", "R9B" }, Registers_ENUM.R9 },
            { new string[] { "R10", "R10D", "R10W", "R10B" }, Registers_ENUM.R10 },
            { new string[] { "R11", "R11D", "R11W", "R11B" }, Registers_ENUM.R11 },
            { new string[] { "R12", "R12D", "R12W", "R12B" }, Registers_ENUM.R12 },
            { new string[] { "R13", "R13D", "R13W", "R13B" }, Registers_ENUM.R13 },
            { new string[] { "R14", "R14D", "R14W", "R14B" }, Registers_ENUM.R14 },
            { new string[] { "R15", "R15D", "R15W", "R15B" }, Registers_ENUM.R15 },
        };
    }
}
