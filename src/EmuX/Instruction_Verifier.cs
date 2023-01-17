using System;
using System.Linq;

/*
 * For those unfortunate to stumble across this file, let me warn you
 * 
 * What you are about to see is some really stupid code
 * 
 * I did not write all of the code, I automated the process with a python script I wrote
 * 
 * But this still does not erase this monstrosity, this.... SIN.... that I have just committed
 * 
 * I am sorry, may He have mercy upon my soul
 * 
 */

namespace EmuX
{
    class Verifier
    {
        /// <summary>
        /// Setter - Sets the instructions to verify
        /// </summary>
        /// <param name="instructions">The instructions to verify</param>
        public void SetInstructionData(List<Instruction> instructions)
        {
            this.instructions = instructions;
            this.valid_instructions = true;
        }

        /// <summary>
        /// Verifies the instructions variant and bitmode
        /// </summary>
        public void VerifyInstructions()
        {
            // check each instruction if it is valid
            for (int index = 0; index < this.instructions.Count && this.valid_instructions; index++)
            {
                Instruction instruction_to_analyze = this.instructions[index];

                // check for the variant
                if (ContainsVariant(instruction_to_analyze) == false)
                {
                    this.instruction_index_error = index;
                    this.valid_instructions = false;
                }

                // check for the bitmode
                if (ContainsBitmode(instruction_to_analyze) == false)
                {
                    this.instruction_index_error = index;
                    this.valid_instructions = false;
                }
            }
        }

        /// <summary>
        /// Getter
        /// </summary>
        /// <returns>A true value if the instructions are valid, a false value if they are not</returns>
        public bool AreInstructionsValid()
        {
            return this.valid_instructions;
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

        /// <summary>
        /// Checks if the variant of the instruction is acceptable or not
        /// </summary>
        /// <param name="instruction">The instruction to analyze</param>
        /// <returns>Returns true if the instruction has the correct variant, false if it doesn't</returns>
        private bool ContainsVariant(Instruction instruction)
        {
            Instruction_Data.Instruction_Variant_ENUM[] specific_instruction_variants = allowed_variants.AllInstructionVariants()[(int) instruction.instruction];

            if (specific_instruction_variants.Contains<Instruction_Data.Instruction_Variant_ENUM>(instruction.variant))
                return false;

            return true;
        }

        /// <summary>
        /// Checks if the bitmode of the instruction is acceptable or not
        /// </summary>
        /// <param name="instruction">The instruction to analyze</param>
        /// <returns>Returns true if the instruction has the correct bitmode, false if it doesn't</returns>
        private bool ContainsBitmode(Instruction instruction)
        {
            Instruction_Data.Bit_Mode_ENUM[] specific_instruction_bitmodes = allowed_bitmodes.AllAllowedBitmodes()[(int)instruction.instruction];

            if (specific_instruction_bitmodes.Contains<Instruction_Data.Bit_Mode_ENUM>(instruction.bit_mode))
                return false;

            return true;
        }

        private Allowed_Instruction_Variants allowed_variants = new Allowed_Instruction_Variants();
        private Allowed_Instruction_Bitmodes allowed_bitmodes = new Allowed_Instruction_Bitmodes();
        private List<Instruction> instructions = new List<Instruction>();
        private bool valid_instructions = true;
        private int instruction_index_error = 0;
    }

    // TODO
    class Allowed_Instruction_Variants
    {
        public Instruction_Data.Instruction_Variant_ENUM[][] AllInstructionVariants()
        {
            Instruction_Data.Instruction_Variant_ENUM[][] toReturn = new Instruction_Data.Instruction_Variant_ENUM[][]
            {
                AAA_ALLOWED_VARIANTS,
                AAD_ALLOWED_VARIANTS,
                AAM_ALLOWED_VARIANTS,
                AAS_ALLOWED_VARIANTS,
                ADC_ALLOWED_VARIANTS,
                ADD_ALLOWED_VARIANTS,
                AND_ALLOWED_VARIANTS,
                CALL_ALLOWED_VARIANTS,
                CBW_ALLOWED_VARIANTS,
                CLC_ALLOWED_VARIANTS,
                CLD_ALLOWED_VARIANTS,
                CLI_ALLOWED_VARIANTS,
                CMC_ALLOWED_VARIANTS,
                CMP_ALLOWED_VARIANTS,
                // CMPSB_ALLOWED_VARIANTS,
                // CMPSW_ALLOWED_VARIANTS,
                CWD_ALLOWED_VARIANTS,
                DAA_ALLOWED_VARIANTS,
                DAS_ALLOWED_VARIANTS,
                DEC_ALLOWED_VARIANTS,
                DIV_ALLOWED_VARIANTS,
                HLT_ALLOWED_VARIANTS,
                IDIV_ALLOWED_VARIANTS,
                IMUL_ALLOWED_VARIANTS,
                IN_ALLOWED_VARIANTS,
                INC_ALLOWED_VARIANTS,
                INT_ALLOWED_VARIANTS,
                INTO_ALLOWED_VARIANTS,
                IRET_ALLOWED_VARIANTS,
                Jcc_ALLOWED_VARIANTS,
                JCXZ_ALLOWED_VARIANTS,
                JMP_ALLOWED_VARIANTS,
                LAHF_ALLOWED_VARIANTS,
                LDS_ALLOWED_VARIANTS,
                LEA_ALLOWED_VARIANTS,
                LES_ALLOWED_VARIANTS,
                LOCK_ALLOWED_VARIANTS,
                LODSB_ALLOWED_VARIANTS,
                LODSW_ALLOWED_VARIANTS,
                MOV_ALLOWED_VARIANTS,
                MOVSB_ALLOWED_VARIANTS,
                MOVSW_ALLOWED_VARIANTS,
                MUL_ALLOWED_VARIANTS,
                NEG_ALLOWED_VARIANTS,
                NOP_ALLOWED_VARIANTS,
                NOT_ALLOWED_VARIANTS,
                OR_ALLOWED_VARIANTS,
                OUT_ALLOWED_VARIANTS,
                POP_ALLOWED_VARIANTS,
                POPF_ALLOWED_VARIANTS,
                PUSH_ALLOWED_VARIANTS,
                PUSHF_ALLOWED_VARIANTS,
                RCL_ALLOWED_VARIANTS,
                RCR_ALLOWED_VARIANTS,
                REPxx_ALLOWED_VARIANTS,
                RET_ALLOWED_VARIANTS,
                RETN_ALLOWED_VARIANTS,
                RETF_ALLOWED_VARIANTS,
                ROL_ALLOWED_VARIANTS,
                ROR_ALLOWED_VARIANTS,
                SAHF_ALLOWED_VARIANTS,
                SAL_ALLOWED_VARIANTS,
                SAR_ALLOWED_VARIANTS,
                SBB_ALLOWED_VARIANTS,
                SCASB_ALLOWED_VARIANTS,
                SCASW_ALLOWED_VARIANTS,
                SHL_ALLOWED_VARIANTS,
                SHR_ALLOWED_VARIANTS,
                STC_ALLOWED_VARIANTS,
                STD_ALLOWED_VARIANTS,
                STI_ALLOWED_VARIANTS,
                STOSB_ALLOWED_VARIANTS,
                STOSW_ALLOWED_VARIANTS,
                SUB_ALLOWED_VARIANTS,
                WAIT_ALLOWED_VARIANTS,
                XCHG_ALLOWED_VARIANTS,
                XLAT_ALLOWED_VARIANTS,
                XOR_ALLOWED_VARIANTS
            };

            return toReturn;
        }

        Instruction_Data.Instruction_Variant_ENUM[] AAA_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[1]
        {
            Instruction_Data.Instruction_Variant_ENUM.SINGLE_VALUE
        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] AAD_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] AAM_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] AAS_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] ADC_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] ADD_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] AND_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] CALL_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] CBW_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] CLC_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] CLD_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] CLI_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] CMC_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] CMP_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        /*
        public readonly Instruction_Data.Instruction_Variant_ENUM[] CMPSB_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] CMPSW_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };
        */

        public readonly Instruction_Data.Instruction_Variant_ENUM[] CWD_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] DAA_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] DAS_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] DEC_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] DIV_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] HLT_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] IDIV_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] IMUL_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] IN_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] INC_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] INT_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] INTO_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] IRET_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] Jcc_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] JCXZ_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] JMP_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] LAHF_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] LDS_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] LEA_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] LES_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] LOCK_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] LODSB_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] LODSW_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] MOV_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] MOVSB_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] MOVSW_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] MUL_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] NEG_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] NOP_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] NOT_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] OR_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] OUT_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] POP_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] POPF_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] PUSH_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] PUSHF_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] RCL_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] RCR_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] REPxx_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] RET_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] RETN_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] RETF_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] ROL_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] ROR_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] SAHF_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] SAL_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] SAR_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] SBB_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] SCASB_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] SCASW_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] SHL_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] SHR_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] STC_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] STD_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] STI_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] STOSB_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] STOSW_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] SUB_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] WAIT_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] XCHG_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] XLAT_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] XOR_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };
    }

    class Allowed_Instruction_Bitmodes
    {
        public Instruction_Data.Bit_Mode_ENUM[][] AllAllowedBitmodes()
        {
            Instruction_Data.Bit_Mode_ENUM[][] toReturn = new Instruction_Data.Bit_Mode_ENUM[][]
            {
                AAA_ALLOWED_BITMODES,
                AAD_ALLOWED_BITMODES,
                AAM_ALLOWED_BITMODES,
                AAS_ALLOWED_BITMODES,
                ADC_ALLOWED_BITMODES,
                ADD_ALLOWED_BITMODES,
                AND_ALLOWED_BITMODES,
                CALL_ALLOWED_BITMODES,
                CBW_ALLOWED_BITMODES,
                CLC_ALLOWED_BITMODES,
                CLD_ALLOWED_BITMODES,
                CLI_ALLOWED_BITMODES,
                CMC_ALLOWED_BITMODES,
                CMP_ALLOWED_BITMODES,
                // CMPSB_ALLOWED_BITMODES,
                // CMPSW_ALLOWED_BITMODES,
                CWD_ALLOWED_BITMODES,
                DAA_ALLOWED_BITMODES,
                DAS_ALLOWED_BITMODES,
                DEC_ALLOWED_BITMODES,
                DIV_ALLOWED_BITMODES,
                HLT_ALLOWED_BITMODES,
                IDIV_ALLOWED_BITMODES,
                IMUL_ALLOWED_BITMODES,
                IN_ALLOWED_BITMODES,
                INC_ALLOWED_BITMODES,
                INT_ALLOWED_BITMODES,
                INTO_ALLOWED_BITMODES,
                IRET_ALLOWED_BITMODES,
                Jcc_ALLOWED_BITMODES,
                JCXZ_ALLOWED_BITMODES,
                JMP_ALLOWED_BITMODES,
                LAHF_ALLOWED_BITMODES,
                LDS_ALLOWED_BITMODES,
                LEA_ALLOWED_BITMODES,
                LES_ALLOWED_BITMODES,
                LOCK_ALLOWED_BITMODES,
                LODSB_ALLOWED_BITMODES,
                LODSW_ALLOWED_BITMODES,
                MOV_ALLOWED_BITMODES,
                MOVSB_ALLOWED_BITMODES,
                MOVSW_ALLOWED_BITMODES,
                MUL_ALLOWED_BITMODES,
                NEG_ALLOWED_BITMODES,
                NOP_ALLOWED_BITMODES,
                NOT_ALLOWED_BITMODES,
                OR_ALLOWED_BITMODES,
                OUT_ALLOWED_BITMODES,
                POP_ALLOWED_BITMODES,
                POPF_ALLOWED_BITMODES,
                PUSH_ALLOWED_BITMODES,
                PUSHF_ALLOWED_BITMODES,
                RCL_ALLOWED_BITMODES,
                RCR_ALLOWED_BITMODES,
                REPxx_ALLOWED_BITMODES,
                RET_ALLOWED_BITMODES,
                RETN_ALLOWED_BITMODES,
                RETF_ALLOWED_BITMODES,
                ROL_ALLOWED_BITMODES,
                ROR_ALLOWED_BITMODES,
                SAHF_ALLOWED_BITMODES,
                SAL_ALLOWED_BITMODES,
                SAR_ALLOWED_BITMODES,
                SBB_ALLOWED_BITMODES,
                SCASB_ALLOWED_BITMODES,
                SCASW_ALLOWED_BITMODES,
                SHL_ALLOWED_BITMODES,
                SHR_ALLOWED_BITMODES,
                STC_ALLOWED_BITMODES,
                STD_ALLOWED_BITMODES,
                STI_ALLOWED_BITMODES,
                STOSB_ALLOWED_BITMODES,
                STOSW_ALLOWED_BITMODES,
                SUB_ALLOWED_BITMODES,
                WAIT_ALLOWED_BITMODES,
                XCHG_ALLOWED_BITMODES,
                XLAT_ALLOWED_BITMODES,
                XOR_ALLOWED_BITMODES
            };

            return toReturn;
        }

        public readonly Instruction_Data.Bit_Mode_ENUM[] AAA_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM.NoN
        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] AAD_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM.NoN
        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] AAM_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM.NoN
        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] AAS_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM.NoN
        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] ADC_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM._8_BIT,
            Instruction_Data.Bit_Mode_ENUM._16_BIT,
            Instruction_Data.Bit_Mode_ENUM._32_BIT
        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] ADD_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM._8_BIT,
            Instruction_Data.Bit_Mode_ENUM._16_BIT,
            Instruction_Data.Bit_Mode_ENUM._32_BIT,
            Instruction_Data.Bit_Mode_ENUM._64_BIT
        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] AND_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM._8_BIT,
            Instruction_Data.Bit_Mode_ENUM._16_BIT,
            Instruction_Data.Bit_Mode_ENUM._32_BIT,
            Instruction_Data.Bit_Mode_ENUM._64_BIT
        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CALL_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM.NoN
        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CBW_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM.NoN
        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CLC_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM.NoN
        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CLD_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM.NoN
        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CLI_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM.NoN
        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CMC_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM.NoN
        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CMP_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM._8_BIT,
            Instruction_Data.Bit_Mode_ENUM._16_BIT,
            Instruction_Data.Bit_Mode_ENUM._32_BIT,
            Instruction_Data.Bit_Mode_ENUM._64_BIT
        };

        /*
        public readonly Instruction_Data.Bit_Mode_ENUM[] CMPSB_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CMPSW_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };
        */

        public readonly Instruction_Data.Bit_Mode_ENUM[] CWD_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM.NoN
        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] DAA_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM.NoN
        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] DAS_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM.NoN
        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] DEC_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM._8_BIT,
            Instruction_Data.Bit_Mode_ENUM._16_BIT,
            Instruction_Data.Bit_Mode_ENUM._32_BIT,
            Instruction_Data.Bit_Mode_ENUM._64_BIT
        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] DIV_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {
            Instruction_Data.Bit_Mode_ENUM._8_BIT,
            Instruction_Data.Bit_Mode_ENUM._16_BIT,
            Instruction_Data.Bit_Mode_ENUM._32_BIT,
            Instruction_Data.Bit_Mode_ENUM._64_BIT
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