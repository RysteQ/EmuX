using EmuX.src.Enums.Emulators.VM;
using EmuX.src.Models.Emulator;
using EmuX.src.Services.Interrupt_Handler;

namespace EmuX;

class Interrupt_Handler
{
    public Bitmap GetVideoOutput() => this.video_handler.Video;

    public void ResetInterrupt()
    {
        this.Interrupt = new();
    }

    public void ExecuteInterrupt()
    {
        this.video_handler.VirtualSystem = this.VirtualSystem;

        switch (Interrupt.interrupt_code)
        {
            case InterruptCode.Set_Cursor_Position:
                this.video_handler.SetCursorPosition(FIRST_ARGUMENT_REGISTER, SECOND_ARGUMENT_REGISTER);
                break;

            case InterruptCode.Get_Cursor_Position:
                this.VirtualSystem = this.video_handler.GetCursorPosition(FIRST_ARGUMENT_REGISTER, SECOND_ARGUMENT_REGISTER);
                break;

            case InterruptCode.Clear_Screen:
                this.video_handler.ClearScreen();
                break;

            case InterruptCode.Read_Character_At_Cursor_Position:
                this.VirtualSystem = this.video_handler.ReadCharacterAtCursorPosition(FIRST_ARGUMENT_REGISTER);
                break;

            case InterruptCode.Write_Character_At_Cursor_Position:
                this.video_handler.WriteCharacterAtCursorPosition(FIRST_ARGUMENT_REGISTER);
                break;

            case InterruptCode.Write_Pixel_At_Position:
                this.video_handler.RefreshVideo();
                break;

            case InterruptCode.Read_From_Disk:
                this.VirtualSystem = this.disk_handler.ReadFromDisk(FIRST_ARGUMENT_REGISTER, SECOND_ARGUMENT_REGISTER);
                break;

            case InterruptCode.Write_To_Disk:
                this.disk_handler.VirtualSystem = this.VirtualSystem;
                this.disk_handler.WriteToDisk(FIRST_ARGUMENT_REGISTER, SECOND_ARGUMENT_REGISTER);
                break;

            case InterruptCode.Wait_Delay:
                WaitDelay();
                break;
        }
    }

    private void WaitDelay()
    {
        int miliseconds_to_sleep_for = (int)VirtualSystem.GetRegisterDouble(FIRST_ARGUMENT_REGISTER);
        Thread.Sleep(miliseconds_to_sleep_for);
    }


    public VirtualSystem VirtualSystem = new();
    public Interrupt Interrupt { private get; set; } = new();
    private VideoHandler video_handler = new();
    private DiskHandler disk_handler = new();

    // the argument registers
    const Registers FIRST_ARGUMENT_REGISTER = Registers.RCX;
    const Registers SECOND_ARGUMENT_REGISTER = Registers.RDX;
}