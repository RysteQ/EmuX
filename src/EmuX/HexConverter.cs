﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX
{
    internal class HexConverter
    {
        /// <summary>
        /// Gets a byte value and returns the hexadecimal value of said byte
        /// </summary>
        /// <param name="to_convert"></param>
        /// <returns></returns>
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
        /// <param name="to_convert"></param>
        /// <returns></returns>
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
        /// <param name="hex_string"></param>
        /// <returns></returns>
        public bool IsHex(string hex_string)
        {
            char[] acceptable_characters = new char[]
            {
                '0', '1', '2', '3',
                '4', '5', '6', '7',
                '8', '9', 'A', 'B',
                'C', 'D', 'E', 'F'
            };

            // go through every character and see if the character is a valid hexadecimal character
            for (int i = 0; i < hex_string.Length; i++)
                if (acceptable_characters.Contains(hex_string[i]) == false)
                    return false;

            return true;
        }

        /// <summary>
        /// Checks if the string is in binary or not
        /// </summary>
        /// <param name="binary_string"></param>
        /// <returns></returns>
        public bool IsBinary(string binary_string)
        {
            for (int i = 0; i < binary_string.Length; i++)
                if (binary_string[i] != '1' && binary_string[i] != '0')
                    return false;

            return true;
        }
    }
}
