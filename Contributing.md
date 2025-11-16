# Contributing

EmuX is open to all kinds of contribution given it is source available. With that said there are some limitation when contributing that must be covered for EmuX to keep a certain level of consistency and a standard in terms of quality of code.

## General guidelines

### Optimisation is second priority

We all love optimised code, but optimised code may have a negative effect in the velocity of the changes we want to push to our users, as such our first priority is to create maintainable and clean software as long as it is reasonably optimised.

### The user is above all

Although this is a hobby project of mine that doesn't mean I place myself above the user. The user is of the upmost priority, so much so that any software that doesn't place the user on top is not open source software or source available, but just another hobby project by the programmers for themselves and no one else.

### Depedency injection is a must

Depedency injection is a must for this code base, for this reason alone if we are to create a single class then we must create an accompanying interface for said class.

### Consistency

Consistency is important for it allows a developer to easily navigate around and understand the code much easier if they are used to a consistent codebase.

### EmuX versus EmuXCore

EmuX is not the same as EmuXCore. EmuXCore is, as the name suggests, the core of the application, that means it is UI agnostic but carries the core functionality which allows us to make it cross platform. EmuX on the other hand is the entirety of the project, EmuXCore included as well as the unit tests and all UI solutions.

## Specific guidelines

There are some specific guidelines you can look at for adding instructions, registers, virtual devices, BIOS interrupts and such. For specific guidlines and how to get started go to the contributing section.