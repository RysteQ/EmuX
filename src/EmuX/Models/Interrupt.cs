namespace EmuX.Models
{
    class Interrupt
    {
        /// <summary>
        /// Translates an integer to its Interrupt_Codes enum value
        /// </summary>
        /// <param name="interrupt_int_code"></param>
        /// <returns></returns>
        public void SetInterruptCode(ulong interrupt_int_code)
        {
            switch (interrupt_int_code)
            {
                case 2:
                    interrupt_code = Interrupt_Codes.Set_Cursor_Position;
                    break;

                case 3:
                    interrupt_code = Interrupt_Codes.Get_Cursor_Position;
                    break;

                case 6:
                    interrupt_code = Interrupt_Codes.Clear_Screen;
                    break;

                case 8:
                    interrupt_code = Interrupt_Codes.Read_Character_At_Cursor_Position;
                    break;

                case 9:
                    interrupt_code = Interrupt_Codes.Write_Character_At_Cursor_Position;
                    break;

                case 12:
                    interrupt_code = Interrupt_Codes.Write_Pixel_At_Position;
                    break;

                case 15:
                    interrupt_code = Interrupt_Codes.Read_From_Disk;
                    break;

                case 16:
                    interrupt_code = Interrupt_Codes.Write_To_Disk;
                    break;

                case 92:
                    interrupt_code = Interrupt_Codes.Wait_Delay;
                    break;

                default:
                    interrupt_code = Interrupt_Codes.NoN;
                    break;
            }
        }

        /// <summary>
        /// Setter - Sets the new Interrupt Code value
        /// </summary>
        /// <param name="interrupt_code"></param>
        public void SetInterruptCode(Interrupt_Codes interrupt_code)
        {
            this.interrupt_code = interrupt_code;
        }

        /// <summary>
        /// Getter = Gets the Interrupt Code value
        /// </summary>
        /// <returns></returns>
        public Interrupt_Codes GetInterruptCode()
        {
            return interrupt_code;
        }

        private Interrupt_Codes interrupt_code = Interrupt_Codes.NoN;

        public enum Interrupt_Codes
        {
            Set_Cursor_Position = 2,
            Get_Cursor_Position = 3,
            Clear_Screen = 6,
            Read_Character_At_Cursor_Position = 8,
            Write_Character_At_Cursor_Position = 9,
            Write_Pixel_At_Position = 12,
            Read_From_Disk = 15,
            Write_To_Disk = 16,
            Wait_Delay = 92,

            NoN
        }
    }
}
