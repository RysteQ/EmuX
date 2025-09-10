using EmuXCore.Common.Enums;
using EmuXCore.Interpreter.RCVM.Enums;
using EmuXCore.Interpreter.RCVM.Interfaces.Models;

namespace EmuXCore.Interpreter.RCVM.Models;

public class VmAction : IVmAction
{
    public VmAction(VmActionCategory action, Size size, byte[] previousValue, string? registerName = null, int? memoryPointer = null)
    {
        Action = action;
        Size = size;
        RegisterName = registerName;
        MemoryPointer = memoryPointer;
        PreviousValue = previousValue;
    }

    public VmActionCategory Action { get; init; }
    public Size Size { get; init; }
    public string? RegisterName { get; init; }
    public int? MemoryPointer { get; init; }
    public byte[] PreviousValue { get; init; }
}