namespace EmuX.src.Enums
{
    public class Instruction_Groups : Opcodes
    {
        public readonly static Opcodes_ENUM[] no_operands =
        {
            Opcodes_ENUM.AAA,
            Opcodes_ENUM.AAD,
            Opcodes_ENUM.AAM,
            Opcodes_ENUM.AAS,
            Opcodes_ENUM.CLC,
            Opcodes_ENUM.CLD,
            Opcodes_ENUM.CLI,
            Opcodes_ENUM.CBW,
            Opcodes_ENUM.CWD,
            Opcodes_ENUM.DAA,
            Opcodes_ENUM.DAS,
            Opcodes_ENUM.HLT,
            Opcodes_ENUM.LAHF,
            Opcodes_ENUM.NOP,
            Opcodes_ENUM.POPF,
            Opcodes_ENUM.PUSHF,
            Opcodes_ENUM.RCR,
            Opcodes_ENUM.RET,
            Opcodes_ENUM.SAHF,
            Opcodes_ENUM.STC,
            Opcodes_ENUM.STD,
            Opcodes_ENUM.STI,
        };

        public readonly static Opcodes_ENUM[] one_label = new Opcodes_ENUM[]
        {
            Opcodes_ENUM.CALL,
            Opcodes_ENUM.JA,
            Opcodes_ENUM.JAE,
            Opcodes_ENUM.JB,
            Opcodes_ENUM.JBE,
            Opcodes_ENUM.JC,
            Opcodes_ENUM.JE,
            Opcodes_ENUM.JG,
            Opcodes_ENUM.JGE,
            Opcodes_ENUM.JL,
            Opcodes_ENUM.JLE,
            Opcodes_ENUM.JNA,
            Opcodes_ENUM.JNE,
            Opcodes_ENUM.JNB,
            Opcodes_ENUM.JNBE,
            Opcodes_ENUM.JNC,
            Opcodes_ENUM.JNE,
            Opcodes_ENUM.JNG,
            Opcodes_ENUM.JNGE,
            Opcodes_ENUM.JNL,
            Opcodes_ENUM.JNLE,
            Opcodes_ENUM.JNO,
            Opcodes_ENUM.JNP,
            Opcodes_ENUM.JNS,
            Opcodes_ENUM.JNZ,
            Opcodes_ENUM.JO,
            Opcodes_ENUM.JP,
            Opcodes_ENUM.JPE,
            Opcodes_ENUM.JPO,
            Opcodes_ENUM.JS,
            Opcodes_ENUM.JZ,
            Opcodes_ENUM.JMP,
        };

        public readonly static Opcodes_ENUM[] one_operands = new Opcodes_ENUM[]
        {
            Opcodes_ENUM.DEC,
            Opcodes_ENUM.INC,
            Opcodes_ENUM.INT,
            Opcodes_ENUM.MUL,
            Opcodes_ENUM.NEG,
            Opcodes_ENUM.NOT,
            Opcodes_ENUM.POP,
            Opcodes_ENUM.PUSH,
        };

        public readonly static Opcodes_ENUM[] two_operands = new Opcodes_ENUM[]
        {
            Opcodes_ENUM.ADC,
            Opcodes_ENUM.ADD,
            Opcodes_ENUM.AND,
            Opcodes_ENUM.CMP,
            Opcodes_ENUM.DIV,
            Opcodes_ENUM.LEA,
            Opcodes_ENUM.MOV,
            Opcodes_ENUM.MUL,
            Opcodes_ENUM.OR,
            Opcodes_ENUM.RCL,
            Opcodes_ENUM.RCR,
            Opcodes_ENUM.ROL,
            Opcodes_ENUM.ROR,
            Opcodes_ENUM.SAL,
            Opcodes_ENUM.SAR,
            Opcodes_ENUM.SBB,
            Opcodes_ENUM.SHL,
            Opcodes_ENUM.SHR,
            Opcodes_ENUM.SUB,
            Opcodes_ENUM.XOR
        };

        public enum Instruction_Group_Enum
        {
            NO_OPERANDS,
            ONE_OPERAND,
            TWO_OPERANDS
        };
    }
}