namespace EmuXCore.Common.Enums;

public enum MemoryOffsetType : byte
{
    Label,
    Register,
    Integer,
    Scale,

    NaN // Internal use and used to mark the first token of a memory offset typically
}