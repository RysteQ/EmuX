using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Internal.BIOS.Enums;
using EmuXCore.VM.Internal.CPU.Enums;

namespace EmuXCore.VM.Interfaces;

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
    /// Sets the value of the specified byte with MSB endianness
    /// </summary>
    /// <param name="memoryLocation">The memory location of the byte</param>
    /// <param name="value">The new value of the byte</param>
    void SetByte(int memoryLocation, byte value);

    /// <summary>
    /// Sets the value of the specified word, the first byte is MSB of the lower byte at location + 0, the second byte is MSB of the higher byte at location + 1
    /// </summary>
    /// <param name="memoryLocation">The memory location of the word</param>
    /// <param name="value">The new value of the word</param>
    void SetWord(int memoryLocation, ushort value);

    /// <summary>
    /// Sets the value of the specified double, the first word is MSB of the lower word at location + 0, the second word is MSB of the higher word at location + 2
    /// </summary>
    /// <param name="memoryLocation">The memory location of the word</param>
    /// <param name="value">The new value of the word</param>
    void SetDouble(int memoryLocation, uint value);

    /// <summary>
    /// Sets the value of the specified quad, the first word is MSB of the lower double at location + 0, the second word is MSB of the higher double at location + 4
    /// </summary>
    /// <param name="memoryLocation">The memory location of the double</param>
    /// <param name="value">The new value of the double</param>
    void SetQuad(int memoryLocation, ulong value);

    /// <summary>
    /// Gets the byte at the specified memory location with MSB endianness
    /// </summary>
    /// <param name="memoryLocation">The memory location of the byte</param>
    byte GetByte(int memoryLocation);

    /// <summary>
    /// Gets the word at the specified memory location with MSB endianness, the lower byte is MSB at location + 0, the higher byte is MSB at location + 1
    /// </summary>
    /// <param name="memoryLocation">The memory location of the word</param>
    ushort GetWord(int memoryLocation);

    /// <summary>
    /// Gets the double at the specified memory location with MSB endianness, the lower word is MSB at location + 0, the higher word is MSB at location + 2
    /// </summary>
    /// <param name="memoryLocation">The memory location of the double</param>
    uint GetDouble(int memoryLocation);

    /// <summary>
    /// Gets the quad at the specified memory location with MSB endianness, the lower double is MSB at location + 0, the higher double is MSB at location + 2
    /// </summary>
    /// <param name="memoryLocation">The memory location of the quad</param>
    ulong GetQuad(int memoryLocation);

    /// <summary>
    /// Pushed a word onto the stack and updates the RSP register
    /// </summary>
    /// <param name="value">The word to push</param>
    void PushWord(ushort value);

    /// <summary>
    /// Pushed a double onto the stack and updates the RSP register
    /// </summary>
    /// <param name="value">The double to push</param>
    void PushDouble(uint value);

    /// <summary>
    /// Pushed a quad onto the stack and updates the RSP register
    /// </summary>
    /// <param name="value">The quad to push</param>
    void PushQuad(ulong value);

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
    /// The virtual devices of the virtual machine that inherits from the IVirtualDevice interface
    /// </summary>
    IVirtualDevice[] Devices { get; init; }
}