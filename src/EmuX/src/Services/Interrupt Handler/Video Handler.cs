using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX.src.Services.Interrupt_Handler
{
    public class Video_Handler
    {
        public Video_Handler()
        {
            this.video = new Bitmap(CHARACTERS_HORIZONTALY * CHAR_WIDTH, CHARACTERS_VERTICALY * CHAR_HEIGHT);
            this.video_graphics = Graphics.FromImage(this.video);

            for (int x = 0; x < this.video.Width; x++)
                for (int y = 0; y < this.video.Height; y++)
                    this.video.SetPixel(x, y, Color.Black);

            this.virtual_system = new VirtualSystem();
        }

        public Bitmap GetVideo()
        {
            return this.video;
        }

        public void SetVirtualSystem(VirtualSystem virtual_system)
        {
            this.virtual_system = virtual_system;
        }

        public void RefreshVideo()
        {
            const int stack_general_memory_limit = 8192;

            byte[] RGB = new byte[3];
            int x = 0;
            int y = 0;

            for (int i = 0; i < 640 * 420; i += 3)
            {
                x = i / 3 % 640;
                y = i / (640 * 3);

                RGB[0] = this.virtual_system.GetByteMemory(stack_general_memory_limit + i);
                RGB[1] = this.virtual_system.GetByteMemory(stack_general_memory_limit + i + 1);
                RGB[2] = this.virtual_system.GetByteMemory(stack_general_memory_limit + i + 2);

                this.video.SetPixel(x, y, Color.FromArgb(RGB[0], RGB[1], RGB[2]));
            }
        }

        public void SetCursorPosition(Registers.Registers_ENUM register_one, Registers.Registers_ENUM register_two)
        {
            ushort new_cursor_x = this.virtual_system.GetRegisterWord(register_one);
            ushort new_cursor_y = this.virtual_system.GetRegisterWord(register_two);

            // make sure the new cursor position is within limits
            if (new_cursor_x < CHARACTERS_HORIZONTALY)
                this.cursor_x = new_cursor_x;

            if (new_cursor_y < CHARACTERS_VERTICALY)
                this.cursor_y = new_cursor_y;
        }

        public VirtualSystem GetCursorPosition(Registers.Registers_ENUM register_one, Registers.Registers_ENUM register_two)
        {
            this.virtual_system.SetRegisterWord(register_one, this.cursor_x);
            this.virtual_system.SetRegisterWord(register_two, this.cursor_y);

            return this.virtual_system;
        }

        public void ClearScreen()
        {
            for (int x = 0; x < this.video.Width; x++)
                for (int y = 0; y < this.video.Height; y++)
                    this.video.SetPixel(x, y, Color.Black);

            for (int x = 0; x < CHAR_WIDTH; x++)
                for (int y = 0; y < CHAR_HEIGHT; y++)
                    this.characters[x, y] = (char)0;

            this.cursor_x = 0;
            this.cursor_y = 0;
        }

        public VirtualSystem ReadCharacterAtCursorPosition(Registers.Registers_ENUM register)
        {
            this.virtual_system.SetRegisterWord(register, characters[this.cursor_x, this.cursor_y]);
            return this.virtual_system;
        }

        public void WriteCharacterAtCursorPosition(Registers.Registers_ENUM register)
        {
            if (this.cursor_y == CHARACTERS_VERTICALY)
                return;

            char character_to_write = (char) this.virtual_system.GetRegisterQuad(register);
            this.characters[this.cursor_x, this.cursor_y] = character_to_write;

            this.video_graphics.DrawString(character_to_write.ToString(), new Font(FontFamily.GenericSerif, 8), Brushes.White, new Point(this.cursor_x * CHAR_WIDTH, cursor_y * CHAR_HEIGHT));
            this.video_graphics.Flush();

            if (this.cursor_x + 1 < CHARACTERS_HORIZONTALY)
            {
                this.cursor_x++;
            }
            else
            {
                this.cursor_x = 0;
                this.cursor_y++;
            }
        }

        private VirtualSystem virtual_system;

        // all the video stuff
        private Bitmap video;
        private Graphics video_graphics;
        private char[,] characters;
        private ushort cursor_x = 0;
        private ushort cursor_y = 0;

        // the screen dimensions
        const int CHARACTERS_HORIZONTALY = 80;
        const int CHARACTERS_VERTICALY = 35;

        // the character dimensions
        const int CHAR_WIDTH = 8;
        const int CHAR_HEIGHT = 12;
    }
}
