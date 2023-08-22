using EmuX.src.Enums.VM;
namespace EmuX.src.Services.Interrupt_Handler;

public class Disk_Handler
{
    public void SetVirtualSystem(VirtualSystem virtual_system)
    {
        this.virtual_system = virtual_system;
    }

    public VirtualSystem ReadFromDisk(Registers first_register, Registers second_register)
    {
        byte current_byte = 0;
        int offset = 0;

        string name_of_file = string.Empty;
        string file_data = string.Empty;

        while ((current_byte = this.virtual_system.GetByteMemory(this.virtual_system.GetRegisterWord(first_register) + offset)) != 0)
        {
            name_of_file += (char)current_byte;
            offset++;
        }

        file_data = File.ReadAllText(name_of_file);

        for (int i = 0; i < file_data.Length; i++)
            this.virtual_system.SetByteMemory((int)(this.virtual_system.GetRegisterDouble(second_register) + i), (byte)file_data[i]);

        return this.virtual_system;
    }

    public void WriteToDisk(Registers first_register, Registers second_register)
    {
        byte current_byte = 0;
        int offset = 0;

        string name_of_file = string.Empty;
        string file_data = string.Empty;

        while ((current_byte = this.virtual_system.GetByteMemory(this.virtual_system.GetRegisterWord(first_register) + offset)) != 0)
        {
            name_of_file += (char)current_byte;
            offset++;
        }

        offset = 0;

        while ((current_byte = this.virtual_system.GetByteMemory(this.virtual_system.GetRegisterWord(second_register) + offset)) != 0)
        {
            file_data += (char)current_byte;
            offset++;
        }

        File.WriteAllText(name_of_file, file_data);
    }

    private VirtualSystem virtual_system = new();
}
