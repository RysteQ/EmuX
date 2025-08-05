using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.BIOS;
using EmuXCore.VM.Interfaces.Components.BIOS.Enums;

namespace EmuXCore.VM.Interfaces;

/// <summary>
/// The IVirtualMachine is meant to emulate an entire system so that the execution of the code is contained. <br/>
/// As such there are multiple advantages, mainly having full, easy to access, control over the inner workings of the VM.
/// </summary>
public interface IVirtualMachine
{
    /// <summary>
    /// Set the specified register bit value one or zero based on the value
    /// </summary>
    /// <param name="flag"></param>
    /// <param name="value"></param>
    void SetFlag(EFlags flag, bool value);

    /// <summary>
    /// Sets the IOPL register
    /// </summary>
    /// <param name="firstBit">The first bit of the IOPL register</param>
    /// <param name="secondBit">The second bit of the IOPL register</param>
    void SetIOPL(bool firstBit, bool secondBit);

    /// <summary>
    /// Get the specified value flag state
    /// </summary>
    /// <param name="flag">The flag bit to retrieve</param>
    /// <returns>The state of the flag</returns>
    bool GetFlag(EFlags flag);

    /// <summary>
    /// A specified GetFlag method for the IOPL register
    /// </summary>
    /// <returns>The byte value of the IOPL register shifted by 12 bits to the right</returns>
    byte GetIOPL();

    /// <summary>
    /// Sets the value of the specified byte
    /// </summary>
    /// <param name="memoryLocation">The memory location of the byte</param>
    /// <param name="value">The new value of the byte</param>
    void SetByte(int memoryLocation, byte value);

    /// <summary>
    /// Sets the value of the specified word, the first byte is of the lower byte at location + 0, the second byte is of the higher byte at location + 1
    /// </summary>
    /// <param name="memoryLocation">The memory location of the word</param>
    /// <param name="value">The new value of the word</param>
    void SetWord(int memoryLocation, ushort value);

    /// <summary>
    /// Sets the value of the specified double, the first word is of the lower word at location + 0, the second word is of the higher word at location + 2
    /// </summary>
    /// <param name="memoryLocation">The memory location of the word</param>
    /// <param name="value">The new value of the word</param>
    void SetDouble(int memoryLocation, uint value);

    /// <summary>
    /// Sets the value of the specified quad, the first word is of the lower double at location + 0, the second word is of the higher double at location + 4
    /// </summary>
    /// <param name="memoryLocation">The memory location of the double</param>
    /// <param name="value">The new value of the double</param>
    void SetQuad(int memoryLocation, ulong value);

    /// <summary>
    /// Gets the byte at the specified memory location
    /// </summary>
    /// <param name="memoryLocation">The memory location of the byte</param>
    byte GetByte(int memoryLocation);

    /// <summary>
    /// Gets the word at the specified memory location, the lower byte is at location + 0, the higher byte is at location + 1
    /// </summary>
    /// <param name="memoryLocation">The memory location of the word</param>
    ushort GetWord(int memoryLocation);

    /// <summary>
    /// Gets the double at the specified memory location, the lower word is at location + 0, the higher word is at location + 2
    /// </summary>
    /// <param name="memoryLocation">The memory location of the double</param>
    uint GetDouble(int memoryLocation);

    /// <summary>
    /// Gets the quad at the specified memory location, the lower double is at location + 0, the higher double is at location + 2
    /// </summary>
    /// <param name="memoryLocation">The memory location of the quad</param>
    ulong GetQuad(int memoryLocation);

    /// <summary>
    /// Pushes byte onto the stack and updates the RSP register
    /// </summary>
    /// <param name="value">The byte to push</param>
    void PushByte(byte value);

    /// <summary>
    /// Pushes a word onto the stack and updates the RSP register
    /// </summary>
    /// <param name="value">The word to push</param>
    void PushWord(ushort value);

    /// <summary>
    /// Pushes a double onto the stack and updates the RSP register
    /// </summary>
    /// <param name="value">The double to push</param>
    void PushDouble(uint value);

    /// <summary>
    /// Pushes a quad onto the stack and updates the RSP register
    /// </summary>
    /// <param name="value">The quad to push</param>
    void PushQuad(ulong value);

    /// <summary>
    /// Pops a word from the stack and updates the RSP register
    /// </summary>
    /// <returns>The word popped from the stack</returns>
    byte PopByte();

    /// <summary>
    /// Pops a word from the stack and updates the RSP register
    /// </summary>
    /// <returns>The word popped from the stack</returns>
    ushort PopWord();

    /// <summary>
    /// Pops a double from the stack and updates the RSP register
    /// </summary>
    /// <returns>The double popped from the stack</returns>
    uint PopDouble();

    /// <summary>
    /// Pops a quad from the stack and updates the RSP register
    /// </summary>
    /// <returns>The quad popped from the stack</returns>
    ulong PopQuad();

    /// <summary>
    /// Triggers a BIOS interrupt
    /// </summary>
    /// <param name="interruptCode">The interrupt code</param>
    /// <param name="subInterrupt">The sub interrupt code</param>
    /// <exception cref="Exception">Thrown if the sub interrupt code has not been found</exception>
    /// <exception cref="NotImplementedException">Thrown if the interrupt functionality has not been implemented</exception>
    void Interrupt(InterruptCode interruptCode, object subInterrupt);

    /// <summary>
    /// The virtual memory of the virtual machine that inherits from the IVirtualMemory interface
    /// </summary>
    IVirtualMemory Memory { get; init; }

    /// <summary>
    /// The virtual CPU of the virtual machine that inherits from the IVirtualCPU interface
    /// </summary>
    IVirtualCPU CPU { get; init; }

    /// <summary>
    /// The virtual disks of the virtual machine that inherits from the IVirtualDisk interface
    /// </summary>
    IVirtualDisk[] Disks { get; init; }

    /// <summary>
    /// The virtual BIOS of the virtual machine that inherits from the IVirtualBIOS interface
    /// </summary>
    IVirtualBIOS BIOS { get; init; }

    /// <summary>
    /// The virtual RTC of the virtual machine that inherits from the IVirtualRTC interface
    /// </summary>
    IVirtualRTC RTC { get; init; }

    /// <summary>
    /// The virtual GPU of the virtual machine that inherits from the IVirtualGPU interface
    /// </summary>
    IVirtualGPU GPU { get; init; }

    /// <summary>
    /// The virtual devices of the virtual machine that inherits from the IVirtualDevice interface
    /// </summary>
    IVirtualDevice[] Devices { get; init; }
}