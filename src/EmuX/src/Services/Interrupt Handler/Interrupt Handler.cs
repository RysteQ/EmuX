using EmuX.src.Enums;
using EmuX.src.Models;
using EmuX.src.Services.Interrupt_Handler;

namespace EmuX
{
    class Interrupt_Handler
    {
        public Interrupt_Handler()
        {
            this.video_handler = new Video_Handler();
            this.disk_handler = new Disk_Handler();

            this.interrupt = new Interrupt();
            this.virtual_system = new VirtualSystem();
        }

        public Bitmap GetVideoOutput()
        {
            return this.video_handler.GetVideo();
        }

        public void SetInterrupt(Interrupt interrupt)
        {
            this.interrupt = interrupt;
        }

        public void ResetInterrupt()
        {
            this.interrupt = new Interrupt();
        }

        public VirtualSystem GetVirtualSystem()
        {
            return this.virtual_system;
        }

        public void SetVirtualSystem(VirtualSystem virtual_system)
        {
            this.virtual_system = virtual_system;
        }

        public void ExecuteInterrupt()
        {
            this.video_handler.SetVirtualSystem(this.virtual_system);

            switch (interrupt.GetInterruptCode())
            {
                case Interrupt_Codes.Interrupt_Code.Set_Cursor_Position:
                    this.video_handler.SetCursorPosition(FIRST_ARGUMENT_REGISTER, SECOND_ARGUMENT_REGISTER);
                    break;

                case Interrupt_Codes.Interrupt_Code.Get_Cursor_Position:
                    this.virtual_system = this.video_handler.GetCursorPosition(FIRST_ARGUMENT_REGISTER, SECOND_ARGUMENT_REGISTER);
                    break;

                case Interrupt_Codes.Interrupt_Code.Clear_Screen:
                    this.video_handler.ClearScreen();
                    break;

                case Interrupt_Codes.Interrupt_Code.Read_Character_At_Cursor_Position:
                    this.virtual_system = this.video_handler.ReadCharacterAtCursorPosition(FIRST_ARGUMENT_REGISTER);
                    break;

                case Interrupt_Codes.Interrupt_Code.Write_Character_At_Cursor_Position:
                    this.video_handler.WriteCharacterAtCursorPosition(FIRST_ARGUMENT_REGISTER);
                    break;

                case Interrupt_Codes.Interrupt_Code.Write_Pixel_At_Position:
                    this.video_handler.RefreshVideo();
                    break;

                case Interrupt_Codes.Interrupt_Code.Read_From_Disk:
                    this.virtual_system = this.disk_handler.ReadFromDisk(FIRST_ARGUMENT_REGISTER, SECOND_ARGUMENT_REGISTER);
                    break;

                case Interrupt_Codes.Interrupt_Code.Write_To_Disk:
                    this.disk_handler.WriteToDisk(FIRST_ARGUMENT_REGISTER, SECOND_ARGUMENT_REGISTER);
                    break;

                case Interrupt_Codes.Interrupt_Code.Wait_Delay:
                    WaitDelay();
                    break;
            }
        }

        private void WaitDelay()
        {
            int miliseconds_to_sleep_for = (int)virtual_system.GetRegisterDouble(FIRST_ARGUMENT_REGISTER);
            Thread.Sleep(miliseconds_to_sleep_for);
        }

        private Interrupt interrupt;
        private VirtualSystem virtual_system;

        private Video_Handler video_handler;
        private Disk_Handler disk_handler;

        // the argument registers
        const Registers.Registers_ENUM FIRST_ARGUMENT_REGISTER = Registers.Registers_ENUM.RCX;
        const Registers.Registers_ENUM SECOND_ARGUMENT_REGISTER = Registers.Registers_ENUM.RDX;
    }
}
