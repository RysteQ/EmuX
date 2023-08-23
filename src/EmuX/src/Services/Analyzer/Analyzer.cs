using EmuX.Services;
using EmuX.src.Enums;
using EmuX.src.Enums.Instruction_Data;
using EmuX.src.Enums.VM;
using EmuX.src.Models;
using EmuX.src.Services.Base.Converter;
using EmuX.src.Services.Base.Verifier;
using Label = EmuX.src.Models.Label;
using Size = EmuX.src.Enums.Size;

namespace EmuX.src.Services.Analyzer;

public class Analyzer
{
    private void Flush()
    {
        this.StaticData = new();
        this.Instructions = new();
        this.Labels = new();
        this.Successful = true;
    }

    public void AnalyzeInstructions(string to_analyze)
    {
        string[] label_names = ScanForLabels(to_analyze);
        int offset = 1024;

        to_analyze = string.Join("\n", StringHandler.RemoveEmptyLines(to_analyze.Split('\n')));
        this.Successful = false;

        Flush();

        if (to_analyze.Contains("section.text") == false)
        {
            AnalyzerError(-1, string.Empty);
            return;
        }

        string[] StaticData_to_analyze = to_analyze.Split("section.text")[0].Split('\n');
        StaticData_to_analyze = RemoveComments(StaticData_to_analyze);
        StaticData_to_analyze = StaticData_to_analyze.Where(line => string.IsNullOrWhiteSpace(line) == false).ToArray();

        string[] Instructions_to_analyze = to_analyze.Split("section.text")[1].Split('\n');
        Instructions_to_analyze = RemoveComments(Instructions_to_analyze);
        Instructions_to_analyze = Instructions_to_analyze.Where(line => string.IsNullOrWhiteSpace(line) == false).ToArray();

        for (int line = 0; line < StaticData_to_analyze.Length; line++)
            offset = AnalyzeStaticData(StaticData_to_analyze[line], offset, line);

        for (int line = 0; line < Instructions_to_analyze.Length && Successful; line++)
        {
            Instruction instruction_to_add = new();
            string instruction_to_analyze = Instructions_to_analyze[line];
            string[] tokens = instruction_to_analyze.Trim().Split(' ');

            // check if the line refers to a label
            if (tokens[0].EndsWith(':'))
            {
                if (tokens.Length == 1)
                {
                    // make a dummy instruction so the labels can work properly
                    instruction_to_add.opcode = Opcodes.LABEL;
                    instruction_to_add.bit_mode = Size.NoN;

                    Labels.Add(new() { name = tokens[0].TrimEnd(':'), line = line });
                    Instructions.Add(instruction_to_add);

                    continue;
                }
                else
                {
                    AnalyzerError(line, instruction_to_analyze);
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
                    AnalyzerError(line, instruction_to_analyze);
                    return;
                }

                instruction_to_add = AssignLabelInstruction(instruction_to_add, label_names, tokens[1], line);
            }
            else if (Instruction_Groups.no_operands.Contains(instruction_to_add.opcode))
            {
                // check if the instruction is valid
                if (tokens.Length != 1)
                {
                    AnalyzerError(line, instruction_to_analyze);
                    return;
                }

                instruction_to_add = AssignNoOperandInstruction(instruction_to_add, tokens, line);
            }
            else if (Instruction_Groups.one_operands.Contains(instruction_to_add.opcode))
            {
                if (tokens.Length != 2 && tokens.Length != 3)
                {
                    AnalyzerError(line, instruction_to_analyze);
                    return;
                }

                instruction_to_add = AssignOneOperandInstruction(instruction_to_add, tokens, offset, line);
            }
            else if (Instruction_Groups.two_operands.Contains(instruction_to_add.opcode))
            {
                if (tokens.Length != 3 && tokens.Length != 4)
                {
                    AnalyzerError(line, instruction_to_analyze);
                    return;
                }

                instruction_to_add = AssignTwoOperandInstruction(instruction_to_add, tokens, offset, line);
            }
            else
            {
                AnalyzerError(line, instruction_to_analyze);
                return;
            }

            Instructions.Add(instruction_to_add);
        }

        this.Successful = true;
    }
    
    private int AnalyzeStaticData(string StaticData_to_analyze, int offset, int line)
    {
        StaticData StaticData_to_add = new();

        if (string.IsNullOrWhiteSpace(StaticData_to_analyze))
            return offset;

        string StaticData_name = StaticData_to_analyze.Split(':')[0].Trim();
        string[] StaticData_tokens = StaticData_to_analyze.Split(StaticData_name + ":");
        ulong StaticData_value = 0;

        for (int i = 0; i < StaticData_tokens.Length; i++)
            StaticData_tokens[i] = StaticData_tokens[i].Trim();

        // the name of the static data
        StaticData_tokens[0] = StaticData_name.TrimEnd(':');

        if (RegisterVerifier.IsRegister(StaticData_tokens[0]))
        {
            AnalyzerError(line, StaticData_to_analyze);
            return offset;
        }

        // check if the static data is a number or a character / list of characters (aka string)
        if (StaticData_tokens[1].Split(' ').Length == 2 && ulong.TryParse(StaticData_tokens[1].Split(' ')[1], out StaticData_value))
        {
            StaticData_to_add.name = StaticData_name;
            StaticData_to_add.value = StaticData_value;
            StaticData_to_add.memory_location = offset;

            switch (StaticData_tokens[1].Split(' ')[0].ToUpper())
            {
                case "DB":
                    StaticData_to_add.size_in_bits = Size._8_BIT;
                    offset++;
                    break;

                case "DW":
                    StaticData_to_add.size_in_bits = Size._16_BIT;
                    offset += 2;
                    break;

                case "DD":
                    StaticData_to_add.size_in_bits = Size._32_BIT;
                    offset += 4;
                    break;

                case "DQ":
                    StaticData_to_add.size_in_bits = Size._64_BIT;
                    offset += 8;
                    break;

                default:
                    AnalyzerError(line, StaticData_to_analyze);
                    break;
            }
        }
        else
        {
            // check if the user made a mistake
            if (StaticData_tokens[1].Split(' ')[0].Trim().ToUpper() != "DB")
            {
                AnalyzerError(line, StaticData_to_analyze);
                return offset;
            }

            StaticData_to_add.name = StaticData_name;
            StaticData_to_add.memory_location = offset;
            StaticData_to_add.is_string_array = true;

            StaticData_tokens = StaticData_tokens[1].Split(',');
            StaticData_tokens[0] = StaticData_tokens[0].Split(' ')[1];

            for (int i = 0; i < StaticData_tokens.Length; i++)
            {
                StaticData_tokens[i] = StaticData_tokens[i].Trim();

                if ((StaticData_tokens[i].StartsWith('\'') == false || StaticData_tokens[i].EndsWith('\'') == false || StaticData_tokens[i].Length != 3)
                && ulong.TryParse(StaticData_tokens[i], out StaticData_value) == false)
                {
                    AnalyzerError(line, StaticData_to_analyze);
                    return offset;
                }

                if (StaticData_tokens[i].StartsWith('\''))
                    StaticData_to_add.characters.Add(StaticData_tokens[i].Trim('\'').ToCharArray()[0]);
                else
                    StaticData_to_add.characters.Add((char)StaticData_value);

                offset++;
            }
        }

        StaticData.Add(StaticData_to_add);

        return offset;
    }

    private Instruction AssignLabelInstruction(Instruction instruction, string[] label_names, string label, int line)
    {
        if (label_names.Where(label_name => label_name == label).Any() == false)
        {
            AnalyzerError(line, label);
            return instruction;
        }

        instruction.variant = GetVariant(instruction.opcode, new string[] { "lol", label });
        instruction.bit_mode = Size.NoN;
        instruction.destination_memory_type = MemoryType.ADDRESS;
        instruction.destination_memory_name = label;

        return instruction;
    }

    private Instruction AssignNoOperandInstruction(Instruction instruction, string[] tokens, int line)
    {
        instruction.variant = GetVariant(instruction.opcode, tokens);

        if (instruction.variant == Variants.LABEL)
        {
            instruction.destination_memory_type = MemoryType.LABEL;
            instruction.destination_memory_name = tokens[1];
            instruction.bit_mode = AssignBitMode(instruction, "");
        }
        else
        {
            instruction.bit_mode = Size.NoN;
        }

        return instruction;
    }

    private Instruction AssignOneOperandInstruction(Instruction instruction, string[] tokens, int memory_offset, int line)
    {
        instruction.variant = GetVariant(instruction.opcode, tokens);
        (ulong, bool) value;

        switch (instruction.variant)
        {
            case Variants.SINGLE_REGISTER:
                instruction.destination_register = GetRegister(tokens[tokens.Length - 1]);

                // check if the instruction has a keyword like byte (example: inc byte al) or not
                if (tokens.Length == 2)
                {
                    instruction.bit_mode = AssignBitMode(instruction, tokens[tokens.Length - 1]);
                }
                else
                {
                    instruction.bit_mode = GetUserAssignedBitmode(tokens[1]);

                    if (instruction.bit_mode == Size.NoN)
                        AnalyzerError(line, string.Join(" ", tokens));
                }

                break;

            case Variants.SINGLE_VALUE:
                value = GetValue(tokens[tokens.Length - 1]);

                if (value.Item2 == false)
                    AnalyzerError(line, string.Join(" ", tokens));

                instruction.destination_memory_name = value.Item1.ToString();
                instruction.destination_memory_type = MemoryType.VALUE;

                break;

            case Variants.SINGLE_ADDRESS_VALUE:
                // check for keywords like byte word etc
                if (tokens.Length == 3)
                    instruction.bit_mode = GetUserAssignedBitmode(tokens[1]);

                instruction.destination_memory_type = MemoryType.ADDRESS;

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
            case Variants.DESTINATION_REGISTER_SOURCE_REGISTER:
                instruction.destination_register = GetRegister(tokens[1]);
                instruction.source_register = GetRegister(tokens[tokens.Length - 1]);

                // assign automatically the bitmode with the source register or allow the user to modify the bitmode
                if (tokens.Length == 3)
                    instruction.bit_mode = AssignBitMode(instruction, tokens[tokens.Length - 1]);
                else
                    instruction.bit_mode = GetUserAssignedBitmode(tokens[tokens.Length - 2]);

                break;

            case Variants.DESTINATION_REGISTER_SOURCE_VALUE:
                value = GetValue(tokens[tokens.Length - 1]);

                instruction.destination_register = GetRegister(tokens[1]);
                instruction.source_memory_type = MemoryType.VALUE;

                // check if the value is valid and assign it, if not then throw an error
                if (value.Item2)
                    instruction.source_memory_name = value.Item1.ToString();
                else
                    AnalyzerError(line, string.Join(" ", tokens));

                // assign the bitmode automatically or let the use assign the bit mode
                if (tokens.Length == 3)
                    instruction.bit_mode = AssignBitMode(instruction, tokens[1]);
                else
                    instruction.bit_mode = GetUserAssignedBitmode(tokens[tokens.Length - 2]);

                // check if the destination register is a 8bit high or low one
                if (tokens[1].ToUpper().EndsWith('H'))
                    instruction.destination_high_or_low = true;

                break;

            case Variants.DESTINATION_REGISTER_SOURCE_ADDRESS:
                instruction.destination_register = GetRegister(tokens[1]);
                instruction.source_register = GetRegister(tokens[tokens.Length - 1].Trim('[', ']'));
                instruction.source_memory_type = MemoryType.ADDRESS;
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

            case Variants.DESTINATION_ADDRESS_SOURCE_REGISTER:
                instruction.destination_register = GetRegister(tokens[1]);
                instruction.source_register = GetRegister(tokens[tokens.Length - 1]);
                instruction.destination_memory_type = MemoryType.ADDRESS;
                instruction.destination_memory_name = tokens[1].Trim('[', ']');
                instruction.bit_mode = AssignBitMode(instruction, tokens[1].Trim('[', ']'));
                instruction = AssignRegisterPointers(instruction, tokens[1], "");

                if (tokens[1].StartsWith('[') && tokens[1].EndsWith(']'))
                    instruction.destination_pointer = true;

                // check if the destination register is a 8bit high or low one
                if (tokens[tokens.Length - 1].ToUpper().EndsWith('H'))
                    instruction.destination_high_or_low = true;

                break;

            case Variants.DESTINATION_ADDRESS_SOURCE_VALUE:
                value = GetValue(tokens[tokens.Length - 1]);

                instruction.destination_memory_type = MemoryType.ADDRESS;
                instruction.destination_pointer = true;
                instruction.destination_register = GetRegister(tokens[1].Trim('[', ']'));

                // check if the value is valid and assign it, if not then throw an error
                if (value.Item2)
                    instruction.source_memory_name = value.Item1.ToString();
                else
                    AnalyzerError(line, string.Join(" ", tokens));

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

    private Opcodes GetInstruction(string opcode_to_analyze)
    {
        if (Enum.TryParse(opcode_to_analyze.ToUpper(), out Opcodes toReturn))
            return toReturn;

        return Opcodes.NoN;
    }

    private Variants GetVariant(Opcodes instruction, string[] tokens)
    {
        string last_token = tokens[tokens.Length - 1];
        string[] bit_mode_keywords = new string[]
        {
            "BYTE",
            "WORD",
            "DOUBLE",
            "QUAD"
        };

        // remove all commas=
        for (int i = 0; i < tokens.Length; i++)
            if (tokens[i].Contains(','))
                tokens[i] = tokens[i].Remove(tokens[i].IndexOf(','));

        // check if the instruction has no operands
        if (Instruction_Groups.no_operands.Contains(instruction) && tokens.Length == 1)
            return Variants.SINGLE;

        // check if the instruction refers to a label
        if (Instruction_Groups.one_label.Contains(instruction) && tokens.Length == 2)
            return Variants.LABEL;

        // check if the Instruction_Data has one operand
        if (Instruction_Groups.one_operands.Contains(instruction) && (tokens.Length == 2 || tokens.Length == 3 && bit_mode_keywords.Contains(tokens[1].ToUpper())))
        {
            // check if it points to a value in memory
            if (last_token.StartsWith('[') && last_token.EndsWith(']'))
                if (StringHandler.GetCharOccurrences(last_token, '[') == 1 && StringHandler.GetCharOccurrences(last_token, ']') == 1)
                    return Variants.SINGLE_ADDRESS_VALUE;

            // check if it refers to a register
            if (GetRegister(last_token.ToUpper()) != Registers.NoN)
                return Variants.SINGLE_REGISTER;

            // check if it refers to an int
            if (int.TryParse(last_token, out _))
                return Variants.SINGLE_VALUE;

            // check if it refers to binary
            if (last_token.ToUpper().EndsWith('B'))
            {
                bool acceptable_binary_number = true;

                for (int i = 0; i < last_token.Length && acceptable_binary_number; i++)
                    if (last_token[i] != '0' || last_token[i] != '1')
                        acceptable_binary_number = false;

                if (acceptable_binary_number)
                    return Variants.SINGLE_VALUE;
            }

            // check if it refers to hex
            if (last_token.ToUpper().EndsWith('H'))
                if (HexadecimalVerifier.IsBase(last_token.ToUpper().TrimEnd('H')))
                    return Variants.SINGLE_VALUE;
        }

        // check if the instruction has two operands
        if (Instruction_Groups.two_operands.Contains(instruction) && (tokens.Length == 3 || tokens.Length == 4 && bit_mode_keywords.Contains(tokens[2].ToUpper())))
        {
            // check if the destination and source are both registers
            if (GetRegister(tokens[1]) != Registers.NoN)
                if (GetRegister(last_token) != Registers.NoN)
                    return Variants.DESTINATION_REGISTER_SOURCE_REGISTER;

            // check if the destination is a register and source a number
            if (GetRegister(tokens[1]) != Registers.NoN)
            {
                // check if it refers to an int
                if (int.TryParse(last_token, out _))
                    return Variants.DESTINATION_REGISTER_SOURCE_VALUE;

                // check if it refers to binary
                if (last_token.ToUpper().EndsWith('B'))
                    if (BinaryVerifier.IsBase(last_token.ToUpper().TrimEnd('B')))
                        return Variants.DESTINATION_REGISTER_SOURCE_VALUE;

                // check if it refers to hex
                if (last_token.ToUpper().EndsWith('H'))
                    if (HexadecimalVerifier.IsBase(last_token.ToUpper().TrimEnd('H')))
                        return Variants.DESTINATION_REGISTER_SOURCE_VALUE;

                // Check if it refers to an ASCII character
                if (last_token.EndsWith('\'') && last_token.StartsWith('\''))
                    if (last_token.Length == 3)
                        return Variants.DESTINATION_REGISTER_SOURCE_VALUE;

                // check if the destination is a register and the source is a value from memory
                if (GetRegister(tokens[1].ToUpper()) != Registers.NoN)
                    if (last_token.StartsWith('[') && last_token.EndsWith(']'))
                        if (StringHandler.GetCharOccurrences(last_token, '[') == 1 && StringHandler.GetCharOccurrences(last_token, ']') == 1)
                            return Variants.DESTINATION_REGISTER_SOURCE_ADDRESS;

                // check if the destination is a register and the source is a location in memory
                if (GetRegister(tokens[1].ToUpper()) != Registers.NoN)
                    for (int i = 0; i < StaticData.Count; i++)
                        if (StaticData[i].name == last_token)
                            return Variants.DESTINATION_REGISTER_SOURCE_ADDRESS;

                // check if the destination is a location in memory and the source is a register
                if (tokens[1].StartsWith('[') && tokens[1].EndsWith(']'))
                    if (StringHandler.GetCharOccurrences(tokens[1], '[') == 1 && StringHandler.GetCharOccurrences(tokens[1], ']') == 1)
                        if (GetRegister(tokens[1].ToUpper()) != Registers.NoN)
                            return Variants.DESTINATION_ADDRESS_SOURCE_REGISTER;
            }

            // check if the destination register is a pointer
            if (GetRegister(tokens[1].Trim('[', ']')) != Registers.NoN)
            {
                // check if it refers to an int
                if (int.TryParse(last_token, out _))
                    return Variants.DESTINATION_ADDRESS_SOURCE_VALUE;

                // check if it refers to binary
                if (last_token.ToUpper().EndsWith('B'))
                    if (BinaryVerifier.IsBase(last_token.ToUpper().TrimEnd('B')))
                        return Variants.DESTINATION_ADDRESS_SOURCE_VALUE;

                // check if it refers to hex
                if (last_token.ToUpper().EndsWith('H'))
                    if (HexadecimalVerifier.IsBase(last_token.ToUpper().TrimEnd('H')))
                        return Variants.DESTINATION_ADDRESS_SOURCE_VALUE;

                // Check if it refers to an ASCII character
                if (last_token.EndsWith('\'') && last_token.StartsWith('\''))
                    if (last_token.Length == 3)
                        return Variants.DESTINATION_ADDRESS_SOURCE_VALUE;

                // check if the destination is a register and the source is a value from memory
                if (GetRegister(last_token.ToUpper()) != Registers.NoN)
                    if (tokens[1].StartsWith('[') && tokens[1].EndsWith(']'))
                        if (StringHandler.GetCharOccurrences(tokens[1], '[') == 1 && StringHandler.GetCharOccurrences(tokens[1], ']') == 1)
                            return Variants.DESTINATION_ADDRESS_SOURCE_REGISTER;
            }
        }

        return Variants.NoN;
    }

    private Registers GetRegister(string register_name)
    {
        Registers[] register_type = new[] 
        {
            Registers.RAX,
            Registers.RBX,
            Registers.RCX,
            Registers.RDX,
            Registers.RSI,
            Registers.RDI,
            Registers.RSP,
            Registers.RBP,
            Registers.RIP,
            Registers.R8,
            Registers.R9,
            Registers.R10,
            Registers.R11,
            Registers.R12,
            Registers.R13,
            Registers.R14,
            Registers.R15
        };

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

        for (int i = 0; i < register_lookup.Length; i++)
            foreach (string register_title in register_lookup[i])
                if (register_title == register_name.ToUpper())
                    return register_type[i];

        return Registers.NoN;
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

    private Size AssignBitMode(Instruction instruction, string register_token)
    {
        if (instruction.destination_register == Registers.NoN && instruction.source_register == Registers.NoN || register_token == "")
            return Size.NoN;

        if (RegisterVerifier.Is64BitRegister(register_token))
            return Size._64_BIT;
        else if (RegisterVerifier.Is32BitRegister(register_token))
            return Size._32_BIT;
        else if (RegisterVerifier.Is16BitRegister(register_token))
            return Size._16_BIT;
        else
            foreach (StaticData data in StaticData)
                if (register_token == data.name)
                    return data.size_in_bits;

        return Size._8_BIT;
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
            if (Enum.TryParse(destination_register_token, out Registers destination_register))
                instruction.destination_register = destination_register;

        if (source_register_pointer)
            if (Enum.TryParse(source_register_token, out Registers source_register))
                instruction.source_register = source_register;

        return instruction;
    }

    private void AnalyzerError(int error_line, string line)
    {
        if (error_line == -1)
        {
            this.error_line = error_line;
            this.ErrorMessage = "No section.text was found";
            return;
        }

        this.ErrorMessage = line;
        this.error_line = error_line;
    }

    private (ulong, bool) GetValue(string token_to_analyze)
    {
        ulong value;
        bool analyzed_Successfuly = ulong.TryParse(token_to_analyze, out value);

        if (token_to_analyze.ToUpper().EndsWith('H'))
        {
            value = HexadecimalConverter.ConvertBaseToUlong(token_to_analyze);
            analyzed_Successfuly = true;
        }

        if (token_to_analyze.ToUpper().EndsWith('B'))
        {
            value = BinaryConverter.ConvertBaseToUlong(token_to_analyze);
            analyzed_Successfuly = true;
        }

        if (token_to_analyze.Length == 3 && token_to_analyze.StartsWith('\'') && token_to_analyze.EndsWith('\''))
        {
            value = token_to_analyze[1];
            analyzed_Successfuly = true;
        }

        return (value, analyzed_Successfuly);
    }

    private Size GetUserAssignedBitmode(string token_to_analyze)
    {
        switch (token_to_analyze.ToUpper())
        {
            case "BYTE": return Size._8_BIT;
            case "WORD": return Size._16_BIT;
            case "DOUBLE": return Size._32_BIT;
            case "QUAD": return Size._64_BIT;
            default: return Size.NoN;
        }
    }

    private int error_line;
    public int ErrorLine 
    { 
        get
        {
            if (this.Successful)
                return -1;

            return this.error_line;
        }

        private set => this.error_line = value;
    }

    private string error_message = string.Empty;
    public string ErrorMessage
    {
        get
        {
            if (this.Successful)
                return string.Empty;

            return this.error_message;
        }

        private set => this.error_message = value;
    }

    public List<Instruction> Instructions = new();
    public List<StaticData> StaticData = new();
    public List<Label> Labels = new();
    public bool Successful;
}