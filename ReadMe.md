# Emux

EmuX is a x86 emulator which gives the programmer the oppurtunity to write and execute x86 code and also debug the code or even modify the memory or registers of the system.

**The project is badly written and no longer maintained**

![EmuX](/misc/EmuX%20screenshot.png)

Contents

- [Emux](#emux)
  - [Memory Map](#memory-map)
  - [Instructions](#instructions)
  - [Future Goals](#future-goals)
  
## Memory Map

![Memory Map](/misc/memory%20map.png)

🟥 **(0x0000 - 0x00400)** Stack Memory <br>
🟦 **(0x00400 - 0x02000)** General Purpose Memory <br>
🟨 **(0x02000 - 0xC4E00)** Screen Memory <br>

## Instructions

EmuX supports almost every instruction of the original x86 instruction set, the supported instruction list and what each instruction does can be found in the application under the Tools -> Instruction Set section.

## Future Goals

The future goals of this project of mine will be adding detachable tabs, having the option to actually compile your program as a .exe file and the ability to take a snapshot of the memory each time an instruction is executed for debugging purposes.

Also a lot, and I mean a **lot** of debugging.

---

*Version: 1.2.1*
