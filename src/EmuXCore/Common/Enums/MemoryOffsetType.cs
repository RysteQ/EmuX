namespace EmuXCore.Common.Enums;

/// <summary>
/// Used for calculating the memory offset by indicating the type of value that are described in the expression
/// </summary>
public enum MemoryOffsetType : byte
{
    Label,
    Register,
    Integer,
    Scale,

    NaN // Internal use and used to mark the first token of a memory offset typically
}