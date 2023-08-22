using EmuX.src.Enums.Instruction_Data;

namespace EmuX.src.Enums;

public class Instruction_Groups
{
    public readonly static Opcodes[] no_operands =
    {
        Opcodes.AAA,
        Opcodes.AAD,
        Opcodes.AAM,
        Opcodes.AAS,
        Opcodes.CLC,
        Opcodes.CLD,
        Opcodes.CLI,
        Opcodes.CBW,
        Opcodes.CWD,
        Opcodes.DAA,
        Opcodes.DAS,
        Opcodes.HLT,
        Opcodes.LAHF,
        Opcodes.NOP,
        Opcodes.POPF,
        Opcodes.PUSHF,
        Opcodes.RCR,
        Opcodes.RET,
        Opcodes.SAHF,
        Opcodes.STC,
        Opcodes.STD,
        Opcodes.STI,
    };

    public readonly static Opcodes[] one_label = new Opcodes[]
    {
        Opcodes.CALL,
        Opcodes.JA,
        Opcodes.JAE,
        Opcodes.JB,
        Opcodes.JBE,
        Opcodes.JC,
        Opcodes.JE,
        Opcodes.JG,
        Opcodes.JGE,
        Opcodes.JL,
        Opcodes.JLE,
        Opcodes.JNA,
        Opcodes.JNE,
        Opcodes.JNB,
        Opcodes.JNBE,
        Opcodes.JNC,
        Opcodes.JNE,
        Opcodes.JNG,
        Opcodes.JNGE,
        Opcodes.JNL,
        Opcodes.JNLE,
        Opcodes.JNO,
        Opcodes.JNP,
        Opcodes.JNS,
        Opcodes.JNZ,
        Opcodes.JO,
        Opcodes.JP,
        Opcodes.JPE,
        Opcodes.JPO,
        Opcodes.JS,
        Opcodes.JZ,
        Opcodes.JMP,
    };

    public readonly static Opcodes[] one_operands = new Opcodes[]
    {
        Opcodes.DEC,
        Opcodes.INC,
        Opcodes.INT,
        Opcodes.MUL,
        Opcodes.NEG,
        Opcodes.NOT,
        Opcodes.POP,
        Opcodes.PUSH,
    };

    public readonly static Opcodes[] two_operands = new Opcodes[]
    {
        Opcodes.ADC,
        Opcodes.ADD,
        Opcodes.AND,
        Opcodes.CMP,
        Opcodes.DIV,
        Opcodes.LEA,
        Opcodes.MOV,
        Opcodes.MUL,
        Opcodes.OR,
        Opcodes.RCL,
        Opcodes.RCR,
        Opcodes.ROL,
        Opcodes.ROR,
        Opcodes.SAL,
        Opcodes.SAR,
        Opcodes.SBB,
        Opcodes.SHL,
        Opcodes.SHR,
        Opcodes.SUB,
        Opcodes.XOR
    };

    public enum InstructionGroupEnum
    {
        NO_OPERANDS,
        ONE_OPERAND,
        TWO_OPERANDS
    };
}