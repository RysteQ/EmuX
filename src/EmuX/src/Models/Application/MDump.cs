using EmuX.src.Enums.Application.MemoryDump;

namespace EmuX.src.Models.Application;

public class MDump
{
    public MemoryDumpModes MemoryDumpMode;
    public bool MemoryDumpEnabled;
    public bool StackDumpEnabled;
}