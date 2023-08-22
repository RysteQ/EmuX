namespace EmuX.src.Services.Base.Converter;

public class HexadecimalConverter
{
    public static string ConvertUlongToBase(ulong to_convert)
    {
        string to_return = string.Empty;

        do
        {
            to_return += valid_characters[to_convert & 0x0F];
            to_convert = to_convert >> 4;
        } while (to_convert > 0);

        return new string(to_return.Reverse().ToArray());
    }

    public static ulong ConvertBaseToUlong(string to_convert)
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

    static readonly char[] valid_characters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
}
