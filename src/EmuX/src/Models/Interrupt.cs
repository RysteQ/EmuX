using EmuX.src.Enums;

namespace EmuX.src.Models
{
    public class Interrupt
    {
        public void SetInterruptCode(ulong interrupt_int_code)
        {
            switch (interrupt_int_code)
            {
                case 2:
                    interrupt_code = Interrupt_Codes.Interrupt_Code.Set_Cursor_Position;
                    break;

                case 3:
                    interrupt_code = Interrupt_Codes.Interrupt_Code.Get_Cursor_Position;
                    break;

                case 6:
                    interrupt_code = Interrupt_Codes.Interrupt_Code.Clear_Screen;
                    break;

                case 8:
                    interrupt_code = Interrupt_Codes.Interrupt_Code.Read_Character_At_Cursor_Position;
                    break;

                case 9:
                    interrupt_code = Interrupt_Codes.Interrupt_Code.Write_Character_At_Cursor_Position;
                    break;

                case 12:
                    interrupt_code = Interrupt_Codes.Interrupt_Code.Write_Pixel_At_Position;
                    break;

                case 15:
                    interrupt_code = Interrupt_Codes.Interrupt_Code.Read_From_Disk;
                    break;

                case 16:
                    interrupt_code = Interrupt_Codes.Interrupt_Code.Write_To_Disk;
                    break;

                case 92:
                    interrupt_code = Interrupt_Codes.Interrupt_Code.Wait_Delay;
                    break;

                default:
                    interrupt_code = Interrupt_Codes.Interrupt_Code.NoN;
                    break;
            }
        }

        public void SetInterruptCode(Interrupt_Codes.Interrupt_Code interrupt_code)
        {
            this.interrupt_code = interrupt_code;
        }

        public Interrupt_Codes.Interrupt_Code GetInterruptCode()
        {
            return interrupt_code;
        }

        private Interrupt_Codes.Interrupt_Code interrupt_code = Interrupt_Codes.Interrupt_Code.NoN;
    }
}
