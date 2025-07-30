using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Interfaces.Components.BIOS.Interfaces;

/// <summary>
/// The disk interrupt handler is used to handle all the disk the sub-interrupt code function calls
/// </summary>
public interface IDiskInterruptHandler
{
    /// <summary>
    /// This method invokes the read track BIOS function call <br/>
    /// Sectors to read: AL <br/>
    /// Starting platter: DH <br/>
    /// Selected track: CH <br/>
    /// Selected sector: CL <br/>
    /// Disk to read from: DL <br/>
    /// Address reference: [ES:BX] <br/>
    /// </summary>
    /// <param name="virtualCPU">The IVirtualCPU implementation to get the parameters this method requires from</param>
    /// <param name="virtualMemory">The IVirtualMemory module to save the results of this BIOS function call</param>
    /// <param name="virtualDisks">All of the IVirtualDisk implementations to get the data from</param>
    /// <exception cref="DriveNotFoundException">Thrown if the specified drive is not found</exception>
    void ReadTrack(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks);

    /// <summary>
    /// This method invokes the write track BIOS function call <br/>
    /// Sectors to read: AL <br/>
    /// Starting platter: DH <br/>
    /// Selected track: CH <br/>
    /// Selected sector: CL <br/>
    /// Disk to read from: DL <br/>
    /// Address reference: [ES:BX] <br/>
    /// </summary>
    /// <param name="virtualCPU">The IVirtualCPU implementation to get the parameters this method requires from</param>
    /// <param name="virtualMemory">The IVirtualMemory module to get the data that are going to be written on to the disk</param>
    /// <param name="virtualDisks">All of the IVirtualDisk implementations to write the data to</param>
    /// <exception cref="DriveNotFoundException">Thrown if the specified drive is not found</exception>
    void WriteTrack(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks);
}