using EmuXCore.InstructionLogic.Interfaces;
using EmuXCore.InstructionLogic.Interfaces.Exceptions;
using EmuXCore.InstructionLogic.Prefixes;

namespace EmuXCore.InstructionLogic;

public class PrefixLookup : IPrefixLookup
{
    public bool DoesPrefixExist(string prefix)
    {
        return _allPrefixNamesAndTypes.ContainsKey(prefix.ToUpper());
    }

    public Type GetPrefixType(string prefix)
    {
        if (!_allPrefixNamesAndTypes.ContainsKey(prefix.ToUpper()))
        {
            throw new InvalidPrefixException($"A prefix with the name {prefix} does not exist");
        }

        return _allPrefixNamesAndTypes[prefix.ToUpper()];
    }

    private readonly Dictionary<string, Type> _allPrefixNamesAndTypes = new()
    {
        { "REP", typeof(PrefixREP) }, { "REPE", typeof(PrefixREPE) }, { "REPNE", typeof(PrefixREPNE) }, { "REPNZ", typeof(PrefixREPNZ) }, { "REPZ", typeof(PrefixREPZ) }
    };
}