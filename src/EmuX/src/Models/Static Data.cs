using static EmuX.src.Enums.BitSize;

namespace EmuX.src.Models
{
    class StaticData
    {
        public StaticData()
        {
            this.name = string.Empty;
            this.characters = new List<char>();
        }

        public string name { get; set; }
        public int memory_location { get; set; }
        public SIZE size_in_bits { get; set; }
        public bool is_string_array { get; set; }
        public ulong value { get; set; }
        public List<char> characters { get; set; }
    }
}
