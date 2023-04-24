using EmuX.src.Enums;
using EmuX.src.Models;

namespace EmuX.src.Services.Emulator
{
    internal class Emulator : Instruction_Data
    {
        /// <summary>
        /// Sets the instruction data to execute but does not execute it, use the <c>Execute()</c> function to do that
        /// </summary>
        public void SetInstructions(List<Instruction> instructions)
        {
            this.instructions = instructions;
            current_instruction_index = 0;
        }

        /// <summary>
        /// Sets the static data list
        /// </summary>
        public void SetStaticData(List<StaticData> static_data)
        {
            this.static_data = static_data;

            for (int i = 0; i < this.static_data.Count; i++)
            {
                switch (this.static_data[i].size_in_bits)
                {
                    case BitSize.SIZE._8_BIT:
                        if (static_data[i].is_string_array)
                            for (int char_index = 0; char_index < static_data[i].characters.Count; char_index++)
                                virtual_system.SetByteMemory(this.static_data[i].memory_location + char_index, (byte)static_data[i].characters[char_index]);
                        else
                            virtual_system.SetByteMemory(this.static_data[i].memory_location, (byte)this.static_data[i].value);

                        break;

                    case BitSize.SIZE._16_BIT:
                        virtual_system.SetWordMemory(this.static_data[i].memory_location, (ushort)this.static_data[i].value);
                        break;

                    case BitSize.SIZE._32_BIT:
                        virtual_system.SetDoubleMemory(this.static_data[i].memory_location, (uint)this.static_data[i].value);
                        break;

                    case BitSize.SIZE._64_BIT:
                        virtual_system.SetQuadMemory(this.static_data[i].memory_location, this.static_data[i].value);
                        break;
                }
            }
        }

        /// <summary>
        /// Sets the label data
        /// </summary>
        /// <param name="label_data">A list tuple of (string, int) for the label names and their lines</param>
        public void SetLabelData(List<(string, int)> label_data)
        {
            labels = label_data;
        }

        /// <summary>
        /// Increases the instruction index
        /// </summary>
        public void NextInstruction()
        {
            if (current_instruction_index < instructions.Count)
                current_instruction_index++;
        }

        /// <summary>
        /// Decreases the instruction index
        /// </summary>
        public void PreviousInstruction()
        {
            if (current_instruction_index != 0)
                current_instruction_index--;
        }

        public void ResetInterrupt()
        {
            interrupt.SetInterruptCode(Interrupt.Interrupt_Codes.NoN);
        }

        public bool GetInterruptOccurance()
        {
            if (interrupt.GetInterruptCode() != Interrupt.Interrupt_Codes.NoN)
                return true;

            return false;
        }

        public Interrupt GetInterrupt()
        {
            return interrupt;
        }

        /// <summary>
        /// Getter - Gets the instruction index
        /// </summary>
        public int GetIndex()
        {
            return current_instruction_index;
        }

        /// <summary>
        /// Getter - Gets the exit_found boolean
        /// </summary>
        public bool GetExit()
        {
            return exit_found;
        }

        /// <summary>
        /// Getter - Gets the count of the instructions list
        /// </summary>
        public int GetInstructionCount()
        {
            return instructions.Count;
        }

        /// <summary>
        /// Getter - Checks if there are any instructions to execute
        /// </summary>
        public bool HasInstructions()
        {
            return instructions.Count != 0;
        }

        /// <summary>
        /// Executes the instructions given earlier with the <c>SetData(instructions)</c> function
        /// </summary>
        public void Execute()
        {
            // make sure there are instructions to run in the first place
            if (instructions.Count == 0)
                return;

            Instruction_Actions actions = new();
            Instruction instruction_to_execute = instructions[current_instruction_index];

            if (instruction_to_execute.instruction == Instruction_ENUM.LABEL)
                return;

            ulong destination_value = AnalyzeInstructionDestination(instruction_to_execute, virtual_system);
            ulong source_value = AnalyzeInstructionSource(instruction_to_execute, virtual_system);
            string memory_destination = instruction_to_execute.destination_memory_name;
            int to_return_to = 0;
            int destination_memory_index = GetMemoryIndex(instruction_to_execute, labels, instruction_to_execute.destination_memory_name);
            int source_memory_index = GetMemoryIndex(instruction_to_execute, labels, instruction_to_execute.source_memory_name);
            int index_to_jump_to = 0;

            if (error)
                return;

            switch (instruction_to_execute.instruction)
            {
                case Instruction_ENUM.AAA:
                    virtual_system.SetRegisterWord(Registers_ENUM.RAX, actions.AAA((ushort)destination_value, virtual_system.EFLAGS).Item1);
                    virtual_system.EFLAGS = actions.AAA((ushort)destination_value, virtual_system.EFLAGS).Item2;
                    break;

                case Instruction_ENUM.AAD:
                    virtual_system.SetRegisterWord(Registers_ENUM.RAX, actions.AAD((ushort)destination_value));
                    break;

                case Instruction_ENUM.AAM:
                    virtual_system.SetRegisterWord(Registers_ENUM.RAX, actions.AAM((ushort)destination_value));
                    break;

                case Instruction_ENUM.AAS:
                    virtual_system.SetVirtualSystem(actions.AAS(virtual_system));
                    break;

                case Instruction_ENUM.ADC:
                    SetValue(instruction_to_execute, destination_memory_index, actions.ADC(destination_value, source_value, virtual_system.EFLAGS));
                    break;

                case Instruction_ENUM.ADD:
                    SetValue(instruction_to_execute, destination_memory_index, actions.ADD(destination_value, source_value));
                    break;

                case Instruction_ENUM.AND:
                    SetValue(instruction_to_execute, destination_memory_index, actions.AND(destination_value, source_value));
                    break;

                case Instruction_ENUM.CALL:
                    (bool, int) to_go_to = actions.CALL(labels, instruction_to_execute.destination_memory_name);

                    if (to_go_to.Item1)
                    {
                        virtual_system.PushCall(current_instruction_index);
                        current_instruction_index = to_go_to.Item2;
                        break;
                    }

                    error = true;
                    break;

                case Instruction_ENUM.RET:
                    to_return_to = virtual_system.PopCall();

                    if (to_return_to == -1)
                    {
                        error = true;
                        break;
                    }

                    current_instruction_index = to_return_to;
                    break;

                case Instruction_ENUM.CBW:
                    virtual_system.SetRegisterByte(Registers_ENUM.RAX, actions.CBW(virtual_system.GetRegisterByte(Registers_ENUM.RAX, false)), true);
                    break;

                case Instruction_ENUM.CLC:
                    virtual_system.EFLAGS = actions.CLC(virtual_system.EFLAGS);
                    break;

                case Instruction_ENUM.CLD:
                    virtual_system.EFLAGS = actions.CLD(virtual_system.EFLAGS);
                    break;

                case Instruction_ENUM.CLI:
                    virtual_system.EFLAGS = actions.CLI(virtual_system.EFLAGS);
                    break;

                case Instruction_ENUM.CMC:
                    virtual_system.EFLAGS = actions.CMC(virtual_system.EFLAGS);
                    break;

                case Instruction_ENUM.CMP:
                    virtual_system.EFLAGS = actions.CMP(destination_value, source_value, virtual_system.EFLAGS);
                    break;

                case Instruction_ENUM.CWD:
                    virtual_system.SetRegisterWord(Registers_ENUM.RDX, actions.CWD(virtual_system.GetRegisterWord(Registers_ENUM.RAX)));
                    break;

                case Instruction_ENUM.DAA:
                    virtual_system.SetRegisterByte(Registers_ENUM.RAX, actions.DAA(virtual_system.GetRegisterByte(Registers_ENUM.RAX, LOW), virtual_system.EFLAGS), LOW);
                    break;

                case Instruction_ENUM.DAS:
                    virtual_system.SetRegisterByte(Registers_ENUM.RAX, actions.DAS(virtual_system.GetRegisterByte(Registers_ENUM.RAX, LOW), virtual_system.EFLAGS), LOW);
                    break;

                case Instruction_ENUM.DEC:
                    SetValue(instruction_to_execute, destination_memory_index, actions.DEC(destination_value, instruction_to_execute.bit_mode));
                    break;

                case Instruction_ENUM.DIV:
                    virtual_system.SetVirtualSystem(actions.DIV(virtual_system, instruction_to_execute.bit_mode, destination_value));
                    break;

                case Instruction_ENUM.HLT:
                    exit_found = true;
                    break;

                case Instruction_ENUM.INC:
                    SetValue(instruction_to_execute, destination_memory_index, actions.INC(destination_value));
                    break;

                case Instruction_ENUM.INT:
                    interrupt.SetInterruptCode(destination_value);

                    if (interrupt.GetInterruptCode() == Interrupt.Interrupt_Codes.NoN)
                        error = true;

                    break;

                case Instruction_ENUM.JA:
                    index_to_jump_to = actions.JA(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[0]) != 0, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[3]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JAE:
                    index_to_jump_to = actions.JAE(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[0]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JB:
                    index_to_jump_to = actions.JB(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[0]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JBE:
                    index_to_jump_to = actions.JBE(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[0]) != 0, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[3]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JC:
                    index_to_jump_to = actions.JC(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[0]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JE:
                    index_to_jump_to = actions.JE(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[3]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JG:
                    index_to_jump_to = actions.JG(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[3]) != 0, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[4]) != 0, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[8]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JGE:
                    index_to_jump_to = actions.JGE(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[4]) != 0, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[8]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JL:
                    index_to_jump_to = actions.JL(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[4]) != 0, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[8]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JNA:
                    index_to_jump_to = actions.JNA(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[0]) != 0, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[3]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JNAE:
                    index_to_jump_to = actions.JNAE(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[0]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JNB:
                    index_to_jump_to = actions.JNB(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[0]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JNBE:
                    index_to_jump_to = actions.JNBE(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[0]) != 0, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[3]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JNC:
                    index_to_jump_to = actions.JNC(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[0]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JNE:
                    index_to_jump_to = actions.JNE(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[3]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JNG:
                    index_to_jump_to = actions.JNG(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[3]) != 0, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[4]) != 0, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[8]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JNGE:
                    index_to_jump_to = actions.JNGE(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[4]) != 0, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[8]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JNL:
                    index_to_jump_to = actions.JNL(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[4]) != 0, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[8]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JNLE:
                    index_to_jump_to = actions.JNLE(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[3]) != 0, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[4]) != 0, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[8]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JNO:
                    index_to_jump_to = actions.JNO(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[8]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JNP:
                    index_to_jump_to = actions.JNP(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[1]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JNS:
                    index_to_jump_to = actions.JNS(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[4]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JNZ:
                    index_to_jump_to = actions.JNZ(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[3]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JO:
                    index_to_jump_to = actions.JO(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[8]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JP:
                    index_to_jump_to = actions.JP(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[1]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JPO:
                    index_to_jump_to = actions.JPO(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[1]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JPE:
                    index_to_jump_to = actions.JPE(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[1]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JS:
                    index_to_jump_to = actions.JS(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[4]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JZ:
                    index_to_jump_to = actions.JZ(labels, instruction_to_execute.destination_memory_name, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[3]) != 0);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.JMP:
                    index_to_jump_to = actions.JMP(labels, instruction_to_execute.destination_memory_name);

                    if (index_to_jump_to != -1)
                        current_instruction_index = index_to_jump_to;

                    break;

                case Instruction_ENUM.LAHF:
                    virtual_system.SetRegisterByte(Registers_ENUM.RAX, actions.LAHF(virtual_system.EFLAGS), true);
                    break;

                case Instruction_ENUM.LEA:
                    SetValue(instruction_to_execute, destination_memory_index, (ulong)actions.LEA(static_data, instruction_to_execute.source_memory_name));
                    break;

                case Instruction_ENUM.MOV:
                    SetValue(instruction_to_execute, destination_memory_index, actions.MOV(source_value));
                    break;

                case Instruction_ENUM.MUL:
                    virtual_system.SetVirtualSystem(actions.MUL(virtual_system, destination_value, instruction_to_execute.bit_mode));
                    break;

                case Instruction_ENUM.NEG:
                    SetValue(instruction_to_execute, destination_memory_index, actions.NEG(destination_value));
                    break;

                case Instruction_ENUM.NOP:
                    actions.NOP();
                    break;

                case Instruction_ENUM.NOT:
                    SetValue(instruction_to_execute, destination_memory_index, actions.NOT(destination_value));
                    break;

                case Instruction_ENUM.OR:
                    SetValue(instruction_to_execute, destination_memory_index, actions.OR(destination_value, source_value));
                    break;

                case Instruction_ENUM.POP:
                    virtual_system.SetVirtualSystem(actions.POP(virtual_system, instruction_to_execute, destination_memory_index));
                    break;

                case Instruction_ENUM.POPF:
                    virtual_system.SetVirtualSystem(actions.POPF(virtual_system));
                    break;

                case Instruction_ENUM.PUSH:
                    virtual_system.SetVirtualSystem(actions.PUSH(virtual_system, instruction_to_execute.bit_mode, destination_value));
                    break;

                case Instruction_ENUM.PUSHF:
                    virtual_system.SetVirtualSystem(actions.PUSHF(virtual_system, virtual_system.EFLAGS));
                    break;

                case Instruction_ENUM.RCL:
                    virtual_system.SetVirtualSystem(actions.RCL(virtual_system, instruction_to_execute, destination_memory_index, destination_value, (int)source_value));
                    break;

                case Instruction_ENUM.RCR:
                    virtual_system.SetVirtualSystem(actions.RCR(virtual_system, instruction_to_execute, destination_memory_index, destination_value, (int)source_value));
                    break;

                case Instruction_ENUM.ROL:
                    SetValue(instruction_to_execute, destination_memory_index, actions.ROL(instruction_to_execute.bit_mode, destination_value, (int)source_value));
                    break;

                case Instruction_ENUM.ROR:
                    SetValue(instruction_to_execute, destination_memory_index, actions.ROR(instruction_to_execute.bit_mode, destination_value, (int)source_value));
                    break;

                case Instruction_ENUM.SAHF:
                    virtual_system.SetRegisterByte(Registers_ENUM.RAX, actions.SAHF(virtual_system.EFLAGS, virtual_system.GetEFLAGSMasks()), true);
                    break;

                case Instruction_ENUM.SAL:
                    SetValue(instruction_to_execute, destination_memory_index, actions.SAL(destination_value, (int)source_value));
                    break;

                case Instruction_ENUM.SAR:
                    SetValue(instruction_to_execute, destination_memory_index, actions.SAR(destination_value, (int)source_value));
                    break;

                case Instruction_ENUM.SBB:
                    SetValue(instruction_to_execute, destination_memory_index, actions.SBB(destination_value, source_value, (virtual_system.EFLAGS & virtual_system.GetEFLAGSMasks()[0]) != 0));
                    break;

                case Instruction_ENUM.SHL:
                    SetValue(instruction_to_execute, destination_memory_index, actions.SHL(destination_value, (int)source_value));
                    break;

                case Instruction_ENUM.SHR:
                    SetValue(instruction_to_execute, destination_memory_index, actions.SHR(destination_value, (int)source_value));
                    break;

                case Instruction_ENUM.STC:
                    virtual_system.EFLAGS = actions.STC(virtual_system.EFLAGS, virtual_system.GetEFLAGSMasks()[0]);
                    break;

                case Instruction_ENUM.STD:
                    virtual_system.EFLAGS = actions.STD(virtual_system.EFLAGS, virtual_system.GetEFLAGSMasks()[6]);
                    break;

                case Instruction_ENUM.STI:
                    virtual_system.EFLAGS = actions.STI(virtual_system.EFLAGS, virtual_system.GetEFLAGSMasks()[7]);
                    break;

                case Instruction_ENUM.SUB:
                    SetValue(instruction_to_execute, destination_memory_index, actions.SUB(destination_value, source_value));
                    break;

                case Instruction_ENUM.XOR:
                    SetValue(instruction_to_execute, destination_memory_index, actions.XOR(destination_value, source_value));
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Getter - Gets the error variable
        /// </summary>
        public bool ErrorEncountered()
        {
            return error;
        }

        /// <summary>
        /// Resets the error and exit_found boolean variable so that execution can return to normal
        /// </summary>
        public void Reset()
        {
            error = false;
            exit_found = false;
        }

        /// <summary>
        /// Getter - Gets the current state of the Virtual System
        /// </summary>
        public VirtualSystem GetVirtualSystem()
        {
            return virtual_system;
        }

        /// <summary>
        /// Setter
        /// </summary>
        public void SetVirtualSystem(VirtualSystem virtual_system)
        {
            this.virtual_system = virtual_system;
        }

        /// <summary>
        /// Finds the memory index of said label
        /// </summary>
        /// <returns>The memory index of the label, if the label is not found it returns -1</returns>
        private int GetMemoryIndex(Instruction instruction, List<(string, int)> labels, string label_name_to_find)
        {
            // check if the destination is a register pointer
            if (instruction.destination_pointer)
                return (int)virtual_system.GetRegisterQuad(instruction.destination_register);

            if (label_name_to_find != "")
                for (int i = 0; i < labels.Count; i++)
                    if (labels[i].Item1 == label_name_to_find)
                        return labels[i].Item2;

            return -1;
        }

        /// <summary>
        /// This is just a shortcut to set a value in a register or memory location so that extensive switch statements are avoided
        /// </summary>
        private void SetValue(Instruction instruction, int memory_index, ulong value_to_set)
        {
            // check if the value needs to be saved in a register or memory location
            if (instruction.destination_memory_type != Memory_Type_ENUM.ADDRESS)
            {
                switch (instruction.bit_mode)
                {
                    case Bit_Mode_ENUM._8_BIT:
                        virtual_system.SetRegisterByte(instruction.destination_register, (byte)value_to_set, instruction.destination_high_or_low);
                        break;

                    case Bit_Mode_ENUM._16_BIT:
                        virtual_system.SetRegisterWord(instruction.destination_register, (ushort)value_to_set);
                        break;

                    case Bit_Mode_ENUM._32_BIT:
                        virtual_system.SetRegisterDouble(instruction.destination_register, (uint)value_to_set);
                        break;

                    case Bit_Mode_ENUM._64_BIT:
                        virtual_system.SetRegisterQuad(instruction.destination_register, value_to_set);
                        break;
                }
            }
            else
            {
                switch (instruction.bit_mode)
                {
                    case Bit_Mode_ENUM._8_BIT:
                        virtual_system.SetByteMemory(memory_index, (byte)value_to_set);
                        break;

                    case Bit_Mode_ENUM._16_BIT:
                        virtual_system.SetWordMemory(memory_index, (ushort)value_to_set);
                        break;

                    case Bit_Mode_ENUM._32_BIT:
                        virtual_system.SetDoubleMemory(memory_index, (uint)value_to_set);
                        break;

                    case Bit_Mode_ENUM._64_BIT:
                        virtual_system.SetQuadMemory(memory_index, value_to_set);
                        break;
                }
            }
        }

        /// <summary>
        /// Analyzes the instruction variant and returns the destination value as a ulong
        /// </summary>
        /// <returns>An unsigned long value of its destination value</returns>
        private ulong AnalyzeInstructionDestination(Instruction instruction, VirtualSystem virtual_system)
        {
            switch (instruction.variant)
            {
                case Instruction_Variant_ENUM.SINGLE_REGISTER:
                    return this.virtual_system.GetRegisterQuad(instruction.destination_register);

                case Instruction_Variant_ENUM.SINGLE_VALUE:
                    return ulong.Parse(instruction.destination_memory_name);

                case Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE:
                    for (int i = 0; i < static_data.Count; i++)
                        if (static_data[i].name == instruction.destination_memory_name)
                            return static_data[i].value;

                    return 0;

                case Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER:
                    switch (instruction.bit_mode)
                    {
                        case Bit_Mode_ENUM._8_BIT:
                            return this.virtual_system.GetRegisterByte(instruction.destination_register, instruction.destination_high_or_low);

                        case Bit_Mode_ENUM._16_BIT:
                            return this.virtual_system.GetRegisterWord(instruction.destination_register);

                        case Bit_Mode_ENUM._32_BIT:
                            return this.virtual_system.GetRegisterDouble(instruction.destination_register);

                        case Bit_Mode_ENUM._64_BIT:
                            return this.virtual_system.GetRegisterQuad(instruction.destination_register);
                    }

                    return 0;

                case Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS:
                    return this.virtual_system.GetRegisterQuad(instruction.destination_register);

                case Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE:
                    return this.virtual_system.GetRegisterQuad(instruction.destination_register);

                case Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER:
                    if (instruction.destination_pointer)
                    {
                        switch (instruction.bit_mode)
                        {
                            case Bit_Mode_ENUM._8_BIT:
                                return this.virtual_system.GetRegisterByte(instruction.destination_register, instruction.destination_high_or_low);

                            case Bit_Mode_ENUM._16_BIT:
                                return this.virtual_system.GetRegisterWord(instruction.destination_register);

                            case Bit_Mode_ENUM._32_BIT:
                                return this.virtual_system.GetRegisterDouble(instruction.destination_register);

                            case Bit_Mode_ENUM._64_BIT:
                                return this.virtual_system.GetRegisterQuad(instruction.destination_register);
                        }
                    }

                    for (int i = 0; i < static_data.Count; i++)
                        if (static_data[i].name == instruction.destination_memory_name)
                            return static_data[i].value;

                    return 0;
            }

            return 0;
        }

        /// <summary>
        /// Analyzes the instruction and return the appropriate source value
        /// </summary>
        /// <returns>The ulong value of the source</returns>
        private ulong AnalyzeInstructionSource(Instruction instruction, VirtualSystem virtual_system)
        {
            if (instruction.variant == Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER || instruction.variant == Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER)
            {
                switch (instruction.bit_mode)
                {
                    case Bit_Mode_ENUM._8_BIT:
                        return this.virtual_system.GetRegisterByte(instruction.source_register, instruction.source_high_or_low);

                    case Bit_Mode_ENUM._16_BIT:
                        return this.virtual_system.GetRegisterWord(instruction.source_register);

                    case Bit_Mode_ENUM._32_BIT:
                        return this.virtual_system.GetRegisterDouble(instruction.source_register);

                    case Bit_Mode_ENUM._64_BIT:
                        return this.virtual_system.GetRegisterQuad(instruction.source_register);
                }
            }
            else if (instruction.variant == Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS)
            {
                int memory_index = -1;

                if (instruction.source_pointer && instruction.source_register != Registers_ENUM.NoN)
                {
                    memory_index = (int)this.virtual_system.GetRegisterDouble(instruction.source_register);
                }
                else
                {
                    for (int i = 0; i < static_data.Count; i++)
                        if (static_data[i].name == instruction.source_memory_name)
                            memory_index = static_data[i].memory_location;
                }

                if (instruction.source_pointer == false)
                    return (ulong)memory_index;

                if (memory_index == -1)
                {
                    error = true;
                    return 0;
                }

                switch (instruction.bit_mode)
                {
                    case Bit_Mode_ENUM._8_BIT:
                        return this.virtual_system.GetByteMemory(memory_index);

                    case Bit_Mode_ENUM._16_BIT:
                        return this.virtual_system.GetWordMemory(memory_index);

                    case Bit_Mode_ENUM._32_BIT:
                        return this.virtual_system.GetDoubleMemory(memory_index);

                    case Bit_Mode_ENUM._64_BIT:
                        return this.virtual_system.GetQuadMemory(memory_index);

                    default:
                        return 0;
                }
            }
            else if (instruction.variant == Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE)
            {
                return uint.Parse(instruction.source_memory_name);
            }
            else
            {
                if (instruction.source_memory_name.StartsWith('-'))
                {
                    error = true;
                    return 0;
                }

                if (instruction.source_register == Registers_ENUM.NoN && instruction.source_memory_type == Memory_Type_ENUM.NoN && instruction.source_pointer == false && instruction.destination_pointer == false)
                    return 0;

                return ulong.Parse(instruction.source_memory_name);
            }

            // if the static data label was not found then return 0 and set the error flag to true
            error = true;

            return 0;
        }

        /// <summary>
        /// Gets the flag from the int EFLAG register value
        /// </summary>
        /// <returns>The bool value for the flag result</returns>
        private bool GetFlag(uint EFLAGS, uint mask)
        {
            uint flag = EFLAGS & mask;

            if (flag == 1)
                return true;

            return false;
        }

        private List<Instruction> instructions = new();
        private List<StaticData> static_data = new();
        private List<(string, int)> labels = new();
        private VirtualSystem virtual_system = new();
        private Interrupt interrupt = new();
        private int current_instruction_index;
        private bool error;
        private bool exit_found;
    }
}
