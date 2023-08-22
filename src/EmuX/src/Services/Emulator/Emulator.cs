using EmuX.src.Enums.Instruction_Data;
using EmuX.src.Enums.VM;
using EmuX.src.Models;
using Label = EmuX.src.Models.Label;
using Size = EmuX.src.Enums.Size;

namespace EmuX.src.Services.Emulator;

public class Emulator
{
    public void PrepareEmulator(List<Instruction> instructions, List<StaticData> static_data, List<Models.Label> labels)
    {
        this.instructions = instructions;
        this.static_data = static_data;
        this.labels = labels;

        this.CurrentInstructionIndex = 0;
        this.Error = false;
        this.ExitFound = false;

        InitStaticData();
    }

    private void InitStaticData()
    {
        foreach (StaticData to_init in this.static_data)
        {
            switch (to_init.size_in_bits)
            {
                case Size._8_BIT:
                    if (to_init.is_string_array)
                        for (int char_index = 0; char_index < to_init.characters.Count; char_index++)
                            this.VirtualSystem.SetByteMemory(to_init.memory_location + char_index, (byte)to_init.characters[char_index]);
                    else
                        this.VirtualSystem.SetByteMemory(to_init.memory_location, (byte)to_init.value);

                    break;

                case Size._16_BIT: this.VirtualSystem.SetWordMemory(to_init.memory_location, (ushort)to_init.value); break;
                case Size._32_BIT: this.VirtualSystem.SetDoubleMemory(to_init.memory_location, (uint)to_init.value); break;
                case Size._64_BIT: this.VirtualSystem.SetQuadMemory(to_init.memory_location, to_init.value); break;
            }
        }
    }

    public bool GetInterruptOccurance() => this.Interrupt.GetInterruptCode() != InterruptCode.NoN;
    
    public bool HasInstructions() => this.instructions.Count != 0;

    public int GetInstructionCount() => this.instructions.Count;

    public void NextInstruction()
    {
        if (this.CurrentInstructionIndex < this.instructions.Count)
            this.CurrentInstructionIndex++;
    }

    public void ResetInterrupt()
    {
        this.Interrupt.SetInterruptCode(InterruptCode.NoN);
    }

    public void Execute()
    {
        // make sure there are instructions to run in the first place
        if (HasInstructions() == false)
            return;

        InstructionActions actions = new();
        Instruction instruction_to_execute = this.instructions[this.CurrentInstructionIndex];

        if (instruction_to_execute.opcode == Opcodes.LABEL)
            return;

        ulong destination_value = AnalyzeInstructionDestination(instruction_to_execute, this.VirtualSystem);
        ulong source_value = AnalyzeInstructionSource(instruction_to_execute, this.VirtualSystem);
        int to_return_to = 0;
        int destination_memory_index = GetMemoryIndex(instruction_to_execute, instruction_to_execute.destination_memory_name);
        int index_to_jump_to = 0;

        if (this.Error)
            return;

        switch (instruction_to_execute.opcode)
        {
            case Opcodes.AAA:
                this.VirtualSystem.SetRegisterWord(Registers.RAX, actions.AAA((ushort)destination_value, this.VirtualSystem.EFLAGS).Item1);
                this.VirtualSystem.EFLAGS = actions.AAA((ushort)destination_value, this.VirtualSystem.EFLAGS).Item2;
                break;

            case Opcodes.AAD:
                this.VirtualSystem.SetRegisterWord(Registers.RAX, actions.AAD((ushort)destination_value));
                break;

            case Opcodes.AAM:
                this.VirtualSystem.SetRegisterWord(Registers.RAX, actions.AAM((ushort)destination_value));
                break;

            case Opcodes.AAS:
                this.VirtualSystem.SetVirtualSystem(actions.AAS(this.VirtualSystem));
                break;

            case Opcodes.ADC:
                SetValue(instruction_to_execute, destination_memory_index, actions.ADC(destination_value, source_value, this.VirtualSystem.EFLAGS));
                break;

            case Opcodes.ADD:
                SetValue(instruction_to_execute, destination_memory_index, actions.ADD(destination_value, source_value));
                break;

            case Opcodes.AND:
                SetValue(instruction_to_execute, destination_memory_index, actions.AND(destination_value, source_value));
                break;

            case Opcodes.CALL:
                (bool, int) to_go_to = actions.CALL(labels, instruction_to_execute.destination_memory_name);

                if (to_go_to.Item1)
                {
                    this.VirtualSystem.PushCall(this.CurrentInstructionIndex);
                    this.CurrentInstructionIndex = to_go_to.Item2;
                    break;
                }

                this.Error = true;
                break;

            case Opcodes.RET:
                to_return_to = this.VirtualSystem.PopCall();

                if (to_return_to == -1)
                {
                    this.Error = true;
                    break;
                }

                this.CurrentInstructionIndex = to_return_to;
                break;

            case Opcodes.CBW:
                this.VirtualSystem.SetRegisterByte(Registers.RAX, actions.CBW(this.VirtualSystem.GetRegisterByte(Registers.RAX, false)), true);
                break;

            case Opcodes.CLC:
                this.VirtualSystem.EFLAGS = actions.CLC(this.VirtualSystem.EFLAGS);
                break;

            case Opcodes.CLD:
                this.VirtualSystem.EFLAGS = actions.CLD(this.VirtualSystem.EFLAGS);
                break;

            case Opcodes.CLI:
                this.VirtualSystem.EFLAGS = actions.CLI(this.VirtualSystem.EFLAGS);
                break;

            case Opcodes.CMC:
                this.VirtualSystem.EFLAGS = actions.CMC(this.VirtualSystem.EFLAGS);
                break;

            case Opcodes.CMP:
                this.VirtualSystem.EFLAGS = actions.CMP(destination_value, source_value, this.VirtualSystem.EFLAGS);
                break;

            case Opcodes.CWD:
                this.VirtualSystem.SetRegisterWord(Registers.RDX, actions.CWD(this.VirtualSystem.GetRegisterWord(Registers.RAX)));
                break;

            case Opcodes.DAA:
                this.VirtualSystem.SetRegisterByte(Registers.RAX, actions.DAA(this.VirtualSystem.GetRegisterByte(Registers.RAX, false), this.VirtualSystem.EFLAGS), false);
                break;

            case Opcodes.DAS:
                this.VirtualSystem.SetRegisterByte(Registers.RAX, actions.DAS(this.VirtualSystem.GetRegisterByte(Registers.RAX, false), this.VirtualSystem.EFLAGS), false);
                break;

            case Opcodes.DEC:
                SetValue(instruction_to_execute, destination_memory_index, actions.DEC(destination_value, instruction_to_execute.bit_mode));
                break;

            case Opcodes.DIV:
                this.VirtualSystem.SetVirtualSystem(actions.DIV(this.VirtualSystem, instruction_to_execute.bit_mode, destination_value));
                break;

            case Opcodes.HLT:
                this.ExitFound = true;
                break;

            case Opcodes.INC:
                SetValue(instruction_to_execute, destination_memory_index, actions.INC(destination_value));
                break;

            case Opcodes.INT:
                this.Interrupt.SetInterruptCode(destination_value);
                this.Error = this.Interrupt.GetInterruptCode() == InterruptCode.NoN;

                break;

            case Opcodes.JA:
                index_to_jump_to = actions.JA(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.CF), GetFlag(EFLAGS.ZF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JAE:
                index_to_jump_to = actions.JAE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.CF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JB:
                index_to_jump_to = actions.JB(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.CF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JBE:
                index_to_jump_to = actions.JBE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.CF), GetFlag(EFLAGS.ZF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JC:
                index_to_jump_to = actions.JC(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.CF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JE:
                index_to_jump_to = actions.JE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.ZF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JG:
                index_to_jump_to = actions.JG(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.ZF), GetFlag(EFLAGS.SF), GetFlag(EFLAGS.OF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JGE:
                index_to_jump_to = actions.JGE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.SF), GetFlag(EFLAGS.OF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JL:
                index_to_jump_to = actions.JL(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.SF), GetFlag(EFLAGS.OF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JNA:
                index_to_jump_to = actions.JNA(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.CF), GetFlag(EFLAGS.ZF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JNAE:
                index_to_jump_to = actions.JNAE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.CF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JNB:
                index_to_jump_to = actions.JNB(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.CF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JNBE:
                index_to_jump_to = actions.JNBE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.CF), GetFlag(EFLAGS.ZF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JNC:
                index_to_jump_to = actions.JNC(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.CF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JNE:
                index_to_jump_to = actions.JNE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.ZF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JNG:
                index_to_jump_to = actions.JNG(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.ZF), GetFlag(EFLAGS.SF), GetFlag(EFLAGS.OF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JNGE:
                index_to_jump_to = actions.JNGE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.SF), GetFlag(EFLAGS.OF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JNL:
                index_to_jump_to = actions.JNL(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.SF), GetFlag(EFLAGS.OF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JNLE:
                index_to_jump_to = actions.JNLE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.ZF), GetFlag(EFLAGS.SF), GetFlag(EFLAGS.OF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JNO:
                index_to_jump_to = actions.JNO(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.OF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JNP:
                index_to_jump_to = actions.JNP(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.PF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JNS:
                index_to_jump_to = actions.JNS(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.SF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JNZ:
                index_to_jump_to = actions.JNZ(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.ZF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JO:
                index_to_jump_to = actions.JO(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.OF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JP:
                index_to_jump_to = actions.JP(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.PF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JPO:
                index_to_jump_to = actions.JPO(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.PF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JPE:
                index_to_jump_to = actions.JPE(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.PF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JS:
                index_to_jump_to = actions.JS(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.SF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JZ:
                index_to_jump_to = actions.JZ(this.labels, instruction_to_execute.destination_memory_name, GetFlag(EFLAGS.ZF));

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.JMP:
                index_to_jump_to = actions.JMP(this.labels, instruction_to_execute.destination_memory_name);

                if (index_to_jump_to != -1)
                    this.CurrentInstructionIndex = index_to_jump_to;

                break;

            case Opcodes.LAHF:
                this.VirtualSystem.SetRegisterByte(Registers.RAX, actions.LAHF(this.VirtualSystem.EFLAGS), true);
                break;

            case Opcodes.LEA:
                SetValue(instruction_to_execute, destination_memory_index, (ulong)actions.LEA(this.static_data, instruction_to_execute.source_memory_name));
                break;

            case Opcodes.MOV:
                SetValue(instruction_to_execute, destination_memory_index, actions.MOV(source_value));
                break;

            case Opcodes.MUL:
                this.VirtualSystem.SetVirtualSystem(actions.MUL(this.VirtualSystem, destination_value, instruction_to_execute.bit_mode));
                break;

            case Opcodes.NEG:
                SetValue(instruction_to_execute, destination_memory_index, actions.NEG(destination_value));
                break;

            case Opcodes.NOP:
                actions.NOP();
                break;

            case Opcodes.NOT:
                SetValue(instruction_to_execute, destination_memory_index, actions.NOT(destination_value));
                break;

            case Opcodes.OR:
                SetValue(instruction_to_execute, destination_memory_index, actions.OR(destination_value, source_value));
                break;

            case Opcodes.POP:
                this.VirtualSystem.SetVirtualSystem(actions.POP(this.VirtualSystem, instruction_to_execute, destination_memory_index));
                break;

            case Opcodes.POPF:
                this.VirtualSystem.SetVirtualSystem(actions.POPF(this.VirtualSystem));
                break;

            case Opcodes.PUSH:
                this.VirtualSystem.SetVirtualSystem(actions.PUSH(this.VirtualSystem, instruction_to_execute.bit_mode, destination_value));
                break;

            case Opcodes.PUSHF:
                this.VirtualSystem.SetVirtualSystem(actions.PUSHF(this.VirtualSystem, this.VirtualSystem.EFLAGS));
                break;

            case Opcodes.RCL:
                this.VirtualSystem.SetVirtualSystem(actions.RCL(this.VirtualSystem, instruction_to_execute, destination_memory_index, destination_value, (int)source_value));
                break;

            case Opcodes.RCR:
                this.VirtualSystem.SetVirtualSystem(actions.RCR(this.VirtualSystem, instruction_to_execute, destination_memory_index, destination_value, (int)source_value));
                break;

            case Opcodes.ROL:
                SetValue(instruction_to_execute, destination_memory_index, actions.ROL(instruction_to_execute.bit_mode, destination_value, (int)source_value));
                break;

            case Opcodes.ROR:
                SetValue(instruction_to_execute, destination_memory_index, actions.ROR(instruction_to_execute.bit_mode, destination_value, (int)source_value));
                break;

            case Opcodes.SAHF:
                this.VirtualSystem.SetRegisterByte(Registers.RAX, actions.SAHF(this.VirtualSystem.EFLAGS, this.VirtualSystem.GetEFLAGSMasks()), true);
                break;

            case Opcodes.SAL:
                SetValue(instruction_to_execute, destination_memory_index, actions.SAL(destination_value, (int)source_value));
                break;

            case Opcodes.SAR:
                SetValue(instruction_to_execute, destination_memory_index, actions.SAR(destination_value, (int)source_value));
                break;

            case Opcodes.SBB:
                SetValue(instruction_to_execute, destination_memory_index, actions.SBB(destination_value, source_value, GetFlag(EFLAGS.CF)));
                break;

            case Opcodes.SHL:
                SetValue(instruction_to_execute, destination_memory_index, actions.SHL(destination_value, (int)source_value));
                break;

            case Opcodes.SHR:
                SetValue(instruction_to_execute, destination_memory_index, actions.SHR(destination_value, (int)source_value));
                break;

            case Opcodes.STC:
                this.VirtualSystem.EFLAGS = actions.STC(this.VirtualSystem.EFLAGS, this.VirtualSystem.GetEFLAGSMasks()[0]);
                break;

            case Opcodes.STD:
                this.VirtualSystem.EFLAGS = actions.STD(this.VirtualSystem.EFLAGS, this.VirtualSystem.GetEFLAGSMasks()[6]);
                break;

            case Opcodes.STI:
                this.VirtualSystem.EFLAGS = actions.STI(this.VirtualSystem.EFLAGS, this.VirtualSystem.GetEFLAGSMasks()[7]);
                break;

            case Opcodes.SUB:
                SetValue(instruction_to_execute, destination_memory_index, actions.SUB(destination_value, source_value));
                break;

            case Opcodes.XOR:
                SetValue(instruction_to_execute, destination_memory_index, actions.XOR(destination_value, source_value));
                break;

            default:
                break;
        }
    }

    private int GetMemoryIndex(Instruction instruction, string label_name_to_find)
    {
        // check if the destination is a register pointer
        if (instruction.destination_pointer)
            return (int)this.VirtualSystem.GetRegisterQuad(instruction.destination_register);

        foreach (Label label in this.labels)
            if (label.name == label_name_to_find)
                return label.line;

        return -1;
    }

    private void SetValue(Instruction instruction, int memory_index, ulong value_to_set)
    {
        // check if the value needs to be saved in a register or memory location
        if (instruction.destination_memory_type != MemoryType.ADDRESS)
        {
            switch (instruction.bit_mode)
            {
                case Size._8_BIT:
                    if (instruction.source_pointer == false)
                        this.VirtualSystem.SetRegisterQuad(instruction.destination_register, value_to_set);
                    else
                        this.VirtualSystem.SetRegisterByte(instruction.destination_register, (byte)value_to_set, instruction.destination_high_or_low);
                    
                    break;

                case Size._16_BIT: this.VirtualSystem.SetRegisterWord(instruction.destination_register, (ushort)value_to_set); break;
                case Size._32_BIT: this.VirtualSystem.SetRegisterDouble(instruction.destination_register, (uint)value_to_set); break;
                case Size._64_BIT: this.VirtualSystem.SetRegisterQuad(instruction.destination_register, value_to_set); break;
            }

            return;
        }

        switch (instruction.bit_mode)
        {
            case Size._8_BIT: this.VirtualSystem.SetByteMemory(memory_index, (byte)value_to_set); break;
            case Size._16_BIT: this.VirtualSystem.SetWordMemory(memory_index, (ushort)value_to_set); break;
            case Size._32_BIT: this.VirtualSystem.SetDoubleMemory(memory_index, (uint)value_to_set); break;
            case Size._64_BIT: this.VirtualSystem.SetQuadMemory(memory_index, value_to_set); break;
        }
    }

    private ulong AnalyzeInstructionDestination(Instruction instruction, VirtualSystem VirtualSystem)
    {
        switch (instruction.variant)
        {
            case Variants.SINGLE_REGISTER: return this.VirtualSystem.GetRegisterQuad(instruction.destination_register);
            case Variants.SINGLE_VALUE: return ulong.Parse(instruction.destination_memory_name);

            case Variants.SINGLE_ADDRESS_VALUE:
                for (int i = 0; i < this.static_data.Count; i++)
                    if (this.static_data[i].name == instruction.destination_memory_name)
                        return this.static_data[i].value;

                return 0;

            case Variants.DESTINATION_REGISTER_SOURCE_REGISTER:
                switch (instruction.bit_mode)
                {
                    case Size._8_BIT: return this.VirtualSystem.GetRegisterByte(instruction.destination_register, instruction.destination_high_or_low);
                    case Size._16_BIT: return this.VirtualSystem.GetRegisterWord(instruction.destination_register);
                    case Size._32_BIT: return this.VirtualSystem.GetRegisterDouble(instruction.destination_register);
                    case Size._64_BIT: return this.VirtualSystem.GetRegisterQuad(instruction.destination_register);
                    default: return 0;
                }

            case Variants.DESTINATION_REGISTER_SOURCE_ADDRESS:
                return this.VirtualSystem.GetRegisterQuad(instruction.destination_register);

            case Variants.DESTINATION_REGISTER_SOURCE_VALUE:
                return this.VirtualSystem.GetRegisterQuad(instruction.destination_register);

            case Variants.DESTINATION_ADDRESS_SOURCE_REGISTER:
                if (instruction.destination_pointer)
                {
                    switch (instruction.bit_mode)
                    {
                        case Size._8_BIT: return this.VirtualSystem.GetRegisterByte(instruction.destination_register, instruction.destination_high_or_low);
                        case Size._16_BIT: return this.VirtualSystem.GetRegisterWord(instruction.destination_register);
                        case Size._32_BIT: return this.VirtualSystem.GetRegisterDouble(instruction.destination_register);
                        case Size._64_BIT: return this.VirtualSystem.GetRegisterQuad(instruction.destination_register);
                    }
                }

                for (int i = 0; i < this.static_data.Count; i++)
                    if (this.static_data[i].name == instruction.destination_memory_name)
                        return this.static_data[i].value;

                return 0;
        }

        return 0;
    }

    private ulong AnalyzeInstructionSource(Instruction instruction, VirtualSystem VirtualSystem)
    {
        if (new Variants[] {
            Variants.SINGLE,
            Variants.SINGLE_REGISTER,
            Variants.SINGLE_VALUE,
            Variants.SINGLE_ADDRESS_VALUE,
            Variants.LABEL
        }.Contains(instruction.variant)) return 0;

        if (instruction.variant == Variants.DESTINATION_ADDRESS_SOURCE_REGISTER || instruction.variant == Variants.DESTINATION_REGISTER_SOURCE_REGISTER)
        {
            switch (instruction.bit_mode)
            {
                case Size._8_BIT: return this.VirtualSystem.GetRegisterByte(instruction.source_register, instruction.source_high_or_low);
                case Size._16_BIT: return this.VirtualSystem.GetRegisterWord(instruction.source_register);
                case Size._32_BIT: return this.VirtualSystem.GetRegisterDouble(instruction.source_register);
                case Size._64_BIT: return this.VirtualSystem.GetRegisterQuad(instruction.source_register);
                default: return 0;
            }
        }
        else if (instruction.variant == Variants.DESTINATION_REGISTER_SOURCE_ADDRESS)
        {
            int memory_index = -1;

            if (instruction.source_pointer && instruction.source_register != Registers.NoN)
                memory_index = (int)this.VirtualSystem.GetRegisterDouble(instruction.source_register);
            else
                memory_index = this.static_data.Where(data => data.name == instruction.source_memory_name).Select(data => data.memory_location).First();

            if (instruction.source_pointer == false)
                return (ulong)memory_index;

            if (memory_index == -1)
            {
                this.Error = true;
                return 0;
            }

            switch (instruction.bit_mode)
            {
                case Size._8_BIT: return this.VirtualSystem.GetByteMemory(memory_index);
                case Size._16_BIT: return this.VirtualSystem.GetWordMemory(memory_index);
                case Size._32_BIT: return this.VirtualSystem.GetDoubleMemory(memory_index);
                case Size._64_BIT: return this.VirtualSystem.GetQuadMemory(memory_index);
                default: return 0;
            }
        }
        else if (instruction.variant == Variants.DESTINATION_ADDRESS_SOURCE_VALUE)
        {
            return uint.Parse(instruction.source_memory_name);
        }
        else
        {
            if (instruction.source_memory_name.StartsWith('-'))
            {
                this.Error = true;
                return 0;
            }
            else if (instruction.source_register == Registers.NoN && instruction.source_memory_type == MemoryType.NoN && instruction.source_pointer == false && instruction.destination_pointer == false)
                return 0;

            return ulong.Parse(instruction.source_memory_name);
        }
    }

    private bool GetFlag(EFLAGS flag)
    {
        return (this.VirtualSystem.EFLAGS & ((int) flag)) != 0;
    }


    public VirtualSystem VirtualSystem = new();
    public Interrupt Interrupt = new();
    public int CurrentInstructionIndex;
    public bool Error;
    public bool ExitFound;

    private List<Instruction> instructions = new();
    private List<StaticData> static_data = new();
    private List<Label> labels = new();
}
