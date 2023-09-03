using EmuX.src.Enums.Emulators.VM;
using EmuX.src.Models.Application;
using EmuX.src.Services.Base.Converter;
using System.Text;

namespace EmuX.src.Services.Emulator;

public class MemoryDumper
{
    public void Dump(VirtualSystem virtual_system, List<int> memory_indexes)
    {
        string dump_contents = string.Empty;

        if (DumpFolderExists() == false)
            CreateDumpFolder();

        CreateDumpFile();

        if (this.MemoryDumpPreference.MemoryDumpMode == Enums.Application.MemoryDump.MemoryDumpModes.Total)
            dump_contents = CompleteMemoryDump(virtual_system);
        else
            dump_contents = PartialMemoryDump(virtual_system, memory_indexes);

        this.memory_dump_output.Write(dump_contents);
        this.memory_dump_output.Close();
    }

    private bool DumpFolderExists() => Directory.Exists(GetChildDirectory());

    private void CreateDumpFolder() => Directory.CreateDirectory(GetChildDirectory());

    private void CreateDumpFile()
    {
        string[] files = Directory.GetFiles(GetChildDirectory());
        int mdump_count = int.Parse(files.Last().Split('\\').Last().Split('-').Last().Split('.').First()) + 1; ;

        this.memory_dump_output = new(Path.Combine(GetChildDirectory(), $"mdump-{mdump_count}.txt"));
    }

    private string CompleteMemoryDump(VirtualSystem virtual_system)
    {
        string dump_contents = GetRegisters(virtual_system);

        if (this.MemoryDumpPreference.MemoryDumpEnabled)
            dump_contents += "\n\nMemory\n" + GetMemory(virtual_system);

        if (this.MemoryDumpPreference.StackDumpEnabled)
            dump_contents += "\n\nStack\n" + GetStackMemory(virtual_system);

        return dump_contents;
    }

    private string PartialMemoryDump(VirtualSystem virtual_system, List<int> memory_indexes)
    {
        string dump_contents = GetRegisters(virtual_system);

        if (this.MemoryDumpPreference.MemoryDumpEnabled)
            dump_contents += "\n\nMemory\n" + GetMemory(virtual_system, memory_indexes);

        if (this.MemoryDumpPreference.StackDumpEnabled)
            dump_contents += "\n\nStack\n" + GetStackMemory(virtual_system);

        return dump_contents;
    }

    private string GetRegisters(VirtualSystem virtual_system)
    {
        return $"RAX: {virtual_system.GetRegisterQuad(Registers.RAX)}\n" +
            $"RBX: {virtual_system.GetRegisterQuad(Registers.RBX)}\n" +
            $"RCX: {virtual_system.GetRegisterQuad(Registers.RCX)}\n" +
            $"RDX: {virtual_system.GetRegisterQuad(Registers.RDX)}\n" +
            $"RSI: {virtual_system.GetRegisterQuad(Registers.RSI)}\n" +
            $"RDI: {virtual_system.GetRegisterQuad(Registers.RDI)}\n" +
            $"RSP: {virtual_system.GetRegisterQuad(Registers.RSP)}\n" +
            $"RBP: {virtual_system.GetRegisterQuad(Registers.RBP)}\n" +
            $"RIP: {virtual_system.GetRegisterQuad(Registers.RIP)}\n" +
            $"R8: {virtual_system.GetRegisterQuad(Registers.R8)}\n" +
            $"R9: {virtual_system.GetRegisterQuad(Registers.R9)}\n" +
            $"R10: {virtual_system.GetRegisterQuad(Registers.R10)}\n" +
            $"R11: {virtual_system.GetRegisterQuad(Registers.R11)}\n" +
            $"R12: {virtual_system.GetRegisterQuad(Registers.R12)}\n" +
            $"R13: {virtual_system.GetRegisterQuad(Registers.R13)}\n" +
            $"R14: {virtual_system.GetRegisterQuad(Registers.R14)}\n" +
            $"R15: {virtual_system.GetRegisterQuad(Registers.R15)}";
    }

    private string GetMemory(VirtualSystem virtual_system)
    {
        StringBuilder to_return = new();
        byte[] memory = virtual_system.GetAllMemory();

        for (int i = 8192; i < memory.Length; i++)
            to_return.AppendLine($"0x{HexadecimalConverter.ConvertUlongToBase((ulong) i)}: {memory[i]}");

        return to_return.ToString();
    }

    private string GetMemory(VirtualSystem virtual_system, List<int> indexes)
    {
        string to_return = string.Empty;
        byte[] memory = virtual_system.GetAllMemory();

        foreach (int index in indexes)
            to_return += $"0x{HexadecimalConverter.ConvertUlongToBase((ulong) index)}: {memory[index]}\n";

        return to_return;
    }

    private string GetStackMemory(VirtualSystem virtual_system)
    {
        string to_return = string.Empty;

        for (int i = 0; i < 8192; i++)
            to_return += $"0x{HexadecimalConverter.ConvertUlongToBase((ulong) i)}: {virtual_system.GetByteMemory(i)}\n";

        return to_return;
    }

    private string GetChildDirectory()
    {
        string root_directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Memory Dumps");
        return Path.Combine(root_directory, DateTime.Now.ToString("dd-MM-yyyy"));
    }

    public MDump MemoryDumpPreference;
    private StreamWriter memory_dump_output;
}