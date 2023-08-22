using Size = EmuX.src.Enums.Size;

namespace EmuX.src.Models;

public struct StaticData
{
    public string name;
    public int memory_location;
    public Size size_in_bits;
    public bool is_string_array;
    public ulong value;
    public List<char> characters;
}
