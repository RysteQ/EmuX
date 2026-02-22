using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Interfaces;

namespace EmuXCore.InstructionLogic;

public class InstructionLookup : IInstructionLookup
{
    public bool DoesInstructionExist(string instruction)
    {
        return _allInstructionNamesAndTypes.ContainsKey(instruction.ToUpper());
    }

    public Type GetInstructionType(string instruction)
    {
        return _allInstructionNamesAndTypes[instruction.ToUpper()];
    }

    private readonly Dictionary<string, Type> _allInstructionNamesAndTypes = new()
    {
        { "AAA", typeof(InstructionAAA) }, { "AAD", typeof(InstructionAAD) }, { "AAM", typeof(InstructionAAM) }, { "AAS", typeof(InstructionAAS) }, { "ADC", typeof(InstructionADC) },
        { "ADD", typeof(InstructionADD) }, { "AND", typeof(InstructionAND) }, { "CALL", typeof(InstructionCALL) }, { "CBW", typeof(InstructionCBW) }, { "CDQE", typeof(InstructionCDQE) },
        { "CWDE", typeof(InstructionCWDE) }, { "CLC", typeof(InstructionCLC) }, { "CLD", typeof(InstructionCLD) }, { "CLI", typeof(InstructionCLI) }, { "CMC", typeof(InstructionCMC) },
        { "CMP", typeof(InstructionCMP) }, { "CMPSB", typeof(InstructionCMPSB) }, { "CMPSW", typeof(InstructionCMPSW) }, { "CWD", typeof(InstructionCWD) }, { "CDQ", typeof(InstructionCDQ) },
        { "DAA", typeof(InstructionDAA) }, { "DAS", typeof(InstructionDAS) }, { "DEC", typeof(InstructionDEC) }, { "DIV", typeof(InstructionDIV) }, { "HLT", typeof(InstructionHLT) },
        { "IDIV", typeof(InstructionIDIV) }, { "IMUL", typeof(InstructionIMUL) }, { "IN", typeof(InstructionIN) }, { "INC", typeof(InstructionINC) }, { "INT", typeof(InstructionINT) },
        { "INTO", typeof(InstructionINTO) }, { "JA", typeof(InstructionJA) }, { "JAE", typeof(InstructionJAE) }, { "JB", typeof(InstructionJB) }, { "JBE", typeof(InstructionJBE) },
        { "JC", typeof(InstructionJC) }, { "JE", typeof(InstructionJE) }, { "JG", typeof(InstructionJG) }, { "JGE", typeof(InstructionJGE) }, { "JL", typeof(InstructionJL) },
        { "JLE", typeof(InstructionJLE) }, { "JMP", typeof(InstructionJMP) }, { "JNA", typeof(InstructionJNA) }, { "JNAE", typeof(InstructionJNAE) }, { "JNB", typeof(InstructionJNB) },
        { "JNBE", typeof(InstructionJNBE) }, { "JNC", typeof(InstructionJNC) }, { "JNE", typeof(InstructionJNE) }, { "JNG", typeof(InstructionJNG) }, { "JNGE", typeof(InstructionJNGE) },
        { "JNL", typeof(InstructionJNL) }, { "JNLE", typeof(InstructionJNLE) }, { "JNO", typeof(InstructionJNO) }, { "JNP", typeof(InstructionJNO) }, { "JNS", typeof(InstructionJNS) },
        { "JNZ", typeof(InstructionJNZ) }, { "JO", typeof(InstructionJO) }, { "JP", typeof(InstructionJP) }, { "JPE", typeof(InstructionJPE) }, { "JPO", typeof(InstructionJPO) },
        { "JS", typeof(InstructionJS) }, { "JZ", typeof(InstructionJZ) }, { "LAHF", typeof(InstructionLAHF) }, { "LDS", typeof(InstructionLDS) }, { "LEA", typeof(InstructionLEA) },
        { "LES", typeof(InstructionLES) }, { "LODSB", typeof(InstructionLODSB) }, { "LODSW", typeof(InstructionLODSW) }, { "LOOP", typeof(InstructionLOOP) }, { "MOV", typeof(InstructionMOV) },
        { "MOVSB", typeof(InstructionMOVSB) }, { "MOVSW", typeof(InstructionMOVSW) }, { "MUL", typeof(InstructionMUL) }, { "NEG", typeof(InstructionNEG) }, { "NOT", typeof(InstructionNOT) },
        { "NOP", typeof(InstructionNOP) },
        { "OR", typeof(InstructionOR) }, { "OUT", typeof(InstructionOUT) }, { "POP", typeof(InstructionPOP) }, { "POPF", typeof(InstructionPOPF) }, { "POPFD", typeof(InstructionPOPFD) },
        { "POPFQ", typeof(InstructionPOPFQ) }, { "PUSH", typeof(InstructionPUSH) }, { "PUSHF", typeof(InstructionPUSHF) }, { "PUSHFD", typeof(InstructionPUSHFD) }, { "PUSHFQ", typeof(InstructionPUSHFQ) },
        { "RCL", typeof(InstructionRCL) }, { "RCR", typeof(InstructionRCR) }, { "RET", typeof(InstructionRET) }, { "ROL", typeof(InstructionROL) }, { "ROR", typeof(InstructionROR) },
        { "SAHF", typeof(InstructionSAHF) }, { "SAL", typeof(InstructionSAL) }, { "SAR", typeof(InstructionSAR) }, { "SBB", typeof(InstructionSBB) }, { "SCASB", typeof(InstructionSCASB) },
        { "SCASW", typeof(InstructionSCASW) }, { "SHL", typeof(InstructionSHL) }, { "SHR", typeof(InstructionSHR) }, { "STC", typeof(InstructionSTC) }, { "STD", typeof(InstructionSTD) },
        { "STI", typeof(InstructionSTI) }, { "STOSB", typeof(InstructionSTOSB) }, { "STOSW", typeof(InstructionSTOSW) }, { "SUB", typeof(InstructionSUB) }, { "TEST", typeof(InstructionTEST) },
        { "XCHG", typeof(InstructionXCHG) }, { "XLAT", typeof(InstructionXLAT) }, { "XOR", typeof(InstructionXOR) },
    };
}
