namespace EmuX.src.Services.Base.Verifier;

public class BinaryVerifier
{
    public static bool IsBase(string to_check) => to_check.Where(character => character != '0' && character != '1').Any();
}
