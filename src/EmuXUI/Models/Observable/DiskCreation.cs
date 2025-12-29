using EmuXCore;
using EmuXUI.Models.Internal;

namespace EmuXUI.Models.Observable;

public sealed class DiskCreation : ObservableObject
{
    public DiskCreation()
    {
        _bytesPerSector = DIFactory.GenerateIVirtualDisk(0, 0, 0, 0).BytesPerSector;
        Platters = 1;
        Tracks = 1;
        Sectors = 1;
    }

    public int DiskNumber
    {
        get => field;
        set
        {
            OnPropertyChanged(ref field, value);
            OnPropertyChanged(nameof(TotalBytes));
        }
    }
    
    public int Platters
    {
        get => field;
        set
        {
            OnPropertyChanged(ref field, value);
            OnPropertyChanged(nameof(TotalBytes));
        }
    }
    
    public int Tracks
    {
        get => field;
        set
        {
            OnPropertyChanged(ref field, value);
            OnPropertyChanged(nameof(TotalBytes));
        }
    }
    
    public int Sectors
    {
        get => field;
        set
        {
            OnPropertyChanged(ref field, value);
            OnPropertyChanged(nameof(TotalBytes));
        }
    }

    public int TotalBytes
    {
        get => Platters * Tracks * Sectors * _bytesPerSector;
    }

    private readonly int _bytesPerSector;
}