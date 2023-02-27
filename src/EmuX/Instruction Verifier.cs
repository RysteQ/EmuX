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

using static EmuX.Instruction_Data;

namespace EmuX
{
    class Verifier
    {
        /// <summary>
        /// Setter - Sets the instructions to verify
        /// </summary>
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
        public bool AreInstructionsValid()
        {
            return this.valid_instructions;
        }

        /// <summary>
        /// Getter - Returns the instruction index the error was found
        /// </summary>
        public int GetInstructionIndexError()
        {
            return this.instruction_index_error;
        }

        /// <summary>
        /// Getter - Returns a message that shows the instruction parameters and the allowed values of said instruction
        /// </summary>
        public string GetErrorMessage()
        {
            return this.error_message;
        }

        /// <summary>
        /// Checks if the variant of the instruction is acceptable or not
        /// </summary>
        private bool ContainsVariant(Instruction instruction)
        {
            // check if the instruction is a label or not
            if (instruction.instruction == Instruction_ENUM.LABEL)
                return true;

            Instruction_Variant_ENUM[] specific_instruction_variants = allowed_variants.AllInstructionVariants()[(int)instruction.instruction];

            return specific_instruction_variants.Contains<Instruction_Variant_ENUM>(instruction.variant);
        }

        /// <summary>
        /// Checks if the bitmode of the instruction is acceptable or not
        /// </summary>
        private bool ContainsBitmode(Instruction instruction)
        {
            // check if the instruction is a label or not
            if (instruction.instruction == Instruction_ENUM.LABEL)
                return true;

            Bit_Mode_ENUM[] specific_instruction_bitmodes = allowed_bitmodes.AllAllowedBitmodes()[(int)instruction.instruction];

            return specific_instruction_bitmodes.Contains<Bit_Mode_ENUM>(instruction.bit_mode);
        }

        private Allowed_Instruction_Variants allowed_variants = new Allowed_Instruction_Variants();
        private Allowed_Instruction_Bitmodes allowed_bitmodes = new Allowed_Instruction_Bitmodes();
        private List<Instruction> instructions = new List<Instruction>();
        private bool valid_instructions = true;
        private int instruction_index_error = 0;
        private string error_message = "";
    }

    class Allowed_Instruction_Variants
    {
        public Instruction_Variant_ENUM[][] AllInstructionVariants()
        {
            Instruction_Variant_ENUM[][] toReturn = new Instruction_Variant_ENUM[][]
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
                CWD_ALLOWED_VARIANTS,
                DAA_ALLOWED_VARIANTS,
                DAS_ALLOWED_VARIANTS,
                DEC_ALLOWED_VARIANTS,
                DIV_ALLOWED_VARIANTS,
                HLT_ALLOWED_VARIANTS,
                INC_ALLOWED_VARIANTS,
                INT_ALLOWED_VARIANTS,
                JA_ALLOWED_VARIANTS,
                JAE_ALLOWED_VARIANTS,
                JB_ALLOWED_VARIANTS,
                JBE_ALLOWED_VARIANTS,
                JC_ALLOWED_VARIANTS,
                JE_ALLOWED_VARIANTS,
                JG_ALLOWED_VARIANTS,
                JGE_ALLOWED_VARIANTS,
                JL_ALLOWED_VARIANTS,
                JLE_ALLOWED_VARIANTS,
                JNA_ALLOWED_VARIANTS,
                JNAE_ALLOWED_VARIANTS,
                JNB_ALLOWED_VARIANTS,
                JNBE_ALLOWED_VARIANTS,
                JNC_ALLOWED_VARIANTS,
                JNE_ALLOWED_VARIANTS,
                JNG_ALLOWED_VARIANTS,
                JNGE_ALLOWED_VARIANTS,
                JNL_ALLOWED_VARIANTS,
                JNLE_ALLOWED_VARIANTS,
                JNO_ALLOWED_VARIANTS,
                JNP_ALLOWED_VARIANTS,
                JNS_ALLOWED_VARIANTS,
                JNZ_ALLOWED_VARIANTS,
                JO_ALLOWED_VARIANTS,
                JP_ALLOWED_VARIANTS,
                JPE_ALLOWED_VARIANTS,
                JPO_ALLOWED_VARIANTS,
                JS_ALLOWED_VARIANTS, //
                JZ_ALLOWED_VARIANTS,
                JMP_ALLOWED_VARIANTS,
                LAHF_ALLOWED_VARIANTS,
                LEA_ALLOWED_VARIANTS,
                MOV_ALLOWED_VARIANTS,
                MUL_ALLOWED_VARIANTS,
                NEG_ALLOWED_VARIANTS,
                NOP_ALLOWED_VARIANTS,
                NOT_ALLOWED_VARIANTS,
                OR_ALLOWED_VARIANTS,
                POP_ALLOWED_VARIANTS,
                POPF_ALLOWED_VARIANTS,
                PUSH_ALLOWED_VARIANTS,
                PUSHF_ALLOWED_VARIANTS,
                RCL_ALLOWED_VARIANTS,
                RCR_ALLOWED_VARIANTS,
                RET_ALLOWED_VARIANTS,
                ROL_ALLOWED_VARIANTS,
                ROR_ALLOWED_VARIANTS,
                SAHF_ALLOWED_VARIANTS,
                SAL_ALLOWED_VARIANTS,
                SAR_ALLOWED_VARIANTS,
                SBB_ALLOWED_VARIANTS,
                SHL_ALLOWED_VARIANTS,
                SHR_ALLOWED_VARIANTS,
                STC_ALLOWED_VARIANTS,
                STD_ALLOWED_VARIANTS,
                STI_ALLOWED_VARIANTS,
                SUB_ALLOWED_VARIANTS,
                XOR_ALLOWED_VARIANTS
            };

            return toReturn;
        }

        Instruction_Variant_ENUM[] AAA_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[1]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] AAD_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] AAM_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] AAS_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] ADC_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        public readonly Instruction_Variant_ENUM[] ADD_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        public readonly Instruction_Variant_ENUM[] AND_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        public readonly Instruction_Variant_ENUM[] CALL_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] CBW_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] CLC_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] CLD_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] CLI_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] CMC_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] CMP_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE
        };

        /*
        public readonly Instruction_Data.Instruction_Variant_ENUM[] CMPSB_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };

        public readonly Instruction_Data.Instruction_Variant_ENUM[] CMPSW_ALLOWED_VARIANTS = new Instruction_Data.Instruction_Variant_ENUM[]
        {

        };
        */

        public readonly Instruction_Variant_ENUM[] CWD_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] DAA_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] DAS_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] DEC_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE,
            Instruction_Variant_ENUM.SINGLE_REGISTER
        };

        public readonly Instruction_Variant_ENUM[] DIV_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE,
            Instruction_Variant_ENUM.SINGLE_REGISTER
        };

        public readonly Instruction_Variant_ENUM[] HLT_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] INC_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE,
            Instruction_Variant_ENUM.SINGLE_REGISTER
        };

        public readonly Instruction_Variant_ENUM[] INT_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE_VALUE
        };

        public readonly Instruction_Variant_ENUM[] JCXZ_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JMP_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JA_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JAE_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JB_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JBE_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JC_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JE_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JG_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JGE_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JL_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JLE_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JNA_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JNAE_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JNB_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JNBE_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JNC_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JNE_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JNG_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JNGE_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JNL_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JNLE_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JNO_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JNP_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JNS_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JNZ_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JO_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JP_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JPE_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JPO_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JS_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] JZ_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.LABEL
        };

        public readonly Instruction_Variant_ENUM[] LAHF_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] LEA_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS
        };

        public readonly Instruction_Variant_ENUM[] MOV_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        public readonly Instruction_Variant_ENUM[] MUL_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE_REGISTER,
            Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE
        };

        public readonly Instruction_Variant_ENUM[] NEG_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE_REGISTER,
            Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE
        };

        public readonly Instruction_Variant_ENUM[] NOP_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] NOT_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE_REGISTER,
            Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE
        };

        public readonly Instruction_Variant_ENUM[] OR_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        public readonly Instruction_Variant_ENUM[] POP_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE_REGISTER,
            Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE
        };

        public readonly Instruction_Variant_ENUM[] POPF_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] PUSH_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE_REGISTER,
            Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE
        };

        public readonly Instruction_Variant_ENUM[] PUSHF_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] RCL_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        public readonly Instruction_Variant_ENUM[] RCR_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        public readonly Instruction_Variant_ENUM[] RET_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] ROL_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        public readonly Instruction_Variant_ENUM[] ROR_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        public readonly Instruction_Variant_ENUM[] SAHF_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] SAL_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        public readonly Instruction_Variant_ENUM[] SAR_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        public readonly Instruction_Variant_ENUM[] SBB_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        public readonly Instruction_Variant_ENUM[] SHL_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE_REGISTER,
            Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE
        };

        public readonly Instruction_Variant_ENUM[] SHR_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE_REGISTER,
            Instruction_Variant_ENUM.SINGLE_ADDRESS_VALUE
        };

        public readonly Instruction_Variant_ENUM[] STC_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] STD_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] STI_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.SINGLE
        };

        public readonly Instruction_Variant_ENUM[] SUB_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        public readonly Instruction_Variant_ENUM[] XOR_ALLOWED_VARIANTS = new Instruction_Variant_ENUM[]
        {
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Instruction_Variant_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Instruction_Variant_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };
    }

    class Allowed_Instruction_Bitmodes
    {
        public Bit_Mode_ENUM[][] AllAllowedBitmodes()
        {
            Bit_Mode_ENUM[][] toReturn = new Bit_Mode_ENUM[][]
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
                CWD_ALLOWED_BITMODES,
                DAA_ALLOWED_BITMODES,
                DAS_ALLOWED_BITMODES,
                DEC_ALLOWED_BITMODES,
                DIV_ALLOWED_BITMODES,
                HLT_ALLOWED_BITMODES,
                INC_ALLOWED_BITMODES,
                INT_ALLOWED_BITMODES,
                JA_ALLOWED_BITMODES,
                JAE_ALLOWED_BITMODES,
                JB_ALLOWED_BITMODES,
                JBE_ALLOWED_BITMODES,
                JC_ALLOWED_BITMODES,
                JE_ALLOWED_BITMODES,
                JG_ALLOWED_BITMODES,
                JGE_ALLOWED_BITMODES,
                JL_ALLOWED_BITMODES,
                JLE_ALLOWED_BITMODES,
                JNA_ALLOWED_BITMODES,
                JNAE_ALLOWED_BITMODES,
                JNB_ALLOWED_BITMODES,
                JNBE_ALLOWED_BITMODES,
                JNC_ALLOWED_BITMODES,
                JNE_ALLOWED_BITMODES,
                JNG_ALLOWED_BITMODES,
                JNGE_ALLOWED_BITMODES,
                JNL_ALLOWED_BITMODES,
                JNLE_ALLOWED_BITMODES,
                JNO_ALLOWED_BITMODES,
                JNP_ALLOWED_BITMODES,
                JNS_ALLOWED_BITMODES,
                JNZ_ALLOWED_BITMODES,
                JO_ALLOWED_BITMODES,
                JP_ALLOWED_BITMODES,
                JPE_ALLOWED_BITMODES,
                JPO_ALLOWED_BITMODES,
                JS_ALLOWED_BITMODES,
                JZ_ALLOWED_BITMODES,
                JMP_ALLOWED_BITMODES,
                LAHF_ALLOWED_BITMODES,
                LEA_ALLOWED_BITMODES,
                MOV_ALLOWED_BITMODES,
                MUL_ALLOWED_BITMODES,
                NEG_ALLOWED_BITMODES,
                NOP_ALLOWED_BITMODES,
                NOT_ALLOWED_BITMODES,
                OR_ALLOWED_BITMODES,
                POP_ALLOWED_BITMODES,
                POPF_ALLOWED_BITMODES,
                PUSH_ALLOWED_BITMODES,
                PUSHF_ALLOWED_BITMODES,
                RCL_ALLOWED_BITMODES,
                RCR_ALLOWED_BITMODES,
                RET_ALLOWED_BITMODES,
                ROL_ALLOWED_BITMODES,
                ROR_ALLOWED_BITMODES,
                SAHF_ALLOWED_BITMODES,
                SAL_ALLOWED_BITMODES,
                SAR_ALLOWED_BITMODES,
                SBB_ALLOWED_BITMODES,
                SHL_ALLOWED_BITMODES,
                SHR_ALLOWED_BITMODES,
                STC_ALLOWED_BITMODES,
                STD_ALLOWED_BITMODES,
                STI_ALLOWED_BITMODES,
                SUB_ALLOWED_BITMODES,
                WAIT_ALLOWED_BITMODES,
                XCHG_ALLOWED_BITMODES,
                XLAT_ALLOWED_BITMODES,
                XOR_ALLOWED_BITMODES
            };

            return toReturn;
        }

        public readonly Bit_Mode_ENUM[] AAA_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] AAD_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] AAM_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] AAS_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] ADC_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] ADD_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] AND_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] CALL_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] CBW_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] CLC_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] CLD_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] CLI_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] CMC_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] CMP_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        /*
        public readonly Instruction_Data.Bit_Mode_ENUM[] CMPSB_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };

        public readonly Instruction_Data.Bit_Mode_ENUM[] CMPSW_ALLOWED_BITMODES = new Instruction_Data.Bit_Mode_ENUM[]
        {

        };
        */

        public readonly Bit_Mode_ENUM[] CWD_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] DAA_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] DAS_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] DEC_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] DIV_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT
        };

        public readonly Bit_Mode_ENUM[] HLT_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] INC_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        // I am lazy to fix the actual bug that just gives the INT instruction a bitmode of NoN, this should do it tho
        public readonly Bit_Mode_ENUM[] INT_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT
        };

        public readonly Bit_Mode_ENUM[] JA_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JAE_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JB_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JBE_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JC_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JE_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JG_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JGE_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JL_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JLE_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JNA_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JNAE_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JNB_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JNBE_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JNC_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JNE_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JNG_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JNGE_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JNL_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JNLE_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JNO_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JNP_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JNS_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JNZ_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JO_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JP_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JPE_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JPO_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JS_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JZ_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] JMP_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] LAHF_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] LEA_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] MOV_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] MUL_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] NEG_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] NOP_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] NOT_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] OR_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] POP_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] POPF_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] PUSH_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] PUSHF_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] RCL_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT

        };

        public readonly Bit_Mode_ENUM[] RCR_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT

        };

        public readonly Bit_Mode_ENUM[] RET_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] ROL_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] ROR_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] SAHF_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] SAL_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] SAR_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] SBB_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] SHL_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] SHR_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] STC_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] STD_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] STI_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] SUB_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] WAIT_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] XCHG_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };

        public readonly Bit_Mode_ENUM[] XLAT_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM.NoN
        };

        public readonly Bit_Mode_ENUM[] XOR_ALLOWED_BITMODES = new Bit_Mode_ENUM[]
        {
            Bit_Mode_ENUM._8_BIT,
            Bit_Mode_ENUM._16_BIT,
            Bit_Mode_ENUM._32_BIT,
            Bit_Mode_ENUM._64_BIT
        };
    }
}