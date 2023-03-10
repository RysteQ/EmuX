using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX
{
    internal class HexConverter
    {
        /// <summary>
        /// Converts an integer to a hex string
        /// </summary>
        /// <returns>The hexadecimal value of the unsigned integer</returns>
        public string ConvertUnsignedLongToHex(ulong to_convert)
        {
            string toReturn = "";

            byte[] bytes = new byte[8]
            {
                (byte) (to_convert & 0xFF00000000000000),
                (byte) (to_convert & 0x00FF000000000000),
                (byte) (to_convert & 0x0000FF0000000000),
                (byte) (to_convert & 0x000000FF00000000),
                (byte) (to_convert & 0x00000000FF000000),
                (byte) (to_convert & 0x0000000000FF0000),
                (byte) (to_convert & 0x000000000000FF00),
                (byte) to_convert
            };

            for (int i = 7; i > -1; i--)
                toReturn += ConvertByteToHex(bytes[i]);

            return toReturn;
        }

        /// <summary>
        /// Gets a byte value and returns the hexadecimal value of said byte
        /// </summary>
        public string ConvertByteToHex(byte to_convert)
        {
            // get the first and last four bits of a byte
            byte first_half = (byte) (to_convert & 0b00001111);
            byte second_half = (byte) (to_convert >> 4);
            string toReturn = "";

            // cunstruct the first 4 bits and last for bits in hex
            if (first_half < 10)
                toReturn += first_half.ToString();
            else
                toReturn += ConvertByteOverTenToHex(first_half).ToString();

            if (second_half < 10)
                toReturn += second_half.ToString();
            else
                toReturn += ConvertByteOverTenToHex(second_half).ToString();

            return toReturn;
        }

        /// <summary>
        /// Converts a byte that's over the value of ten to its hexadecimal value
        /// </summary>
        private char ConvertByteOverTenToHex(byte to_convert)
        {
            // I know I can write something better but I am just bored fam
            switch (to_convert)
            {
                case 10: return 'A';
                case 11: return 'B';
                case 12: return 'C';
                case 13: return 'D';
                case 14: return 'E';
                case 15: return 'F';
            }

            return 'F';
        }

        /// <summary>
        /// Checks if the given string is in hexadecimal or not
        /// </summary>
        public bool IsHex(string hex_string)
        {
            char[] acceptable_characters = new char[]
            {
                '0', '1', '2', '3',
                '4', '5', '6', '7',
                '8', '9', 'A', 'B',
                'C', 'D', 'E', 'F'
            };

            // make sure the input string is uppercase
            hex_string = hex_string.ToUpper();

            // go through every character and see if the character is a valid hexadecimal character
            for (int i = 0; i < hex_string.Length; i++)
                if (acceptable_characters.Contains(hex_string[i]) == false)
                    return false;

            return true;
        }

        /// <summary>
        /// Checks if the string is in binary or not
        /// </summary>
        public bool IsBinary(string binary_string)
        {
            if (binary_string.Length == 0)
                return false;

            for (int i = 0; i < binary_string.Length; i++)
                if (binary_string[i] != '1' && binary_string[i] != '0')
                    return false;

            return true;
        }
    }
}
