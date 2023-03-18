# Emux

EmuX is a x86 emulator which gives the programmer the oppurtunity to write and execute x86 code and also debug the code or even modify the memory or registers of the system.

![EmuX](/misc/EmuX%20screenshot.png)

Contents

- [Emux](#emux)
  - [Memory Map](#memory-map)
  - [Instructions](#instructions)
  - [Future Goals](#future-goals)
  
## Memory Map

The following image is of the memory map for version 1.0.0, the upcoming version (1.1.0) will have a vastly different memory map.

![Memory Map](/misc/memory%20map.png)

🟥 **(0x0000 - 0x0400)** Stack Memory
🟦 **(0x0400 - 0x2000)** General Purpose Memory

## Instructions

EmuX supports almost every instruction of the original x86 instruction set, the supported instruction list and what each instruction does can be found in the application under the Tools -> Instruction Set section.

## Future Goals

The future goals of this project of mine will be adding a framebuffer, having detachable tabs, having the option to actually compile your program as a .exe file and the ability to take a snapshot of the memory each time an instruction is executed for debugging purposes.

---

*Version: 1.0.0*