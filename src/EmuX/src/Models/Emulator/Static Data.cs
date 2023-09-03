using Size = EmuX.src.Enums.Emulators.Size;

namespace EmuX.src.Models.Emulator;

public struct StaticData
{
    public string name;
    public int memory_location;
    public Size size_in_bits;
    public bool is_string_array;
    public ulong value;
    public List<char> characters;
}