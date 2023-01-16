using System;

namespace EmuX
{
    // TODO
    class Instruction_Verifier
    {
        /// <summary>
        /// Setter - Sets the instructions to verify
        /// </summary>
        /// <param name="instructions">The instructions to verify</param>
        public void SetInstructionData(List<Instruction> instructions)
        {
            this.instructions = instructions;
        }

        /// <summary>
        /// Getter - Verifies the instructions
        /// </summary>
        /// <returns>True if the instructions are correct, flase if they are not</returns>
        public bool VerifyInstructions()
        {
            for (int index = 0; index < this.instructions.Count; index++)
            {
                // TODO
            }

            return true;
        }

        /// <summary>
        /// Getter - Returns the instruction index the error was found
        /// </summary>
        /// <returns>An integer value</returns>
        public int GetInstructionIndexError()
        {
            return this.instruction_index_error;
        }

        /// <summary>
        /// Getter - Returns a message that shows the instruction parameters and the allowed values of said instruction
        /// </summary>
        /// <returns>A string value</returns>
        public string GetErrorMessage()
        {
            string toReturn = "";

            // TODO

            return toReturn;
        }

        Allowed_Instruction_Variants allowed_variants = new Allowed_Instruction_Variants();
        Allowed_Instruction_Bitmodes allowed_bitmodes = new Allowed_Instruction_Bitmodes();
        List<Instruction> instructions;
        int instruction_index_error = 0;
    }

    // TODO
    class Allowed_Instruction_Variants
    {
        public readonly Instruction_Data.Bit_Mode_ENUM[] AAA_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
{

};

        public readonly Instruction_Data.Bit_Mode_ENUM[] AAD_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] AAM_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] AAS_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] ADC_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] ADD_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] AND_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CALL_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CBW_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CLC_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CLD_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CLI_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CMC_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CMP_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CMPSB_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CMPSW_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CWD_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] DAA_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] DAS_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] DEC_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] DIV_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] ESC_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] HLT_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] IDIV_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] IMUL_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] IN_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] INC_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] INT_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] INTO_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] IRET_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] Jcc_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] JCXZ_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] JMP_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] LAHF_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] LDS_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] LEA_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] LES_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] LOCK_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] LODSB_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] LODSW_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] MOV_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] MOVSB_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] MOVSW_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] MUL_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] NEG_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] NOP_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] NOT_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] OR_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] OUT_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] POP_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] POPF_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] PUSH_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] PUSHF_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] RCL_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] RCR_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] REPxx_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] RET_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] RETN_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] RETF_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] ROL_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] ROR_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SAHF_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SAL_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SAR_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SBB_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SCASB_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SCASW_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SHL_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SHR_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] STC_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] STD_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] STI_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] STOSB_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] STOSW_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SUB_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] WAIT_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] XCHG_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] XLAT_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] XOR_ALLOWED_VARIANTS = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };
    }

    class Allowed_Instruction_Bitmodes
    {
        public readonly Instruction_Data.Bit_Mode_ENUM[] AAA_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
{

};

        public readonly Instruction_Data.Bit_Mode_ENUM[] AAD_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] AAM_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] AAS_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] ADC_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] ADD_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] AND_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CALL_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CBW_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CLC_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CLD_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CLI_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CMC_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CMP_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CMPSB_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CMPSW_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CWD_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] DAA_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] DAS_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] DEC_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] DIV_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] ESC_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] HLT_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] IDIV_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] IMUL_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] IN_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] INC_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] INT_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] INTO_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] IRET_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] Jcc_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] JCXZ_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] JMP_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] LAHF_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] LDS_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] LEA_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] LES_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] LOCK_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] LODSB_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] LODSW_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] MOV_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] MOVSB_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] MOVSW_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] MUL_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] NEG_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] NOP_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] NOT_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] OR_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] OUT_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] POP_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] POPF_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] PUSH_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] PUSHF_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] RCL_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] RCR_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] REPxx_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] RET_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] RETN_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] RETF_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] ROL_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] ROR_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SAHF_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SAL_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SAR_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SBB_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SCASB_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SCASW_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SHL_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SHR_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] STC_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] STD_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] STI_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] STOSB_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] STOSW_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] SUB_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] WAIT_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] XCHG_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] XLAT_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] XOR_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };
    }
}