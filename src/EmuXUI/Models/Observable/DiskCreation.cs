using EmuXCore;
using EmuXUI.Models.Internal;

namespace EmuXUI.Models.Observable;

public sealed class DiskCreation : ObservableObject
{
    public DiskCreation()
    {
        _bytesPerSector = DIFactory.GenerateIVirtualDisk(0, 0, 0, 0).BytesPerSector;
        _platters = 1;
        _tracks = 1;
        _sectors = 1;
    }

    public int DiskNumber
    {
        get => _diskNumber;
        set
        {
            OnPropertyChanged(ref _diskNumber, value);
            OnPropertyChanged(nameof(TotalBytes));
        }
    }
    
    public int Platters
    {
        get => _platters;
        set
        {
            OnPropertyChanged(ref _platters, value);
            OnPropertyChanged(nameof(TotalBytes));
        }
    }
    
    public int Tracks
    {
        get => _tracks;
        set
        {
            OnPropertyChanged(ref _tracks, value);
            OnPropertyChanged(nameof(TotalBytes));
        }
    }
    
    public int Sectors
    {
        get => _sectors;
        set
        {
            OnPropertyChanged(ref _sectors, value);
            OnPropertyChanged(nameof(TotalBytes));
        }
    }

    public int TotalBytes
    {
        get => _platters * _tracks * _sectors * _bytesPerSector;
    }

    private int _diskNumber;
    private int _platters;
    private int _tracks;
    private int _sectors;

    private readonly int _bytesPerSector;
}