namespace EmuX.Models
{
    class StaticData
    {
        public string name { get; set; } = string.Empty;
        public int memory_location { get; set; }
        public SIZE size_in_bits { get; set; }
        public bool is_string_array { get; set; }
        public ulong value { get; set; }
        public List<char> characters { get; set; } = new List<char>();

        // If the static data is a single character or an array of characters
        // the bool is_string_array will be set to true

        public enum SIZE
        {
            _8_BIT,
            _16_BIT,
            _32_BIT,
            _64_BIT
        }
    }
}
