namespace EmuX.Services.Base.Converter
{
    public class Binary_Converter
    {
        public static string ConvertUlongToBase(ulong to_convert)
        {
            string to_return = string.Empty;

            do
            {
                to_return += (to_convert % 2).ToString();
                to_convert = to_convert / 2;
            } while (to_convert != 0);

            return new string(to_return.Reverse().ToArray());
        }

        public static ulong ConvertBaseToUlong(string to_convert)
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
    }
}
