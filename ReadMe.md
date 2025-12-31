# Emux

![EmuX IDE](Images/emux_ide.png)

EmuX is an open source software meant to simplify the process of learning x86 assembly by allowing the user to write and debug their application inside and IDE while having all of that code being execute on top of an intermediate layer.

```
+--------------------+
|        Code        |
+--------------------+
           |
           v
+--------------------+
| Intermediate layer |  <- EmuX containarised environment
+--------------------+
           |
           v
+--------------------+
|      Hardware      |
+--------------------+
```

This gives the freedom to the user of going back and forth the code execution, changing the system specifications on the fly (CPU, GPU, disks, and devices), view and modify register and memory values.

Also EmuX being open source means that it won't have the same fate as Emu8086, a software many people are aware of in this sphere that has sadly gone out of support. It has, on purpose, been designed to allow easy modification and addition of new instructions by other developers, for more about how to contribute click [here](Contributing.md).

---

Version: 2.0.1