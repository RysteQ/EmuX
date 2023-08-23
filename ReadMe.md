# Emux

EmuX is a x86 emulator which gives the programmer the oppurtunity to write and execute x86 code and also debug the code or even modify the memory or registers of the system. It supports almost every instruction of the original x86 instruction set.

![EmuX](/misc/EmuX%20screenshot.png)

Contents

- [Emux](#emux)
  - [Memory Map](#memory-map)
  - [Future Goals](#future-goals)
  
## Memory Map

![Memory Map](/misc/memory%20map.png)

🟥 **(0x0000 - 0x00400)** Stack Memory <br>
🟦 **(0x00400 - 0x02000)** General Purpose Memory <br>
🟨 **(0x02000 - 0xC4E00)** Screen Memory <br>

## Future Goals

The future goals of this project is having the option to actually compile your program as a .exe file and the ability to take a snapshot of the memory each time an instruction is executed for debugging purposes. Also, this might come first, making the UI look better with winforms since I have been thinking of using MAUI but I decided not to, it uses quite a bit of RAM.

And of course, a lot, and I mean a **lot** of debugging.
