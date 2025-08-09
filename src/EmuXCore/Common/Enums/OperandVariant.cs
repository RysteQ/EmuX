namespace EmuXCore.Common.Enums;

/// <summary>
/// Indicates what type the operand of any instruction is
/// </summary>
public enum OperandVariant : byte
{
    Value,
    Memory,
    Register,
    Label,
    NaN
}