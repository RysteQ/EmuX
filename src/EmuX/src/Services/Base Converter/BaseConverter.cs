using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX.src.Services.Base_Converter
{
    public class BaseConverter
    {
        public static string ConvertUlongToBinary(ulong to_convert)
        {
            string to_return = string.Empty;

            do
            {
                to_return += (to_convert % 2).ToString();
                to_convert = to_convert / 2;
            } while (to_convert != 0);

            return new string(to_return.Reverse().ToArray());
        }

        public static ulong ConvertBinaryToUlong(string to_convert)
        {
            ulong toReturn = 0;
            ulong multiplier = 1;

            to_convert = new string(to_convert.Reverse().ToArray());

            foreach (char character in to_convert)
            {
                if (character == '1')
                    toReturn += multiplier;

                multiplier *= 2;
            }

            return toReturn;
        }

        public static string ConvertUlongToHex(ulong to_convert)
        {
            string to_return = string.Empty;

            do
            {
                to_return += valid_characters[to_convert & 0x0F];
                to_convert = to_convert >> 4;
            } while (to_convert > 0);

            return new string(to_return.Reverse().ToArray());
        }

        public static ulong ConvertHexToUlong(string to_convert)
        {
            ulong to_return = 0;

            foreach (char character in to_convert.ToUpper())
            {
                to_return = to_return << 4;
                to_return += ConvertNibbleToUlong(character);
            }

            return to_return;
        }

        private static ulong ConvertNibbleToUlong(char nibble)
        {
            for (int i = 0; i < valid_characters.Length; i++)
                if (nibble == valid_characters[i])
                    return (ulong)i;

            return 0;
        }

        private static readonly char[] valid_characters =
        {
            '0', '1', '2', '3',
            '4', '5', '6', '7',
            '8', '9', 'A', 'B',
            'C', 'D', 'E', 'F'
        };
    }
}
