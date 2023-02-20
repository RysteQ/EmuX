namespace EmuX
{
    class Interrupt_Handler
    {
        /// <summary>
        /// Translates an integer to its Interrupt_Codes enum value
        /// </summary>
        /// <param name="interrupt_int_code"></param>
        /// <returns></returns>
        public Interrupt_Codes TranslateInterruptCode(int interrupt_int_code)
        {
            switch (interrupt_int_code)
            {
                case 0:
                    return Interrupt_Codes.Set_Video_Mode;

                case 2:
                    return Interrupt_Codes.Set_Cursor_Position;

                case 3:
                    return Interrupt_Codes.Get_Cursor_Position;

                case 6:
                    return Interrupt_Codes.Clear_Screen;

                case 8:
                    return Interrupt_Codes.Read_Character_At_Cursor_Position;

                case 9:
                    return Interrupt_Codes.Write_Character_At_Cursor_Position;

                case 12:
                    return Interrupt_Codes.Write_Pixel_At_Position;

                case 15:
                    return Interrupt_Codes.Read_From_Disk;

                case 16:
                    return Interrupt_Codes.Write_To_Disk;

                case 92:
                    return Interrupt_Codes.Wait_Delay;

                default:
                    return Interrupt_Codes.NoN;
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
            Set_Video_Mode = 0,
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
