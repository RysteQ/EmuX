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

                // ---
                uint flags = 0;
                // ---

                switch (instruction_to_execute.instruction)
                {
                    case Instruction.Instruction_ENUM.ADC:
                        switch (instruction_to_execute.variant)
                        {
                            case Instruction.Instruction_Variant_ENUM.DESTINATION_REGISTER_VALUE:
                                break;

                            case Instruction.Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER:
                                break;

                            case Instruction.Instruction_Variant_ENUM.DESTINATION_REGISTER_ADDRESS:
                                break;

                            case Instruction.Instruction_Variant_ENUM.ADDRESS_VALUE_SOURCE_REGISTER:
                                break;
                        }

                        break;

                    case Instruction.Instruction_ENUM.ADD:
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

        private List<Instruction> instructions = new List<Instruction>();
    }
}
