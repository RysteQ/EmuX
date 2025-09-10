using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;

namespace EmuXCore.Interpreter.Encoder.Interfaces.Logic;

public interface IInstructionEncoder
{
    public IInstructionEncoderResult Parse(IList<IInstruction> instructionsToParse);
}