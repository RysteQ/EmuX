public class Instruction_Data
{
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
        CWD,
        DAA,
        DAS,
        DEC,
        DIV,
        HLT,
        INC,
        INT,
        JA,
        JAE,
        JB,
        JBE,
        JC,
        JE,
        JG,
        JGE,
        JL,
        JLE,
        JNA,
        JNAE,
        JNB,
        JNBE,
        JNC,
        JNE,
        JNG,
        JNGE,
        JNL,
        JNLE,
        JNO,
        JNP,
        JNS,
        JNZ,
        JO,
        JP,
        JPE,
        JPO,
        JS,
        JZ,
        JMP,
        LAHF,
        LEA,
        MOV,
        MUL,
        NEG,
        NOP,
        NOT,
        OR,
        POP,
        POPF,
        PUSH,
        PUSHF,
        RCL,
        RCR,
        RET,
        ROL,
        ROR,
        SAHF,
        SAL,
        SAR,
        SBB,
        SHL,
        SHR,
        STC,
        STD,
        STI,
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
        DESTINATION_ADDRESS_SOURCE_VALUE,

        LABEL,
        NoN
    }

    public enum Memory_Type_ENUM
    {
        VALUE,
        ADDRESS,

        LABEL,
        Mem,
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

    public bool HIGH { get; } = true;
    public bool LOW { get; } = false;
}