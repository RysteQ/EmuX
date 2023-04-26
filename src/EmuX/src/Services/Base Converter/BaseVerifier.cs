using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX.src.Services.Base_Converter
{
    public class BaseVerifier
    {
        public static bool IsBinary(string to_check)
        {
            foreach (char character in to_check.ToUpper())
                if (new char[] { '0', '1' }.Contains(character) == false)
                    return false;

            return true;
        }
        public static bool IsHex(string to_check)
        {
            foreach (char character in to_check.ToUpper())
                if (valid_characters.Contains(character) == false)
                    return false;

            return true;
        }

        private static readonly char[] valid_characters = new char[]
        {
            '0', '1', '2', '3',
            '4', '5', '6', '7',
            '8', '9', 'A', 'B',
            'C', 'D', 'E', 'F'
        };
    }
}
