using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX
{
    internal class Emulator
    {
        public void SetData(List<Instruction> instructions)
        {
            this.instructions = instructions;
        }

        public void Execute()
        {
            VirtualSystem virtual_system = new VirtualSystem();
            Instruction_Actions actions = new Instruction_Actions();

            for (int index = 0; index < instructions.Count; index++)
            {
                Instruction instruction_to_execute = instructions[index];

                // I will work on this after I make sure I get the rest of the code up to a working order / been able to recognize a far more complex program

                // ---
                uint flags = 0;
                ulong source_value = AnalyzeInstructionVariant(instruction_to_execute, virtual_system);
                // ---

                switch (instruction_to_execute.instruction)
                {
                    case Instruction.Instruction_ENUM.ADC:
                        if (instruction_to_execute.destination_register != Instruction.Registers_ENUM.NoN)
                        {
                            virtual_system.SetRegisterValue(instruction_to_execute.destination_register, actions.ADC(virtual_system.GetRegisterQuad(instruction_to_execute.destination_register), source_value, GetFlag(virtual_system.GetEFLAGS(), 1)));
                        }
                        else
                        {
                            // virtual_system.SetQuadMemory(GetAddressOffset() + 1024, actions.ADC(virtual_system.GetRegisterQuad(instruction_to_execute.destination_register), source_value, GetFlag(virtual_system.GetEFLAGS(), 1)));
                        }

                        break;

                    case Instruction.Instruction_ENUM.ADD:
                        virtual_system.SetRegisterValue(instruction_to_execute.destination_register, actions.ADD(virtual_system.GetRegisterQuad(instruction_to_execute.destination_register), source_value));
                        break;

                    case Instruction.Instruction_ENUM.AND:
                        break;

                    case Instruction.Instruction_ENUM.CALL:
                        // TODO
                        break;

                    case Instruction.Instruction_ENUM.CBW:
                        break;

                    case Instruction.Instruction_ENUM.CLC:
                        flags = virtual_system.GetEFLAGS();

                        if (flags % 2 == 1)
                            flags--;

                        virtual_system.SetEflags(flags);
                        break;

                    case Instruction.Instruction_ENUM.CLD:
                        flags = virtual_system.GetEFLAGS();

                        flags &= 0xFFFFFBFF;

                        virtual_system.SetEflags(flags);
                        break;

                    case Instruction.Instruction_ENUM.CLI:
                        flags = virtual_system.GetEFLAGS();

                        flags &= 0xFFFFFDFF;

                        virtual_system.SetEflags(flags);
                        break;

                    case Instruction.Instruction_ENUM.CMC:
                        flags = virtual_system.GetEFLAGS();

                        if (flags % 2 == 0)
                            flags++;
                        else
                            flags--;

                        virtual_system.SetEflags(flags);
                        break;
                }
            }
        }

        private ulong AnalyzeInstructionVariant(Instruction instruction, VirtualSystem virtual_system)
        {
            ulong toReturn = 0;

            string address_name;
            ulong value;

            switch (instruction.variant)
            {
                case Instruction.Instruction_Variant_ENUM.SINGLE:
                    toReturn = ulong.Parse(instruction.destination_memory_name);
                    break;

                case Instruction.Instruction_Variant_ENUM.SINGLE_REGISTER:
                    toReturn = virtual_system.GetRegisterQuad(instruction.destination_register);
                    break;

                case Instruction.Instruction_Variant_ENUM.SINGLE_VALUE:
                    toReturn = ulong.Parse(instruction.destination_memory_name);
                    break;

                case Instruction.Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE:
                    address_name = instruction.destination_memory_name.Trim('[').Trim(']');
                    value = virtual_system.GetQuadMemory(GetAddressOffset(address_name));
                    toReturn = value;
                    break;
            }

            return toReturn;
        }

        private int GetAddressOffset(string address_name)
        {
            return address_names.IndexOf(address_name);
        }

        private bool GetFlag(uint EFLAGS, uint mask)
        {
            uint flag = EFLAGS & mask;

            if (flag == 1)
                return true;

            return false;
        }

        private List<Instruction> instructions = new List<Instruction>();
        private List<string> address_names = new List<string>();
    }
}
