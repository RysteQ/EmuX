namespace EmuX
{
    class Interrupt_Handler
    {
        public Interrupt_Handler()
        {
            video_graphics = Graphics.FromImage(video);

            for (int x = 0; x < video.Width; x++)
                for (int y = 0; y < video.Height; y++)
                    video.SetPixel(x, y, Color.Black);
        }

        public Bitmap GetVideoOutput()
        {
            return video;
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
            interrupt = new Interrupt();
        }

        public void ExecuteInterrupt()
        {
            switch (interrupt.GetInterruptCode())
            {
                case Interrupt.Interrupt_Codes.Set_Cursor_Position:
                    SetCursorPosition();
                    break;

                case Interrupt.Interrupt_Codes.Get_Cursor_Position:
                    GetCursorPosition();
                    break;

                case Interrupt.Interrupt_Codes.Clear_Screen:
                    ClearScreen();
                    break;

                case Interrupt.Interrupt_Codes.Read_Character_At_Cursor_Position:
                    ReadCharacterAtCursorPosition();
                    break;

                case Interrupt.Interrupt_Codes.Write_Character_At_Cursor_Position:
                    WriteCharacterAtCursorPosition();
                    break;

                case Interrupt.Interrupt_Codes.Write_Pixel_At_Position:
                    RefreshVideo();
                    break;

                case Interrupt.Interrupt_Codes.Read_From_Disk:
                    ReadFromDisk();
                    break;

                case Interrupt.Interrupt_Codes.Write_To_Disk:
                    WriteToDisk();
                    break;

                case Interrupt.Interrupt_Codes.Wait_Delay:
                    WaitDelay();
                    break;
            }
        }

        /// <summary>
        /// Sets the cursor position based on the CX and DX register value
        /// </summary>
        private void SetCursorPosition()
        {
            ushort new_cursor_x = virtual_system.GetRegisterWord(FIRST_ARGUMENT_REGISTER);
            ushort new_cursor_y = virtual_system.GetRegisterWord(SECOND_ARGUMENT_REGISTER);

            // make sure the new cursor position is within limits
            if (new_cursor_x < CHARACTERS_HORIZONTALY)
                cursor_x = new_cursor_x;

            if (new_cursor_y < CHARACTERS_VERTICALY)
                cursor_y = new_cursor_y;
        }

        /// <summary>
        /// Saves the cursor position in the CX and DX registers
        /// </summary>
        private void GetCursorPosition()
        {
            virtual_system.SetRegisterWord(FIRST_ARGUMENT_REGISTER, cursor_x);
            virtual_system.SetRegisterWord(SECOND_ARGUMENT_REGISTER, cursor_y);
        }

        /// <summary>
        /// Clears the screen and makes it all black, also clears the screen character buffer
        /// </summary>
        private void ClearScreen()
        {
            for (int x = 0; x < video.Width; x++)
                for (int y = 0; y < video.Height; y++)
                    video.SetPixel(x, y, Color.Black);

            for (int x = 0; x < CHAR_WIDTH; x++)
                for (int y = 0; y < CHAR_HEIGHT; y++)
                    characters[x, y] = (char)0;

            cursor_x = 0;
            cursor_y = 0;
        }

        /// <summary>
        /// Reads the character buffer and saves the result in the CX register
        /// </summary>
        private void ReadCharacterAtCursorPosition()
        {
            virtual_system.SetRegisterWord(FIRST_ARGUMENT_REGISTER, characters[cursor_x, cursor_y]);
        }

        /// <summary>
        /// Write the character saved in the CX register and updates the cursor position
        /// </summary>
        private void WriteCharacterAtCursorPosition()
        {
            if (cursor_y == CHARACTERS_VERTICALY)
                return;

            char character_to_write = (char)virtual_system.GetRegisterQuad(FIRST_ARGUMENT_REGISTER);
            characters[cursor_x, cursor_y] = character_to_write;

            video_graphics.DrawString(character_to_write.ToString(), new Font(FontFamily.GenericSerif, 8), Brushes.White, new Point(cursor_x * CHAR_WIDTH, cursor_y * CHAR_HEIGHT));
            video_graphics.Flush();

            if (cursor_x + 1 < CHARACTERS_HORIZONTALY)
            {
                cursor_x++;
            }
            else
            {
                cursor_x = 0;
                cursor_y++;
            }

        }

        /// <summary>
        /// Draws a pixel to the screen based on the CX and DX for it's x and y coordinates
        /// </summary>
        private void RefreshVideo()
        {
            const int stack_general_memory_limit = 8192;

            byte[] RGB = new byte[3];
            int x = 0;
            int y = 0;

            for (int i = 0; i < 640 * 420; i += 3)
            {
                x = i / 3 % 640;
                y = i / (640 * 3);

                RGB[0] = virtual_system.GetByteMemory(stack_general_memory_limit + i);
                RGB[1] = virtual_system.GetByteMemory(stack_general_memory_limit + i + 1);
                RGB[2] = virtual_system.GetByteMemory(stack_general_memory_limit + i + 2);

                video.SetPixel(x, y, Color.FromArgb(RGB[0], RGB[1], RGB[2]));
            }
        }

        /// <summary>
        /// Reads from disk the file that has the same name as the data loaded in the CX register
        /// and saves the result to the memory location pointed of the RDX register
        /// </summary>
        private void ReadFromDisk()
        {
            byte current_byte = 0;
            int offset = 0;

            string name_of_file = "";
            string file_data = "";

            while ((current_byte = virtual_system.GetByteMemory(virtual_system.GetRegisterWord(FIRST_ARGUMENT_REGISTER) + offset)) != 0)
            {
                name_of_file += (char)current_byte;
                offset++;
            }

            file_data = File.ReadAllText(name_of_file);

            for (int i = 0; i < file_data.Length; i++)
                virtual_system.SetByteMemory((int)(virtual_system.GetRegisterDouble(SECOND_ARGUMENT_REGISTER) + i), (byte)file_data[i]);
        }

        /// <summary>
        /// Write to the disk a new file with the name loaded and pointed by the CX register and the contents will
        /// be loaded by the memory location pointed by the DX register
        /// </summary>
        private void WriteToDisk()
        {
            byte current_byte = 0;
            int offset = 0;

            string name_of_file = "";
            string file_data = "";

            while ((current_byte = virtual_system.GetByteMemory(virtual_system.GetRegisterWord(FIRST_ARGUMENT_REGISTER) + offset)) != 0)
            {
                name_of_file += (char)current_byte;
                offset++;
            }

            offset = 0;

            while ((current_byte = virtual_system.GetByteMemory(virtual_system.GetRegisterWord(SECOND_ARGUMENT_REGISTER) + offset)) != 0)
            {
                file_data += (char)current_byte;
                offset++;
            }

            File.WriteAllText(name_of_file, file_data);
        }

        /// <summary>
        /// Sleeps for the amount of miliseconds equal to the value of the ECX register
        /// </summary>
        private void WaitDelay()
        {
            int miliseconds_to_sleep_for = (int)virtual_system.GetRegisterDouble(FIRST_ARGUMENT_REGISTER);
            Thread.Sleep(miliseconds_to_sleep_for);
        }

        private Interrupt interrupt = new Interrupt();
        public VirtualSystem virtual_system { get; set; }

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
