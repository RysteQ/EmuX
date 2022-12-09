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

            for (int i = 0; i < this.instructions_to_analyze.Length && this.successful; i++)
            {
                string[] tokens = this.instructions_to_analyze[i].Split(' ');

                // check if the token is a label or not
                if (tokens[0].EndsWith(':') && tokens[0].Length > 1 && tokens[0].Contains(' ') == false)
                {
                    instruction_to_add.instruction = Instruction.Instruction_ENUM.LABEL;
                    instruction_to_add.variant = Instruction.Instruction_Variant_ENUM.LABEL;
                    instruction_to_add.destination_memory_type = Instruction.Memory_Type_ENUM.LABEL;
                    instruction_to_add.destination_memory_name = tokens[0].TrimEnd(':');

                    continue;
                }

                instruction_to_add.instruction = GetInstruction(tokens[0]);

                // check if the instruction exists or not
                if (instruction_to_add.instruction == Instruction.Instruction_ENUM.NoN)
                {
                    AnalyzerError(false, i + 1);
                    return;
                }

                // TODO: Improve the GetVariant function
                instruction_to_add.variant = GetVariant(instruction_to_add.instruction, tokens);

                // check if the instruction is of a valid variant or not
                if (instruction_to_add.variant == Instruction.Instruction_Variant_ENUM.NoN)
                {
                    AnalyzerError(false, i + 1);
                    return;
                }

                Instruction.Registers_ENUM destination_register;
                Instruction.Registers_ENUM source_register;
                int value;
                
                switch(instruction_to_add.variant)
                {
                    case Instruction.Instruction_Variant_ENUM.SINGLE:
                        instruction_to_add = AssignRegisterParameters(instruction_to_add, Instruction.Registers_ENUM.NoN, Instruction.Registers_ENUM.NoN);
                        instruction_to_add = AssignMemoryTypeParameters(instruction_to_add, Instruction.Memory_Type_ENUM.NoN, Instruction.Memory_Type_ENUM.NoN);
                        instruction_to_add = AssignMemoryNameParameters(instruction_to_add, null, null);
                        break;

                    case Instruction.Instruction_Variant_ENUM.SINGLE_REGISTER:
                        destination_register = Enum.Parse<Instruction.Registers_ENUM>(tokens[1].ToUpper());

                        instruction_to_add = AssignRegisterParameters(instruction_to_add, destination_register, Instruction.Registers_ENUM.NoN);
                        instruction_to_add = AssignMemoryTypeParameters(instruction_to_add, Instruction.Memory_Type_ENUM.NoN, Instruction.Memory_Type_ENUM.NoN);
                        instruction_to_add = AssignMemoryNameParameters(instruction_to_add, null, null);
                        // instruction_to_add.bit_mode = Instruction.Bit_Mode_ENUM._32_BIT;
                        break;

                    case Instruction.Instruction_Variant_ENUM.SINGLE_VALUE:
                        // convert the number from base 10 / binary / hex to an integer
                        if (int.TryParse(tokens[1], out value) == false)
                        {
                            BaseConverter base_converter = new BaseConverter();

                            if (tokens[1].ToUpper().EndsWith('B'))
                                value = base_converter.ConvertBinaryToInt(tokens[1]);
                            else
                                value = base_converter.ConvertHexToInt(tokens[1]);
                        }
                            
                        instruction_to_add = AssignRegisterParameters(instruction_to_add, Instruction.Registers_ENUM.NoN, Instruction.Registers_ENUM.NoN);
                        instruction_to_add = AssignMemoryTypeParameters(instruction_to_add, Instruction.Memory_Type_ENUM.VALUE, Instruction.Memory_Type_ENUM.NoN);
                        instruction_to_add = AssignMemoryNameParameters(instruction_to_add, value.ToString(), null);
                        break;

                    case Instruction.Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE:
                        instruction_to_add = AssignRegisterParameters(instruction_to_add, Instruction.Registers_ENUM.NoN, Instruction.Registers_ENUM.NoN);
                        instruction_to_add = AssignMemoryTypeParameters(instruction_to_add, Instruction.Memory_Type_ENUM.ADDRESS, Instruction.Memory_Type_ENUM.NoN);
                        instruction_to_add = AssignMemoryNameParameters(instruction_to_add, tokens[1].Trim('[', ']'), null);
                        break;

                    case Instruction.Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER:
                        destination_register = Enum.Parse<Instruction.Registers_ENUM>(tokens[1].ToUpper());
                        source_register = Enum.Parse<Instruction.Registers_ENUM>(tokens[2].ToUpper());

                        instruction_to_add = AssignRegisterParameters(instruction_to_add, destination_register, source_register);
                        instruction_to_add = AssignMemoryTypeParameters(instruction_to_add, Instruction.Memory_Type_ENUM.NoN, Instruction.Memory_Type_ENUM.NoN);
                        instruction_to_add = AssignMemoryNameParameters(instruction_to_add, null, null);
                        break;

                    case Instruction.Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE:
                        destination_register = Enum.Parse<Instruction.Registers_ENUM>(tokens[1].ToUpper());

                        // convert the number from base 10 / binary / hex to an integer
                        if (int.TryParse(tokens[2], out value) == false)
                        {
                            BaseConverter base_converter = new BaseConverter();

                            if (tokens[1].ToUpper().EndsWith('B'))
                                value = base_converter.ConvertBinaryToInt(tokens[1]);
                            else
                                value = base_converter.ConvertHexToInt(tokens[1]);
                        }

                        instruction_to_add = AssignRegisterParameters(instruction_to_add, destination_register, Instruction.Registers_ENUM.NoN);
                        instruction_to_add = AssignMemoryTypeParameters(instruction_to_add, Instruction.Memory_Type_ENUM.VALUE, Instruction.Memory_Type_ENUM.NoN);
                        instruction_to_add = AssignMemoryNameParameters(instruction_to_add, null, value.ToString());
                        break;

                    case Instruction.Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS:
                        destination_register = Enum.Parse<Instruction.Registers_ENUM>(tokens[1].ToUpper());

                        instruction_to_add = AssignRegisterParameters(instruction_to_add, destination_register, Instruction.Registers_ENUM.NoN);
                        instruction_to_add = AssignMemoryTypeParameters(instruction_to_add, Instruction.Memory_Type_ENUM.ADDRESS, Instruction.Memory_Type_ENUM.NoN);
                        instruction_to_add = AssignMemoryNameParameters(instruction_to_add, null, tokens[2]);
                        break;

                    case Instruction.Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER:
                        source_register = Enum.Parse<Instruction.Registers_ENUM>(tokens[2].ToUpper());

                        instruction_to_add = AssignRegisterParameters(instruction_to_add, Instruction.Registers_ENUM.NoN, source_register);
                        instruction_to_add = AssignMemoryTypeParameters(instruction_to_add, Instruction.Memory_Type_ENUM.ADDRESS, Instruction.Memory_Type_ENUM.NoN);
                        instruction_to_add = AssignMemoryNameParameters(instruction_to_add, tokens[1], null);
                        break;
                }
            }
        }

        public bool AnalyzingSuccessful()
        {
            return this.successful;
        }

        public int GetErrorLine()
        {
            return this.error_line;
        }

        public List<Instruction> GetInstructions()
        {
            return this.instructions;
        }

        private Instruction.Instruction_ENUM GetInstruction(string opcode_to_analyze)
        {
            Instruction.Instruction_ENUM toReturn;

            if (Enum.TryParse<Instruction.Instruction_ENUM>(opcode_to_analyze.ToUpper(), out toReturn))
                return toReturn;

            return Instruction.Instruction_ENUM.NoN;
        }

        private Instruction.Instruction_Variant_ENUM GetVariant(Instruction.Instruction_ENUM instruction, string[] tokens)
        {
            Instruction_Groups instruction_groups = new Instruction_Groups();

            // check if the instruction has no operands
            if (instruction_groups.no_operands.Contains(instruction) && tokens.Length == 1)
                return Instruction.Instruction_Variant_ENUM.SINGLE;

            // check if the instruction has one operand
            if (instruction_groups.one_operands.Contains(instruction) && tokens.Length == 2)
            {
                Instruction.Registers_ENUM junk;
                int integer_junk;

                // check if it points to a value in memory
                if (tokens[1].StartsWith('[') && tokens[1].EndsWith(']'))
                    if (GetCharOccurrences(tokens[1], '[') == 1 && GetCharOccurrences(tokens[1], ']') == 1)
                        return Instruction.Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE;

                // check if it refers to a register
                if (Enum.TryParse<Instruction.Registers_ENUM>(tokens[1].ToUpper(), out junk))
                    return Instruction.Instruction_Variant_ENUM.SINGLE_REGISTER;

                // check if it refers to an int
                if (int.TryParse(tokens[1], out integer_junk))
                    return Instruction.Instruction_Variant_ENUM.SINGLE_VALUE;

                // check if it refers to binary
                if (tokens[1].ToUpper().EndsWith('B'))
                {
                    bool acceptable_binary_number = true;

                    for (int i = 0; i < tokens[1].Length && acceptable_binary_number; i++)
                        if (tokens[1][i] != '0' || tokens[1][i] != '1')
                            acceptable_binary_number = false;

                    if (acceptable_binary_number)
                        return Instruction.Instruction_Variant_ENUM.SINGLE_VALUE;
                }

                // check if it refers to hex
                if (tokens[1].ToUpper().EndsWith('H'))
                    if (new HexConverter().IsHex(tokens[1].ToUpper().TrimEnd('H')))
                        return Instruction.Instruction_Variant_ENUM.SINGLE_VALUE;
            }

            // check if the instruction has two operands
            if (instruction_groups.two_operands.Contains(instruction) && tokens.Length == 3)
            {
                Instruction.Instruction_Variant_ENUM junk;
                int integer_junk;

                // check if the destination and source are both registers
                if (Enum.TryParse<Instruction.Instruction_Variant_ENUM>(tokens[1].ToUpper(), out junk))
                    if (Enum.TryParse<Instruction.Instruction_Variant_ENUM>(tokens[2].ToUpper(), out junk))
                        return Instruction.Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER;

                // check if the destination is a register and source a number
                if (Enum.TryParse<Instruction.Instruction_Variant_ENUM>(tokens[1].ToUpper(), out junk))
                {
                    // check if it refers to an int
                    if (int.TryParse(tokens[2], out integer_junk))
                        return Instruction.Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE;

                    // check if it refers to binary
                    if (tokens[2].ToUpper().EndsWith('B'))
                        if (new HexConverter().IsBinary(tokens[2].ToUpper().TrimEnd('B')))
                            return Instruction.Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE;

                    // check if it refers to hex
                    if (tokens[2].ToUpper().EndsWith('H'))
                        if (new HexConverter().IsHex(tokens[2].ToUpper().TrimEnd('H')))
                            return Instruction.Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE;
                    
                    // TODO, add a check for ASCII characters

                    // check if the destination is a register and the source is a value from memory
                    if (Enum.TryParse<Instruction.Instruction_Variant_ENUM>(tokens[1].ToUpper(), out junk))
                        if (tokens[2].StartsWith('[') && tokens[2].EndsWith(']'))
                            if (GetCharOccurrences(tokens[2], '[') == 1 && GetCharOccurrences(tokens[2], ']') == 1)
                                return Instruction.Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS;

                    // check if the destination is a location in memory and the source is a register
                    if (tokens[1].StartsWith('[') && tokens[1].EndsWith(']'))
                        if (GetCharOccurrences(tokens[1], '[') == 1 && GetCharOccurrences(tokens[1], ']') == 1)
                            if (Enum.TryParse<Instruction.Instruction_Variant_ENUM>(tokens[2].ToUpper(), out junk))
                                return Instruction.Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER;
                }
            }

            return Instruction.Instruction_Variant_ENUM.NoN;
        }

        private int GetCharOccurrences(string string_to_check, char char_to_check)
        {
            int toReturn = 0;

            for (int i = 0; i < string_to_check.Length; i++)
                if (string_to_check[i] == char_to_check)
                    toReturn++;

            return toReturn;
        }

        private string[] RemoveComments(string[] to_remove_from)
        {
            List<string> toReturn = new List<string>();

            for (int i = 0; i < to_remove_from.Length; i++)
            {
                if (to_remove_from[i].Contains(';'))
                    toReturn.Add(to_remove_from[i].Split(';')[0]);
                else
                    toReturn.Add(to_remove_from[i]);
            }

            return toReturn.ToArray();
        }

        private string[] RemoveEmptyLines(string[] to_remove_from)
        {
            List<string> toReturn = new List<string>();

            for (int i = 0; i < to_remove_from.Length; i++)
                if (to_remove_from[i].Trim().Length != 0)
                    toReturn.Add(to_remove_from[i]);

            return toReturn.ToArray();
        }

        private Instruction AssignRegisterParameters(Instruction instruction, Instruction.Registers_ENUM destination_register, Instruction.Registers_ENUM source_register)
        {
            instruction.destination_register = destination_register;
            instruction.source_register = source_register;

            return instruction;
        }

        private Instruction AssignMemoryTypeParameters(Instruction instruction, Instruction.Memory_Type_ENUM destination_memory_type, Instruction.Memory_Type_ENUM source_memory_type)
        {
            instruction.source_memory_type = source_memory_type;
            instruction.destination_memory_type = destination_memory_type;

            return instruction;
        }

        private Instruction AssignMemoryNameParameters(Instruction instruction, string destination_memory_name, string source_memory_name)
        {
            instruction.destination_memory_name = destination_memory_name;
            instruction.source_memory_name = destination_memory_name;

            return instruction;
        }

        private Instruction Assign64BitRegister(Instruction instruction)
        {
            // TODO

            return instruction;
        }

        private void AnalyzerError(bool successful, int error_line)
        {
            this.successful = successful;
            this.error_line = error_line;
        }

        private string instructions_data;
        private string[] instructions_to_analyze;
        private List<Instruction> instructions = new List<Instruction>();
        private bool successful = false;
        private int error_line = 0;

        // to continue from JCXZ
        private class Instruction_Groups
        {
            public Instruction.Instruction_ENUM[] no_operands = new Instruction.Instruction_ENUM[]
            {
                // Instruction.Instruction_ENUM.AAA, Look at Instruction.cs for further information
                Instruction.Instruction_ENUM.AAD,
                Instruction.Instruction_ENUM.AAM,
                // Instruction.Instruction_ENUM.AAS,
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
