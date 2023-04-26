namespace EmuX.src.Models
{
    class StaticData : SIZE
    {
        public string name = string.Empty;
        public int memory_location;
        public Size_ENUM size_in_bits;
        public bool is_string_array;
        public ulong value;
        public List<char> characters = new List<char>();
    }
}
