using EmuX.src.Enums.VM;

namespace EmuX.src.Services.Interrupt_Handler;

public class VideoHandler
{
    public VideoHandler()
    {
        this.Video = new(CHARACTERS_HORIZONTALY * CHAR_WIDTH, CHARACTERS_VERTICALY * CHAR_HEIGHT);
        this.Video_graphics = Graphics.FromImage(this.Video);

        for (int x = 0; x < this.Video.Width; x++)
            for (int y = 0; y < this.Video.Height; y++)
                this.Video.SetPixel(x, y, Color.Black);

        this.VirtualSystem = new();
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

            RGB[0] = this.VirtualSystem.GetByteMemory(stack_general_memory_limit + i);
            RGB[1] = this.VirtualSystem.GetByteMemory(stack_general_memory_limit + i + 1);
            RGB[2] = this.VirtualSystem.GetByteMemory(stack_general_memory_limit + i + 2);

            this.Video.SetPixel(x, y, Color.FromArgb(RGB[0], RGB[1], RGB[2]));
        }
    }

    public void SetCursorPosition(Registers register_one, Registers register_two)
    {
        ushort new_cursor_x = this.VirtualSystem.GetRegisterWord(register_one);
        ushort new_cursor_y = this.VirtualSystem.GetRegisterWord(register_two);

        // make sure the new cursor position is within limits
        if (new_cursor_x < CHARACTERS_HORIZONTALY)
            this.cursor_x = new_cursor_x;

        if (new_cursor_y < CHARACTERS_VERTICALY)
            this.cursor_y = new_cursor_y;
    }

    public VirtualSystem GetCursorPosition(Registers register_one, Registers register_two)
    {
        this.VirtualSystem.SetRegisterWord(register_one, this.cursor_x);
        this.VirtualSystem.SetRegisterWord(register_two, this.cursor_y);

        return this.VirtualSystem;
    }

    public void ClearScreen()
    {
        for (int x = 0; x < this.Video.Width; x++)
            for (int y = 0; y < this.Video.Height; y++)
                this.Video.SetPixel(x, y, Color.Black);

        for (int x = 0; x < CHAR_WIDTH; x++)
            for (int y = 0; y < CHAR_HEIGHT; y++)
                this.characters[x, y] = (char)0;

        this.cursor_x = 0;
        this.cursor_y = 0;
    }

    public VirtualSystem ReadCharacterAtCursorPosition(Registers register)
    {
        this.VirtualSystem.SetRegisterWord(register, characters[this.cursor_x, this.cursor_y]);
        return this.VirtualSystem;
    }

    public void WriteCharacterAtCursorPosition(Registers register)
    {
        if (this.cursor_y == CHARACTERS_VERTICALY)
            return;

        char character_to_write = (char) this.VirtualSystem.GetRegisterQuad(register);
        this.characters[this.cursor_x, this.cursor_y] = character_to_write;

        this.Video_graphics.DrawString(character_to_write.ToString(), new Font(FontFamily.GenericSerif, 8), Brushes.White, new Point(this.cursor_x * CHAR_WIDTH, cursor_y * CHAR_HEIGHT));
        this.Video_graphics.Flush();

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

    public VirtualSystem VirtualSystem;
    public Bitmap Video;

    private Graphics Video_graphics;
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