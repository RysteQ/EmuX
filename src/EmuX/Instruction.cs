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
        public string label;

        // All the possible instructions (I will slowly build upon this)
        public enum Instruction_ENUM
        {
            AAA,
            AAD,
            AAM,
            AAS,
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
            LOCK,
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
            OUT,
            POP,
            POPF,
            PUSH,
            PUSHF,
            RCL,
            RCR,
            RET,
            RETN,
            RETF,
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
            WAIT,
            XCHG,
            XLAT,
            XOR,

            LABEL,
            NoN
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
            ADDRESS_SOURCE_REGISTER,

            LABEL
        }

        public enum Memory_Type_ENUM
        {
            VALUE,
            ADDRESS,

            LABEL
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
            RIP,
            R8,
            R9,
            R10,
            R11,
            R12,
            R13,
            R14,
            R15,
            CR0,
            CR1,
            CR2,
            CR3,
            CR4,
            CR5,
            CR6,
            CR7,
            CR8,
            CR9,
            CR10,
            CR11,
            CR12,
            CR13,
            CR14,
            CR15,
            DR0,
            DR1,
            DR2,
            DR3,
            DR4,
            DR5,
            DR6,
            DR7,
            DR8,
            DR9,
            DR10,
            DR11,
            DR12,
            DR13,
            DR14,
            DR15,
            EFLAGS,

            LAST    // <----- DO NOT USE THIS
        }
    }
}
