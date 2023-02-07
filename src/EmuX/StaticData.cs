namespace EmuX
{
    class StaticData
    {
        public string name = "";
        public int memory_location;
        public SIZE size_in_bits;
        public bool is_string_array;
        public ulong value;
        public List<char> characters = new List<char>();
        
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
