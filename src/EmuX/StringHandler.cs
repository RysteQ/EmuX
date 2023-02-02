namespace EmuX
{
    internal class StringHandler
    {
        /// <summary>
        /// Removes all empty lines from the string array
        /// </summary>
        /// <param name="lines_to_remove_from">The string array to remove the empty lines from</param>
        /// <returns>All of the non empty lines</returns>
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
        /// <param name="string_to_check">The string to scan</param>
        /// <param name="char_to_scan_for">The character to scan</param>
        /// <returns>The total occurrences of said character</returns>
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
        /// <param name="string_to_reverse">The string to reverse</param>
        /// <returns>The reversed string</returns>
        public string ReverseString(string string_to_reverse)
        {
            string toReturn = "";

            for (int i = string_to_reverse.Length; i != 0; i--)
                toReturn += string_to_reverse[i - 1];

            return toReturn;
        }
    }
}