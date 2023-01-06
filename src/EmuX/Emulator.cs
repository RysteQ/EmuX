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
            if (this.current_instruction_index < this.instructions.Count)
                this.current_instruction_index++;
        }
        
        /// <summary>
        /// Decreases the instruction index
        /// </summary>
        public void PreviousInstruction()
        {
            if (this.current_instruction_index != 0)
                this.current_instruction_index--;
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
        /// Getter - Gets the exit_found boolean
        /// </summary>
        /// <returns>A value of true if an exit was found and false if it wasn't</returns>
        public bool GetExit()
        {
            return this.exit_found;
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
            for (int i = 0; i < this.static_data.Count; i++)
            {
                switch (this.static_data[i].size_in_bits)
                {
                    case StaticData.SIZE._8_BIT:
                        this.virtual_system.SetByteMemory(this.static_data[i].memory_location, (byte) this.static_data[i].value);
                        break;

                    case StaticData.SIZE._16_BIT:
                        this.virtual_system.SetShortMemory(this.static_data[i].memory_location, (ushort) this.static_data[i].value);
                        break;

                    case StaticData.SIZE._32_BIT:
                        this.virtual_system.SetDoubleMemory(this.static_data[i].memory_location, (uint) this.static_data[i].value);
                        break;

                    case StaticData.SIZE._64_BIT:
                        this.virtual_system.SetQuadMemory(this.static_data[i].memory_location, this.static_data[i].value);
                        break;
                }
            }
        }

        /// <summary>
        /// Executes the instructions given earlier with the <c>SetData(instructions)</c> function
        /// </summary>
        public void Execute()
        {
            // make sure there are instructions to run in the first place
            if (this.instructions.Count == 0)
                return;

            Instruction_Actions actions = new Instruction_Actions();
            Instruction instruction_to_execute = this.instructions[this.current_instruction_index];

            // I will work on this after I make sure I get the rest of the code up to a working order / been able to recognize a far more complex program

            // ---
            uint flags = this.virtual_system.GetEFLAGS();
            ulong source_value = AnalyzeInstructionSource(instruction_to_execute, this.virtual_system);
            string memory_destination = instruction_to_execute.destination_memory_name;
            bool label_found = false;
            int to_return_to = 0;
            ulong value = 0;
            // ---

            if (this.error)
                return;

            switch (instruction_to_execute.instruction)
            {
                case Instruction_Data.Instruction_ENUM.ADC:
                    break;

                case Instruction_Data.Instruction_ENUM.ADD:
                    break;

                case Instruction_Data.Instruction_ENUM.AND:
                    break;

                case Instruction_Data.Instruction_ENUM.CALL:
                    label_found = false;

                    for (int i = 0; i < this.labels.Count; i++)
                    {
                        // if the corresponding label was found, go to the index of said label and add the index to return to the call stack
                        if (this.labels[i].Item1 == instruction_to_execute.destination_memory_name)
                        {
                            this.virtual_system.PushCall(this.current_instruction_index);
                            this.current_instruction_index = this.labels[i].Item2;
                            label_found = true;
                        }
                    }

                    if (label_found == false)
                        this.error = true;

                    break;

                case Instruction_Data.Instruction_ENUM.RET:
                    to_return_to = this.virtual_system.PopCall();

                    if (to_return_to == -1)
                    {
                        this.error = true;
                        break;
                    }

                    // pop the value from the call stack
                    this.current_instruction_index = to_return_to;

                    break;

                case Instruction_Data.Instruction_ENUM.CBW:
                    break;

                case Instruction_Data.Instruction_ENUM.CLC:
                    flags = this.virtual_system.GetEFLAGS();

                    if (flags % 2 == 1)
                        flags--;

                    this.virtual_system.SetEflags(flags);
                    break;

                case Instruction_Data.Instruction_ENUM.CLD:
                    flags = this.virtual_system.GetEFLAGS();

                    flags &= 0xFFFFFBFF;

                    this.virtual_system.SetEflags(flags);
                    break;

                case Instruction_Data.Instruction_ENUM.CLI:
                    flags = this.virtual_system.GetEFLAGS();

                    flags &= 0xFFFFFDFF;

                    this.virtual_system.SetEflags(flags);
                    break;

                case Instruction_Data.Instruction_ENUM.CMC:
                    flags = this.virtual_system.GetEFLAGS();

                    if (flags % 2 == 0)
                        flags++;
                    else
                        flags--;

                    this.virtual_system.SetEflags(flags);
                    break;

                case Instruction_Data.Instruction_ENUM.EXIT:
                    this.exit_found = true;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Getter - Gets the error variable
        /// </summary>
        /// <returns>A boolean value, true if an error was encountered</returns>
        public bool ErrorEncountered()
        {
            return this.error;
        }


        /// <summary>
        /// Resets the error and exit_found boolean variable so that execution can return to normal
        /// </summary>
        public void Reset()
        {
            this.error = false;
            this.exit_found = false;
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
        /// Analyzes the instruction variant and returns the destination value as a ulong
        /// </summary>
        /// <param name="instruction">The instruction to analyze</param>
        /// <param name="virtual_system"></param>
        /// <returns>An unsigned long value of its destination value</returns>
        private ulong AnalyzeInstructionDestination(Instruction instruction, VirtualSystem virtual_system)
        {
            ulong toReturn = 0;

            // This might be expanded upon in the future so I am keeping it for now
            switch (instruction.variant)
            {
                case Instruction_Data.Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE:
                    toReturn = ulong.Parse(instruction.source_memory_name);
                    break;

                case Instruction_Data.Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER:
                    toReturn = this.virtual_system.GetRegisterQuad(instruction.source_register);
                    break;
            }

            return toReturn;
        }

        /// <summary>
        /// Analyzes the instruction and return the appropriate source value
        /// </summary>
        /// <param name="instruction">The current instruction</param>
        /// <param name="virtual_system">The current virtual system</param>
        /// <returns>The ulong value of the source</returns>
        private ulong AnalyzeInstructionSource(Instruction instruction, VirtualSystem virtual_system)
        {
            ulong toReturn = 0;

            if (instruction.variant == Instruction_Data.Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER || instruction.variant == Instruction_Data.Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER)
            {
                switch (instruction.bit_mode)
                {
                    case Instruction_Data.Bit_Mode_ENUM._8_BIT:
                        toReturn = virtual_system.GetRegisterByte(instruction.source_register, instruction.high_or_low);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._16_BIT:
                        toReturn = virtual_system.GetRegisterShort(instruction.source_register);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._32_BIT:
                        toReturn = virtual_system.GetRegisterDouble(instruction.source_register);
                        break;

                    case Instruction_Data.Bit_Mode_ENUM._64_BIT:
                        toReturn = virtual_system.GetRegisterQuad(instruction.source_register);
                        break;
                }

                return toReturn;
            } else if (instruction.variant == Instruction_Data.Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS)
            {
                // return the value of the static data
                for (int i = 0; i < static_data.Count; i++)
                    if (static_data[i].name == instruction.source_memory_name)
                        return static_data[i].value;
            } else
            {
                return ulong.Parse(instruction.source_memory_name);
            }

            // if the static data label was not found then return 0 and set the error flag to true
            error = true;

            return 0;
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
        private bool error = false;
        private bool exit_found = false;
    }
}
