using EmuX.src.Models;

namespace EmuX.src.Services.Analyzer
{
    public class Instruction_Verifier
    {
        public static bool VerifyInstructions(List<Instruction> instructions)
        {
            foreach (Instruction instruction in instructions)
            {
                if (ContainsVariant(instruction) == false)
                    return false;

                if (ContainsBitmode(instruction) == false)
                    return false;
            }

            return true;
        }

        private static bool ContainsVariant(Instruction instruction)
        {
            // check if the instruction is a label or not
            if (instruction.opcode == Opcodes.Opcodes_ENUM.LABEL)
                return true;

            Variants.Variants_ENUM[] specific_instruction_variants = Allowed_Instruction_Variants.AllInstructionVariants()[(int)instruction.opcode];

            return specific_instruction_variants.Contains(instruction.variant);
        }

        private static bool ContainsBitmode(Instruction instruction)
        {
            // check if the instruction is a label or not
            if (instruction.opcode == Opcodes.Opcodes_ENUM.LABEL)
                return true;

            SIZE.Size_ENUM[] specific_instruction_bitmodes = Allowed_Instruction_Bitmodes.AllAllowedBitmodes()[(int)instruction.opcode];

            return specific_instruction_bitmodes.Contains(instruction.bit_mode);
        }
    }

    public class Allowed_Instruction_Variants : Variants
    {
        public static Variants_ENUM[][] AllInstructionVariants()
        {
            Variants_ENUM[][] toReturn = new Variants_ENUM[][]
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
                JS_ALLOWED_VARIANTS,
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
                XOR_ALLOWED_VARIANTS,
                LABEL_ALLOWED_VARIANTS
            };

            return toReturn;
        }

        private static readonly Variants_ENUM[] AAA_ALLOWED_VARIANTS = new Variants_ENUM[1]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] AAD_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] AAM_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] AAS_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] ADC_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        private static readonly Variants_ENUM[] ADD_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        private static readonly Variants_ENUM[] AND_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        private static readonly Variants_ENUM[] CALL_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] CBW_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] CLC_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] CLD_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] CLI_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] CMC_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] CMP_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE
        };

        /*
        private static readonly Instruction_Data.Variants_ENUM[] CMPSB_ALLOWED_VARIANTS = new Instruction_Data.Variants_ENUM[]
        {

        };

        private static readonly Instruction_Data.Variants_ENUM[] CMPSW_ALLOWED_VARIANTS = new Instruction_Data.Variants_ENUM[]
        {

        };
        */

        private static readonly Variants_ENUM[] CWD_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] DAA_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] DAS_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] DEC_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE_ADDRESS_VALUE,
            Variants_ENUM.SINGLE_REGISTER
        };

        private static readonly Variants_ENUM[] DIV_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE_ADDRESS_VALUE,
            Variants_ENUM.SINGLE_REGISTER
        };

        private static readonly Variants_ENUM[] HLT_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] INC_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE_ADDRESS_VALUE,
            Variants_ENUM.SINGLE_REGISTER
        };

        private static readonly Variants_ENUM[] INT_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE_VALUE
        };

        private static readonly Variants_ENUM[] JCXZ_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JMP_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JA_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JAE_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JB_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JBE_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JC_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JE_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JG_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JGE_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JL_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JLE_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JNA_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JNAE_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JNB_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JNBE_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JNC_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JNE_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JNG_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JNGE_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JNL_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JNLE_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JNO_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JNP_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JNS_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JNZ_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JO_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JP_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JPE_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JPO_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JS_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] JZ_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.LABEL
        };

        private static readonly Variants_ENUM[] LAHF_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] LEA_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS
        };

        private static readonly Variants_ENUM[] MOV_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        private static readonly Variants_ENUM[] MUL_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE_REGISTER,
            Variants_ENUM.SINGLE_ADDRESS_VALUE
        };

        private static readonly Variants_ENUM[] NEG_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE_REGISTER,
            Variants_ENUM.SINGLE_ADDRESS_VALUE
        };

        private static readonly Variants_ENUM[] NOP_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] NOT_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE_REGISTER,
            Variants_ENUM.SINGLE_ADDRESS_VALUE
        };

        private static readonly Variants_ENUM[] OR_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        private static readonly Variants_ENUM[] POP_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE_REGISTER,
            Variants_ENUM.SINGLE_ADDRESS_VALUE
        };

        private static readonly Variants_ENUM[] POPF_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] PUSH_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE_REGISTER,
            Variants_ENUM.SINGLE_ADDRESS_VALUE
        };

        private static readonly Variants_ENUM[] PUSHF_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] RCL_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        private static readonly Variants_ENUM[] RCR_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        private static readonly Variants_ENUM[] RET_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] ROL_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        private static readonly Variants_ENUM[] ROR_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        private static readonly Variants_ENUM[] SAHF_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] SAL_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        private static readonly Variants_ENUM[] SAR_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        private static readonly Variants_ENUM[] SBB_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        private static readonly Variants_ENUM[] SHL_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        private static readonly Variants_ENUM[] SHR_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        private static readonly Variants_ENUM[] STC_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] STD_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] STI_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.SINGLE
        };

        private static readonly Variants_ENUM[] SUB_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        private static readonly Variants_ENUM[] XOR_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_ADDRESS,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_REGISTER,
            Variants_ENUM.DESTINATION_REGISTER_SOURCE_VALUE,
            Variants_ENUM.DESTINATION_ADDRESS_SOURCE_VALUE
        };

        private static readonly Variants_ENUM[] LABEL_ALLOWED_VARIANTS = new Variants_ENUM[]
        {
            Variants_ENUM.NoN
        };
    }

    class Allowed_Instruction_Bitmodes : SIZE
    {
        public static Size_ENUM[][] AllAllowedBitmodes()
        {
            Size_ENUM[][] toReturn = new Size_ENUM[][]
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
                XOR_ALLOWED_BITMODES,
                LABEL_ALLOWED_BITMODES
            };

            return toReturn;
        }

        private static readonly Size_ENUM[] AAA_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] AAD_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] AAM_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] AAS_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] ADC_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] ADD_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] AND_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] CALL_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] CBW_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] CLC_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] CLD_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] CLI_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] CMC_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] CMP_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        /*
        private static readonly Instruction_Data.Size_ENUM[] CMPSB_ALLOWED_BITMODES = new Instruction_Data.Size_ENUM[]
        {

        };

        private static readonly Instruction_Data.Size_ENUM[] CMPSW_ALLOWED_BITMODES = new Instruction_Data.Size_ENUM[]
        {

        };
        */

        private static readonly Size_ENUM[] CWD_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] DAA_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] DAS_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] DEC_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] DIV_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT
        };

        private static readonly Size_ENUM[] HLT_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] INC_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        // I am lazy to fix the actual bug that just gives the INT instruction a bitmode of NoN, this should do it tho
        private static readonly Size_ENUM[] INT_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT
        };

        private static readonly Size_ENUM[] JA_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JAE_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JB_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JBE_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JC_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JE_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JG_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JGE_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JL_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JLE_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JNA_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JNAE_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JNB_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JNBE_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JNC_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JNE_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JNG_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JNGE_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JNL_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JNLE_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JNO_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JNP_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JNS_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JNZ_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JO_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JP_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JPE_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JPO_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JS_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JZ_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] JMP_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] LAHF_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] LEA_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] MOV_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] MUL_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] NEG_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] NOP_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] NOT_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] OR_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] POP_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] POPF_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] PUSH_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] PUSHF_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] RCL_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT

        };

        private static readonly Size_ENUM[] RCR_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT

        };

        private static readonly Size_ENUM[] RET_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] ROL_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] ROR_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] SAHF_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] SAL_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] SAR_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] SBB_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] SHL_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] SHR_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] STC_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] STD_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] STI_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };

        private static readonly Size_ENUM[] SUB_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] XOR_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM._8_BIT,
            Size_ENUM._16_BIT,
            Size_ENUM._32_BIT,
            Size_ENUM._64_BIT
        };

        private static readonly Size_ENUM[] LABEL_ALLOWED_BITMODES = new Size_ENUM[]
        {
            Size_ENUM.NoN
        };
    }
}