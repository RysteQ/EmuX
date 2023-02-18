using static EmuX.Instruction_Data;

namespace EmuX
{
    /// <summary>
    /// Analyzes a string value and returns the instructions and the data necessary for the Emulator.cs class
    /// </summary>
    internal class Analyzer
    {
        public void Flush()
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
            int offset = 1024;

            // clear the instructions and labels list
            this.instructions.Clear();
            this.labels.Clear();

            this.instructions_to_analyze = this.instructions_data.Split('\n');
            this.instructions_to_analyze = this.RemoveComments(this.instructions_to_analyze);
            this.instructions_to_analyze = new StringHandler().RemoveEmptyLines(this.instructions_to_analyze);

            for (int i = 0; i < this.instructions_to_analyze.Length && this.successful; i++)
            {
                Instruction instruction_to_add = new Instruction();
                string instruction_to_analyze = this.instructions_to_analyze[i];
                string[] tokens = instruction_to_analyze.Split(' ');

                Registers_ENUM destination_register;
                Registers_ENUM source_register;
                ulong value;

                // make sure the comma is outside of the ' character before removing it
                if (instruction_to_analyze.Contains(',') && ((instruction_to_analyze.Split(',')[0].IndexOf('\'') != instruction_to_analyze.IndexOf(',') - 1) && (instruction_to_analyze.Split(',')[1].IndexOf('\'') != instruction_to_analyze.IndexOf(',') + 1)))
                    instruction_to_analyze = instruction_to_analyze.Remove(instruction_to_analyze.IndexOf(','), 1);

                // check if the token is a label or not
                if (tokens[0].EndsWith(':') && tokens[0].Length > 1 && tokens[0].Contains(' ') == false)
                {
                    if (tokens.Length == 1)
                    {
                        string label_name = tokens[0].TrimEnd(':');
                        int label_line = i;

                        this.labels.Add((label_name, label_line));
                    } else
                    {
                        offset = AnalyzeStaticData(instruction_to_analyze, offset, i);
                    }

                    // add to the instruction the label
                    instruction_to_add.instruction = Instruction_ENUM.LABEL;
                    instruction_to_add = this.AssignRegisterParameters(instruction_to_add, Registers_ENUM.NoN, Registers_ENUM.NoN);
                    instruction_to_add = this.AssignMemoryTypeParameters(instruction_to_add, Memory_Type_ENUM.NoN, Memory_Type_ENUM.NoN);
                    instruction_to_add = this.AssignMemoryNameParameters(instruction_to_add, "", "");
                    instruction_to_add = this.AssignBitMode(instruction_to_add, "");
                    this.instructions.Add(instruction_to_add);

                    continue;
                }

                instruction_to_add.instruction = this.GetInstruction(tokens[0]);

                // check if the instruction exists or not
                if (instruction_to_add.instruction == Instruction_ENUM.NoN)
                {
                    this.AnalyzerError(i);
                    return;
                }

                // TODO: Improve the GetVariant function
                instruction_to_add.variant = this.GetVariant(instruction_to_add.instruction, tokens);

                // check if the instruction is of a valid variant or not
                if (instruction_to_add.variant == Instruction_Variant_ENUM.NoN)
                {
                    this.AnalyzerError(i);
                    return;
                }

                switch (instruction_to_add.variant)
                {
                    case Instruction_Variant_ENUM.SINGLE:
                        instruction_to_add = this.AssignRegisterParameters(instruction_to_add, Registers_ENUM.NoN, Registers_ENUM.NoN);
                        instruction_to_add = this.AssignMemoryTypeParameters(instruction_to_add, Memory_Type_ENUM.NoN, Memory_Type_ENUM.NoN);
                        instruction_to_add = this.AssignMemoryNameParameters(instruction_to_add, "", "");
                        instruction_to_add = this.AssignBitMode(instruction_to_add, "");
                        break;

                    case Instruction_Variant_ENUM.LABEL:
                        instruction_to_add = this.AssignRegisterParameters(instruction_to_add, Registers_ENUM.NoN, Registers_ENUM.NoN);
                        instruction_to_add = this.AssignMemoryTypeParameters(instruction_to_add, Memory_Type_ENUM.LABEL, Memory_Type_ENUM.NoN);
                        instruction_to_add = this.AssignMemoryNameParameters(instruction_to_add, tokens[1], "");
                        instruction_to_add = this.AssignBitMode(instruction_to_add, "");
                        break;

                    case Instruction_Variant_ENUM.SINGLE_REGISTER:
                        destination_register = this.GetRegister(tokens[1].ToUpper());

                        instruction_to_add = this.AssignRegisterParameters(instruction_to_add, destination_register, Registers_ENUM.NoN);
                        instruction_to_add = this.AssignMemoryTypeParameters(instruction_to_add, Memory_Type_ENUM.NoN, Memory_Type_ENUM.NoN);
                        instruction_to_add = this.AssignMemoryNameParameters(instruction_to_add, "", "");
                        instruction_to_add = this.AssignBitMode(instruction_to_add, tokens[1].ToUpper());

                        // check if the register is 8 bit or not for the destination register
                        if (tokens[1].ToUpper().EndsWith('H'))
                            instruction_to_add.destination_high_or_low = true;

                        break;

                    case Instruction_Variant_ENUM.SINGLE_VALUE:
                        // convert the number from base 10 / binary / hex to an integer
                        if (ulong.TryParse(tokens[1], out value) == false)
                        {
                            BaseConverter base_converter = new BaseConverter();

                            if (tokens[1].ToUpper().EndsWith('B'))
                                value = base_converter.ConvertBinaryToUnsignedLong(tokens[1]);
                            else
                                value = base_converter.ConvertHexToUnsignedLong(tokens[1]);
                        }

                        instruction_to_add = this.AssignRegisterParameters(instruction_to_add, Registers_ENUM.NoN, Registers_ENUM.NoN);
                        instruction_to_add = this.AssignMemoryTypeParameters(instruction_to_add, Memory_Type_ENUM.VALUE, Memory_Type_ENUM.NoN);
                        instruction_to_add = this.AssignMemoryNameParameters(instruction_to_add, value.ToString(), "");
                        instruction_to_add = this.AssignBitMode(instruction_to_add, "");
                        break;

                    case Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE:
                        instruction_to_add = this.AssignRegisterParameters(instruction_to_add, Registers_ENUM.NoN, Registers_ENUM.NoN);
                        instruction_to_add = this.AssignMemoryTypeParameters(instruction_to_add, Memory_Type_ENUM.ADDRESS, Memory_Type_ENUM.NoN);
                        instruction_to_add = this.AssignMemoryNameParameters(instruction_to_add, tokens[1].Trim('[', ']'), "");
                        instruction_to_add = this.AssignBitMode(instruction_to_add, "");
                        break;

                    case Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER:
                        destination_register = this.GetRegister(tokens[1].ToUpper());
                        source_register = this.GetRegister(tokens[2].ToUpper());

                        instruction_to_add = this.AssignRegisterParameters(instruction_to_add, destination_register, source_register);
                        instruction_to_add = this.AssignMemoryTypeParameters(instruction_to_add, Memory_Type_ENUM.NoN, Memory_Type_ENUM.NoN);
                        instruction_to_add = this.AssignMemoryNameParameters(instruction_to_add, "", "");
                        instruction_to_add = this.AssignBitMode(instruction_to_add, tokens[2].ToUpper());

                        // check if the register is 8 bit or not for the destination register
                        if (tokens[1].ToUpper().EndsWith('H'))
                            instruction_to_add.destination_high_or_low = true;

                        // check if the register is 8 bit or not for the source register
                        if (tokens[2].ToUpper().EndsWith('H'))
                            instruction_to_add.source_high_or_low = true;

                        break;

                    case Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE:
                        destination_register = this.GetRegister(tokens[1].ToUpper());

                        // convert the number from base 10 / binary / hex to an integer
                        if (ulong.TryParse(tokens[2], out value) == false)
                        {
                            BaseConverter base_converter = new BaseConverter();

                            if (tokens[1].ToUpper().EndsWith('B'))
                                value = base_converter.ConvertBinaryToUnsignedLong(tokens[1]);
                            else
                                value = base_converter.ConvertHexToUnsignedLong(tokens[1]);
                        }

                        instruction_to_add = this.AssignRegisterParameters(instruction_to_add, destination_register, Registers_ENUM.NoN);
                        instruction_to_add = this.AssignMemoryTypeParameters(instruction_to_add, Memory_Type_ENUM.NoN, Memory_Type_ENUM.VALUE);
                        instruction_to_add = this.AssignMemoryNameParameters(instruction_to_add, "", value.ToString());
                        instruction_to_add = this.AssignBitMode(instruction_to_add, tokens[1].ToUpper());

                        // check if the register is 8 bit or not for the destination register
                        if (tokens[1].ToUpper().EndsWith('H'))
                            instruction_to_add.destination_high_or_low = true;

                        break;

                    case Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS:
                        destination_register = GetRegister(tokens[1].ToUpper());

                        instruction_to_add = this.AssignRegisterParameters(instruction_to_add, destination_register, Registers_ENUM.NoN);
                        instruction_to_add = this.AssignMemoryTypeParameters(instruction_to_add, Memory_Type_ENUM.ADDRESS, Memory_Type_ENUM.NoN);
                        instruction_to_add = this.AssignMemoryNameParameters(instruction_to_add, "", tokens[2].Trim('[', ']'));
                        instruction_to_add = this.AssignMemoryBitmode(instruction_to_add, tokens[2].Trim('[', ']'));
                        instruction_to_add = this.AssignRegisterPointers(instruction_to_add, "", tokens[2].Trim('[', ']'));

                        // check if the register is 8 bit or not for the destination register
                        if (tokens[1].ToUpper().EndsWith('H'))
                            instruction_to_add.destination_high_or_low = true;

                        break;

                    case Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER:
                        source_register = this.GetRegister(tokens[2].ToUpper());

                        instruction_to_add = this.AssignRegisterParameters(instruction_to_add, Registers_ENUM.NoN, source_register);
                        instruction_to_add = this.AssignMemoryTypeParameters(instruction_to_add, Memory_Type_ENUM.ADDRESS, Memory_Type_ENUM.NoN);
                        instruction_to_add = this.AssignMemoryNameParameters(instruction_to_add, tokens[1], "");
                        instruction_to_add = this.AssignRegisterPointers(instruction_to_add, tokens[1].Trim('[', ']'), "");

                        // check if the register is 8 bit or not for the source register
                        if (tokens[2].ToUpper().EndsWith('H'))
                            instruction_to_add.source_high_or_low = true;

                        break;

                    case Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE:
                        // convert the number from base 10 / binary / hex to an integer
                        if (ulong.TryParse(tokens[2], out value) == false)
                        {
                            BaseConverter base_converter = new BaseConverter();

                            if (tokens[1].ToUpper().EndsWith('B'))
                                value = base_converter.ConvertBinaryToUnsignedLong(tokens[1]);
                            else
                                value = base_converter.ConvertHexToUnsignedLong(tokens[1]);
                        }

                        instruction_to_add = this.AssignRegisterParameters(instruction_to_add, Registers_ENUM.NoN, Registers_ENUM.NoN);
                        instruction_to_add = this.AssignMemoryTypeParameters(instruction_to_add, Memory_Type_ENUM.ADDRESS, Memory_Type_ENUM.NoN);
                        instruction_to_add = this.AssignMemoryNameParameters(instruction_to_add, "", value.ToString());
                        instruction_to_add = this.AssignRegisterPointers(instruction_to_add, tokens[1].Trim('[', ']'), "");

                        break;
                }

                this.instructions.Add(instruction_to_add);
            }
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
        /// <returns>The instruction Enum (MOV, ADD, etc)</returns>
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

            // check if the instruction has no operands
            if (instruction_groups.no_operands.Contains(instruction) && tokens.Length == 1)
                return Instruction_Variant_ENUM.SINGLE;

            // check if the instruction refers to a label
            if (instruction_groups.one_label.Contains(instruction) && tokens.Length == 2)
                return Instruction_Variant_ENUM.LABEL;

            // check if the Instruction_Data has one operand
            if (instruction_groups.one_operands.Contains(instruction) && tokens.Length == 2)
            {
                // check if it points to a value in memory
                if (tokens[1].StartsWith('[') && tokens[1].EndsWith(']'))
                    if (string_handler.GetCharOccurrences(tokens[1], '[') == 1 && string_handler.GetCharOccurrences(tokens[1], ']') == 1)
                        return Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE;

                // check if it refers to a register
                if (this.GetRegister(tokens[1].ToUpper()) != Registers_ENUM.NoN)
                    return Instruction_Variant_ENUM.SINGLE_REGISTER;

                // check if it refers to an int
                if (int.TryParse(tokens[1], out integer_junk))
                    return Instruction_Variant_ENUM.SINGLE_VALUE;

                // check if it refers to binary
                if (tokens[1].ToUpper().EndsWith('B'))
                {
                    bool acceptable_binary_number = true;

                    for (int i = 0; i < tokens[1].Length && acceptable_binary_number; i++)
                        if (tokens[1][i] != '0' || tokens[1][i] != '1')
                            acceptable_binary_number = false;

                    if (acceptable_binary_number)
                        return Instruction_Variant_ENUM.SINGLE_VALUE;
                }

                // check if it refers to hex
                if (tokens[1].ToUpper().EndsWith('H'))
                    if (hex_converter.IsHex(tokens[1].ToUpper().TrimEnd('H')))
                        return Instruction_Variant_ENUM.SINGLE_VALUE;
            }

            // check if the instruction has two operands
            if (instruction_groups.two_operands.Contains(instruction) && tokens.Length == 3)
            {
                // check if the destination and source are both registers
                if (this.GetRegister(tokens[1]) != Registers_ENUM.NoN)
                    if (this.GetRegister(tokens[2]) != Registers_ENUM.NoN)
                        return Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER;

                // check if the destination is a register and source a number
                if (this.GetRegister(tokens[1]) != Registers_ENUM.NoN)
                {
                    // check if it refers to an int
                    if (int.TryParse(tokens[2], out integer_junk))
                        return Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE;

                    // check if it refers to binary
                    if (tokens[2].ToUpper().EndsWith('B'))
                        if (hex_converter.IsBinary(tokens[2].ToUpper().TrimEnd('B')))
                            return Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE;

                    // check if it refers to hex
                    if (tokens[2].ToUpper().EndsWith('H'))
                        if (hex_converter.IsHex(tokens[2].ToUpper().TrimEnd('H')))
                            return Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE;

                    // Check if it refers to an ASCII character
                    if (tokens[2].EndsWith('\'') && tokens[2].StartsWith('\''))
                        if (tokens[2].Length == 3)
                            return Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE;

                    // check if the destination is a register and the source is a value from memory
                    if (this.GetRegister(tokens[1].ToUpper()) != Registers_ENUM.NoN)
                        if (tokens[2].StartsWith('[') && tokens[2].EndsWith(']'))
                            if (string_handler.GetCharOccurrences(tokens[2], '[') == 1 && string_handler.GetCharOccurrences(tokens[2], ']') == 1)
                                return Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS;

                    // check if the destination is a register and the source is a location in memory
                    if (this.GetRegister(tokens[1].ToUpper()) != Registers_ENUM.NoN)
                        for (int i = 0; i < this.static_data.Count; i++)
                            if (static_data[i].name == tokens[2])
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
                    if (int.TryParse(tokens[2], out integer_junk))
                        return Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE;

                    // check if it refers to binary
                    if (tokens[2].ToUpper().EndsWith('B'))
                        if (hex_converter.IsBinary(tokens[2].ToUpper().TrimEnd('B')))
                            return Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE;

                    // check if it refers to hex
                    if (tokens[2].ToUpper().EndsWith('H'))
                        if (hex_converter.IsHex(tokens[2].ToUpper().TrimEnd('H')))
                            return Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE;

                    // Check if it refers to an ASCII character
                    if (tokens[2].EndsWith('\'') && tokens[2].StartsWith('\''))
                        if (tokens[2].Length == 3)
                            return Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE;

                    // check if the destination is a register and the source is a value from memory
                    if (this.GetRegister(tokens[2].ToUpper()) != Registers_ENUM.NoN)
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
        /// Assigns the register parameters
        /// </summary>
        private Instruction AssignRegisterParameters(Instruction instruction, Registers_ENUM destination_register, Registers_ENUM source_register)
        {
            instruction.destination_register = destination_register;
            instruction.source_register = source_register;

            return instruction;
        }

        /// <summary>
        /// Assigns the memory type parameters
        /// </summary>
        private Instruction AssignMemoryTypeParameters(Instruction instruction, Memory_Type_ENUM destination_memory_type, Memory_Type_ENUM source_memory_type)
        {
            instruction.source_memory_type = source_memory_type;
            instruction.destination_memory_type = destination_memory_type;

            return instruction;
        }

        /// <summary>
        /// Assigns the memory name parameters
        /// </summary>
        private Instruction AssignMemoryNameParameters(Instruction instruction, string destination_memory_name, string source_memory_name)
        {
            instruction.destination_memory_name = destination_memory_name;
            instruction.source_memory_name = source_memory_name;

            return instruction;
        }

        private Instruction AssignMemoryBitmode(Instruction instruction, string static_data_name)
        {
            StaticData.SIZE size_of_static_data = StaticData.SIZE._8_BIT;

            for (int i = 0; i < this.static_data.Count; i++)
                if (static_data[i].name == static_data_name)
                    size_of_static_data = static_data[i].size_in_bits;

            switch (size_of_static_data)
            {
                case StaticData.SIZE._8_BIT:
                    instruction.bit_mode = Bit_Mode_ENUM._8_BIT;
                    break;

                case StaticData.SIZE._16_BIT:
                    instruction.bit_mode = Bit_Mode_ENUM._16_BIT;
                    break;

                case StaticData.SIZE._32_BIT:
                    instruction.bit_mode = Bit_Mode_ENUM._32_BIT;
                    break;

                case StaticData.SIZE._64_BIT:
                    instruction.bit_mode = Bit_Mode_ENUM._64_BIT;
                    break;
            }

            return instruction;
        }

        /// <summary>
        /// Assigns the bit mode specified by the register used, if no register is used, 
        /// for example <c>inc [to_increment]</c> then it returns NoN if register_token is set to ""
        /// </summary>
        private Instruction AssignBitMode(Instruction instruction, string register_token)
        {
            if ((instruction.destination_register == Registers_ENUM.NoN && instruction.source_register == Registers_ENUM.NoN) || register_token == "")
            {
                instruction.bit_mode = Bit_Mode_ENUM.NoN;
                return instruction;
            }

            Instruction_Data instruction_object = new Instruction_Data();

            if (instruction_object._64_bit_registers.Contains(register_token))
                instruction.bit_mode = Bit_Mode_ENUM._64_BIT;
            else if (instruction_object._32_bit_registers.Contains(register_token))
                instruction.bit_mode = Bit_Mode_ENUM._32_BIT;
            else if (instruction_object._16_bit_registers.Contains(register_token))
                instruction.bit_mode = Bit_Mode_ENUM._16_BIT;
            else
                instruction.bit_mode = Bit_Mode_ENUM._8_BIT;

            return instruction;
        }

        /// <summary>
        /// Returns the modified version of the current instruction with the boolean values for register pointers
        /// modified if applicable, it also modifies the destination register and source register if a pointer
        /// and at last it also modifies the bit mode, again, if applicable
        /// was found
        /// </summary>
        private Instruction AssignRegisterPointers(Instruction instruction, string destination_register_token, string source_register_token)
        {
            Instruction_Data instruction_object = new Instruction_Data();

            Instruction_Data.Registers_ENUM destination_register = Registers_ENUM.NoN;
            Instruction_Data.Registers_ENUM source_register = Registers_ENUM.NoN;
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
                if (Enum.TryParse<Instruction_Data.Registers_ENUM>(destination_register_token, out destination_register))
                    instruction.destination_register = destination_register;

            if (source_register_pointer)
                if (Enum.TryParse<Instruction_Data.Registers_ENUM>(source_register_token, out source_register))
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
        }
    }
}
