using EmuXCore.VM.Interfaces.Exceptions;

namespace EmuXCore.VM.Interfaces.Components;

/// <summary>
/// The IVirtualDisk module meant to emulate a real hard drive.
/// </summary>
public interface IVirtualDisk : IVirtualComponent
{
    /// <summary>
    /// Reads a disk sector.
    /// </summary>
    /// <param name="platter">The platter to read from, zero indexed.</param>
    /// <param name="track">The track to read from, zero indexed.</param>
    /// <param name="sector">The sector to read from, zero indexed.</param>
    /// <exception cref="InvalidDiskPlatterException" />
    /// <exception cref="InvalidDiskTrackException" />
    /// <exception cref="InvalidDiskSectorException" />
    /// <returns>The read data.</returns>
    byte[] ReadSector(byte platter, ushort track, byte sector);

    /// <summary>
    /// Writes to a disk sector.
    /// </summary>
    /// <param name="platter">The platter to write to, zero indexed.</param>
    /// <param name="track">The track to write to, zero indexed.</param>
    /// <param name="sector">The sector to write to, zero indexed.</param>
    /// <param name="bytesToWrite">Must be of length defined in BytesPerSector.</param>
    /// <exception cref="InvalidDiskPlatterException" />
    /// <exception cref="InvalidDiskTrackException" />
    /// <exception cref="InvalidDiskSectorException" />
    /// <exception cref="DiskWriteOperationExceedsAvailableMemoryException" />
    void WriteSector(byte platter, ushort track, byte sector, byte[] bytesToWrite);

    /// <summary>
    /// The disk number ID, must be unique.
    /// </summary>
    byte DiskNumber { get; init; }

    /// <summary>
    /// The amount of platters the disk has.
    /// </summary>
    byte Platters { get; init; }

    /// <summary>
    /// The amount of tracks per platter.
    /// </summary>
    ushort Tracks { get; init; }

    /// <summary>
    /// The amount of sectors per track.
    /// </summary>
    byte Sectors { get; init; }

    /// <summary>
    /// The amount of bytes per sector.
    /// </summary>
    ushort BytesPerSector { get; }

    /// <summary>
    /// The total bytes the disk can hold.
    /// </summary>
    uint TotalBytes { get; }
}