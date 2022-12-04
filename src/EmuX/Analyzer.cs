using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmuX
{
    internal class Analyzer
    {
        public void SetInstructions(string instructions_to_analyze)
        {
            this.instructions_data = instructions_to_analyze;
        }

        public void AnalyzeInstructions()
        {
            this.instructions_to_analyze = this.instructions_data.Split('\n');
            this.instructions_to_analyze = RemoveComments(this.instructions_to_analyze);
            this.instructions_to_analyze = RemoveEmptyLines(this.instructions_to_analyze);

            Instruction instruction_to_add = new Instruction();

            for (int i = 0; i < this.instructions_to_analyze.Length; i++)
            {
                string[] tokens = this.instructions_to_analyze[i].Split(' ');

                // check if the token is a label or not
                if (tokens[0].EndsWith(':') && tokens[0].Length > 1)
                {
                    instruction_to_add.instruction = Instruction.Instruction_ENUM.LABEL;
                    instruction_to_add.variant = Instruction.Instruction_Variant_ENUM.LABEL;
                    instruction_to_add.destination_memory_type = Instruction.Memory_Type_ENUM.LABEL;
                    instruction_to_add.destination_memory_name = tokens[0].Trim(':');

                    continue;
                }

                instruction_to_add.instruction = GetInstruction(tokens[0]);
            }
        }

        public bool AnalyzingSuccessful()
        {
            return this.successful;
        }

        public List<Instruction> GetInstructions()
        {
            return this.instructions;
        }

        private Instruction.Instruction_ENUM GetInstruction(string opcode_to_analyze)
        {
            switch (opcode_to_analyze)
            {
                case "AAA": return Instruction.Instruction_ENUM.AAA;
                case "AAD": return Instruction.Instruction_ENUM.AAD;
                case "AAM": return Instruction.Instruction_ENUM.AAM;
                case "AAS": return Instruction.Instruction_ENUM.AAS;
                case "ADC": return Instruction.Instruction_ENUM.ADC;
                case "ADD": return Instruction.Instruction_ENUM.ADD;
                case "AND": return Instruction.Instruction_ENUM.AND;
                case "CALL": return Instruction.Instruction_ENUM.CALL;
                case "CBW": return Instruction.Instruction_ENUM.CBW;
                case "CLC": return Instruction.Instruction_ENUM.CLC;
                case "CLD": return Instruction.Instruction_ENUM.CLD;
                case "CLI": return Instruction.Instruction_ENUM.CLI;
                case "CMC": return Instruction.Instruction_ENUM.CMC;
                case "CMP": return Instruction.Instruction_ENUM.CMP;
                case "CMPSB": return Instruction.Instruction_ENUM.CMPSB;
                case "CMPSW": return Instruction.Instruction_ENUM.CMPSW;
                case "CWD": return Instruction.Instruction_ENUM.CWD;
                case "DAA": return Instruction.Instruction_ENUM.DAA;
                case "DAS": return Instruction.Instruction_ENUM.DAS;
                case "DEC": return Instruction.Instruction_ENUM.DEC;
                case "DIV": return Instruction.Instruction_ENUM.DIV;
                case "ESC": return Instruction.Instruction_ENUM.ESC;
                case "HLT": return Instruction.Instruction_ENUM.HTL;
                case "IDIV": return Instruction.Instruction_ENUM.IDIV;
                case "IMUL": return Instruction.Instruction_ENUM.IMUL;
                case "INC": return Instruction.Instruction_ENUM.INC;
                case "INT": return Instruction.Instruction_ENUM.INT;
                case "INTO": return Instruction.Instruction_ENUM.INTO;
                case "IRET": return Instruction.Instruction_ENUM.IRET;
                case "JA": return Instruction.Instruction_ENUM.JA;
                case "JAE": return Instruction.Instruction_ENUM.JAE;
                case "JB": return Instruction.Instruction_ENUM.JB;
                case "JBE": return Instruction.Instruction_ENUM.JBE;
                case "JC": return Instruction.Instruction_ENUM.JC;
                case "JE": return Instruction.Instruction_ENUM.JE;
                case "JG": return Instruction.Instruction_ENUM.JG;
                case "JGE": return Instruction.Instruction_ENUM.JGE;
                case "JL": return Instruction.Instruction_ENUM.JL;
                case "JLE": return Instruction.Instruction_ENUM.JLE;
                case "JNA": return Instruction.Instruction_ENUM.JNA;
                case "JNAE": return Instruction.Instruction_ENUM.JNAE;
                case "JNB": return Instruction.Instruction_ENUM.JNB;
                case "JNBE": return Instruction.Instruction_ENUM.JNBE;
                case "JNC": return Instruction.Instruction_ENUM.JNC;
                case "JNE": return Instruction.Instruction_ENUM.JNE;
                case "JNG": return Instruction.Instruction_ENUM.JNG;
                case "JNGE": return Instruction.Instruction_ENUM.JNGE;
                case "JNL": return Instruction.Instruction_ENUM.JNL;
                case "JNLE": return Instruction.Instruction_ENUM.JNLE;
                case "JNO": return Instruction.Instruction_ENUM.JNO;
                case "JNP": return Instruction.Instruction_ENUM.JNP;
                case "JNS": return Instruction.Instruction_ENUM.JNS;
                case "JNZ": return Instruction.Instruction_ENUM.JNZ;
                case "JO": return Instruction.Instruction_ENUM.JO;
                case "JP": return Instruction.Instruction_ENUM.JP;
                case "JPE": return Instruction.Instruction_ENUM.JPE;
                case "JPO": return Instruction.Instruction_ENUM.JPO;
                case "JS": return Instruction.Instruction_ENUM.JS;
                case "JZ": return Instruction.Instruction_ENUM.JZ;
                case "JCXZ": return Instruction.Instruction_ENUM.JCXZ;
                case "JMP": return Instruction.Instruction_ENUM.JMP;
                case "LAHF": return Instruction.Instruction_ENUM.LAHF;
                case "LDS": return Instruction.Instruction_ENUM.LDS;
                case "LEA": return Instruction.Instruction_ENUM.LEA;
                case "LES": return Instruction.Instruction_ENUM.LES;
                case "LOCK": return Instruction.Instruction_ENUM.LOCK;
                case "LODSB": return Instruction.Instruction_ENUM.LODSB;
                case "LODSW": return Instruction.Instruction_ENUM.LODSW;
                case "MOV": return Instruction.Instruction_ENUM.MOV;
                case "MOVSB": return Instruction.Instruction_ENUM.MOVSB;
                case "MOVSW": return Instruction.Instruction_ENUM.MOVSW;
                case "MUL": return Instruction.Instruction_ENUM.MUL;
                case "NEG": return Instruction.Instruction_ENUM.NEG;
                case "NOP": return Instruction.Instruction_ENUM.NOP;
                case "NOT": return Instruction.Instruction_ENUM.NOT;
                case "OR": return Instruction.Instruction_ENUM.OR;
                case "OUT": return Instruction.Instruction_ENUM.OUT;
                case "POP": return Instruction.Instruction_ENUM.POP;
                case "POPF": return Instruction.Instruction_ENUM.POPF;
                case "PUSH": return Instruction.Instruction_ENUM.PUSH;
                case "PUSHF": return Instruction.Instruction_ENUM.PUSHF;
                case "RCL": return Instruction.Instruction_ENUM.RCL;
                case "RCR": return Instruction.Instruction_ENUM.RCR;
                case "RET": return Instruction.Instruction_ENUM.RET;
                case "RETN": return Instruction.Instruction_ENUM.RETN;
                case "RETF": return Instruction.Instruction_ENUM.RETF;
                case "ROL": return Instruction.Instruction_ENUM.ROL;
                case "ROR": return Instruction.Instruction_ENUM.ROR;
                case "SAHF": return Instruction.Instruction_ENUM.SAHF;
                case "SAL": return Instruction.Instruction_ENUM.SAL;
                case "SAR": return Instruction.Instruction_ENUM.SAR;
                case "SBB": return Instruction.Instruction_ENUM.SBB;
                case "SCASB": return Instruction.Instruction_ENUM.SCASB;
                case "SCASW": return Instruction.Instruction_ENUM.SCASW;
                case "SHL": return Instruction.Instruction_ENUM.SHL;
                case "SHR": return Instruction.Instruction_ENUM.SHR;
                case "STC": return Instruction.Instruction_ENUM.STC;
                case "STD": return Instruction.Instruction_ENUM.STD;
                case "STI": return Instruction.Instruction_ENUM.STI;
                case "STOSB": return Instruction.Instruction_ENUM.STOSB;
                case "STOSW": return Instruction.Instruction_ENUM.STOSW;
                case "SUB": return Instruction.Instruction_ENUM.SUB;
                case "WAIT": return Instruction.Instruction_ENUM.WAIT;
                case "XCHG": return Instruction.Instruction_ENUM.XCHG;
                case "XLAT": return Instruction.Instruction_ENUM.XLAT;
                case "XOR": return Instruction.Instruction_ENUM.XOR;

                default:
                    return Instruction.Instruction_ENUM.NoN;
            }
        }

        private string[] RemoveComments(string[] to_remove_from)
        {
            List<string> toReturn = new List<string>();

            for (int i = 0; i < to_remove_from.Length; i++)
                toReturn.Add(to_remove_from[i].Split(';')[0]);

            return toReturn.ToArray();
        }

        private string[] RemoveEmptyLines(string[] to_remove_from)
        {
            List<string> toReturn = new List<string>();

            for (int i = 0; i < to_remove_from.Length; i++)
                if (to_remove_from[i].Length == 0)
                    toReturn.Add(to_remove_from[i].Trim());

            return toReturn.ToArray();
        }

        private string instructions_data;
        private string[] instructions_to_analyze;
        private List<Instruction> instructions = new List<Instruction>();
        private bool successful = false;

        // to continue from JCXZ
        private class Instruction_Groups
        {
            public Instruction.Instruction_ENUM[] no_operands = new Instruction.Instruction_ENUM[]
            {
                Instruction.Instruction_ENUM.AAA,
                Instruction.Instruction_ENUM.AAD,
                Instruction.Instruction_ENUM.AAM,
                Instruction.Instruction_ENUM.AAS,
                Instruction.Instruction_ENUM.CLC,
                Instruction.Instruction_ENUM.CLD,
                Instruction.Instruction_ENUM.CLI,
                Instruction.Instruction_ENUM.CBW,
                Instruction.Instruction_ENUM.CWD,
                Instruction.Instruction_ENUM.DAA,
                Instruction.Instruction_ENUM.DAS,
                Instruction.Instruction_ENUM.HTL,
                Instruction.Instruction_ENUM.INTO,
                Instruction.Instruction_ENUM.IRET,
                Instruction.Instruction_ENUM.NOP,
            };

            public Instruction.Instruction_ENUM[] one_operands = new Instruction.Instruction_ENUM[]
            {
                Instruction.Instruction_ENUM.CALL,
                Instruction.Instruction_ENUM.DEC,
                Instruction.Instruction_ENUM.INC,
                Instruction.Instruction_ENUM.INT,
            };

            public Instruction.Instruction_ENUM[] two_operands = new Instruction.Instruction_ENUM[]
            {
                Instruction.Instruction_ENUM.ADC,
                Instruction.Instruction_ENUM.ADD,
                Instruction.Instruction_ENUM.AND,
                Instruction.Instruction_ENUM.CMP,
                Instruction.Instruction_ENUM.CMPSB,
                Instruction.Instruction_ENUM.CMPSW,
                Instruction.Instruction_ENUM.DIV,
                Instruction.Instruction_ENUM.ESC,
                Instruction.Instruction_ENUM.IDIV,
                Instruction.Instruction_ENUM.IMUL,
                Instruction.Instruction_ENUM.JA,
                Instruction.Instruction_ENUM.JAE,
                Instruction.Instruction_ENUM.JB,
                Instruction.Instruction_ENUM.JBE,
                Instruction.Instruction_ENUM.JC,
                Instruction.Instruction_ENUM.JE,
                Instruction.Instruction_ENUM.JG,
                Instruction.Instruction_ENUM.JGE,
                Instruction.Instruction_ENUM.JL,
                Instruction.Instruction_ENUM.JLE,
                Instruction.Instruction_ENUM.JNA,
                Instruction.Instruction_ENUM.JNE,
                Instruction.Instruction_ENUM.JNB,
                Instruction.Instruction_ENUM.JNBE,
                Instruction.Instruction_ENUM.JNC,
                Instruction.Instruction_ENUM.JNE,
                Instruction.Instruction_ENUM.JNG,
                Instruction.Instruction_ENUM.JNGE,
                Instruction.Instruction_ENUM.JNL,
                Instruction.Instruction_ENUM.JNLE,
                Instruction.Instruction_ENUM.JNO,
                Instruction.Instruction_ENUM.JNP,
                Instruction.Instruction_ENUM.JNS,
                Instruction.Instruction_ENUM.JNZ,
                Instruction.Instruction_ENUM.JO,
                Instruction.Instruction_ENUM.JP,
                Instruction.Instruction_ENUM.JPE,
                Instruction.Instruction_ENUM.JPO,
                Instruction.Instruction_ENUM.JS,
                Instruction.Instruction_ENUM.JZ
            };
        }
    }
}
