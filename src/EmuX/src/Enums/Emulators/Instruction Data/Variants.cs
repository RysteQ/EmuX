﻿namespace EmuX.src.Enums.Instruction_Data;

public enum Variants
{
    SINGLE,
    SINGLE_REGISTER,
    SINGLE_VALUE,
    SINGLE_ADDRESS_VALUE,
    DESTINATION_REGISTER_SOURCE_REGISTER,
    DESTINATION_REGISTER_SOURCE_VALUE,
    DESTINATION_REGISTER_SOURCE_ADDRESS,
    DESTINATION_ADDRESS_SOURCE_REGISTER,
    DESTINATION_ADDRESS_SOURCE_VALUE,

    LABEL,
    NoN
}