namespace EmuX.src.Enums.VM;

public enum InterruptCode
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