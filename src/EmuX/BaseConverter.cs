using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX
{
    internal class BaseConverter
    {
        /// <summary>
        /// Converts a binary value to its integer form
        /// </summary>
        /// <param name="binary_value"></param>
        /// <returns></returns>
        public int ConvertBinaryToInt(string binary_value)
        {
            int toReturn = 0;
            int multiplier = 1;

            binary_value = ReverseString(binary_value);

            for (int i = 0; i < binary_value.Length; i++)
            {
                if (binary_value[i] == '1')
                    toReturn += multiplier;

                multiplier = multiplier << 1;
            }

            return toReturn;
        }

        public int ConvertHexToInt(string hex_value)
        {
            return ConvertBinaryToInt(ConvertHexToBinary(hex_value));
        }

        /// <summary>
        /// Converts a hexadecimal value to it's binary form
        /// </summary>
        /// <param name="hex_value"></param>
        /// <returns></returns>
        private string ConvertHexToBinary(string hex_value)
        {
            string toReturn = "";

            hex_value = hex_value.ToUpper();
            hex_value = ReverseString(hex_value);

            for (int i = 0; i < hex_value.Length; i++)
            {
                switch (hex_value[i])
                {
                    case '0': toReturn += "0000"; break;
                    case '1': toReturn += "0001"; break;
                    case '2': toReturn += "0010"; break;
                    case '3': toReturn += "0011"; break;
                    case '4': toReturn += "0100"; break;
                    case '5': toReturn += "0101"; break;
                    case '6': toReturn += "0110"; break;
                    case '7': toReturn += "0111"; break;
                    case '8': toReturn += "1000"; break;
                    case '9': toReturn += "1001"; break;
                    case 'A': toReturn += "1010"; break;
                    case 'B': toReturn += "1011"; break;
                    case 'C': toReturn += "1100"; break;
                    case 'D': toReturn += "1101"; break;
                    case 'E': toReturn += "1110"; break;
                    case 'F': toReturn += "1111"; break;
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Gets a string value as an input and returns the string in reverse order
        /// </summary>
        /// <param name="string_to_reverse"></param>
        /// <returns></returns>
        private string ReverseString(string string_to_reverse)
        {
            string toReturn = "";

            for (int i = string_to_reverse.Length - 1; i > -1; i--)
                toReturn += string_to_reverse[i];

            return toReturn;
        }
    }
}
