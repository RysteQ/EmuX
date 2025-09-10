using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCore.VM.Interfaces;

namespace EmuXCore.Interpreter;

public class Interpreter : IInterpreter
{
    // TODO
    public IVirtualMachine VirtualMachine { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IList<IInstruction> Instructions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IList<ILabel> Labels { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IInstruction CurrentInstruction => throw new NotImplementedException();

    public int CurrentInstructionIndex => throw new NotImplementedException();

    public void Execute()
    {
        throw new NotImplementedException();
    }

    public void ExecuteStep()
    {
        throw new NotImplementedException();
    }

    public void ResetExecution()
    {
        throw new NotImplementedException();
    }
}