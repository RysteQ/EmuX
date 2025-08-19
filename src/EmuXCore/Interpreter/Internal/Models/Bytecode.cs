using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Interfaces.Models;

namespace EmuXCore.Interpreter.Internal.Models;

public record Bytecode : IBytecode
{
    public Bytecode(IInstruction? instruction = null, ILabel? label = null)
    {
        if ((instruction == null && label == null) || (instruction != null && label != null))
        {
            throw new ArgumentNullException("Only one parameter can be null");
        }

        Instruction = instruction;
        Label = label;
    }

    public Type Type { get => Instruction != null ? typeof(IInstruction) : typeof(ILabel); }

    public IInstruction? Instruction { get; init; }
    public ILabel? Label { get; init; }
}