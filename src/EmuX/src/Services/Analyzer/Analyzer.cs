using EmuX.Services;
using EmuX.src.Enums;
using EmuX.src.Models;
using EmuX.src.Services.Base_Converter;

namespace EmuX.src.Services.Analyzer
{
    internal class Analyzer
    {
        private void Flush()
        {
            static_data = new List<StaticData>();
            instructions = new List<Instruction>();
            labels = new List<Models.Label>();
            successful = true;
        }

        public void AnalyzeInstructions(string to_analyze)
        {
            string[] label_names = ScanForLabels(to_analyze);
            int offset = 1024;

            Flush();

            if (to_analyze.Contains("section.text") == false)
            {
                AnalyzerError(-1);
                return;
            }

            string[] labels_to_analyze = to_analyze.Split("section.text")[0].Split('\n');
            labels_to_analyze = RemoveComments(static_data_to_analyze);
            labels_to_analyze = StringHandler.RemoveEmptyLines(static_data_to_analyze);

            string[] instructions_to_analyze = to_analyze.Split("section.text")[1].Split('\n');
            instructions_to_analyze = RemoveComments(instructions_to_analyze);
            instructions_to_analyze = StringHandler.RemoveEmptyLines(instructions_to_analyze);

            for (int line = 0; line < static_data_to_analyze.Length; line++)
                offset = AnalyzeStaticData(static_data_to_analyze[line], offset, line);

            for (int line = 0; line < instructions_to_analyze.Length && successful; line++)
            {
                Instruction instruction_to_add = new();
                string instruction_to_analyze = instructions_to_analyze[line];
                string[] tokens = instruction_to_analyze.Split(' ');

                // check if the line refers to a label
                if (tokens[0].EndsWith(':'))
                {
                    if (tokens.Length == 1)
                    {
                        // make a dummy instruction so the labels can work properly
                        instruction_to_add.opcode = Opcodes.Opcodes_ENUM.LABEL;
                        instruction_to_add.bit_mode = SIZE.Size_ENUM.NoN;

                        labels.Add(new(tokens[0].TrimEnd(':'), line));
                        instructions.Add(instruction_to_add);

                        continue;
                    }
                    else
                    {
                        AnalyzerError(line);
                        break;
                    }
                }

                // get the instruction
                instruction_to_add.opcode = GetInstruction(tokens[0]);

                // check to which parameter group the instruction belongs to
                if (Instruction_Groups.one_label.Contains(instruction_to_add.opcode))
                {
                    if (tokens.Length != 2)
                    {
                        AnalyzerError(line);
                        return;
                    }

                    instruction_to_add = AssignLabelInstruction(instruction_to_add, label_names, tokens[1], line);
                }
                else if (Instruction_Groups.no_operands.Contains(instruction_to_add.opcode))
                {
                    // check if the instruction is valid
                    if (tokens.Length != 1)
                    {
                        AnalyzerError(line);
                        return;
                    }

                    instruction_to_add = AssignNoOperandInstruction(instruction_to_add, tokens, line);
                }
                else if (Instruction_Groups.one_operands.Contains(instruction_to_add.opcode))
                {
                    if (tokens.Length != 2 && tokens.Length != 3)
                    {
                        AnalyzerError(line);
                        return;
                    }

                    instruction_to_add = AssignOneOperandInstruction(instruction_to_add, tokens, offset, line);
                }
                else if (Instruction_Groups.two_operands.Contains(instruction_to_add.opcode))
                {
                    if (tokens.Length != 3 && tokens.Length != 4)
                    {
                        AnalyzerError(line);
                        return;
                    }

                    instruction_to_add = AssignTwoOperandInstruction(instruction_to_add, tokens, offset, line);
                }
                else
                {
                    AnalyzerError(line);
                    return;
                }

                instructions.Add(instruction_to_add);
            }
        }
        
        private int AnalyzeStaticData(string static_data_to_analyze, int offset, int line)
        {
            StaticData static_data_to_add = new();

            string static_data_name = static_data_to_analyze.Split(':')[0].Trim();
            string[] static_data_tokens = static_data_to_analyze.Split(static_data_name + ":");
            ulong static_data_value = 0;

            for (int i = 0; i < static_data_tokens.Length; i++)
                static_data_tokens[i] = static_data_tokens[i].Trim();

            // the name of the static data
            static_data_tokens[0] = static_data_name.TrimEnd(':');

            // check if the static data name is valid or not aka if the static data name is a register name or not
            if (Register_Verifier.IsRegister(static_data_tokens[0]))
            {
                AnalyzerError(line);
                return offset;
            }

            // check if the static data is a number or a character / list of characters (aka string)
            if (static_data_tokens[1].Split(' ').Length == 2 && ulong.TryParse(static_data_tokens[1].Split(' ')[1], out static_data_value))
            {
                static_data_to_add.name = static_data_name;
                static_data_to_add.value = static_data_value;
                static_data_to_add.memory_location = offset;

                switch (static_data_tokens[1].Split(' ')[0].ToUpper())
                {
                    case "DB":
                        static_data_to_add.size_in_bits = SIZE.Size_ENUM._8_BIT;
                        offset++;
                        break;

                    case "DW":
                        static_data_to_add.size_in_bits = SIZE.Size_ENUM._16_BIT;
                        offset += 2;
                        break;

                    case "DD":
                        static_data_to_add.size_in_bits = SIZE.Size_ENUM._32_BIT;
                        offset += 4;
                        break;

                    case "DQ":
                        static_data_to_add.size_in_bits = SIZE.Size_ENUM._64_BIT;
                        offset += 8;
                        break;

                    default:
                        AnalyzerError(line);
                        break;
                }
            }
            else
            {
                // check if the user made a mistake
                if (static_data_tokens[1].Split(' ')[0].Trim().ToUpper() != "DB")
                {
                    AnalyzerError(line);
                    return offset;
                }

                static_data_to_add.name = static_data_name;
                static_data_to_add.memory_location = offset;
                static_data_to_add.is_string_array = true;

                static_data_tokens = static_data_tokens[1].Split(',');
                static_data_tokens[0] = static_data_tokens[0].Split(' ')[1];

                for (int i = 0; i < static_data_tokens.Length; i++)
                {
                    static_data_tokens[i] = static_data_tokens[i].Trim();

                    if ((static_data_tokens[i].StartsWith('\'') == false || static_data_tokens[i].EndsWith('\'') == false || static_data_tokens[i].Length != 3)
                    && ulong.TryParse(static_data_tokens[i], out static_data_value) == false)
                    {
                        AnalyzerError(line);
                        return offset;
                    }

                    if (static_data_tokens[i].StartsWith('\''))
                        static_data_to_add.characters.Add(static_data_tokens[i].Trim('\'').ToCharArray()[0]);
                    else
                        static_data_to_add.characters.Add((char)static_data_value);

                    offset++;
                }
            }

            // add the new static data to the list
            static_data.Add(static_data_to_add);

            return offset;
        }

        private Instruction AssignLabelInstruction(Instruction instruction, string[] label_names, string label, int line)
        {
            bool label_found = false;

            for (int i = 0; i < label_names.Length; i++)
                if (label_names[i] == label)
                    label_found = true;

            if (label_found == false)
            {
                AnalyzerError(line);
                return instruction;
            }

            instruction.variant = GetVariant(instruction.opcode, new string[] { "lol", label });
            instruction.bit_mode = SIZE.Size_ENUM.NoN;
            instruction.destination_memory_type = Memory_Type.Memory_Type_ENUM.ADDRESS;
            instruction.destination_memory_name = label;

            return instruction;
        }

        private Instruction AssignNoOperandInstruction(Instruction instruction, string[] tokens, int line)
        {
            instruction.variant = GetVariant(instruction.opcode, tokens);

            // check if the variant is a label or a single variant instruction
            if (instruction.variant == Variants.Variants_ENUM.LABEL)
            {
                instruction.destination_memory_type = Memory_Type.Memory_Type_ENUM.LABEL;
                instruction.destination_memory_name = tokens[1];
                instruction.bit_mode = AssignBitMode(instruction, "");
            }
            else
            {
                instruction.bit_mode = SIZE.Size_ENUM.NoN;
            }

            return instruction;
        }

        private Instruction AssignOneOperandInstruction(Instruction instruction, string[] tokens, int memory_offset, int line)
        {
            instruction.variant = GetVariant(instruction.opcode, tokens);
            (ulong, bool) value;

            switch (instruction.variant)
            {
                case Variants.Variants_ENUM.SINGLE_REGISTER:
                    instruction.destination_register = GetRegister(tokens[tokens.Length - 1]);

                    // check if the instruction has a keyword like byte (example: inc byte al) or not
                    if (tokens.Length == 2)
                    {
                        instruction.bit_mode = AssignBitMode(instruction, tokens[tokens.Length - 1]);
                    }
                    else
                    {
                        instruction.bit_mode = GetUserAssignedBitmode(tokens[1]);

                        if (instruction.bit_mode == SIZE.Size_ENUM.NoN)
                            AnalyzerError(line);
                    }

                    break;

                case Variants.Variants_ENUM.SINGLE_VALUE:
                    value = GetValue(tokens[tokens.Length - 1]);

                    if (value.Item2 == false)
                        AnalyzerError(line);

                    instruction.destination_memory_name = value.Item1.ToString();
                    instruction.destination_memory_type = Memory_Type.Memory_Type_ENUM.VALUE;

                    break;

                case Variants.Variants_ENUM.SINGLE_ADDRESS_VALUE:
                    // check for keywords like byte word etc
                    if (tokens.Length == 3)
                        instruction.bit_mode = GetUserAssignedBitmode(tokens[1]);

                    instruction.destination_memory_type = Memory_Type.Memory_Type_ENUM.ADDRESS;

                    break;
            }

            return instruction;
        }

        private Instruction AssignTwoOperandInstruction(Instruction instruction, string[] tokens, int memory_offset, int line)
        {
            instruction.variant = GetVariant(instruction.opcode, tokens);
            (ulong, bool) value;

            switch (instruction.variant)
            {
                case Variants.Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER:
                    instruction.destination_register = GetRegister(tokens[1]);
                    instruction.source_register = GetRegister(tokens[tokens.Length - 1]);

                    // assign automatically the bitmode with the source register or allow the user to modify the bitmode
                    if (tokens.Length == 3)
                        instruction.bit_mode = AssignBitMode(instruction, tokens[tokens.Length - 1]);
                    else
                        instruction.bit_mode = GetUserAssignedBitmode(tokens[tokens.Length - 2]);

                    break;

                case Variants.Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE:
                    value = GetValue(tokens[tokens.Length - 1]);

                    instruction.destination_register = GetRegister(tokens[1]);
                    instruction.source_memory_type = Memory_Type.Memory_Type_ENUM.VALUE;

                    // check if the value is valid and assign it, if not then throw an error
                    if (value.Item2)
                        instruction.source_memory_name = value.Item1.ToString();
                    else
                        AnalyzerError(line);

                    // assign the bitmode automatically or let the use assign the bit mode
                    if (tokens.Length == 3)
                        instruction.bit_mode = AssignBitMode(instruction, tokens[1]);
                    else
                        instruction.bit_mode = GetUserAssignedBitmode(tokens[tokens.Length - 2]);

                    // check if the destination register is a 8bit high or low one
                    if (tokens[1].ToUpper().EndsWith('H'))
                        instruction.destination_high_or_low = true;

                    break;

                case Variants.Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS:
                    instruction.destination_register = GetRegister(tokens[1]);
                    instruction.source_register = GetRegister(tokens[tokens.Length - 1].Trim('[', ']'));
                    instruction.source_memory_type = Memory_Type.Memory_Type_ENUM.ADDRESS;
                    instruction.source_memory_name = tokens[tokens.Length - 1].Trim('[', ']');

                    // assign the bitmode automatically or let the use assign the bit mode
                    if (tokens.Length == 3)
                        instruction.bit_mode = AssignBitMode(instruction, tokens[tokens.Length - 1]);
                    else
                        instruction.bit_mode = GetUserAssignedBitmode(tokens[tokens.Length - 2]);

                    if (tokens[tokens.Length - 1].StartsWith('[') && tokens[tokens.Length - 1].EndsWith(']'))
                        instruction.source_pointer = true;

                    // check if the destination register is a 8bit high or low one
                    if (tokens[1].ToUpper().EndsWith('H'))
                        instruction.destination_high_or_low = true;

                    break;

                case Variants.Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER:
                    instruction.destination_register = GetRegister(tokens[1]);
                    instruction.source_register = GetRegister(tokens[tokens.Length - 1]);
                    instruction.destination_memory_type = Memory_Type.Memory_Type_ENUM.ADDRESS;
                    instruction.destination_memory_name = tokens[1].Trim('[', ']');
                    instruction.bit_mode = AssignBitMode(instruction, tokens[1].Trim('[', ']'));
                    instruction = AssignRegisterPointers(instruction, tokens[1], "");

                    if (tokens[1].StartsWith('[') && tokens[1].EndsWith(']'))
                        instruction.destination_pointer = true;

                    // check if the destination register is a 8bit high or low one
                    if (tokens[tokens.Length - 1].ToUpper().EndsWith('H'))
                        instruction.destination_high_or_low = true;

                    break;

                case Variants.Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE:
                    value = GetValue(tokens[tokens.Length - 1]);

                    instruction.destination_memory_type = Memory_Type.Memory_Type_ENUM.ADDRESS;
                    instruction.destination_pointer = true;
                    instruction.destination_register = GetRegister(tokens[1].Trim('[', ']'));

                    // check if the value is valid and assign it, if not then throw an error
                    if (value.Item2)
                        instruction.source_memory_name = value.Item1.ToString();
                    else
                        AnalyzerError(line);

                    // assign the bitmode automatically or let the use assign the bit mode
                    if (tokens.Length == 3)
                        instruction.bit_mode = AssignBitMode(instruction, tokens[1]);
                    else
                        instruction.bit_mode = GetUserAssignedBitmode(tokens[tokens.Length - 2]);

                    break;
            }

            return instruction;
        }

        private string[] ScanForLabels(string to_analyze)
        {
            List<string> toReturn = new();
            string[] lines = to_analyze.Split('\n');

            for (int line = 0; line < lines.Length; line++)
            {
                string[] tokens = lines[line].Split(' ');

                if (tokens.Length == 1 && tokens[0].EndsWith(':'))
                    toReturn.Add(tokens[0].TrimEnd(':'));
            }

            return toReturn.ToArray();
        }

        public bool AnalyzingSuccessful()
        {
            return successful;
        }

        public int GetErrorLine()
        {
            if (successful == false)
                return error_line;

            return -1;
        }

        public string GetErrorLineData()
        {
            if (successful == false)
                return error_line_data;

            return "";
        }

        public List<Instruction> GetInstructions()
        {
            return instructions;
        }

        public List<StaticData> GetStaticData()
        {
            return static_data;
        }

        public List<Models.Label> GetLabelData()
        {
            return this.labels;
        }

        private Opcodes.Opcodes_ENUM GetInstruction(string opcode_to_analyze)
        {
            if (Enum.TryParse(opcode_to_analyze.ToUpper(), out Opcodes.Opcodes_ENUM toReturn))
                return toReturn;

            return Opcodes.Opcodes_ENUM.NoN;
        }

        private Variants.Variants_ENUM GetVariant(Opcodes.Opcodes_ENUM instruction, string[] tokens)
        {
            string last_token = tokens[tokens.Length - 1];
            string[] bit_mode_keywords = new string[]
            {
                "BYTE",
                "WORD",
                "DOUBLE",
                "QUAD"
            };

            // remove all commas
            for (int i = 0; i < tokens.Length; i++)
                if (tokens[i].Contains(','))
                    tokens[i] = tokens[i].Remove(tokens[i].IndexOf(','));

            // check if the instruction has no operands
            if (Instruction_Groups.no_operands.Contains(instruction) && tokens.Length == 1)
                return Variants.Variants_ENUM.SINGLE;

            // check if the instruction refers to a label
            if (Instruction_Groups.one_label.Contains(instruction) && tokens.Length == 2)
                return Variants.Variants_ENUM.LABEL;

            // check if the Instruction_Data has one operand
            if (Instruction_Groups.one_operands.Contains(instruction) && (tokens.Length == 2 || tokens.Length == 3 && bit_mode_keywords.Contains(tokens[1].ToUpper())))
            {
                // check if it points to a value in memory
                if (last_token.StartsWith('[') && last_token.EndsWith(']'))
                    if (StringHandler.GetCharOccurrences(last_token, '[') == 1 && StringHandler.GetCharOccurrences(last_token, ']') == 1)
                        return Variants.Variants_ENUM.SINGLE_ADDRESS_VALUE;

                // check if it refers to a register
                if (GetRegister(last_token.ToUpper()) != Registers.Registers_ENUM.NoN)
                    return Variants.Variants_ENUM.SINGLE_REGISTER;

                // check if it refers to an int
                if (int.TryParse(last_token, out _))
                    return Variants.Variants_ENUM.SINGLE_VALUE;

                // check if it refers to binary
                if (last_token.ToUpper().EndsWith('B'))
                {
                    bool acceptable_binary_number = true;

                    for (int i = 0; i < last_token.Length && acceptable_binary_number; i++)
                        if (last_token[i] != '0' || last_token[i] != '1')
                            acceptable_binary_number = false;

                    if (acceptable_binary_number)
                        return Variants.Variants_ENUM.SINGLE_VALUE;
                }

                // check if it refers to hex
                if (last_token.ToUpper().EndsWith('H'))
                    if (BaseVerifier.IsHex(last_token.ToUpper().TrimEnd('H')))
                        return Variants.Variants_ENUM.SINGLE_VALUE;
            }

            // check if the instruction has two operands
            if (Instruction_Groups.two_operands.Contains(instruction) && (tokens.Length == 3 || tokens.Length == 4 && bit_mode_keywords.Contains(tokens[2].ToUpper())))
            {
                // check if the destination and source are both registers
                if (GetRegister(tokens[1]) != Registers.Registers_ENUM.NoN)
                    if (GetRegister(last_token) != Registers.Registers_ENUM.NoN)
                        return Variants.Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER;

                // check if the destination is a register and source a number
                if (GetRegister(tokens[1]) != Registers.Registers_ENUM.NoN)
                {
                    // check if it refers to an int
                    if (int.TryParse(last_token, out _))
                        return Variants.Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE;

                    // check if it refers to binary
                    if (last_token.ToUpper().EndsWith('B'))
                        if (BaseVerifier.IsBinary(last_token.ToUpper().TrimEnd('B')))
                            return Variants.Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE;

                    // check if it refers to hex
                    if (last_token.ToUpper().EndsWith('H'))
                        if (BaseVerifier.IsHex(last_token.ToUpper().TrimEnd('H')))
                            return Variants.Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE;

                    // Check if it refers to an ASCII character
                    if (last_token.EndsWith('\'') && last_token.StartsWith('\''))
                        if (last_token.Length == 3)
                            return Variants.Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE;

                    // check if the destination is a register and the source is a value from memory
                    if (GetRegister(tokens[1].ToUpper()) != Registers.Registers_ENUM.NoN)
                        if (last_token.StartsWith('[') && last_token.EndsWith(']'))
                            if (StringHandler.GetCharOccurrences(last_token, '[') == 1 && StringHandler.GetCharOccurrences(last_token, ']') == 1)
                                return Variants.Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS;

                    // check if the destination is a register and the source is a location in memory
                    if (GetRegister(tokens[1].ToUpper()) != Registers.Registers_ENUM.NoN)
                        for (int i = 0; i < static_data.Count; i++)
                            if (static_data[i].name == last_token)
                                return Variants.Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS;

                    // check if the destination is a location in memory and the source is a register
                    if (tokens[1].StartsWith('[') && tokens[1].EndsWith(']'))
                        if (StringHandler.GetCharOccurrences(tokens[1], '[') == 1 && StringHandler.GetCharOccurrences(tokens[1], ']') == 1)
                            if (GetRegister(tokens[1].ToUpper()) != Registers.Registers_ENUM.NoN)
                                return Variants.Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER;
                }

                // check if the destination register is a pointer
                if (GetRegister(tokens[1].Trim('[', ']')) != Registers.Registers_ENUM.NoN)
                {
                    // check if it refers to an int
                    if (int.TryParse(last_token, out _))
                        return Variants.Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE;

                    // check if it refers to binary
                    if (last_token.ToUpper().EndsWith('B'))
                        if (BaseVerifier.IsBinary(last_token.ToUpper().TrimEnd('B')))
                            return Variants.Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE;

                    // check if it refers to hex
                    if (last_token.ToUpper().EndsWith('H'))
                        if (BaseVerifier.IsHex(last_token.ToUpper().TrimEnd('H')))
                            return Variants.Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE;

                    // Check if it refers to an ASCII character
                    if (last_token.EndsWith('\'') && last_token.StartsWith('\''))
                        if (last_token.Length == 3)
                            return Variants.Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE;

                    // check if the destination is a register and the source is a value from memory
                    if (GetRegister(last_token.ToUpper()) != Registers.Registers_ENUM.NoN)
                        if (tokens[1].StartsWith('[') && tokens[1].EndsWith(']'))
                            if (StringHandler.GetCharOccurrences(tokens[1], '[') == 1 && StringHandler.GetCharOccurrences(tokens[1], ']') == 1)
                                return Variants.Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER;
                }
            }

            return Variants.Variants_ENUM.NoN;
        }

        private Registers.Registers_ENUM GetRegister(string register_name)
        {
            // the register types
            Registers.Registers_ENUM[] register_type = new Registers.Registers_ENUM[] {
                Registers.Registers_ENUM.RAX,
                Registers.Registers_ENUM.RBX,
                Registers.Registers_ENUM.RCX,
                Registers.Registers_ENUM.RDX,
                Registers.Registers_ENUM.RSI,
                Registers.Registers_ENUM.RDI,
                Registers.Registers_ENUM.RSP,
                Registers.Registers_ENUM.RBP,
                Registers.Registers_ENUM.RIP,
                Registers.Registers_ENUM.R8,
                Registers.Registers_ENUM.R9,
                Registers.Registers_ENUM.R10,
                Registers.Registers_ENUM.R11,
                Registers.Registers_ENUM.R12,
                Registers.Registers_ENUM.R13,
                Registers.Registers_ENUM.R14,
                Registers.Registers_ENUM.R15
            };

            // the lookup table
            string[][] register_lookup = new string[][]
            {
                new string[] { "RAX", "EAX", "AX", "AH", "AL" },
                new string[] { "RBX", "EBX", "BX", "BH", "BL" },
                new string[] { "RCX", "ECX", "CX", "CH", "CL" },
                new string[] { "RDX", "EDX", "DX", "DH", "DL" },
                new string[] { "RSI", "ESI", "SI", "SIL" },
                new string[] { "RDI", "EDI", "DI", "DIL" },
                new string[] { "RSP", "ESP", "SP", "SPL" },
                new string[] { "RBP", "EBP", "BP", "BPL" },
                new string[] { "RIP", "EIP", "IP" },
                new string[] { "R8", "R8D", "R8W", "R8B" },
                new string[] { "R9", "R9D", "R9W", "R9B" },
                new string[] { "R10", "R10D", "R10W", "R10B" },
                new string[] { "R11", "R11D", "R11W", "R11B" },
                new string[] { "R12", "R12D", "R12W", "R12B" },
                new string[] { "R13", "R13D", "R13W", "R13B" },
                new string[] { "R14", "R14D", "R14W", "R14B" },
                new string[] { "R15", "R15D", "R15W", "R15B" }
            };

            // goes through every element of the lookup table until it finds a match
            // then it returns the register type
            for (int i = 0; i < register_lookup.Length; i++)
                foreach (string register_title in register_lookup[i])
                    if (register_title == register_name.ToUpper())
                        return register_type[i];

            // return NoN if a match wasnt found
            return Registers.Registers_ENUM.NoN;
        }

        private string[] RemoveComments(string[] to_remove_from)
        {
            List<string> toReturn = new();

            for (int i = 0; i < to_remove_from.Length; i++)
            {
                if (to_remove_from[i].Contains(';'))
                    toReturn.Add(to_remove_from[i].Split(';')[0]);
                else
                    toReturn.Add(to_remove_from[i]);
            }

            return toReturn.ToArray();
        }

        private SIZE.Size_ENUM AssignBitMode(Instruction instruction, string register_token)
        {
            if (instruction.destination_register == Registers.Registers_ENUM.NoN && instruction.source_register == Registers.Registers_ENUM.NoN || register_token == "")
                return SIZE.Size_ENUM.NoN;

            if (Register_Verifier.Is64BitRegister(register_token))
                return SIZE.Size_ENUM._64_BIT;
            else if (Register_Verifier.Is32BitRegister(register_token))
                return SIZE.Size_ENUM._32_BIT;
            else if (Register_Verifier.Is16BitRegister(register_token))
                return SIZE.Size_ENUM._16_BIT;
            else
                foreach (StaticData data in static_data)
                    if (register_token == data.name)
                        return data.size_in_bits;

            return SIZE.Size_ENUM._8_BIT;
        }

        private Instruction AssignRegisterPointers(Instruction instruction, string destination_register_token, string source_register_token)
        {
            bool destination_register_pointer = false;
            bool source_register_pointer = false;

            destination_register_token = destination_register_token.ToUpper();
            source_register_token = source_register_token.ToUpper();

            if (destination_register_token != "" && destination_register_token.StartsWith('[') && destination_register_token.EndsWith(']'))
                instruction.destination_pointer = true;

            if (source_register_token != "" && source_register_token.StartsWith('[') && source_register_token.EndsWith(']'))
                instruction.source_pointer = true;

            destination_register_token = destination_register_token.Trim('[', ']');
            source_register_token = source_register_token.Trim('[', ']');

            // modify the destination / source register enums if applicable
            if (destination_register_pointer)
                if (Enum.TryParse(destination_register_token, out Registers.Registers_ENUM destination_register))
                    instruction.destination_register = destination_register;

            if (source_register_pointer)
                if (Enum.TryParse(source_register_token, out Registers.Registers_ENUM source_register))
                    instruction.source_register = source_register;

            return instruction;
        }

        private void AnalyzerError(int error_line)
        {
            string[] lines = this.static_data_to_analyze.Concat(this.instructions_to_analyze).ToArray();

            if (error_line == -1)
            {
                this.error_line = error_line;
                this.error_line_data = "No section.text was found";
                this.successful = false;
                return;
            }

            this.error_line_data = lines[error_line];
            this.error_line = error_line;
            this.successful = false;
        }

        private (ulong, bool) GetValue(string token_to_analyze)
        {
            ulong value;
            bool analyzed_successfuly;

            analyzed_successfuly = ulong.TryParse(token_to_analyze, out value);

            // check if the token is in hex
            if (token_to_analyze.ToUpper().EndsWith('H'))
            {
                value = BaseConverter.ConvertHexToUlong(token_to_analyze);
                analyzed_successfuly = true;
            }

            // check if the token is in binary
            if (token_to_analyze.ToUpper().EndsWith('B'))
            {
                value = BaseConverter.ConvertBinaryToUlong(token_to_analyze);
                analyzed_successfuly = true;
            }

            // check if the token is a character
            if (token_to_analyze.Length == 3 && token_to_analyze.StartsWith('\'') && token_to_analyze.EndsWith('\''))
            {
                value = token_to_analyze[1];
                analyzed_successfuly = true;
            }

            return (value, analyzed_successfuly);
        }

        private SIZE.Size_ENUM GetUserAssignedBitmode(string token_to_analyze)
        {
            switch (token_to_analyze.ToUpper())
            {
                case "BYTE":
                    return SIZE.Size_ENUM._8_BIT;

                case "WORD":
                    return SIZE.Size_ENUM._16_BIT;

                case "DOUBLE":
                    return SIZE.Size_ENUM._32_BIT;

                case "QUAD":
                    return SIZE.Size_ENUM._64_BIT;

                default:
                    return SIZE.Size_ENUM.NoN;
            }
        }

        private string[] static_data_to_analyze = Array.Empty<string>();
        private string[] instructions_to_analyze = Array.Empty<string>();
        private List<Instruction> instructions = new List<Instruction>();
        private List<StaticData> static_data = new List<StaticData>();
        private List<Models.Label> labels = new List<Models.Label>();
        private string error_line_data = string.Empty;
        private bool successful;
        private int error_line;
    }
}
