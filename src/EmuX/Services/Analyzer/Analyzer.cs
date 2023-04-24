﻿using EmuX.Models;
using EmuX.Services;

namespace EmuX.Services.Analyzer
{
    /// <summary>
    /// Analyzes a string value and returns the instructions and the data necessary for the Emulator.cs class
    /// </summary>
    internal class Analyzer : Instruction_Data
    {
        private void Flush()
        {
            static_data = new List<StaticData>();
            instructions = new List<Instruction>();
            labels = new List<(string, int)>();
            successful = true;
        }

        /// <summary>
        /// Setter - Sets the instructions data to analyze with <c>AnalyzeInstructions()</c>
        /// </summary>
        public void SetInstructions(string instructions_to_analyze)
        {
            instructions_data = instructions_to_analyze;
        }

        /// <summary>
        /// Analyzes the instructions specified earlier with <c>SetInstructions(string instructions_to_analyze)</c>
        /// It does not return any value
        /// </summary>
        public void AnalyzeInstructions()
        {
            Instruction_Groups instruction_groups = new();
            string[] label_names = ScanForLabels(instructions_data);
            int offset = 1024;

            Flush();

            if (instructions_data.Contains("section.text") == false)
            {
                AnalyzerError(-1);
                return;
            }

            static_data_to_analyze = instructions_data.Split("section.text")[0].Split('\n');
            static_data_to_analyze = RemoveComments(static_data_to_analyze);
            static_data_to_analyze = StringHandler.RemoveEmptyLines(static_data_to_analyze);

            instructions_to_analyze = instructions_data.Split("section.text")[1].Split('\n');
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
                        instruction_to_add.instruction = Instruction_ENUM.LABEL;
                        instruction_to_add.bit_mode = Bit_Mode_ENUM.NoN;

                        labels.Add((tokens[0].TrimEnd(':'), line));
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
                instruction_to_add.instruction = GetInstruction(tokens[0]);

                // check to which parameter group the instruction belongs to
                if (instruction_groups.one_label.Contains(instruction_to_add.instruction))
                {
                    if (tokens.Length != 2)
                    {
                        AnalyzerError(line);
                        return;
                    }

                    instruction_to_add = AssignLabelInstruction(instruction_to_add, label_names, tokens[1], line);
                }
                else if (instruction_groups.no_operands.Contains(instruction_to_add.instruction))
                {
                    // check if the instruction is valid
                    if (tokens.Length != 1)
                    {
                        AnalyzerError(line);
                        return;
                    }

                    instruction_to_add = AssignNoOperandInstruction(instruction_to_add, tokens, line);
                }
                else if (instruction_groups.one_operands.Contains(instruction_to_add.instruction))
                {
                    if (tokens.Length != 2 && tokens.Length != 3)
                    {
                        AnalyzerError(line);
                        return;
                    }

                    instruction_to_add = AssignOneOperandInstruction(instruction_to_add, tokens, offset, line);
                }
                else if (instruction_groups.two_operands.Contains(instruction_to_add.instruction))
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

            instruction.variant = GetVariant(instruction.instruction, new string[] { "lol", label });
            instruction.bit_mode = Bit_Mode_ENUM.NoN;
            instruction.destination_memory_type = Memory_Type_ENUM.ADDRESS;
            instruction.destination_memory_name = label;

            return instruction;
        }

        private Instruction AssignNoOperandInstruction(Instruction instruction, string[] tokens, int line)
        {
            instruction.variant = GetVariant(instruction.instruction, tokens);

            // check if the variant is a label or a single variant instruction
            if (instruction.variant == Instruction_Variant_ENUM.LABEL)
            {
                instruction.destination_memory_type = Memory_Type_ENUM.LABEL;
                instruction.destination_memory_name = tokens[1];
                instruction.bit_mode = AssignBitMode(instruction, "");
            }
            else
            {
                instruction.bit_mode = Bit_Mode_ENUM.NoN;
            }

            return instruction;
        }

        private Instruction AssignOneOperandInstruction(Instruction instruction, string[] tokens, int memory_offset, int line)
        {
            instruction.variant = GetVariant(instruction.instruction, tokens);
            (ulong, bool) value;

            switch (instruction.variant)
            {
                case Instruction_Variant_ENUM.SINGLE_REGISTER:
                    instruction.destination_register = GetRegister(tokens[tokens.Length - 1]);

                    // check if the instruction has a keyword like byte (example: inc byte al) or not
                    if (tokens.Length == 2)
                    {
                        instruction.bit_mode = AssignBitMode(instruction, tokens[tokens.Length - 1]);
                    }
                    else
                    {
                        instruction.bit_mode = GetUserAssignedBitmode(tokens[1]);

                        if (instruction.bit_mode == Bit_Mode_ENUM.NoN)
                            AnalyzerError(line);
                    }

                    break;

                case Instruction_Variant_ENUM.SINGLE_VALUE:
                    value = GetValue(tokens[tokens.Length - 1]);

                    if (value.Item2 == false)
                        AnalyzerError(line);

                    instruction.destination_memory_name = value.Item1.ToString();
                    instruction.destination_memory_type = Memory_Type_ENUM.VALUE;

                    break;

                case Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE:
                    // check for keywords like byte word etc
                    if (tokens.Length == 3)
                        instruction.bit_mode = GetUserAssignedBitmode(tokens[1]);

                    instruction.destination_memory_type = Memory_Type_ENUM.ADDRESS;

                    break;
            }

            return instruction;
        }

        private Instruction AssignTwoOperandInstruction(Instruction instruction, string[] tokens, int memory_offset, int line)
        {
            instruction.variant = GetVariant(instruction.instruction, tokens);
            (ulong, bool) value;

            switch (instruction.variant)
            {
                case Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER:
                    instruction.destination_register = GetRegister(tokens[1]);
                    instruction.source_register = GetRegister(tokens[tokens.Length - 1]);

                    // assign automatically the bitmode with the source register or allow the user to modify the bitmode
                    if (tokens.Length == 3)
                        instruction.bit_mode = AssignBitMode(instruction, tokens[tokens.Length - 1]);
                    else
                        instruction.bit_mode = GetUserAssignedBitmode(tokens[tokens.Length - 2]);

                    break;

                case Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE:
                    value = GetValue(tokens[tokens.Length - 1]);

                    instruction.destination_register = GetRegister(tokens[1]);
                    instruction.source_memory_type = Memory_Type_ENUM.VALUE;

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

                case Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS:
                    instruction.destination_register = GetRegister(tokens[1]);
                    instruction.source_register = GetRegister(tokens[tokens.Length - 1].Trim('[', ']'));
                    instruction.source_memory_type = Memory_Type_ENUM.ADDRESS;
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

                case Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER:
                    instruction.destination_register = GetRegister(tokens[1]);
                    instruction.source_register = GetRegister(tokens[tokens.Length - 1]);
                    instruction.destination_memory_type = Memory_Type_ENUM.ADDRESS;
                    instruction.destination_memory_name = tokens[1].Trim('[', ']');
                    instruction.bit_mode = AssignBitMode(instruction, tokens[1].Trim('[', ']'));
                    instruction = AssignRegisterPointers(instruction, tokens[1], "");

                    if (tokens[1].StartsWith('[') && tokens[1].EndsWith(']'))
                        instruction.destination_pointer = true;

                    // check if the destination register is a 8bit high or low one
                    if (tokens[tokens.Length - 1].ToUpper().EndsWith('H'))
                        instruction.destination_high_or_low = true;

                    break;

                case Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE:
                    value = GetValue(tokens[tokens.Length - 1]);

                    instruction.destination_memory_type = Memory_Type_ENUM.ADDRESS;
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

        /// <summary>
        /// Analyzes and saves the static data
        /// If an error was encountered then it throws an error at the current line
        /// </summary>
        private int AnalyzeStaticData(string static_data_to_analyze, int offset, int line)
        {
            StaticData static_data_to_add = new();
            Instruction_Data register_name_lookup = new();

            string static_data_name = static_data_to_analyze.Split(':')[0].Trim();
            string[] static_data_tokens = static_data_to_analyze.Split(static_data_name + ":");
            ulong static_data_value = 0;

            for (int i = 0; i < static_data_tokens.Length; i++)
                static_data_tokens[i] = static_data_tokens[i].Trim();

            // the name of the static data
            static_data_tokens[0] = static_data_name.TrimEnd(':');

            // check if the static data name is valid or not aka if the static data name is a register name or not
            if (register_name_lookup._8_bit_registers.Contains(static_data_tokens[0].ToUpper())
            || register_name_lookup._16_bit_registers.Contains(static_data_tokens[0].ToUpper())
            || register_name_lookup._32_bit_registers.Contains(static_data_tokens[0].ToUpper())
            || register_name_lookup._64_bit_registers.Contains(static_data_tokens[0].ToUpper()))
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
                        static_data_to_add.size_in_bits = StaticData.SIZE._8_BIT;
                        offset++;
                        break;

                    case "DW":
                        static_data_to_add.size_in_bits = StaticData.SIZE._16_BIT;
                        offset += 2;
                        break;

                    case "DD":
                        static_data_to_add.size_in_bits = StaticData.SIZE._32_BIT;
                        offset += 4;
                        break;

                    case "DQ":
                        static_data_to_add.size_in_bits = StaticData.SIZE._64_BIT;
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

        /// <summary>
        /// Getter - Returns a boolean value based on if the analyzing step was successful or not
        /// </summary>
        public bool AnalyzingSuccessful()
        {
            return successful;
        }

        /// <summary>
        /// Getter - Returns the error line the analyzer failed (if the analyzer threw an error that is)
        /// </summary>
        public int GetErrorLine()
        {
            if (successful == false)
                return error_line;

            return -1;
        }

        /// <summary>
        /// Getter - Returns the error line data the analyzer failed (if the analyzer threw an error that is)
        /// </summary>
        public string GetErrorLineData()
        {
            if (successful == false)
                return error_line_data;

            return "";
        }

        /// <summary>
        /// Getter - Gets the instructions analyzed earlier
        /// </summary>
        public List<Instruction> GetInstructions()
        {
            return instructions;
        }

        /// <summary>
        /// Returns a tuple list of the names and addresses of the static data region alongside the value
        /// </summary>
        public List<StaticData> GetStaticData()
        {
            return static_data;
        }

        /// <summary>
        /// Returns a list of tuples of the names of the labels and the lines of said labels
        /// </summary>
        public List<(string, int)> GetLabelData()
        {
            return labels;
        }

        /// <summary>
        /// Parses and returns the instruction enum, returns NoN if a match wasnt found
        /// </summary>
        private Instruction_ENUM GetInstruction(string opcode_to_analyze)
        {
            Instruction_ENUM toReturn;

            if (Enum.TryParse(opcode_to_analyze.ToUpper(), out toReturn))
                return toReturn;

            return Instruction_ENUM.NoN;
        }

        /// <summary>
        /// Analyzes and return the variant of the specified instruction
        /// </summary>
        /// <param name="tokens">The static data name to analyze</param>
        private Instruction_Variant_ENUM GetVariant(Instruction_ENUM instruction, string[] tokens)
        {
            Instruction_Groups instruction_groups = new();
            HexConverter hex_converter = new();

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
            if (instruction_groups.one_operands.Contains(instruction) && (tokens.Length == 2 || tokens.Length == 3 && bit_mode_keywords.Contains(tokens[1].ToUpper())))
            {
                // check if it points to a value in memory
                if (last_token.StartsWith('[') && last_token.EndsWith(']'))
                    if (StringHandler.GetCharOccurrences(last_token, '[') == 1 && StringHandler.GetCharOccurrences(last_token, ']') == 1)
                        return Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE;

                // check if it refers to a register
                if (GetRegister(last_token.ToUpper()) != Registers_ENUM.NoN)
                    return Instruction_Variant_ENUM.SINGLE_REGISTER;

                // check if it refers to an int
                if (int.TryParse(last_token, out _))
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
            if (instruction_groups.two_operands.Contains(instruction) && (tokens.Length == 3 || tokens.Length == 4 && bit_mode_keywords.Contains(tokens[2].ToUpper())))
            {
                // check if the destination and source are both registers
                if (GetRegister(tokens[1]) != Registers_ENUM.NoN)
                    if (GetRegister(last_token) != Registers_ENUM.NoN)
                        return Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER;

                // check if the destination is a register and source a number
                if (GetRegister(tokens[1]) != Registers_ENUM.NoN)
                {
                    // check if it refers to an int
                    if (int.TryParse(last_token, out _))
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
                    if (GetRegister(tokens[1].ToUpper()) != Registers_ENUM.NoN)
                        if (last_token.StartsWith('[') && last_token.EndsWith(']'))
                            if (StringHandler.GetCharOccurrences(last_token, '[') == 1 && StringHandler.GetCharOccurrences(last_token, ']') == 1)
                                return Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS;

                    // check if the destination is a register and the source is a location in memory
                    if (GetRegister(tokens[1].ToUpper()) != Registers_ENUM.NoN)
                        for (int i = 0; i < static_data.Count; i++)
                            if (static_data[i].name == last_token)
                                return Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS;

                    // check if the destination is a location in memory and the source is a register
                    if (tokens[1].StartsWith('[') && tokens[1].EndsWith(']'))
                        if (StringHandler.GetCharOccurrences(tokens[1], '[') == 1 && StringHandler.GetCharOccurrences(tokens[1], ']') == 1)
                            if (GetRegister(tokens[1].ToUpper()) != Registers_ENUM.NoN)
                                return Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER;
                }

                // check if the destination register is a pointer
                if (GetRegister(tokens[1].Trim('[', ']')) != Registers_ENUM.NoN)
                {
                    // check if it refers to an int
                    if (int.TryParse(last_token, out _))
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
                    if (GetRegister(last_token.ToUpper()) != Registers_ENUM.NoN)
                        if (tokens[1].StartsWith('[') && tokens[1].EndsWith(']'))
                            if (StringHandler.GetCharOccurrences(tokens[1], '[') == 1 && StringHandler.GetCharOccurrences(tokens[1], ']') == 1)
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

        /// <summary>
        /// Assigns the bit mode specified by the register used, if no register is used, 
        /// for example <c>inc [to_increment]</c> then it returns NoN if register_token is set to ""
        /// </summary>
        private Bit_Mode_ENUM AssignBitMode(Instruction instruction, string register_token)
        {
            if (instruction.destination_register == Registers_ENUM.NoN && instruction.source_register == Registers_ENUM.NoN || register_token == "")
                return Bit_Mode_ENUM.NoN;

            Instruction_Data instruction_object = new();
            register_token = register_token.ToUpper();

            if (instruction_object._64_bit_registers.Contains(register_token))
                return Bit_Mode_ENUM._64_BIT;
            else if (instruction_object._32_bit_registers.Contains(register_token))
                return Bit_Mode_ENUM._32_BIT;
            else if (instruction_object._16_bit_registers.Contains(register_token))
                return Bit_Mode_ENUM._16_BIT;
            else
            {
                for (int i = 0; i < static_data.Count; i++)
                {
                    if (register_token == static_data[i].name)
                    {
                        switch (static_data[i].size_in_bits)
                        {
                            case StaticData.SIZE._8_BIT:
                                return Bit_Mode_ENUM._8_BIT;

                            case StaticData.SIZE._16_BIT:
                                return Bit_Mode_ENUM._16_BIT;

                            case StaticData.SIZE._32_BIT:
                                return Bit_Mode_ENUM._32_BIT;

                            case StaticData.SIZE._64_BIT:
                                return Bit_Mode_ENUM._8_BIT;
                        }
                    }
                }
            }

            return Bit_Mode_ENUM._8_BIT;
        }

        /// <summary>
        /// Returns the modified version of the current instruction with the boolean values for register pointers
        /// modified if applicable, it also modifies the destination register and source register if a pointer
        /// and at last it also modifies the bit mode, again, if applicable
        /// </summary>
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
                if (Enum.TryParse(destination_register_token, out Registers_ENUM destination_register))
                    instruction.destination_register = destination_register;

            if (source_register_pointer)
                if (Enum.TryParse(source_register_token, out Registers_ENUM source_register))
                    instruction.source_register = source_register;

            return instruction;
        }

        /// <summary>
        /// Used to specify an error
        /// </summary>
        private void AnalyzerError(int error_line)
        {
            string[] lines = static_data_to_analyze.Concat(instructions_to_analyze).ToArray();

            if (error_line == -1)
            {
                this.error_line = error_line;
                error_line_data = "No section.text was found";
                successful = false;
                return;
            }

            error_line_data = lines[error_line];
            this.error_line = error_line;
            successful = false;
        }

        /// <summary>
        /// Analyzes the token and returns its value in ulong, it accepts character ('a'), binary (01011010b), hex (FFh) and numbers
        /// </summary>
        /// <param name="token_to_analyze"></param>
        /// <returns>The converted value and a boolean value if the analyzing was successful</returns>
        private (ulong, bool) GetValue(string token_to_analyze)
        {
            BaseConverter base_converter = new();

            ulong value;
            bool analyzed_successfuly;

            analyzed_successfuly = ulong.TryParse(token_to_analyze, out value);

            // check if the token is in hex
            if (token_to_analyze.ToUpper().EndsWith('H'))
            {
                value = base_converter.ConvertHexToUnsignedLong(token_to_analyze);
                analyzed_successfuly = true;
            }

            // check if the token is in binary
            if (token_to_analyze.ToUpper().EndsWith('B'))
            {
                value = base_converter.ConvertBinaryToUnsignedLong(token_to_analyze);
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

        private Bit_Mode_ENUM GetUserAssignedBitmode(string token_to_analyze)
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

                default:
                    return Bit_Mode_ENUM.NoN;
            }
        }

        private string instructions_data = "";
        private string[] static_data_to_analyze = Array.Empty<string>();
        private string[] instructions_to_analyze = Array.Empty<string>();
        private List<Instruction> instructions = new();
        private List<StaticData> static_data = new();
        private List<(string, int)> labels = new();
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
