namespace EmuXCore.VM.Interfaces.Components.BIOS.Interfaces;

/// <summary>
/// The <see cref="IVirtualDisk"/> interrupt handler.
/// </summary>
public interface IDiskInterruptHandler
{
    /// <summary>
    /// This method invokes the read track BIOS function call, <br/>
    /// Sectors to read: AL <br/>
    /// Starting platter: DH <br/>
    /// Selected track: CH <br/>
    /// Selected sector: CL <br/>
    /// Disk to read from: DL <br/>
    /// Address reference: [ES:BX] <br/>
    /// </summary>
    /// <param name="virtualCPU">The <see cref="IVirtualCPU"/> implementation to get the parameters this method requires from.</param>
    /// <param name="virtualMemory">The <see cref="IVirtualMemory"/> module to get the data that are going to be written on to the disk.</param>
    /// <param name="virtualDisks">All of the <see cref="IVirtualDisk"/> implementations to write the data to.</param>
    /// <exception cref="DriveNotFoundException" />
    void ReadTrack(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks);

    /// <summary>
    /// This method invokes the write track BIOS function call, <br/>
    /// Sectors to read: AL <br/>
    /// Starting platter: DH <br/>
    /// Selected track: CH <br/>
    /// Selected sector: CL <br/>
    /// Disk to read from: DL <br/>
    /// Address reference: [ES:BX] <br/>
    /// </summary>
    /// <param name="virtualCPU">The <see cref="IVirtualCPU"/> implementation to get the parameters this method requires from.</param>
    /// <param name="virtualMemory">The <see cref="IVirtualMemory"/> module to get the data that are going to be written on to the disk.</param>
    /// <param name="virtualDisks">All of the <see cref="IVirtualDisk"/> implementations to write the data to.</param>
    /// <exception cref="DriveNotFoundException" />
    void WriteTrack(IVirtualCPU virtualCPU, IVirtualMemory virtualMemory, IVirtualDisk[] virtualDisks);
}