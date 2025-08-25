using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Interfaces.Models;

namespace EmuXCore.Interpreter.Interfaces.Logic;

public interface IInstructionEncoder
{
    public IInstructionEncoderResult Parse(IList<IInstruction> instructionsToParse);
}