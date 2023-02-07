namespace EmuX
{
    internal class StringHandler
    {
        /// <summary>
        /// Removes all empty lines from the string array
        /// </summary>
        public string[] RemoveEmptyLines(string[] lines_to_remove_from)
        {
            List<string> toReturn = new List<string>();

            for (int i = 0; i < lines_to_remove_from.Length; i++)
                if (lines_to_remove_from[i].Trim().Length != 0)
                    toReturn.Add(lines_to_remove_from[i]);

            return toReturn.ToArray();
        }

        /// <summary>
        /// Scans the string and count all of the occurrences of the given character
        /// </summary>
        public int GetCharOccurrences(string string_to_check, char char_to_scan_for)
        {
            int toReturn = 0;

            for (int i = 0; i < string_to_check.Length; i++)
                if (string_to_check[i] == char_to_scan_for)
                    toReturn++;

            return toReturn;
        }

        /// <summary>
        /// Reverses a string
        /// </summary>
        public string ReverseString(string string_to_reverse)
        {
            string toReturn = "";

            for (int i = string_to_reverse.Length; i != 0; i--)
                toReturn += string_to_reverse[i - 1];

            return toReturn;
        }
    }
}