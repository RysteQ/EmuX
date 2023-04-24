using static Instruction_Data;

namespace EmuX.src.Enums
{
    public class Instruction_Groups
    {
        public readonly static Instruction_ENUM[] no_operands =
        {
            Instruction_ENUM.AAA,
            Instruction_ENUM.AAD,
            Instruction_ENUM.AAM,
            Instruction_ENUM.AAS,
            Instruction_ENUM.CLC,
            Instruction_ENUM.CLD,
            Instruction_ENUM.CLI,
            Instruction_ENUM.CBW,
            Instruction_ENUM.CWD,
            Instruction_ENUM.DAA,
            Instruction_ENUM.DAS,
            Instruction_ENUM.HLT,
            Instruction_ENUM.LAHF,
            Instruction_ENUM.NOP,
            Instruction_ENUM.POPF,
            Instruction_ENUM.PUSHF,
            Instruction_ENUM.RCR,
            Instruction_ENUM.RET,
            Instruction_ENUM.SAHF,
            Instruction_ENUM.STC,
            Instruction_ENUM.STD,
            Instruction_ENUM.STI,
        };

        public readonly static Instruction_ENUM[] one_label = new Instruction_ENUM[]
        {
            Instruction_ENUM.CALL,
            Instruction_ENUM.JA,
            Instruction_ENUM.JAE,
            Instruction_ENUM.JB,
            Instruction_ENUM.JBE,
            Instruction_ENUM.JC,
            Instruction_ENUM.JE,
            Instruction_ENUM.JG,
            Instruction_ENUM.JGE,
            Instruction_ENUM.JL,
            Instruction_ENUM.JLE,
            Instruction_ENUM.JNA,
            Instruction_ENUM.JNE,
            Instruction_ENUM.JNB,
            Instruction_ENUM.JNBE,
            Instruction_ENUM.JNC,
            Instruction_ENUM.JNE,
            Instruction_ENUM.JNG,
            Instruction_ENUM.JNGE,
            Instruction_ENUM.JNL,
            Instruction_ENUM.JNLE,
            Instruction_ENUM.JNO,
            Instruction_ENUM.JNP,
            Instruction_ENUM.JNS,
            Instruction_ENUM.JNZ,
            Instruction_ENUM.JO,
            Instruction_ENUM.JP,
            Instruction_ENUM.JPE,
            Instruction_ENUM.JPO,
            Instruction_ENUM.JS,
            Instruction_ENUM.JZ,
            Instruction_ENUM.JMP,
        };

        public readonly static Instruction_ENUM[] one_operands = new Instruction_ENUM[]
        {
            Instruction_ENUM.DEC,
            Instruction_ENUM.INC,
            Instruction_ENUM.INT,
            Instruction_ENUM.MUL,
            Instruction_ENUM.NEG,
            Instruction_ENUM.NOT,
            Instruction_ENUM.POP,
            Instruction_ENUM.PUSH,
        };

        public readonly static Instruction_ENUM[] two_operands = new Instruction_ENUM[]
        {
            Instruction_ENUM.ADC,
            Instruction_ENUM.ADD,
            Instruction_ENUM.AND,
            Instruction_ENUM.CMP,
            Instruction_ENUM.DIV,
            Instruction_ENUM.LEA,
            Instruction_ENUM.MOV,
            Instruction_ENUM.MUL,
            Instruction_ENUM.OR,
            Instruction_ENUM.RCL,
            Instruction_ENUM.RCR,
            Instruction_ENUM.ROL,
            Instruction_ENUM.ROR,
            Instruction_ENUM.SAL,
            Instruction_ENUM.SAR,
            Instruction_ENUM.SBB,
            Instruction_ENUM.SHL,
            Instruction_ENUM.SHR,
            Instruction_ENUM.SUB,
            Instruction_ENUM.XOR
        };

        public enum Instruction_Group_Enum
        {
            NO_OPERANDS,
            ONE_OPERAND,
            TWO_OPERANDS
        };
    }
}