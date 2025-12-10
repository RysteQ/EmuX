using EmuXUI.Models.Internal;

namespace EmuXUI.Models.Observable;

public sealed class DiskCreation : ObservableObject
{
    public byte DiskNumber
    {
        get => _diskNumber;
        set => OnPropertyChanged(ref _diskNumber, value);
    }
    
    public byte Platters
    {
        get => _platters;
        set => OnPropertyChanged(ref _platters, value);
    }
    
    public ushort Tracks
    {
        get => _tracks;
        set => OnPropertyChanged(ref _tracks, value);
    }
    
    public byte Sectors
    {
        get => _sectors;
        set => OnPropertyChanged(ref _sectors, value);
    }

    private byte _diskNumber;
    private byte _platters;
    private byte _tracks;
    private byte _sectors;
}