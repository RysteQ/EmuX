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
        public void SetInstructions(List<Instruction> instructions)
        {
            this.instructions = instructions;
            this.current_instruction_index = 0;
        }

        /// <summary>
        /// Sets the static data list
        /// </summary>
        /// <param name="instructions">A list of StaticData</param>
        public void SetStaticData(List<StaticData> static_data)
        {
            this.static_data = static_data;
        }

        /// <summary>
        /// Sets the label data
        /// </summary>
        /// <param name="label_data">A list tuple of (string, int)</param>
        public void SetLabelData(List<(string, int)> label_data)
        {
            this.labels = label_data;
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
        /// Initializes the static data segment, all of it at once, must be run before the execute function
        /// </summary>
        public void InitStaticData()
        {
            for (int i = 0; i < static_data.Count; i++)
            {
                switch (static_data[i].size_in_bits)
                {
                    case StaticData.SIZE._8_BIT:
                        this.virtual_system.SetByteMemory(static_data[i].memory_location, (byte) static_data[i].value);
                        break;

                    case StaticData.SIZE._16_BIT:
                        this.virtual_system.SetShortMemory(static_data[i].memory_location, (ushort) static_data[i].value);
                        break;

                    case StaticData.SIZE._32_BIT:
                        this.virtual_system.SetDoubleMemory(static_data[i].memory_location, (uint) static_data[i].value);
                        break;

                    case StaticData.SIZE._64_BIT:
                        this.virtual_system.SetQuadMemory(static_data[i].memory_location, static_data[i].value);
                        break;
                }
            }
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
            string memory_destination = instruction_to_execute.destination_memory_name;
            // ---

            switch (instruction_to_execute.instruction)
            {
                case Instruction_Data.Instruction_ENUM.ADC:
                    break;

                case Instruction_Data.Instruction_ENUM.ADD:
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
        private List<StaticData> static_data = new List<StaticData>();
        private List<(string, int)> labels = new List<(string, int)>();
        private VirtualSystem virtual_system = new VirtualSystem();
        private int current_instruction_index = 0;
    }
}
