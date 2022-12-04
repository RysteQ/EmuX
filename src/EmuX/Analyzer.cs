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
            Instruction.Instruction_ENUM toReturn;

            if (Enum.TryParse<Instruction.Instruction_ENUM>(opcode_to_analyze, out toReturn))
                return toReturn;

            return Instruction.Instruction_ENUM.NoN;
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
