using EmuX.src.Enums.Emulators.VM;

namespace EmuX.src.Services.Interrupt_Handler;

public class DiskHandler
{
    public VirtualSystem ReadFromDisk(Registers first_register, Registers second_register)
    {
        byte current_byte = 0;
        int offset = 0;

        string name_of_file = string.Empty;
        string file_data = string.Empty;

        while ((current_byte = this.VirtualSystem.GetByteMemory(this.VirtualSystem.GetRegisterWord(first_register) + offset)) != 0)
        {
            name_of_file += (char)current_byte;
            offset++;
        }

        file_data = File.ReadAllText(name_of_file);

        for (int i = 0; i < file_data.Length; i++)
            this.VirtualSystem.SetByteMemory((int)(this.VirtualSystem.GetRegisterDouble(second_register) + i), (byte)file_data[i]);

        return this.VirtualSystem;
    }

    public void WriteToDisk(Registers first_register, Registers second_register)
    {
        byte current_byte = 0;
        int offset = 0;

        string name_of_file = string.Empty;
        string file_data = string.Empty;

        while ((current_byte = this.VirtualSystem.GetByteMemory(this.VirtualSystem.GetRegisterWord(first_register) + offset)) != 0)
        {
            name_of_file += (char)current_byte;
            offset++;
        }

        offset = 0;

        while ((current_byte = this.VirtualSystem.GetByteMemory(this.VirtualSystem.GetRegisterWord(second_register) + offset)) != 0)
        {
            file_data += (char)current_byte;
            offset++;
        }

        File.WriteAllText(name_of_file, file_data);
    }

    public VirtualSystem VirtualSystem = new();
}
