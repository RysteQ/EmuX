using EmuXCore.InstructionLogic.Interfaces;
using EmuXCore.InstructionLogic.Prefixes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuXCore.InstructionLogic;

public class PrefixLookup : IPrefixLookup
{
    public bool DoesPrefixExist(string prefix)
    {
        return _allPrefixNamesAndTypes.ContainsKey(prefix.ToUpper());
    }

    public Type GetPrefixType(string prefix)
    {
        return _allPrefixNamesAndTypes[prefix.ToUpper()];
    }

    private readonly Dictionary<string, Type> _allPrefixNamesAndTypes = new()
    {
        { "REP", typeof(PrefixREP) }, { "REPE", typeof(PrefixREPE) }, { "REPNE", typeof(PrefixREPNE) }, { "REPNZ", typeof(PrefixREPNZ) }, { "REPZ", typeof(PrefixREPZ) }
    };
}