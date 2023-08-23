namespace EmuX.src.Services.Base.Verifier;

public class HexadecimalVerifier
{
    public static bool IsBase(string to_check)
    {
        char[] chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        return to_check.ToUpper().Where(character => chars.Contains(character) == false).Any();
    }
}