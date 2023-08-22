namespace EmuX.src.Services.Analyzer;

public class RegisterVerifier
{
    public static bool IsRegister(string token) => Is64BitRegister(token) | Is32BitRegister(token) | Is16BitRegister(token) | Is8BitRegister(token);

    public static bool Is64BitRegister(string token) => _64_bit_registers.Contains(token.ToUpper());

    public static bool Is32BitRegister(string token) => _32_bit_registers.Contains(token.ToUpper());

    public static bool Is16BitRegister(string token) => _16_bit_registers.Contains(token.ToUpper());

    public static bool Is8BitRegister(string token) => _8_bit_registers.Contains(token.ToUpper());

    private static readonly string[] _64_bit_registers =
    {
        "RAX", "RBX", "RCX", "RDX",
        "RSI", "RDI", "RSP", "RBP",
        "RIP", "R8", "R9", "R10",
        "R11", "R12", "R13", "R14",
        "R15"
    };

    private static readonly string[] _32_bit_registers =
    {
        "EAX", "EBX", "ECX", "EDX",
        "ESI", "EDI", "ESP", "EBP",
        "EIP", "R8D", "R9D", "R10D",
        "R11D", "R12D", "R13D", "R14D",
        "R15D"
    };

    private static readonly string[] _16_bit_registers =
    {
        "AX", "BX", "CX", "DX",
        "SI", "DI", "SP", "BP",
        "IP", "R8W", "R9W", "R10W",
        "R11W", "R12W", "R13W", "R14W",
        "R15W"
    };

    private static readonly string[] _8_bit_registers =
    {
        "AH", "AL", "BH", "BL",
        "CH", "CL", "DH", "DL",
        "SIL", "DIL", "SPL", "BPL",
        "R8B", "R9B", "R10B", "R11B",
        "R12B", "R13B", "R14B", "R15W"
    };
}
