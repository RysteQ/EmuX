namespace EmuXCore.VM.Internal.CPU.Enums;

public enum EFlags : int
{
    CF = 1 << 0,
    PF = 1 << 2,
    AF = 1 << 4,
    ZF = 1 << 6,
    SF = 1 << 7,
    TF = 1 << 8,
    IF = 1 << 9,
    DF = 1 << 10,
    OF = 1 << 11,
    IOPL = 0b_0000_0000_0000_0000_0011_0000_0000_0000,
    NT = 1 << 14,
    RF = 1 << 16,
    VM = 1 << 17,
    AC = 1 << 18,
    VIF = 1 << 19,
    VIP = 1 << 20,
    ID = 1 << 21
}