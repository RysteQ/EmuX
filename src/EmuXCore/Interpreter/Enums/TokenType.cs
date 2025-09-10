namespace EmuXCore.Interpreter.Enums;

/// <summary>
/// Indicated what type the lexer token is
/// </summary>
public enum TokenType : byte
{
    // Start of an instruction
    PREFIX,
    INSTRUCTION,

    // Generic operands
    REGISTER,
    VALUE,
    LABEL,
    POINTER,

    // Memory related
    OPEN_BRACKET,
    ADDITION,
    SUBTRACTION,
    SCALE,
    CLOSE_BRACKET,

    // Miscelenious stuff
    SIZE,
    COMMA,
    COLON,
    EOL,
    EOF,

    // Internal usage
    NaN
}