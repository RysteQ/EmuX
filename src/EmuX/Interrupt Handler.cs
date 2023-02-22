using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX
{
    class Interrupt_Handler
    {
        public Interrupt_Handler() 
        {
            this.video_graphics = Graphics.FromImage(this.video);

            // set the video output to black
            for (int x = 0; x < this.video.Width; x++)
                for (int y = 0; y < this.video.Height; y++)
                    this.video.SetPixel(x, y, Color.Black);
        }

        public VirtualSystem GetVirtualSystem()
        {
            return this.virtual_system;
        }

        public Bitmap GetVideoOutput()
        {
            return this.video;
        }

        public void SetVirtualSystem(VirtualSystem virtual_system)
        {
            this.virtual_system = virtual_system;
        }

        public void SetInterrupt(Interrupt interrupt)
        {
            this.interrupt = interrupt;
        }

        public void ResetInterruptHandler()
        {
            ClearScreen();
            ResetInterrupt();
        }

        public void ResetInterrupt()
        {
            this.interrupt = new Interrupt();
        }

        public void ExecuteInterrupt()
        {
            switch (this.interrupt.GetInterruptCode())
            {
                case Interrupt.Interrupt_Codes.Set_Cursor_Position:
                    this.SetCursorPosition();
                    break;

                case Interrupt.Interrupt_Codes.Get_Cursor_Position:
                    this.GetCursorPosition();
                    break;

                case Interrupt.Interrupt_Codes.Clear_Screen:
                    this.ClearScreen();
                    break;

                case Interrupt.Interrupt_Codes.Read_Character_At_Cursor_Position:
                    this.ReadCharacterAtCursorPosition();
                    break;

                case Interrupt.Interrupt_Codes.Write_Character_At_Cursor_Position:
                    this.WriteCharacterAtCursorPosition();
                    break;

                case Interrupt.Interrupt_Codes.Write_Pixel_At_Position:
                    this.WritePixelAtPosition();
                    break;

                case Interrupt.Interrupt_Codes.Read_From_Disk:
                    this.ReadFromDisk();
                    break;

                case Interrupt.Interrupt_Codes.Write_To_Disk:
                    this.WriteToDisk();
                    break;

                case Interrupt.Interrupt_Codes.Wait_Delay:
                    this.WaitDelay();
                    break;
            }
        }

        /// <summary>
        /// Sets the cursor position based on the CX and DX register value
        /// </summary>
        private void SetCursorPosition()
        {
            ushort new_cursor_x = this.virtual_system.GetRegisterWord(FIRST_ARGUMENT_REGISTER);
            ushort new_cursor_y = this.virtual_system.GetRegisterWord(SECOND_ARGUMENT_REGISTER);

            // make sure the new cursor position is within limits
            this.cursor_x = (ushort) (new_cursor_x % CHAR_WIDTH);
            this.cursor_y = (ushort) (new_cursor_y % CHAR_HEIGHT);
        }

        /// <summary>
        /// Saves the cursor position in the CX and DX registers
        /// </summary>
        private void GetCursorPosition()
        {
            this.virtual_system.SetRegisterWord(FIRST_ARGUMENT_REGISTER, this.cursor_x);
            this.virtual_system.SetRegisterWord(SECOND_ARGUMENT_REGISTER, this.cursor_y);
        }

        /// <summary>
        /// Clears the screen and makes it all black, also clears the screen character buffer
        /// </summary>
        private void ClearScreen()
        {
            // set the video output to black
            for (int x = 0; x < this.video.Width; x++)
                for (int y = 0; y < this.video.Height; y++)
                    this.video.SetPixel(x, y, Color.Black);

            // clear the character buffer
            for (int x = 0; x < CHAR_WIDTH; x++)
                for (int y = 0; y < CHAR_HEIGHT; y++)
                    this.characters[x, y] = (char) 0;

            this.cursor_x = 0;
            this.cursor_y = 0;
        }

        /// <summary>
        /// Reads the character buffer and saves the result in the CX register
        /// </summary>
        private void ReadCharacterAtCursorPosition()
        {
            this.virtual_system.SetRegisterWord(FIRST_ARGUMENT_REGISTER, this.characters[cursor_x, cursor_y]);
        }

        /// <summary>
        /// Write the character saved in the CX register and updates the cursor position
        /// </summary>
        private void WriteCharacterAtCursorPosition()
        {
            if (this.cursor_y == CHARACTERS_VERTICALY)
                return;

            char character_to_write = (char) this.virtual_system.GetRegisterQuad(FIRST_ARGUMENT_REGISTER);
            this.characters[cursor_x, cursor_y] = character_to_write;

            // draw the character on the screen
            this.video_graphics.DrawString(character_to_write.ToString(), new Font(FontFamily.GenericSerif, 8), Brushes.White, new Point(this.cursor_x * CHAR_WIDTH, this.cursor_y * CHAR_HEIGHT));
            this.video_graphics.Flush();

            // update the cursor position or exit the function all together
            if ((this.cursor_x + 1) < CHARACTERS_HORIZONTALY)
            {
                this.cursor_x++;
            }
            else
            {
                this.cursor_x = 0;
                this.cursor_y++;
            }

        }

        /// <summary>
        /// Draws a pixel to the screen based on the CX and DX for it's x and y coordinates
        /// </summary>
        private void WritePixelAtPosition()
        {
            ushort x_pos = (ushort) this.virtual_system.GetRegisterQuad(FIRST_ARGUMENT_REGISTER);
            ushort y_pos = (ushort) this.virtual_system.GetRegisterQuad(SECOND_ARGUMENT_REGISTER);

            this.video.SetPixel(x_pos, y_pos, Color.White);
        }

        /// <summary>
        /// Reads from disk the file that has the same name as the data loaded in the CX register
        /// and saves the result to the memory location pointed of the RSI register
        /// </summary>
        private void ReadFromDisk()
        {
            ulong RCX_register_backup = this.virtual_system.GetRegisterQuad(FIRST_ARGUMENT_REGISTER);
            string name_of_file = "";
            string file_data = "";

            do
            {
                name_of_file += this.virtual_system.GetByteMemory(this.virtual_system.GetRegisterWord(FIRST_ARGUMENT_REGISTER));
                this.virtual_system.SetRegisterWord(FIRST_ARGUMENT_REGISTER, (ushort) (this.virtual_system.GetRegisterWord(FIRST_ARGUMENT_REGISTER) + 1));
            } while (this.virtual_system.GetByteMemory(this.virtual_system.GetRegisterWord(FIRST_ARGUMENT_REGISTER)) != 0);

            // read and save the file data to memory
            file_data = File.ReadAllText(name_of_file);

            for (int i = 0; i < file_data.Length; i++)
                this.virtual_system.SetByteMemory((int) (this.virtual_system.GetRegisterDouble(Instruction_Data.Registers_ENUM.RSI) + i), (byte) file_data[i]);

            this.virtual_system.SetRegisterQuad(FIRST_ARGUMENT_REGISTER, RCX_register_backup);
        }

        /// <summary>
        /// Write to the disk a new file with the name loaded and pointed by the CX register and the contents will
        /// be loaded by the memory location pointed by the DX register
        /// </summary>
        private void WriteToDisk()
        {
            ulong RCX_register_backup = this.virtual_system.GetRegisterQuad(FIRST_ARGUMENT_REGISTER);
            ulong RDX_register_backup = this.virtual_system.GetRegisterQuad(SECOND_ARGUMENT_REGISTER);
            string name_of_file = "";
            string file_data = "";

            // get the name of the file
            do
            {
                name_of_file += this.virtual_system.GetByteMemory(this.virtual_system.GetRegisterWord(FIRST_ARGUMENT_REGISTER));
                this.virtual_system.SetRegisterWord(FIRST_ARGUMENT_REGISTER, (ushort) (this.virtual_system.GetRegisterWord(FIRST_ARGUMENT_REGISTER) + 1));
            } while (this.virtual_system.GetByteMemory(this.virtual_system.GetRegisterWord(FIRST_ARGUMENT_REGISTER)) != 0);

            // get the contents of the file
            do
            {
                file_data += this.virtual_system.GetByteMemory(this.virtual_system.GetRegisterWord(SECOND_ARGUMENT_REGISTER));
                this.virtual_system.SetRegisterWord(SECOND_ARGUMENT_REGISTER, (ushort) (this.virtual_system.GetRegisterWord(SECOND_ARGUMENT_REGISTER) + 1));
            } while (this.virtual_system.GetByteMemory(this.virtual_system.GetRegisterWord(SECOND_ARGUMENT_REGISTER)) != 0);

            // save the file
            File.WriteAllText(name_of_file, file_data);

            this.virtual_system.SetRegisterQuad(FIRST_ARGUMENT_REGISTER, RCX_register_backup);
            this.virtual_system.SetRegisterQuad(SECOND_ARGUMENT_REGISTER, RDX_register_backup);
        }

        /// <summary>
        /// Sleeps for the amount of miliseconds equal to the value of the ECX register
        /// </summary>
        private void WaitDelay()
        {
            int miliseconds_to_sleep_for = (int) this.virtual_system.GetRegisterDouble(FIRST_ARGUMENT_REGISTER);
            Thread.Sleep(miliseconds_to_sleep_for);
        }

        private Interrupt interrupt = new Interrupt();
        private VirtualSystem virtual_system = new VirtualSystem();

        // all of the video stuff
        private Bitmap video = new Bitmap(CHARACTERS_HORIZONTALY * CHAR_WIDTH, CHARACTERS_VERTICALY * CHAR_HEIGHT);
        private Graphics video_graphics;
        private char[,] characters = new char[CHARACTERS_HORIZONTALY, CHARACTERS_VERTICALY];
        private ushort cursor_x = 0;
        private ushort cursor_y = 0;

        // the argument registers
        const Instruction_Data.Registers_ENUM FIRST_ARGUMENT_REGISTER = Instruction_Data.Registers_ENUM.RCX;
        const Instruction_Data.Registers_ENUM SECOND_ARGUMENT_REGISTER = Instruction_Data.Registers_ENUM.RDX;

        // the screen dimensions
        const int CHARACTERS_HORIZONTALY = 80;
        const int CHARACTERS_VERTICALY = 35;

        // the character dimensions
        const int CHAR_WIDTH = 8;
        const int CHAR_HEIGHT = 12;
    }
}
