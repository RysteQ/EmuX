using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces.Actions;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Interfaces.Components.BIOS;
using EmuXCore.VM.Interfaces.Components.BIOS.Enums;
using EmuXCore.VM.Interfaces.Exceptions;
using EmuXCore.VM.Interfaces.Events;

namespace EmuXCore.VM.Interfaces;

/// <summary>
/// The IVirtualMachine is meant to emulate an entire system so that the execution of the code is contained. <br/>
/// As such there are multiple advantages, mainly having full, easy to access, control over the inner workings of the VM.
/// </summary>
public interface IVirtualMachine
{
    /// <summary>
    /// Set the specified register bit value one or zero based on the value.
    /// </summary>
    /// <param name="flag"></param>
    /// <param name="value"></param>
    void SetFlag(EFlags flag, bool value);

    /// <summary>
    /// Sets the IOPL register.
    /// </summary>
    /// <param name="firstBit">The first bit of the IOPL register.</param>
    /// <param name="secondBit">The second bit of the IOPL register.</param>
    void SetIOPL(bool firstBit, bool secondBit);

    /// <summary>
    /// Get the specified value flag state.
    /// </summary>
    /// <param name="flag">The flag bit to retrieve.</param>
    /// <returns>The state of the flag.</returns>
    bool GetFlag(EFlags flag);

    /// <summary>
    /// A specified GetFlag method for the IOPL register.
    /// </summary>
    /// <returns>The byte value of the IOPL register shifted by 12 bits to the right.</returns>
    byte GetIOPL();

    /// <summary>
    /// Sets the value of the specified byte.
    /// </summary>
    /// <param name="memoryLocation">The memory location of the byte.</param>
    /// <param name="value">The new value of the byte.</param>
    void SetByte(int memoryLocation, byte value);

    /// <summary>
    /// Sets the value of the specified word, the first byte is of the lower byte at location + 0, the second byte is of the higher byte at location + 1.
    /// </summary>
    /// <param name="memoryLocation">The memory location of the word.</param>
    /// <param name="value">The new value of the word.</param>
    void SetWord(int memoryLocation, ushort value);

    /// <summary>
    /// Sets the value of the specified double, the first word is of the lower word at location + 0, the second word is of the higher word at location + 2.
    /// </summary>
    /// <param name="memoryLocation">The memory location of the word.</param>
    /// <param name="value">The new value of the word.</param>
    void SetDouble(int memoryLocation, uint value);

    /// <summary>
    /// Sets the value of the specified quad, the first word is of the lower double at location + 0, the second word is of the higher double at location + 4.
    /// </summary>
    /// <param name="memoryLocation">The memory location of the double.</param>
    /// <param name="value">The new value of the double.</param>
    void SetQuad(int memoryLocation, ulong value);

    /// <summary>
    /// Gets the byte at the specified memory location.
    /// </summary>
    /// <param name="memoryLocation">The memory location of the byte.</param>
    byte GetByte(int memoryLocation);

    /// <summary>
    /// Gets the word at the specified memory location, the lower byte is at location + 0, the higher byte is at location + 1.
    /// </summary>
    /// <param name="memoryLocation">The memory location of the word.</param>
    ushort GetWord(int memoryLocation);

    /// <summary>
    /// Gets the double at the specified memory location, the lower word is at location + 0, the higher word is at location + 2.
    /// </summary>
    /// <param name="memoryLocation">The memory location of the double.</param>
    uint GetDouble(int memoryLocation);

    /// <summary>
    /// Gets the quad at the specified memory location, the lower double is at location + 0, the higher double is at location + 2.
    /// </summary>
    /// <param name="memoryLocation">The memory location of the quad.</param>
    ulong GetQuad(int memoryLocation);

    /// <summary>
    /// Pushes byte onto the stack and updates the RSP <see cref="IVirtualRegister"/>.
    /// </summary>
    /// <param name="value">The byte to push.</param>
    void PushByte(byte value);

    /// <summary>
    /// Pushes a word onto the stack and updates the RSP <see cref="IVirtualRegister"/>.
    /// </summary>
    /// <param name="value">The word to push.</param>
    void PushWord(ushort value);

    /// <summary>
    /// Pushes a double onto the stack and updates the RSP <see cref="IVirtualRegister"/>.
    /// </summary>
    /// <param name="value">The double to push.</param>
    void PushDouble(uint value);

    /// <summary>
    /// Pushes a quad onto the stack and updates the RSP <see cref="IVirtualRegister"/>.
    /// </summary>
    /// <param name="value">The quad to push.</param>
    void PushQuad(ulong value);

    /// <summary>
    /// Pops a word from the stack and updates the RSP <see cref="IVirtualRegister"/>.
    /// </summary>
    /// <returns>The word popped from the stack.</returns>
    byte PopByte();

    /// <summary>
    /// Pops a word from the stack and updates the RSP <see cref="IVirtualRegister"/>.
    /// </summary>
    /// <returns>The word popped from the stack.</returns>
    ushort PopWord();

    /// <summary>
    /// Pops a double from the stack and updates the RSP <see cref="IVirtualRegister"/>.
    /// </summary>
    /// <returns>The double popped from the stack.</returns>
    uint PopDouble();

    /// <summary>
    /// Pops a quad from the stack and updates the RSP <see cref="IVirtualRegister"/>.
    /// </summary>
    /// <returns>The quad popped from the stack.</returns>
    ulong PopQuad();

    /// <summary>
    /// Triggers a <see cref="IVirtualBIOS"/> interrupt.
    /// </summary>
    /// <param name="interruptCode">The interrupt code.</param>
    /// <param name="subInterrupt">The sub interrupt code.</param>
    /// <exception cref="VirtualBIOSInterruptCodeNotFound">Thrown if the sub interrupt code has not been found.</exception>
    /// <exception cref="VirtualBIOSInterruptNotImplementedException">Thrown if the interrupt functionality has not been implemented.</exception>
    void Interrupt(InterruptCode interruptCode, object subInterrupt);

    /// <summary>
    /// Registers an <see cref="IVmAction"> to the <see cref="IVirtualMachine.Actions"> instance.
    /// </summary>
    /// <param name="action">The type of the action to register as.</param>
    /// <param name="size">The size of the action to register.</param>
    /// <param name="previousValue">The previous value before the action was taken.</param>
    /// <param name="newValue">The new value after the action was taken.</param>
    /// <param name="registerName">The register name if applicable.</param>
    /// <param name="memoryPointer">The memory pointer if applicable.</param>
    void RegisterAction(VmActionCategory action, Size size, byte[] previousValue, byte[] newValue, string? registerName = null, int? memoryPointer = null, int? deviceId = null, byte? diskId = null);

    /// <summary>
    /// Undos N <see cref="IVmAction">.
    /// </summary>
    /// <param name="actionsToUndo">The amount of <see cref="IVmAction"> to undo, default is one.</param>
    void UndoActions(int actionsToUndo = 1);

    /// <summary>
    /// Redos N <see cref="IVmAction">.
    /// </summary>
    /// <param name="actionsToRedo">The amount of <see cref="IVmAction"> to redo, default is one</param>
    void RedoActions(int actionsToRedo = 1);

    /// <summary>
    /// Raises an access event.
    /// </summary>
    /// <param name="args">The arguments to pass on the event data.</param>
    /// <exception cref="ArgumentException" />
    void InvokeAccessEvent(EventArgs args);

    /// <summary>
    /// The event is raised when the <see cref="IVirtualMemory"/> module is accessed from the <see cref="IVirtualMachine"/> layer. <br/>
    /// Returns an <see cref="EventArgs"/> object of type <see cref="IMemoryAccess"/>.
    /// </summary>
    event EventHandler? MemoryAccessed;

    /// <summary>
    /// The event is raised when the stack inside the <see cref="IVirtualMemory "/> is accessed from the <see cref="IVirtualMachine"/> layer. <br/>
    /// Returns an <see cref="EventArgs"/> object of type <see cref="IStackAccess"/>.
    /// </summary>
    event EventHandler? StackAccessed;

    /// <summary>
    /// The event is raised when a <see cref="IVirtualGPU"/> flag is accessed from the <see cref="IVirtualMachine"/> layer. <br/>
    /// Returns an <see cref="EventArgs"/> object of type <see cref="IFlagAccess"/>.
    /// </summary>
    event EventHandler? FlagAccessed;

    /// <summary>
    /// The event is raised when a <see cref="IVirtualCPU"/> flag is modified. <br/>
    /// Returns an <see cref="EventArgs"/> object of type <see cref="IRegisterAccess"/>.
    /// </summary>
    event EventHandler? RegisterAccessed;

    /// <summary>
    /// The event is raised when the <see cref="IVirtualGPU"/> memory buffer is modified. <br/>
    /// Returns an <see cref="EventArgs"/> object of type <see cref="IVirtualGPU"/>.
    /// </summary>
    event EventHandler? VideoCardAccessed;

    /// <summary>
    /// The actions that modified the state of the <see cref="IVirtualMachine"/> for memory related operations.
    /// </summary>
    public IList<IVmAction> Actions { get; set; }

    /// <summary>
    /// The <see cref="IVirtualMemory"/> module implementation.
    /// </summary>
    IVirtualMemory Memory { get; init; }

    /// <summary>
    /// The <see cref="IVirtualCPU"/> module implementation.
    /// </summary>
    IVirtualCPU CPU { get; init; }

    /// <summary>
    /// The <see cref="IVirtualDisk"/> module implementations.
    /// </summary>
    IVirtualDisk[] Disks { get; init; }

    /// <summary>
    /// The <see cref="IVirtualBIOS"/> module implementation.
    /// </summary>
    IVirtualBIOS BIOS { get; init; }

    /// <summary>
    /// The <see cref="IVirtualRTC"/> module implementation.
    /// </summary>
    IVirtualRTC RTC { get; init; }

    /// <summary>
    /// The <see cref="IVirtualGPU"/> module implementation.
    /// </summary>
    IVirtualGPU GPU { get; init; }

    /// <summary>
    /// The <see cref="IVirtualDevice"/> module implementations.
    /// </summary>
    IVirtualDevice[] Devices { get; init; }
}