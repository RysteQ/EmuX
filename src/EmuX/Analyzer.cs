using System.Reflection.Metadata.Ecma335;
using static EmuX.Instruction_Data;

namespace EmuX
{
    /// <summary>
    /// Analyzes a string value and returns the instructions and the data necessary for the Emulator.cs class
    /// </summary>
    internal class Analyzer
    {
        private void Flush()
        {
            this.static_data = new List<StaticData>();
            this.instructions = new List<Instruction>();
            this.labels = new List<(string, int)>();
            this.successful = true;
        }

        /// <summary>
        /// Setter - Sets the instructions data to analyze with <c>AnalyzeInstructions()</c>
        /// </summary>
        public void SetInstructions(string instructions_to_analyze)
        {
            this.instructions_data = instructions_to_analyze;
        }

        /// <summary>
        /// Analyzes the instructions specified earlier with <c>SetInstructions(string instructions_to_analyze)</c>
        /// It does not return any value
        /// </summary>
        public void AnalyzeInstructions()
        {
            Instruction_Groups instruction_groups = new Instruction_Groups();
            int offset = 1024;

            // flush everything and analyze the static data
            this.Flush();

            this.instructions_to_analyze = this.instructions_data.Split('\n');
            this.instructions_to_analyze = this.RemoveComments(this.instructions_to_analyze);
            this.instructions_to_analyze = new StringHandler().RemoveEmptyLines(this.instructions_to_analyze);

            for (int line = 0; line < this.instructions_to_analyze.Length && this.successful; line++)
            {
                Instruction instruction_to_add = new Instruction();
                string instruction_to_analyze = this.instructions_to_analyze[line];
                string[] tokens = instruction_to_analyze.Split(' ');

                // check if the line refers to static data
                if (tokens[0].EndsWith(':'))
                {
                    if (tokens.Length == 1)
                    {
                        this.labels.Add((tokens[0].TrimEnd(':'), line));
                        continue;
                    } else
                    {
                        offset = this.AnalyzeStaticData(instruction_to_analyze, offset, line);
                        continue;
                    }
                }

                // get the instruction
                instruction_to_add.instruction = this.GetInstruction(tokens[0]);

                // check to which parameter group the instruction belongs to
                if (instruction_groups.one_label.Contains(instruction_to_add.instruction))
                {
                    if (tokens.Length != 2)
                    {
                        this.AnalyzerError(line);
                        return;
                    }

                    instruction_to_add = AssignLabelInstruction(instruction_to_add, tokens[1], line);
                } else if (instruction_groups.no_operands.Contains(instruction_to_add.instruction))
                {
                    // check if the instruction is valid
                    if (tokens.Length != 1)
                    {
                        this.AnalyzerError(line);
                        return;
                    }

                    instruction_to_add = this.AssignNoOperandInstruction(instruction_to_add, tokens, line);
                } else if (instruction_groups.one_operands.Contains(instruction_to_add.instruction))
                {
                    if (tokens.Length != 2 && tokens.Length != 3)
                    {
                        this.AnalyzerError(line);
                        return;
                    }

                    instruction_to_add = this.AssignOneOperandInstruction(instruction_to_add, tokens, offset, line);
                } else if (instruction_groups.two_operands.Contains(instruction_to_add.instruction))
                {
                    if (tokens.Length != 3 && tokens.Length != 4)
                    {
                        this.AnalyzerError(line);
                        return;
                    }

                    instruction_to_add = this.AssignTwoOperandInstruction(instruction_to_add, tokens, offset, line);
                } else
                {
                    this.AnalyzerError(line);
                    return;
                }

                this.instructions.Add(instruction_to_add);
            }
        }

        private Instruction AssignLabelInstruction(Instruction instruction, string label, int line)
        {
            bool label_found = false;

            // check if the label exist, if don't then throw an error
            for (int i = 0; i < labels.Count; i++)
                if (labels[i].Item1 == label)
                    label_found = true;

            if (label_found == false)
            {
                this.AnalyzerError(line);
                return instruction;
            }

            instruction.variant = this.GetVariant(instruction.instruction, new string[] { "lol", label });
            instruction.bit_mode = Bit_Mode_ENUM.NoN;
            instruction.destination_memory_type = Memory_Type_ENUM.ADDRESS;
            instruction.destination_memory_name = label;

            return instruction;
        }

        private Instruction AssignNoOperandInstruction(Instruction instruction, string[] tokens, int line)
        {
            instruction.variant = this.GetVariant(instruction.instruction, tokens);

            // check if the variant is a label or a single variant instruction
            if (instruction.variant == Instruction_Variant_ENUM.LABEL)
            {
                instruction.destination_memory_type = Memory_Type_ENUM.LABEL;
                instruction.destination_memory_name = tokens[1];
                instruction.bit_mode = this.AssignBitMode(instruction, "");
            }

            return instruction;
        }

        private Instruction AssignOneOperandInstruction(Instruction instruction, string[] tokens, int memory_offset, int line)
        {
            instruction.variant = this.GetVariant(instruction.instruction, tokens);
            (ulong, bool) value;

            switch (instruction.variant)
            {
                case Instruction_Variant_ENUM.SINGLE_REGISTER:
                    instruction.destination_register = this.GetRegister(tokens[tokens.Length - 1]);

                    // check if the instruction has a keyword like byte (example: inc byte al) or not
                    if (tokens.Length == 2)
                    {
                        instruction.bit_mode = this.AssignBitMode(instruction, tokens[tokens.Length - 1]);
                    }
                    else
                    {
                        instruction.bit_mode = this.GetUserAssignedBitmode(tokens[1]);

                        if (instruction.bit_mode == Bit_Mode_ENUM.NoN)
                            this.AnalyzerError(line);
                    }

                    break;

                case Instruction_Variant_ENUM.SINGLE_VALUE:
                    value = this.GetValue(tokens[tokens.Length - 1]);

                    if (value.Item2 == false)
                        this.AnalyzerError(line);

                    instruction.destination_memory_name = value.Item1.ToString();
                    instruction.destination_memory_type = Memory_Type_ENUM.VALUE;

                    break;

                case Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE:
                    // check for keywords like byte word etc
                    if (tokens.Length == 3)
                        instruction.bit_mode = this.GetUserAssignedBitmode(tokens[1]);

                    instruction.destination_memory_type = Memory_Type_ENUM.ADDRESS;

                    break;
            }

            return instruction;
        }

        private Instruction AssignTwoOperandInstruction(Instruction instruction, string[] tokens, int memory_offset, int line)
        {
            instruction.variant = this.GetVariant(instruction.instruction, tokens);
            (ulong, bool) value;

            switch (instruction.variant)
            {
                case Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER:
                    instruction.destination_register = this.GetRegister(tokens[1]);
                    instruction.source_register = this.GetRegister(tokens[tokens.Length - 1]);

                    // assign automatically the bitmode with the source register or allow the user to modify the bitmode
                    if (tokens.Length == 3)
                        instruction.bit_mode = this.AssignBitMode(instruction, tokens[tokens.Length - 1]);
                    else
                        instruction.bit_mode = this.GetUserAssignedBitmode(tokens[tokens.Length - 2]);

                    break;

                case Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE:
                    value = this.GetValue(tokens[tokens.Length - 1]);

                    instruction.destination_register = this.GetRegister(tokens[1]);
                    instruction.source_memory_type = Memory_Type_ENUM.VALUE;

                    // check if the value is valid and assign it, if not then throw an error
                    if (value.Item2)
                        instruction.source_memory_name = value.Item1.ToString();
                    else
                        this.AnalyzerError(line);

                    // assign the bitmode automatically or let the use assign the bit mode
                    if (tokens.Length == 3)
                        instruction.bit_mode = this.AssignBitMode(instruction, tokens[1]);
                    else
                        instruction.bit_mode = this.GetUserAssignedBitmode(tokens[tokens.Length - 2]);

                    // check if the destination register is a 8bit high or low one
                    if (tokens[1].ToUpper().EndsWith('H'))
                        instruction.destination_high_or_low = true;

                    break;

                case Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS:
                    instruction.destination_register = this.GetRegister(tokens[1]);
                    instruction.source_register = this.GetRegister(tokens[tokens.Length - 1].Trim('[', ']'));
                    instruction.source_memory_type = Memory_Type_ENUM.ADDRESS;
                    instruction.source_memory_name = tokens[tokens.Length - 1].Trim('[', ']');
                    instruction = this.AssignRegisterPointers(instruction, "", tokens[tokens.Length - 1].Trim('[', ']'));

                    // assign the bitmode automatically or let the use assign the bit mode
                    if (tokens.Length == 3)
                        instruction.bit_mode = this.AssignBitMode(instruction, tokens[tokens.Length - 1].Trim('[', ']'));
                    else
                        instruction.bit_mode = this.GetUserAssignedBitmode(tokens[tokens.Length - 2]);

                    // check if the destination register is a 8bit high or low one
                    if (tokens[1].ToUpper().EndsWith('H'))
                        instruction.destination_high_or_low = true;

                    break;

                case Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER:
                    instruction.destination_register = this.GetRegister(tokens[1]);
                    instruction.source_register = this.GetRegister(tokens[tokens.Length - 1]);
                    instruction.destination_memory_type = Memory_Type_ENUM.ADDRESS;
                    instruction.destination_memory_name = tokens[1];
                    instruction = this.AssignRegisterPointers(instruction, tokens[1], tokens[tokens.Length - 1]);

                    // check if the destination register is a 8bit high or low one
                    if (tokens[tokens.Length - 1].ToUpper().EndsWith('H'))
                        instruction.destination_high_or_low = true;

                    break;

                case Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE:
                    value = this.GetValue(tokens[tokens.Length - 1]);

                    instruction.destination_memory_type = Memory_Type_ENUM.ADDRESS;
                    instruction = this.AssignRegisterPointers(instruction, tokens[1].Trim('[', ']'), "");
                    
                    // check if the value is valid and assign it, if not then throw an error
                    if (value.Item2)
                        instruction.source_memory_name = value.Item1.ToString();
                    else
                        this.AnalyzerError(line);

                    // assign the bitmode automatically or let the use assign the bit mode
                    if (tokens.Length == 3)
                        instruction.bit_mode = this.AssignBitMode(instruction, tokens[1].Trim('[', ']'));
                    else
                        instruction.bit_mode = this.GetUserAssignedBitmode(tokens[tokens.Length - 2]);

                    break;
            }

            return instruction;
        }

        /// <summary>
        /// Analyzes and saves the static data
        /// If an error was encountered then it throws an error at the current line
        /// </summary>
        private int AnalyzeStaticData(string static_data_to_analyze, int offset, int line)
        {
            string[] static_data_tokens = static_data_to_analyze.Split(' ');
            ulong static_data_value = 0;

            // the name of the static data
            static_data_tokens[0] = static_data_tokens[0].TrimEnd(':').Trim();

            StaticData static_data_to_add = new StaticData();
            Instruction_Data register_name_lookup = new Instruction_Data();

            // check if the static data name is valid or not aka if the static data name is a register name or not
            if (register_name_lookup._8_bit_registers.Contains<string>(static_data_tokens[0].ToUpper()) 
            || register_name_lookup._16_bit_registers.Contains<string>(static_data_tokens[0].ToUpper()) 
            || register_name_lookup._32_bit_registers.Contains<string>(static_data_tokens[0].ToUpper()) 
            || register_name_lookup._64_bit_registers.Contains<string>(static_data_tokens[0].ToUpper()))
            {
                this.AnalyzerError(line);
                return offset;
            }

            // check if the static data is a number or a character / list of characters (aka string)
            if (static_data_tokens.Length == 3 && ulong.TryParse(static_data_tokens[2].Trim(), out static_data_value))
            {
                // fill in the necessary information
                static_data_to_add.name = static_data_tokens[0];
                static_data_to_add.value = static_data_value;

                // find out the bit size
                switch (static_data_tokens[1].ToUpper().Trim())
                {
                    case "DB":
                        static_data_to_add.size_in_bits = StaticData.SIZE._8_BIT;
                        static_data_to_add.memory_location = offset;
                        offset++;
                        break;

                    case "DW":
                        static_data_to_add.size_in_bits = StaticData.SIZE._16_BIT;
                        static_data_to_add.memory_location = offset;
                        offset += 2;
                        break;

                    case "DD":
                        static_data_to_add.size_in_bits = StaticData.SIZE._32_BIT;
                        static_data_to_add.memory_location = offset;
                        offset += 4;
                        break;

                    case "DQ":
                        static_data_to_add.size_in_bits = StaticData.SIZE._64_BIT;
                        static_data_to_add.memory_location = offset;
                        offset += 8;
                        break;

                    default:
                        this.AnalyzerError(line);
                        break;
                }
            } else
            {
                // check if the user made a mistake
                if (static_data_tokens[1].ToUpper().Trim() != "DB")
                {
                    this.AnalyzerError(line);
                    return offset;
                }

                // fill in the necessary information
                static_data_to_add.name = static_data_tokens[0].TrimEnd(':').Trim();
                static_data_to_add.memory_location = offset;
                static_data_to_add.is_string_array = true;

                // append every character after the DB to the List<char> in StaticData
                for (int i = 2; i < static_data_tokens.Length; i++)
                {
                    static_data_tokens[i] = static_data_tokens[i].Trim(',');

                    // make sure the user entered the ' character on the start and end of the token
                    if (static_data_tokens[i].Trim().StartsWith('\'') == false || static_data_tokens[i].Trim().EndsWith('\'') == false || static_data_tokens[i].Trim().Length != 3)
                    {
                        this.AnalyzerError(line);
                        return offset;
                    }

                    // add the character and update the offset
                    static_data_to_add.characters.Add(static_data_tokens[i].Trim(' ').Trim(',').Trim('\'').ToCharArray()[0]);
                    offset++;
                }
            }

            // add the new static data to the list
            this.static_data.Add(static_data_to_add);

            return offset;
        }

        /// <summary>
        /// Getter - Returns a boolean value based on if the analyzing step was successful or not
        /// </summary>
        public bool AnalyzingSuccessful()
        {
            return this.successful;
        }

        /// <summary>
        /// Getter - Returns the error line the analyzer failed (if the analyzer threw an error that is)
        /// </summary>
        public int GetErrorLine()
        {
            if (this.successful == false)
                return this.error_line;

            return -1;
        }

        /// <summary>
        /// Getter - Returns the error line data the analyzer failed (if the analyzer threw an error that is)
        /// </summary>
        public string GetErrorLineData()
        {
            if (this.successful == false)
                return this.error_line_data;

            return "";
        }

        /// <summary>
        /// Getter - Gets the instructions analyzed earlier
        /// </summary>
        public List<Instruction> GetInstructions()
        {
            return this.instructions;
        }

        /// <summary>
        /// Returns a tuple list of the names and addresses of the static data region alongside the value
        /// </summary>
        public List<StaticData> GetStaticData()
        {
            return this.static_data;
        }

        /// <summary>
        /// Returns a list of tuples of the names of the labels and the lines of said labels
        /// </summary>
        public List<(string, int)> GetLabelData()
        {
            return this.labels;
        }

        /// <summary>
        /// Parses and returns the instruction enum, returns NoN if a match wasnt found
        /// </summary>
        private Instruction_ENUM GetInstruction(string opcode_to_analyze)
        {
            Instruction_ENUM toReturn;

            if (Enum.TryParse<Instruction_ENUM>(opcode_to_analyze.ToUpper(), out toReturn))
                return toReturn;

            return Instruction_ENUM.NoN;
        }

        /// <summary>
        /// Analyzes and return the variant of the specified instruction
        /// </summary>
        /// <param name="tokens">The static data name to analyze</param>
        private Instruction_Variant_ENUM GetVariant(Instruction_ENUM instruction, string[] tokens)
        {
            Instruction_Groups instruction_groups = new Instruction_Groups();
            HexConverter hex_converter = new HexConverter();
            StringHandler string_handler = new StringHandler();
            int integer_junk;

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
            if (instruction_groups.no_operands.Contains(instruction) && tokens.Length == 1)
                return Instruction_Variant_ENUM.SINGLE;

            // check if the instruction refers to a label
            if (instruction_groups.one_label.Contains(instruction) && tokens.Length == 2)
                return Instruction_Variant_ENUM.LABEL;

            // check if the Instruction_Data has one operand
            if (instruction_groups.one_operands.Contains(instruction) && (tokens.Length == 2 || (tokens.Length == 3) && bit_mode_keywords.Contains(tokens[1].ToUpper())))
            {
                // check if it points to a value in memory
                if (last_token.StartsWith('[') && last_token.EndsWith(']'))
                    if (string_handler.GetCharOccurrences(last_token, '[') == 1 && string_handler.GetCharOccurrences(last_token, ']') == 1)
                        return Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE;

                // check if it refers to a register
                if (this.GetRegister(last_token.ToUpper()) != Registers_ENUM.NoN)
                    return Instruction_Variant_ENUM.SINGLE_REGISTER;

                // check if it refers to an int
                if (int.TryParse(last_token, out integer_junk))
                    return Instruction_Variant_ENUM.SINGLE_VALUE;

                // check if it refers to binary
                if (last_token.ToUpper().EndsWith('B'))
                {
                    bool acceptable_binary_number = true;

                    for (int i = 0; i < last_token.Length && acceptable_binary_number; i++)
                        if (last_token[i] != '0' || last_token[i] != '1')
                            acceptable_binary_number = false;

                    if (acceptable_binary_number)
                        return Instruction_Variant_ENUM.SINGLE_VALUE;
                }

                // check if it refers to hex
                if (last_token.ToUpper().EndsWith('H'))
                    if (hex_converter.IsHex(last_token.ToUpper().TrimEnd('H')))
                        return Instruction_Variant_ENUM.SINGLE_VALUE;
            }

            // check if the instruction has two operands
            if (instruction_groups.two_operands.Contains(instruction) && (tokens.Length == 3 || (tokens.Length == 4) && bit_mode_keywords.Contains(tokens[2].ToUpper())))
            {
                // check if the destination and source are both registers
                if (this.GetRegister(tokens[1]) != Registers_ENUM.NoN)
                    if (this.GetRegister(last_token) != Registers_ENUM.NoN)
                        return Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER;

                // check if the destination is a register and source a number
                if (this.GetRegister(tokens[1]) != Registers_ENUM.NoN)
                {
                    // check if it refers to an int
                    if (int.TryParse(last_token, out integer_junk))
                        return Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE;

                    // check if it refers to binary
                    if (last_token.ToUpper().EndsWith('B'))
                        if (hex_converter.IsBinary(last_token.ToUpper().TrimEnd('B')))
                            return Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE;

                    // check if it refers to hex
                    if (last_token.ToUpper().EndsWith('H'))
                        if (hex_converter.IsHex(last_token.ToUpper().TrimEnd('H')))
                            return Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE;

                    // Check if it refers to an ASCII character
                    if (last_token.EndsWith('\'') && last_token.StartsWith('\''))
                        if (last_token.Length == 3)
                            return Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE;

                    // check if the destination is a register and the source is a value from memory
                    if (this.GetRegister(tokens[1].ToUpper()) != Registers_ENUM.NoN)
                        if (last_token.StartsWith('[') && last_token.EndsWith(']'))
                            if (string_handler.GetCharOccurrences(last_token, '[') == 1 && string_handler.GetCharOccurrences(last_token, ']') == 1)
                                return Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS;

                    // check if the destination is a register and the source is a location in memory
                    if (this.GetRegister(tokens[1].ToUpper()) != Registers_ENUM.NoN)
                        for (int i = 0; i < this.static_data.Count; i++)
                            if (static_data[i].name == last_token)
                                return Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS;

                    // check if the destination is a location in memory and the source is a register
                    if (tokens[1].StartsWith('[') && tokens[1].EndsWith(']'))
                        if (string_handler.GetCharOccurrences(tokens[1], '[') == 1 && string_handler.GetCharOccurrences(tokens[1], ']') == 1)
                            if (this.GetRegister(tokens[1].ToUpper()) != Registers_ENUM.NoN)
                                return Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER;
                }

                // check if the destination register is a pointer
                if (this.GetRegister(tokens[1].Trim('[', ']')) != Registers_ENUM.NoN)
                {
                    // check if it refers to an int
                    if (int.TryParse(last_token, out integer_junk))
                        return Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE;

                    // check if it refers to binary
                    if (last_token.ToUpper().EndsWith('B'))
                        if (hex_converter.IsBinary(last_token.ToUpper().TrimEnd('B')))
                            return Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE;

                    // check if it refers to hex
                    if (last_token.ToUpper().EndsWith('H'))
                        if (hex_converter.IsHex(last_token.ToUpper().TrimEnd('H')))
                            return Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE;

                    // Check if it refers to an ASCII character
                    if (last_token.EndsWith('\'') && last_token.StartsWith('\''))
                        if (last_token.Length == 3)
                            return Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE;

                    // check if the destination is a register and the source is a value from memory
                    if (this.GetRegister(last_token.ToUpper()) != Registers_ENUM.NoN)
                        if (tokens[1].StartsWith('[') && tokens[1].EndsWith(']'))
                            if (string_handler.GetCharOccurrences(tokens[1], '[') == 1 && string_handler.GetCharOccurrences(tokens[1], ']') == 1)
                                return Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER;
                }
            }

            return Instruction_Variant_ENUM.NoN;
        }

        /// <summary>
        /// Returns the Enum value for said register, returns NoN if a match is not found
        /// </summary>
        /// <returns>The 64 bit register enum of the matched register</returns>
        private Registers_ENUM GetRegister(string register_name)
        {
            // the register types
            Registers_ENUM[] register_type = new Registers_ENUM[] {
                Registers_ENUM.RAX,
                Registers_ENUM.RBX,
                Registers_ENUM.RCX,
                Registers_ENUM.RDX,
                Registers_ENUM.RSI,
                Registers_ENUM.RDI,
                Registers_ENUM.RSP,
                Registers_ENUM.RBP,
                Registers_ENUM.RIP,
                Registers_ENUM.R8,
                Registers_ENUM.R9,
                Registers_ENUM.R10,
                Registers_ENUM.R11,
                Registers_ENUM.R12,
                Registers_ENUM.R13,
                Registers_ENUM.R14,
                Registers_ENUM.R15
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
                for (int j = 0; j < register_lookup[i].Length; j++)
                    if (register_lookup[i][j] == register_name.ToUpper())
                        return register_type[i];

            // return NoN if a match wasnt found
            return Registers_ENUM.NoN;
        }

        /// <summary>
        /// Removes the comments from the code, for example if the code is <c>mov rax, 60 ; sets rax to 60</c> then the return value will be <c>mov rax, 60</c>
        /// </summary>
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

        /// <summary>
        /// Assigns the bit mode specified by the register used, if no register is used, 
        /// for example <c>inc [to_increment]</c> then it returns NoN if register_token is set to ""
        /// </summary>
        private Bit_Mode_ENUM AssignBitMode(Instruction instruction, string register_token)
        {
            if ((instruction.destination_register == Registers_ENUM.NoN && instruction.source_register == Registers_ENUM.NoN) || register_token == "")
                return Bit_Mode_ENUM.NoN;

            Instruction_Data instruction_object = new Instruction_Data();

            if (instruction_object._64_bit_registers.Contains(register_token))
                return Bit_Mode_ENUM._64_BIT;
            else if (instruction_object._32_bit_registers.Contains(register_token))
                return Bit_Mode_ENUM._32_BIT;
            else if (instruction_object._16_bit_registers.Contains(register_token))
                return Bit_Mode_ENUM._16_BIT;
            else
                return Bit_Mode_ENUM._8_BIT;
        }

        /// <summary>
        /// Returns the modified version of the current instruction with the boolean values for register pointers
        /// modified if applicable, it also modifies the destination register and source register if a pointer
        /// and at last it also modifies the bit mode, again, if applicable
        /// </summary>
        private Instruction AssignRegisterPointers(Instruction instruction, string destination_register_token, string source_register_token)
        {
            Instruction_Data instruction_object = new Instruction_Data();

            Registers_ENUM destination_register = Registers_ENUM.NoN;
            Registers_ENUM source_register = Registers_ENUM.NoN;
            bool destination_register_pointer = false;
            bool source_register_pointer = false;

            destination_register_token = destination_register_token.ToUpper();
            source_register_token = source_register_token.ToUpper();

            // check if the value is an actual register or not
            destination_register_pointer = instruction_object._64_bit_registers.Contains(destination_register_token)
                                             || instruction_object._32_bit_registers.Contains(destination_register_token)
                                             || instruction_object._16_bit_registers.Contains(destination_register_token)
                                             || instruction_object._8_bit_registers.Contains(destination_register_token);

            // check if the value is an actual register or not
            source_register_pointer = instruction_object._64_bit_registers.Contains(source_register_token)
                                        || instruction_object._32_bit_registers.Contains(source_register_token)
                                        || instruction_object._16_bit_registers.Contains(source_register_token)
                                        || instruction_object._8_bit_registers.Contains(source_register_token);

            instruction.destination_register_pointer = destination_register_pointer;
            instruction.source_register_pointer = source_register_pointer;

            // modify the destination / source register enums if applicable
            if (destination_register_pointer)
                if (Enum.TryParse<Registers_ENUM>(destination_register_token, out destination_register))
                    instruction.destination_register = destination_register;

            if (source_register_pointer)
                if (Enum.TryParse<Registers_ENUM>(source_register_token, out source_register))
                    instruction.source_register = source_register;

            // modify the instruction bit mode if applicable
            if (destination_register_pointer)
            {
                if (instruction_object._64_bit_registers.Contains(destination_register_token))
                    instruction.bit_mode = Bit_Mode_ENUM._64_BIT;
                else if (instruction_object._32_bit_registers.Contains(destination_register_token))
                    instruction.bit_mode = Bit_Mode_ENUM._32_BIT;
                else if (instruction_object._16_bit_registers.Contains(destination_register_token))
                    instruction.bit_mode = Bit_Mode_ENUM._16_BIT;
                else
                    instruction.bit_mode = Bit_Mode_ENUM._8_BIT;
            }

            if (source_register_pointer)
            {
                if (instruction_object._64_bit_registers.Contains(source_register_token))
                    instruction.bit_mode = Bit_Mode_ENUM._64_BIT;
                else if (instruction_object._32_bit_registers.Contains(source_register_token))
                    instruction.bit_mode = Bit_Mode_ENUM._32_BIT;
                else if (instruction_object._16_bit_registers.Contains(source_register_token))
                    instruction.bit_mode = Bit_Mode_ENUM._16_BIT;
                else
                    instruction.bit_mode = Bit_Mode_ENUM._8_BIT;
            }

            return instruction;
        }

        /// <summary>
        /// Used to specift an error
        /// </summary>
        private void AnalyzerError(int error_line)
        {
            this.error_line_data = this.instructions_to_analyze[error_line];
            this.error_line = error_line;
            this.successful = false;
        }

        /// <summary>
        /// Analyzes the token and returns its value in ulong, it accepts character ('a'), binary (01011010b), hex (FFh) and numbers
        /// </summary>
        /// <param name="token_to_analyze"></param>
        /// <returns>The converted value and a boolean value if the analyzing was successful</returns>
        private (ulong, bool) GetValue(string token_to_analyze)
        {
            ulong value = 0;
            bool analyzed_successfuly = false;

            // check if the token is in hex
            if (token_to_analyze.ToUpper().EndsWith('H'))
            {
                BaseConverter base_converter = new BaseConverter();

                value = base_converter.ConvertHexToUnsignedLong(token_to_analyze);
                analyzed_successfuly = true;
            }

            // check if the token is in binary
            if (token_to_analyze.ToUpper().EndsWith('B'))
            {
                BaseConverter base_converter = new BaseConverter();

                value = base_converter.ConvertBinaryToUnsignedLong(token_to_analyze);
                analyzed_successfuly = true;
            }

            // check if the token is a character
            if (token_to_analyze.Length == 3 && token_to_analyze.StartsWith('\'') && token_to_analyze.EndsWith('\''))
            {
                value = (ulong) token_to_analyze[1];
                analyzed_successfuly = true;
            }

            // check if the token is a number
            if (ulong.TryParse(token_to_analyze, out value))
                analyzed_successfuly = true;

            return (value, analyzed_successfuly);
        }

        private Instruction_Data.Bit_Mode_ENUM GetUserAssignedBitmode(string token_to_analyze)
        {
            switch (token_to_analyze.ToUpper()) 
            {
                case "BYTE":
                    return Bit_Mode_ENUM._8_BIT;

                case "WORD":
                    return Bit_Mode_ENUM._16_BIT;

                case "DOUBLE":
                    return Bit_Mode_ENUM._32_BIT;

                case "QUAD":
                    return Bit_Mode_ENUM._64_BIT;
            }

            return Bit_Mode_ENUM.NoN;
        }

        private string instructions_data = "";
        private string[] instructions_to_analyze = new string[] { };
        private List<Instruction> instructions = new List<Instruction>();
        private List<StaticData> static_data = new List<StaticData>();
        private List<(string, int)> labels = new List<(string, int)>();
        private string error_line_data = "";
        private bool successful = false;
        private int error_line = 0;

        private class Instruction_Groups
        {
            public Instruction_ENUM[] no_operands = new Instruction_ENUM[]
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

            public Instruction_ENUM[] one_label = new Instruction_ENUM[]
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

            public Instruction_ENUM[] one_operands = new Instruction_ENUM[]
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

            public Instruction_ENUM[] two_operands = new Instruction_ENUM[]
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
}
