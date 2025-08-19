using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuXCore.InstructionLogic.Interfaces;

public interface IPrefixLookup
{
    public bool DoesPrefixExist(string prefix);
    public Type GetPrefixType(string prefix);
}
