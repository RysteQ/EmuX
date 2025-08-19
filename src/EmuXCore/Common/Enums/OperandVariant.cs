namespace EmuXCore.Common.Enums;

/// <summary>
/// Indicates what type the operand of any instruction is
/// </summary>
public enum OperandVariant : short
{
    Value = 1,
    Memory,
    Register,
    
    NaN = 0,
}