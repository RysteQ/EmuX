namespace EmuXCore.InstructionLogic.Interfaces;

public interface IInstructionLookup
{
    public bool DoesInstructionExist(string instruction);
    public Type GetInstructionType(string instruction);
}