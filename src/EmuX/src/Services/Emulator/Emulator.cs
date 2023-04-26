using EmuX.src.Enums;
using EmuX.src.Models;

namespace EmuX.src.Services.Emulator
{
    internal class Emulator
    {
        public void PrepareEmulator(List<Instruction> instructions, List<StaticData> static_data, List<Models.Label> labels)
        {
            this.instructions = instructions;
            this.static_data = static_data;
            this.labels = labels;

            this.current_instruction_index = 0;
            this.error = false;
            this.exit_found = false;

            InitStaticData();
        }

        public void SetInstructions(List<Instruction> instructions)
        {
            this.instructions = instructions;
        }

        public void SetVirtualSystem(VirtualSystem virtual_system)
        {
            this.virtual_system = virtual_system;
        }

        private void InitStaticData()
        {
            foreach (StaticData to_init in this.static_data)
            {
                switch (to_init.size_in_bits)
                {
                    case SIZE.Size_ENUM._8_BIT:
                        if (to_init.is_string_array)
                            for (int char_index = 0; char_index < to_init.characters.Count; char_index++)
                                this.virtual_system.SetByteMemory(to_init.memory_location + char_index, (byte)to_init.characters[char_index]);
                        else
                            this.virtual_system.SetByteMemory(to_init.memory_location, (byte)to_init.value);

                        break;

                    case SIZE.Size_ENUM._16_BIT:
                        this.virtual_system.SetWordMemory(to_init.memory_location, (ushort)to_init.value);
                        break;

                    case SIZE.Size_ENUM._32_BIT:
                        this.virtual_system.SetDoubleMemory(to_init.memory_location, (uint)to_init.value);
                        break;

                    case SIZE.Size_ENUM._64_BIT:
                        this.virtual_system.SetQuadMemory(to_init.memory_location, to_init.value);
                        break;
                }
            }
        }

        public void NextInstruction()
        {
            if (this.current_instruction_index < this.instructions.Count)
                this.current_instruction_index++;
        }

        public void PreviousInstruction()
        {
            if (this.current_instruction_index != 0)
                this.current_instruction_index--;
        }

        public void ResetInterrupt()
        {
            this.interrupt.SetInterruptCode(Interrupt_Codes.Interrupt_Code.NoN);
        }

        public bool GetInterruptOccurance()
        {
            return this.interrupt.GetInterruptCode() != Interrupt_Codes.Interrupt_Code.NoN;
        }

        public Interrupt GetInterrupt()
        {
            return this.interrupt;
        }

        public int GetIndex()
        {
            return this.current_instruction_index;
        }

        public bool GetExit()
        {
            return this.exit_found;
        }

        public int GetInstructionCount()
        {
            return this.instructions.Count;
        }

        public bool HasInstructions()
        {
            return this.instructions.Count != 0;
        }

        public void Execute()
        {
            // make sure there are instructions to run in the first place
            if (HasInstructions() == false)
                return;

            Instruction_Actions actions = new Instruction_Actions();
            Instruction instruction_to_execute = this.instructions[this.current_instruction_index];

            if (instruction_to_execute.opcode == Opcodes.Opcodes_ENUM.LABEL)
                return;

            ulong destination_value = AnalyzeInstructionDestination(instruction_to_execute, this.virtual_system);
            ulong source_value = AnalyzeInstructionSource(instruction_to_execute, this.virtual_system);
            string memory_destination = instruction_to_execute.destination_memory_name;
            int to_return_to = 0;
            int destination_memory_index = GetMemoryIndex(instruction_to_execute, this.labels, instruction_to_execute.destination_memory_name);
            int source_memory_index = GetMemoryIndex(instruction_to_execute, this.labels, instruction_to_execute.source_memory_name);
            int index_to_jump_to = 0;

            if (this.error)
                return;

            switch (instruction_to_execute.opcode)
            {
                case Opcodes.Opcodes_ENUM.AAA:
                    this.virtual_system.SetRegisterWord(Registers.Registers_ENUM.RAX, actions.AAA((ushort)destination_value, this.virtual_system.EFLAGS).Item1);
                    this.virtual_system.EFLAGS = actions.AAA((ushort)destination_value, this.virtual_system.EFLAGS).Item2;
                    break;

                case Opcodes.Opcodes_ENUM.AAD:
                    this.virtual_system.SetRegisterWord(Registers.Registers_ENUM.RAX, actions.AAD((ushort)destination_value));
                    break;

                case Opcodes.Opcodes_ENUM.AAM:
                    this.virtual_system.SetRegisterWord(Registers.Registers_ENUM.RAX, actions.AAM((ushort)destination_value));
                    break;

                case Opcodes.Opcodes_ENUM.AAS:
                    this.virtual_system.SetVirtualSystem(actions.AAS(this.virtual_system));
                    break;

                case Opcodes.Opcodes_ENUM.ADC:
                    SetValue(instruction_to_execute, destination_memory_index, actions.ADC(destination_value, source_value, this.virtual_system.EFLAGS));
                    break;

                case Opcodes.Opcodes_ENUM.ADD:
                    SetValue(instruction_to_execute, destination_memory_index, actions.ADD(destination_value, source_value));
                    break;

                case Opcodes.Opcodes_ENUM.AND:
                    SetValue(instruction_to_execute, destination_memory_index, actions.AND(destination_value, source_value));
                    break;

                case Opcodes.Opcodes_ENUM.CALL:
                    (bool, int) to_go_to = actions.CALL(labels, instruction_to_execute.destination_memory_name);

                    if (to_go_to.Item1)
                    {
                        this.virtual_system.PushCall(this.current_instruction_index);
                        this.current_instruction_index = to_go_to.Item2;
                        break;
                    }

                    error = true;
                    break;

                case Opcodes.Opcodes_ENUM.RET:
                    to_return_to = this.virtual_system.PopCall();

                    if (to_return_to == -1)
                    {
                        this.error = true;
                        break;
                    }

                    this.current_instruction_index = to_return_to;
                    break;

                case Opcodes.Opcodes_ENUM.CBW:
                    this.virtual_system.SetRegisterByte(Registers.Registers_ENUM.RAX, actions.CBW(this.virtual_system.GetRegisterByte(Registers.Registers_ENUM.RAX, false)), true);
                    break;

                case Opcodes.Opcodes_ENUM.CLC:
                    this.virtual_system.EFLAGS = actions.CLC(this.virtual_system.EFLAGS);
                    break;

                case Opcodes.Opcodes_ENUM.CLD:
                    this.virtual_system.EFLAGS = actions.CLD(this.virtual_system.EFLAGS);
                    break;

                case Opcodes.Opcodes_ENUM.CLI:
                    this.virtual_system.EFLAGS = actions.CLI(this.virtual_system.EFLAGS);
                    break;

                case Opcodes.Opcodes_ENUM.CMC:
                    this.virtual_system.EFLAGS = actions.CMC(this.virtual_system.EFLAGS);
                    break;

                case Opcodes.Opcodes_ENUM.CMP:
                    this.virtual_system.EFLAGS = actions.CMP(destination_value, source_value, this.virtual_system.EFLAGS);
                    break;

                case Opcodes.Opcodes_ENUM.CWD:
                    this.virtual_system.SetRegisterWord(Registers.Registers_ENUM.RDX, actions.CWD(this.virtual_system.GetRegisterWord(Registers.Registers_ENUM.RAX)));
                    break;

                case Opcodes.Opcodes_ENUM.DAA:
                    this.virtual_system.SetRegisterByte(Registers.Registers_ENUM.RAX, actions.DAA(this.virtual_system.GetRegisterByte(Registers.Registers_ENUM.RAX, false), this.virtual_system.EFLAGS), false);
                    break;

                case Opcodes.Opcodes_ENUM.DAS:
                    this.virtual_system.SetRegisterByte(Registers.Registers_ENUM.RAX, actions.DAS(this.virtual_system.GetRegisterByte(Registers.Registers_ENUM.RAX, false), this.virtual_system.EFLAGS), false);
                    break;

                case Opcodes.Opcodes_ENUM.DEC:
                    SetValue(instruction_to_execute, destination_memory_index, actions.DEC(destination_value, instruction_to_execute.bit_mode));
                    break;

                case Opcodes.Opcodes_ENUM.DIV:
                    this.virtual_system.SetVirtualSystem(actions.DIV(this.virtual_system, instruction_to_execute.bit_mode, destination_value));
                    break;

                case Opcodes.Opcodes_ENUM.HLT:
                    this.exit_found = true;
                    break;

                case Opcodes.Opcodes_ENUM.INC:
                    SetValue(instruction_to_execute, destination_memory_index, actions.INC(destination_value));
                    break;

                case Opcodes.Opcodes_ENUM.INT:
                    this.interrupt.SetInterruptCode(destination_value);

                    if (this.interrupt.GetInterruptCode() == Interrupt_Codes.Interrupt_Code.NoN)
                        this.error = true;

                    break;

                case Opcodes.Opcodes_ENUM.JA:
                    index_to_jump_to = actions.JA(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.CF), GetFlag(EFLAGS.EFLAGS_ENUM.ZF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JAE:
                    index_to_jump_to = actions.JAE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.CF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JB:
                    index_to_jump_to = actions.JB(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.CF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JBE:
                    index_to_jump_to = actions.JBE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.CF), GetFlag(EFLAGS.EFLAGS_ENUM.ZF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JC:
                    index_to_jump_to = actions.JC(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.CF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JE:
                    index_to_jump_to = actions.JE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.ZF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JG:
                    index_to_jump_to = actions.JG(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.ZF), GetFlag(EFLAGS.EFLAGS_ENUM.SF), GetFlag(EFLAGS.EFLAGS_ENUM.OF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JGE:
                    index_to_jump_to = actions.JGE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.SF), GetFlag(EFLAGS.EFLAGS_ENUM.OF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JL:
                    index_to_jump_to = actions.JL(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.SF), GetFlag(EFLAGS.EFLAGS_ENUM.OF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JNA:
                    index_to_jump_to = actions.JNA(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.CF), GetFlag(EFLAGS.EFLAGS_ENUM.ZF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JNAE:
                    index_to_jump_to = actions.JNAE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.CF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JNB:
                    index_to_jump_to = actions.JNB(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.CF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JNBE:
                    index_to_jump_to = actions.JNBE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.CF), GetFlag(EFLAGS.EFLAGS_ENUM.ZF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JNC:
                    index_to_jump_to = actions.JNC(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.CF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JNE:
                    index_to_jump_to = actions.JNE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.ZF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JNG:
                    index_to_jump_to = actions.JNG(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.ZF), GetFlag(EFLAGS.EFLAGS_ENUM.SF), GetFlag(EFLAGS.EFLAGS_ENUM.OF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JNGE:
                    index_to_jump_to = actions.JNGE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.SF), GetFlag(EFLAGS.EFLAGS_ENUM.OF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JNL:
                    index_to_jump_to = actions.JNL(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.SF), GetFlag(EFLAGS.EFLAGS_ENUM.OF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JNLE:
                    index_to_jump_to = actions.JNLE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.ZF), GetFlag(EFLAGS.EFLAGS_ENUM.SF), GetFlag(EFLAGS.EFLAGS_ENUM.OF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JNO:
                    index_to_jump_to = actions.JNO(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.OF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JNP:
                    index_to_jump_to = actions.JNP(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.PF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JNS:
                    index_to_jump_to = actions.JNS(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.SF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JNZ:
                    index_to_jump_to = actions.JNZ(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.ZF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JO:
                    index_to_jump_to = actions.JO(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.OF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JP:
                    index_to_jump_to = actions.JP(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.PF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JPO:
                    index_to_jump_to = actions.JPO(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.PF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JPE:
                    index_to_jump_to = actions.JPE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.PF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JS:
                    index_to_jump_to = actions.JS(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.SF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JZ:
                    index_to_jump_to = actions.JZ(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.EFLAGS_ENUM.ZF));

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.JMP:
                    index_to_jump_to = actions.JMP(this.labels, instruction_to_execute.destination_memory_name);

                    if (index_to_jump_to != -1)
                        this.current_instruction_index = index_to_jump_to;

                    break;

                case Opcodes.Opcodes_ENUM.LAHF:
                    this.virtual_system.SetRegisterByte(Registers.Registers_ENUM.RAX, actions.LAHF(this.virtual_system.EFLAGS), true);
                    break;

                case Opcodes.Opcodes_ENUM.LEA:
                    SetValue(instruction_to_execute, destination_memory_index, (ulong)actions.LEA(this.static_data, instruction_to_execute.source_memory_name));
                    break;

                case Opcodes.Opcodes_ENUM.MOV:
                    SetValue(instruction_to_execute, destination_memory_index, actions.MOV(source_value));
                    break;

                case Opcodes.Opcodes_ENUM.MUL:
                    this.virtual_system.SetVirtualSystem(actions.MUL(this.virtual_system, destination_value, instruction_to_execute.bit_mode));
                    break;

                case Opcodes.Opcodes_ENUM.NEG:
                    SetValue(instruction_to_execute, destination_memory_index, actions.NEG(destination_value));
                    break;

                case Opcodes.Opcodes_ENUM.NOP:
                    actions.NOP();
                    break;

                case Opcodes.Opcodes_ENUM.NOT:
                    SetValue(instruction_to_execute, destination_memory_index, actions.NOT(destination_value));
                    break;

                case Opcodes.Opcodes_ENUM.OR:
                    SetValue(instruction_to_execute, destination_memory_index, actions.OR(destination_value, source_value));
                    break;

                case Opcodes.Opcodes_ENUM.POP:
                    this.virtual_system.SetVirtualSystem(actions.POP(this.virtual_system, instruction_to_execute, destination_memory_index));
                    break;

                case Opcodes.Opcodes_ENUM.POPF:
                    this.virtual_system.SetVirtualSystem(actions.POPF(this.virtual_system));
                    break;

                case Opcodes.Opcodes_ENUM.PUSH:
                    this.virtual_system.SetVirtualSystem(actions.PUSH(this.virtual_system, instruction_to_execute.bit_mode, destination_value));
                    break;

                case Opcodes.Opcodes_ENUM.PUSHF:
                    this.virtual_system.SetVirtualSystem(actions.PUSHF(this.virtual_system, this.virtual_system.EFLAGS));
                    break;

                case Opcodes.Opcodes_ENUM.RCL:
                    this.virtual_system.SetVirtualSystem(actions.RCL(this.virtual_system, instruction_to_execute, destination_memory_index, destination_value, (int)source_value));
                    break;

                case Opcodes.Opcodes_ENUM.RCR:
                    this.virtual_system.SetVirtualSystem(actions.RCR(this.virtual_system, instruction_to_execute, destination_memory_index, destination_value, (int)source_value));
                    break;

                case Opcodes.Opcodes_ENUM.ROL:
                    SetValue(instruction_to_execute, destination_memory_index, actions.ROL(instruction_to_execute.bit_mode, destination_value, (int)source_value));
                    break;

                case Opcodes.Opcodes_ENUM.ROR:
                    SetValue(instruction_to_execute, destination_memory_index, actions.ROR(instruction_to_execute.bit_mode, destination_value, (int)source_value));
                    break;

                case Opcodes.Opcodes_ENUM.SAHF:
                    this.virtual_system.SetRegisterByte(Registers.Registers_ENUM.RAX, actions.SAHF(this.virtual_system.EFLAGS, this.virtual_system.GetEFLAGSMasks()), true);
                    break;

                case Opcodes.Opcodes_ENUM.SAL:
                    SetValue(instruction_to_execute, destination_memory_index, actions.SAL(destination_value, (int)source_value));
                    break;

                case Opcodes.Opcodes_ENUM.SAR:
                    SetValue(instruction_to_execute, destination_memory_index, actions.SAR(destination_value, (int)source_value));
                    break;

                case Opcodes.Opcodes_ENUM.SBB:
                    SetValue(instruction_to_execute, destination_memory_index, actions.SBB(destination_value, source_value, GetFlag(EFLAGS.EFLAGS_ENUM.CF)));
                    break;

                case Opcodes.Opcodes_ENUM.SHL:
                    SetValue(instruction_to_execute, destination_memory_index, actions.SHL(destination_value, (int)source_value));
                    break;

                case Opcodes.Opcodes_ENUM.SHR:
                    SetValue(instruction_to_execute, destination_memory_index, actions.SHR(destination_value, (int)source_value));
                    break;

                case Opcodes.Opcodes_ENUM.STC:
                    this.virtual_system.EFLAGS = actions.STC(this.virtual_system.EFLAGS, this.virtual_system.GetEFLAGSMasks()[0]);
                    break;

                case Opcodes.Opcodes_ENUM.STD:
                    this.virtual_system.EFLAGS = actions.STD(this.virtual_system.EFLAGS, this.virtual_system.GetEFLAGSMasks()[6]);
                    break;

                case Opcodes.Opcodes_ENUM.STI:
                    this.virtual_system.EFLAGS = actions.STI(this.virtual_system.EFLAGS, this.virtual_system.GetEFLAGSMasks()[7]);
                    break;

                case Opcodes.Opcodes_ENUM.SUB:
                    SetValue(instruction_to_execute, destination_memory_index, actions.SUB(destination_value, source_value));
                    break;

                case Opcodes.Opcodes_ENUM.XOR:
                    SetValue(instruction_to_execute, destination_memory_index, actions.XOR(destination_value, source_value));
                    break;

                default:
                    break;
            }
        }

        public bool ErrorEncountered()
        {
            return this.error;
        }

        public VirtualSystem GetVirtualSystem()
        {
            return this.virtual_system;
        }

        private int GetMemoryIndex(Instruction instruction, List<Models.Label> labels, string label_name_to_find)
        {
            // check if the destination is a register pointer
            if (instruction.destination_pointer)
                return (int)this.virtual_system.GetRegisterQuad(instruction.destination_register);

            foreach (Models.Label label in this.labels)
                if (label.name == label_name_to_find)
                    return label.line;

            return -1;
        }

        private void SetValue(Instruction instruction, int memory_index, ulong value_to_set)
        {
            // check if the value needs to be saved in a register or memory location
            if (instruction.destination_memory_type != Memory_Type.Memory_Type_ENUM.ADDRESS)
            {
                switch (instruction.bit_mode)
                {
                    case SIZE.Size_ENUM._8_BIT:
                        if (instruction.source_pointer == false)
                            this.virtual_system.SetRegisterQuad(instruction.destination_register, value_to_set);
                        else
                            this.virtual_system.SetRegisterByte(instruction.destination_register, (byte)value_to_set, instruction.destination_high_or_low);
                        
                        break;

                    case SIZE.Size_ENUM._16_BIT:
                        this.virtual_system.SetRegisterWord(instruction.destination_register, (ushort)value_to_set);
                        break;

                    case SIZE.Size_ENUM._32_BIT:
                        this.virtual_system.SetRegisterDouble(instruction.destination_register, (uint)value_to_set);
                        break;

                    case SIZE.Size_ENUM._64_BIT:
                        this.virtual_system.SetRegisterQuad(instruction.destination_register, value_to_set);
                        break;
                }
            }
            else
            {
                switch (instruction.bit_mode)
                {
                    case SIZE.Size_ENUM._8_BIT:
                        this.virtual_system.SetByteMemory(memory_index, (byte)value_to_set);
                        break;

                    case SIZE.Size_ENUM._16_BIT:
                        this.virtual_system.SetWordMemory(memory_index, (ushort)value_to_set);
                        break;

                    case SIZE.Size_ENUM._32_BIT:
                        this.virtual_system.SetDoubleMemory(memory_index, (uint)value_to_set);
                        break;

                    case SIZE.Size_ENUM._64_BIT:
                        this.virtual_system.SetQuadMemory(memory_index, value_to_set);
                        break;
                }
            }
        }

        private ulong AnalyzeInstructionDestination(Instruction instruction, VirtualSystem virtual_system)
        {
            switch (instruction.variant)
            {
                case Variants.Variants_ENUM.SINGLE_REGISTER:
                    return this.virtual_system.GetRegisterQuad(instruction.destination_register);

                case Variants.Variants_ENUM.SINGLE_VALUE:
                    return ulong.Parse(instruction.destination_memory_name);

                case Variants.Variants_ENUM.SINGLE_ADDRESS_VALUE:
                    for (int i = 0; i < this.static_data.Count; i++)
                        if (this.static_data[i].name == instruction.destination_memory_name)
                            return this.static_data[i].value;

                    return 0;

                case Variants.Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER:
                    switch (instruction.bit_mode)
                    {
                        case SIZE.Size_ENUM._8_BIT:
                            return this.virtual_system.GetRegisterByte(instruction.destination_register, instruction.destination_high_or_low);

                        case SIZE.Size_ENUM._16_BIT:
                            return this.virtual_system.GetRegisterWord(instruction.destination_register);

                        case SIZE.Size_ENUM._32_BIT:
                            return this.virtual_system.GetRegisterDouble(instruction.destination_register);

                        case SIZE.Size_ENUM._64_BIT:
                            return this.virtual_system.GetRegisterQuad(instruction.destination_register);
                    }

                    return 0;

                case Variants.Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS:
                    return this.virtual_system.GetRegisterQuad(instruction.destination_register);

                case Variants.Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE:
                    return this.virtual_system.GetRegisterQuad(instruction.destination_register);

                case Variants.Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER:
                    if (instruction.destination_pointer)
                    {
                        switch (instruction.bit_mode)
                        {
                            case SIZE.Size_ENUM._8_BIT:
                                return this.virtual_system.GetRegisterByte(instruction.destination_register, instruction.destination_high_or_low);

                            case SIZE.Size_ENUM._16_BIT:
                                return this.virtual_system.GetRegisterWord(instruction.destination_register);

                            case SIZE.Size_ENUM._32_BIT:
                                return this.virtual_system.GetRegisterDouble(instruction.destination_register);

                            case SIZE.Size_ENUM._64_BIT:
                                return this.virtual_system.GetRegisterQuad(instruction.destination_register);
                        }
                    }

                    for (int i = 0; i < this.static_data.Count; i++)
                        if (this.static_data[i].name == instruction.destination_memory_name)
                            return this.static_data[i].value;

                    return 0;
            }

            return 0;
        }

        private ulong AnalyzeInstructionSource(Instruction instruction, VirtualSystem virtual_system)
        {
            if (instruction.variant == Variants.Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER || instruction.variant == Variants.Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER)
            {
                switch (instruction.bit_mode)
                {
                    case SIZE.Size_ENUM._8_BIT:
                        return this.virtual_system.GetRegisterByte(instruction.source_register, instruction.source_high_or_low);

                    case SIZE.Size_ENUM._16_BIT:
                        return this.virtual_system.GetRegisterWord(instruction.source_register);

                    case SIZE.Size_ENUM._32_BIT:
                        return this.virtual_system.GetRegisterDouble(instruction.source_register);

                    case SIZE.Size_ENUM._64_BIT:
                        return this.virtual_system.GetRegisterQuad(instruction.source_register);

                    default:
                        return 0;
                }
            }
            else if (instruction.variant == Variants.Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS)
            {
                int memory_index = -1;

                if (instruction.source_pointer && instruction.source_register != Registers.Registers_ENUM.NoN)
                {
                    memory_index = (int)this.virtual_system.GetRegisterDouble(instruction.source_register);
                }
                else
                {
                    foreach (StaticData to_analyze in this.static_data)
                        if (to_analyze.name == instruction.source_memory_name)
                            memory_index = to_analyze.memory_location;
                }

                if (instruction.source_pointer == false)
                    return (ulong)memory_index;

                if (memory_index == -1)
                {
                    this.error = true;
                    return 0;
                }

                switch (instruction.bit_mode)
                {
                    case SIZE.Size_ENUM._8_BIT:
                        return this.virtual_system.GetByteMemory(memory_index);

                    case SIZE.Size_ENUM._16_BIT:
                        return this.virtual_system.GetWordMemory(memory_index);

                    case SIZE.Size_ENUM._32_BIT:
                        return this.virtual_system.GetDoubleMemory(memory_index);

                    case SIZE.Size_ENUM._64_BIT:
                        return this.virtual_system.GetQuadMemory(memory_index);

                    default:
                        return 0;
                }
            }
            else if (instruction.variant == Variants.Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE)
            {
                return uint.Parse(instruction.source_memory_name);
            }
            else
            {
                if (instruction.source_memory_name.StartsWith('-'))
                {
                    this.error = true;
                    return 0;
                }
                else if (instruction.source_register == Registers.Registers_ENUM.NoN && instruction.source_memory_type == Memory_Type.Memory_Type_ENUM.NoN && instruction.source_pointer == false && instruction.destination_pointer == false)
                    return 0;

                return ulong.Parse(instruction.source_memory_name);
            }
        }

        private bool GetFlag(EFLAGS.EFLAGS_ENUM flag)
        {
            return (this.virtual_system.EFLAGS & ((int) flag)) == 1;
        }

        private List<Instruction> instructions = new List<Instruction>();
        private List<StaticData> static_data = new List<StaticData>();
        private List<Models.Label> labels = new List<Models.Label>();
        private VirtualSystem virtual_system = new VirtualSystem();
        private Interrupt interrupt = new Interrupt();
        private int current_instruction_index;
        private bool error;
        private bool exit_found;
    }
}
