using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.Design.AxImporter;

namespace EmuX
{
    internal class Emulator
    {
        /// <summary>
        /// Sets the instruction data to execute but does not execute it, use the <c>Execute()</c> function to do that
        /// </summary>
        /// <param name="instructions">The instruction data to execute</param>
        public void SetData(List<Instruction> instructions)
        {
            this.instructions = instructions;
            this.current_instruction_index = 0;
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

        /// <summary>
        /// Getter - Gets the instruction index
        /// </summary>
        /// <returns>The instruction index</returns>
        public int GetIndex()
        {
            return this.current_instruction_index;
        }

        /// <summary>
        /// Getter - Gets the count of the instructions list
        /// </summary>
        /// <returns>The count of the instructions</returns>
        public int GetInstructionCount()
        {
            return this.instructions.Count;
        }

        /// <summary>
        /// Getter - Checks if there are any instructions to execute
        /// </summary>
        /// <returns></returns>
        public bool HasInstructions()
        {
            if (this.instructions.Count != 0)
                return true;

            return false;
        }

        /// <summary>
        /// Executes the instructions given earlier with the <c>SetData(instructions)</c> function
        /// </summary>
        public void Execute()
        {
            Instruction_Actions actions = new Instruction_Actions();
            Instruction instruction_to_execute = instructions[current_instruction_index];

            // I will work on this after I make sure I get the rest of the code up to a working order / been able to recognize a far more complex program

            // ---
            uint flags = virtual_system.GetEFLAGS();
            ulong source_value = AnalyzeInstructionVariant(instruction_to_execute, virtual_system);
            // ---

            switch (instruction_to_execute.instruction)
            {
                case Instruction_Data.Instruction_ENUM.ADC:
                    if (instruction_to_execute.destination_register != Instruction_Data.Registers_ENUM.NoN)
                    {
                        virtual_system.SetRegisterValue(instruction_to_execute.destination_register, actions.ADC(virtual_system.GetRegisterQuad(instruction_to_execute.destination_register), source_value, GetFlag(flags, 1)));
                    }
                    else
                    {
                        // virtual_system.SetQuadMemory(GetAddressOffset() + 1024, actions.ADC(virtual_system.GetRegisterQuad(instruction_to_execute.destination_register), source_value, GetFlag(virtual_system.GetEFLAGS(), 1)));
                    }

                    break;

                case Instruction_Data.Instruction_ENUM.ADD:
                    virtual_system.SetRegisterValue(instruction_to_execute.destination_register, actions.ADD(virtual_system.GetRegisterQuad(instruction_to_execute.destination_register), source_value));
                    break;

                case Instruction_Data.Instruction_ENUM.AND:
                    break;

                case Instruction_Data.Instruction_ENUM.CALL:
                    // TODO
                    break;

                case Instruction_Data.Instruction_ENUM.CBW:
                    break;

                case Instruction_Data.Instruction_ENUM.CLC:
                    flags = virtual_system.GetEFLAGS();

                    if (flags % 2 == 1)
                        flags--;

                    virtual_system.SetEflags(flags);
                    break;

                case Instruction_Data.Instruction_ENUM.CLD:
                    flags = virtual_system.GetEFLAGS();

                    flags &= 0xFFFFFBFF;

                    virtual_system.SetEflags(flags);
                    break;

                case Instruction_Data.Instruction_ENUM.CLI:
                    flags = virtual_system.GetEFLAGS();

                    flags &= 0xFFFFFDFF;

                    virtual_system.SetEflags(flags);
                    break;

                case Instruction_Data.Instruction_ENUM.CMC:
                    flags = virtual_system.GetEFLAGS();

                    if (flags % 2 == 0)
                        flags++;
                    else
                        flags--;

                    virtual_system.SetEflags(flags);
                    break;
            }
        }

        /// <summary>
        /// Getter
        /// </summary>
        /// <returns>The Virtual System currently in use</returns>
        public VirtualSystem GetVirtualSystem()
        {
            return this.virtual_system;
        }

        /// <summary>
        /// Setter
        /// </summary>
        /// <param name="virtual_system">The virtual system to set to</param>
        public void SetVirtualSystem(VirtualSystem virtual_system)
        {
            this.virtual_system = virtual_system;
        }

        /// <summary>
        /// Analyzes the instruction variant
        /// </summary>
        /// <param name="instruction">The instruction to analyze</param>
        /// <param name="virtual_system"></param>
        /// <returns>An unsigned long value of its destination value</returns>
        private ulong AnalyzeInstructionVariant(Instruction instruction, VirtualSystem virtual_system)
        {
            ulong toReturn = 0;

            string address_name;
            ulong value;

            // This might be expanded upon in the future so I am keeping it for now
            switch (instruction.variant)
            {
                case Instruction_Data.Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE:
                    toReturn = ulong.Parse(instruction.source_memory_name);
                    break;

                case Instruction_Data.Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER:
                    toReturn = virtual_system.GetRegisterQuad(instruction.source_register);
                    break;
            }

            return toReturn;
        }

        /// <summary>
        /// Gets the address offset, will be used for code like
        /// <c>my_word: dw 0</c>
        /// </summary>
        /// <param name="address_name">The address name</param>
        /// <returns>The int value of the memory offset</returns>
        private int GetAddressOffset(string address_name)
        {
            return address_names.IndexOf(address_name);
        }

        /// <summary>
        /// Gets the flag from the int EFLAG register value
        /// </summary>
        /// <param name="EFLAGS">The EFLAG register value</param>
        /// <param name="mask">The mask to use for the bitwise operation</param>
        /// <returns>The bool value for the flag result</returns>
        private bool GetFlag(uint EFLAGS, uint mask)
        {
            uint flag = EFLAGS & mask;

            if (flag == 1)
                return true;

            return false;
        }

        private List<Instruction> instructions = new List<Instruction>();
        private List<string> address_names = new List<string>();
        private VirtualSystem virtual_system = new VirtualSystem();
        private int current_instruction_index = 0;
    }
}
