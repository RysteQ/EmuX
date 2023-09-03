using EmuX.src.Enums.Emulators.VM;
using EmuX.src.Enums.Instruction_Data;
using Size = EmuX.src.Enums.Emulators.Size;

namespace EmuX.src.Models.Emulator;

public struct Instruction
{
    public Opcodes opcode;
    public Variants variant;
    public Registers destination_register;
    public Registers source_register;
    public MemoryType destination_memory_type;
    public MemoryType source_memory_type;
    public Size bit_mode;
    public bool destination_pointer;
    public bool source_pointer;
    public string destination_memory_name;
    public string source_memory_name;
    public bool destination_high_or_low;
    public bool source_high_or_low;
}