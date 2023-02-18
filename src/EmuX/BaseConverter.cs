namespace EmuX
{
    internal class BaseConverter
    {
        /// <summary>
        /// Converts a binary value to its integer form
        /// </summary>
        public ulong ConvertBinaryToUnsignedLong(string binary_value)
        {
            ulong toReturn = 0;
            ulong multiplier = 1;

            binary_value = new StringHandler().ReverseString(binary_value);

            for (int i = 0; i < binary_value.Length; i++)
            {
                if (binary_value[i] == '1')
                    toReturn += multiplier;

                multiplier = multiplier << 1;
            }

            return toReturn;
        }

        /// <summary>
        /// Converts a hexadecimal value to an integer
        /// </summary>
        public ulong ConvertHexToUnsignedLong(string hex_value)
        {
            return this.ConvertBinaryToUnsignedLong(this.ConvertHexToBinary(hex_value));
        }

        /// <summary>
        /// Converts and int value to a binary string
        /// </summary>
        public string ConvertUnsignedLongToBinaryString(ulong value)
        {
            StringHandler string_handler = new StringHandler();
            string toReturn = "";

            do
            {
                toReturn += (value % 2).ToString();
                value = value / 2;
            } while (value != 0);

            return string_handler.ReverseString(toReturn);
        }

        /// <summary>
        /// Converts a hex string to a binary string
        /// </summary>
        private string ConvertHexToBinary(string hex_value)
        {
            string toReturn = "";

            hex_value = hex_value.ToUpper();
            hex_value = new StringHandler().ReverseString(hex_value);

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
    }
}
