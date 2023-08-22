using EmuX.src.Enums;
using EmuX.src.Enums.VM;

namespace EmuX.src.Models;

public class Interrupt
{
    public void SetInterruptCode(ulong interrupt_int_code)
    {
        switch (interrupt_int_code)
        {
            case 2:
                interrupt_code = InterruptCode.Set_Cursor_Position;
                break;

            case 3:
                interrupt_code = InterruptCode.Get_Cursor_Position;
                break;

            case 6:
                interrupt_code = InterruptCode.Clear_Screen;
                break;

            case 8:
                interrupt_code = InterruptCode.Read_Character_At_Cursor_Position;
                break;

            case 9:
                interrupt_code = InterruptCode.Write_Character_At_Cursor_Position;
                break;

            case 12:
                interrupt_code = InterruptCode.Write_Pixel_At_Position;
                break;

            case 15:
                interrupt_code = InterruptCode.Read_From_Disk;
                break;

            case 16:
                interrupt_code = InterruptCode.Write_To_Disk;
                break;

            case 92:
                interrupt_code = InterruptCode.Wait_Delay;
                break;

            default:
                interrupt_code = InterruptCode.NoN;
                break;
        }
    }

    public void SetInterruptCode(InterruptCode interrupt_code)
    {
        this.interrupt_code = interrupt_code;
    }

    public InterruptCode GetInterruptCode()
    {
        return interrupt_code;
    }

    private InterruptCode interrupt_code;
}
