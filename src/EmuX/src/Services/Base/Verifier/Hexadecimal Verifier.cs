using EmuX.src.Services.Base.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX.src.Services.Base.Verifier
{
    public class Hexadecimal_Verifier : Hexadecimal
    {
        public static bool IsBase(string to_check)
        {
            foreach (char character in to_check.ToUpper())
                if (valid_characters.Contains(character) == false)
                    return false;

            return true;
        }
    }
}
