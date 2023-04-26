namespace EmuX.src.Models
{
    public class Instruction
    {
        public Opcodes.Opcodes_ENUM opcode;
        public Variants.Variants_ENUM variant;
        public Registers.Registers_ENUM destination_register;
        public Registers.Registers_ENUM source_register;
        public Memory_Type.Memory_Type_ENUM destination_memory_type;
        public Memory_Type.Memory_Type_ENUM source_memory_type;
        public SIZE.Size_ENUM bit_mode;
        public bool destination_pointer;
        public bool source_pointer;
        public string destination_memory_name = string.Empty;
        public string source_memory_name = string.Empty;
        public bool destination_high_or_low;
        public bool source_high_or_low;
    }
}
