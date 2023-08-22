using EmuX.src.Enums.Instruction_Data;
using EmuX.src.Models;
using Size = EmuX.src.Enums.Size;

namespace EmuX.src.Services.Analyzer;

public class InstructionVerifier
{
    public static bool VerifyInstructions(List<Instruction> instructions)
    {
        return instructions.Where(instruction => ContainsVariant(instruction) == false || ContainsBitmode(instruction) == false).Any() == false;
    }

    private static bool ContainsVariant(Instruction instruction)
    {
        if (instruction.opcode == Opcodes.LABEL)
            return true;

        Variants[] specific_instruction_variants = Allowed_Instruction_Variants.AllInstructionVariants()[(int)instruction.opcode];

        return specific_instruction_variants.Contains(instruction.variant);
    }

    private static bool ContainsBitmode(Instruction instruction)
    {
        if (instruction.opcode == Opcodes.LABEL)
            return true;

        Size[] specific_instruction_bitmodes = Allowed_Instruction_Bitmodes.AllAllowedBitmodes()[(int)instruction.opcode];

        return specific_instruction_bitmodes.Contains(instruction.bit_mode);
    }
}

public class Allowed_Instruction_Variants
{
    public static Variants[][] AllInstructionVariants()
    {
        Variants[][] toReturn = new Variants[][]
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

    private static readonly Variants[] AAA_ALLOWED_VARIANTS = new Variants[1]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] AAD_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] AAM_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] AAS_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] ADC_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_REGISTER_SOURCE_ADDRESS,
        Variants.DESTINATION_REGISTER_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_VALUE,
        Variants.DESTINATION_ADDRESS_SOURCE_REGISTER,
        Variants.DESTINATION_ADDRESS_SOURCE_VALUE
    };

    private static readonly Variants[] ADD_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_REGISTER_SOURCE_ADDRESS,
        Variants.DESTINATION_REGISTER_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_VALUE,
        Variants.DESTINATION_ADDRESS_SOURCE_REGISTER,
        Variants.DESTINATION_ADDRESS_SOURCE_VALUE
    };

    private static readonly Variants[] AND_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_REGISTER_SOURCE_ADDRESS,
        Variants.DESTINATION_REGISTER_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_VALUE,
        Variants.DESTINATION_ADDRESS_SOURCE_REGISTER,
        Variants.DESTINATION_ADDRESS_SOURCE_VALUE
    };

    private static readonly Variants[] CALL_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] CBW_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] CLC_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] CLD_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] CLI_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] CMC_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] CMP_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_REGISTER_SOURCE_ADDRESS,
        Variants.DESTINATION_REGISTER_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_VALUE
    };

    private static readonly Variants[] CWD_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] DAA_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] DAS_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] DEC_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE_ADDRESS_VALUE,
        Variants.SINGLE_REGISTER
    };

    private static readonly Variants[] DIV_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE_ADDRESS_VALUE,
        Variants.SINGLE_REGISTER
    };

    private static readonly Variants[] HLT_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] INC_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE_ADDRESS_VALUE,
        Variants.SINGLE_REGISTER
    };

    private static readonly Variants[] INT_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE_VALUE
    };

    private static readonly Variants[] JCXZ_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JMP_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JA_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JAE_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JB_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JBE_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JC_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JE_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JG_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JGE_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JL_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JLE_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JNA_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JNAE_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JNB_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JNBE_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JNC_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JNE_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JNG_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JNGE_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JNL_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JNLE_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JNO_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JNP_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JNS_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JNZ_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JO_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JP_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JPE_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JPO_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JS_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] JZ_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.LABEL
    };

    private static readonly Variants[] LAHF_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] LEA_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_REGISTER_SOURCE_ADDRESS
    };

    private static readonly Variants[] MOV_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_ADDRESS_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_ADDRESS,
        Variants.DESTINATION_REGISTER_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_VALUE,
        Variants.DESTINATION_ADDRESS_SOURCE_VALUE
    };

    private static readonly Variants[] MUL_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE_REGISTER,
        Variants.SINGLE_ADDRESS_VALUE
    };

    private static readonly Variants[] NEG_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE_REGISTER,
        Variants.SINGLE_ADDRESS_VALUE
    };

    private static readonly Variants[] NOP_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] NOT_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE_REGISTER,
        Variants.SINGLE_ADDRESS_VALUE
    };

    private static readonly Variants[] OR_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_ADDRESS_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_ADDRESS,
        Variants.DESTINATION_REGISTER_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_VALUE,
        Variants.DESTINATION_ADDRESS_SOURCE_VALUE
    };

    private static readonly Variants[] POP_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE_REGISTER,
        Variants.SINGLE_ADDRESS_VALUE
    };

    private static readonly Variants[] POPF_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] PUSH_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE_REGISTER,
        Variants.SINGLE_ADDRESS_VALUE
    };

    private static readonly Variants[] PUSHF_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] RCL_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_ADDRESS_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_ADDRESS,
        Variants.DESTINATION_REGISTER_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_VALUE,
        Variants.DESTINATION_ADDRESS_SOURCE_VALUE
    };

    private static readonly Variants[] RCR_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_ADDRESS_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_ADDRESS,
        Variants.DESTINATION_REGISTER_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_VALUE,
        Variants.DESTINATION_ADDRESS_SOURCE_VALUE
    };

    private static readonly Variants[] RET_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] ROL_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_ADDRESS_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_ADDRESS,
        Variants.DESTINATION_REGISTER_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_VALUE,
        Variants.DESTINATION_ADDRESS_SOURCE_VALUE
    };

    private static readonly Variants[] ROR_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_ADDRESS_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_ADDRESS,
        Variants.DESTINATION_REGISTER_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_VALUE,
        Variants.DESTINATION_ADDRESS_SOURCE_VALUE
    };

    private static readonly Variants[] SAHF_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] SAL_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_ADDRESS_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_ADDRESS,
        Variants.DESTINATION_REGISTER_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_VALUE,
        Variants.DESTINATION_ADDRESS_SOURCE_VALUE
    };

    private static readonly Variants[] SAR_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_ADDRESS_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_ADDRESS,
        Variants.DESTINATION_REGISTER_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_VALUE,
        Variants.DESTINATION_ADDRESS_SOURCE_VALUE
    };

    private static readonly Variants[] SBB_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_ADDRESS_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_ADDRESS,
        Variants.DESTINATION_REGISTER_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_VALUE,
        Variants.DESTINATION_ADDRESS_SOURCE_VALUE
    };

    private static readonly Variants[] SHL_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_REGISTER_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_VALUE,
        Variants.DESTINATION_ADDRESS_SOURCE_REGISTER,
        Variants.DESTINATION_ADDRESS_SOURCE_VALUE
    };

    private static readonly Variants[] SHR_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_REGISTER_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_VALUE,
        Variants.DESTINATION_ADDRESS_SOURCE_REGISTER,
        Variants.DESTINATION_ADDRESS_SOURCE_VALUE
    };

    private static readonly Variants[] STC_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] STD_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] STI_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.SINGLE
    };

    private static readonly Variants[] SUB_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_ADDRESS_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_ADDRESS,
        Variants.DESTINATION_REGISTER_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_VALUE,
        Variants.DESTINATION_ADDRESS_SOURCE_VALUE
    };

    private static readonly Variants[] XOR_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.DESTINATION_ADDRESS_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_ADDRESS,
        Variants.DESTINATION_REGISTER_SOURCE_REGISTER,
        Variants.DESTINATION_REGISTER_SOURCE_VALUE,
        Variants.DESTINATION_ADDRESS_SOURCE_VALUE
    };

    private static readonly Variants[] LABEL_ALLOWED_VARIANTS = new Variants[]
    {
        Variants.NoN
    };
}

class Allowed_Instruction_Bitmodes 
{
    public static Size[][] AllAllowedBitmodes()
    {
        Size[][] toReturn =
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

    private static readonly Size[] AAA_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] AAD_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] AAM_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] AAS_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] ADC_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] ADD_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] AND_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] CALL_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] CBW_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] CLC_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] CLD_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] CLI_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] CMC_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] CMP_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    /*
    private static readonly Instruction_Data.Size[] CMPSB_ALLOWED_BITMODES = new Instruction_Data.Size[]
    {

    };

    private static readonly Instruction_Data.Size[] CMPSW_ALLOWED_BITMODES = new Instruction_Data.Size[]
    {

    };
    */

    private static readonly Size[] CWD_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] DAA_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] DAS_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] DEC_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] DIV_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT
    };

    private static readonly Size[] HLT_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] INC_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    // I am lazy to fix the actual bug that just gives the INT instruction a bitmode of NoN, this should do it tho
    private static readonly Size[] INT_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT
    };

    private static readonly Size[] JA_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JAE_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JB_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JBE_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JC_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JE_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JG_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JGE_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JL_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JLE_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JNA_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JNAE_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JNB_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JNBE_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JNC_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JNE_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JNG_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JNGE_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JNL_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JNLE_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JNO_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JNP_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JNS_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JNZ_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JO_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JP_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JPE_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JPO_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JS_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JZ_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] JMP_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] LAHF_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] LEA_ALLOWED_BITMODES = new Size[]
    {
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] MOV_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] MUL_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] NEG_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] NOP_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] NOT_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] OR_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] POP_ALLOWED_BITMODES = new Size[]
    {
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] POPF_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] PUSH_ALLOWED_BITMODES = new Size[]
    {
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] PUSHF_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] RCL_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT

    };

    private static readonly Size[] RCR_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT

    };

    private static readonly Size[] RET_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] ROL_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] ROR_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] SAHF_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] SAL_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] SAR_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] SBB_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] SHL_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] SHR_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] STC_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] STD_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] STI_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };

    private static readonly Size[] SUB_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] XOR_ALLOWED_BITMODES = new Size[]
    {
        Size._8_BIT,
        Size._16_BIT,
        Size._32_BIT,
        Size._64_BIT
    };

    private static readonly Size[] LABEL_ALLOWED_BITMODES = new Size[]
    {
        Size.NoN
    };
}