namespace EmuX
{
    internal class Instruction : Instruction_Data
    {
        public Instruction()
        {
            instruction = Instruction_ENUM.NoN;
            variant = Instruction_Variant_ENUM.NoN;
            destination_register = Registers_ENUM.NoN;
            source_register = Registers_ENUM.NoN;
            destination_memory_type = Memory_Type_ENUM.NoN;
            source_memory_type = Memory_Type_ENUM.NoN;
            bit_mode = Bit_Mode_ENUM._8_BIT;
            destination_pointer = false;
            source_pointer = false;
            destination_memory_name = "";
            source_memory_name = "";
            destination_high_or_low = false;
            source_high_or_low = false;
        }

        public Instruction_ENUM instruction;
        public Instruction_Variant_ENUM variant;
        public Registers_ENUM destination_register;
        public Registers_ENUM source_register;
        public Memory_Type_ENUM destination_memory_type;
        public Memory_Type_ENUM source_memory_type;
        public Bit_Mode_ENUM bit_mode;
        public bool destination_pointer;
        public bool source_pointer;
        public string destination_memory_name;
        public string source_memory_name;
        public bool destination_high_or_low;
        public bool source_high_or_low;
    }
}
