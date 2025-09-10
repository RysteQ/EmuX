namespace EmuXCore.InstructionLogic.Interfaces;

public interface IPrefixLookup
{
    public bool DoesPrefixExist(string prefix);
    public Type GetPrefixType(string prefix);
}
