using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;

namespace EmuXCore.VM.Internal.Disk;

public class VirtualDisk : IVirtualDisk
{
    public VirtualDisk(byte diskNumber, byte platters, ushort tracksPerPlatter, byte sectorsPerTrack, IVirtualMachine? parentVirtualMachine = null)
    {
        DiskNumber = diskNumber;
        Platters = platters;
        Tracks = tracksPerPlatter;
        Sectors = sectorsPerTrack;

        _data = new byte[Platters][][][];

        for (int platter = 0; platter < Platters; platter++)
        {
            _data[platter] = new byte[Tracks][][];

            for (int track = 0; track < Tracks; track++)
            {
                _data[platter][track] = new byte[Sectors][];

                for (int sector = 0; sector < sectorsPerTrack; sector++)
                {
                    _data[platter][track][sector] = new byte[BytesPerSector];

                    Random.Shared.NextBytes(_data[platter][track][sector]);
                }
            }
        }

        ParentVirtualMachine = parentVirtualMachine;
    }

    public byte[] ReadSector(byte platter, ushort track, byte sector)
    {
        CheckIfAddressIsValid(platter, track, sector);
    
        return _data[platter][track][sector];
    }

    public void WriteSector(byte platter, ushort track, byte sector, byte[] bytesToWrite)
    {
        CheckIfAddressIsValid(platter, track, sector);

        Array.Copy(bytesToWrite, _data[platter][track][sector], BytesPerSector);
    }

    private void CheckIfAddressIsValid(byte platter, ushort track, byte sector)
    {
        if (platter >= Platters)
        {
            throw new IndexOutOfRangeException($"Platter {platter + 1} is not accessible since there are only {Platters} platters available");
        }

        if (track >= Tracks)
        {
            throw new IndexOutOfRangeException($"Track {track + 1} is not accessible since there are only {Tracks} tracks per platter available");
        }

        if (sector >= Sectors)
        {
            throw new IndexOutOfRangeException($"Sector {sector + 1} is not accessible since there are only {Sectors} tracks per sector available");
        }
    }

    public IVirtualMachine? ParentVirtualMachine { get; set; }
    public byte DiskNumber { get; init; }
    public byte Platters { get; init; }
    public ushort Tracks { get; init; }
    public byte Sectors { get; init; }
    public ushort BytesPerSector { get => 512; }
    public uint TotalBytes { get => (uint)(Platters * Tracks * Sectors * BytesPerSector); }

    private byte[][][][] _data;
}