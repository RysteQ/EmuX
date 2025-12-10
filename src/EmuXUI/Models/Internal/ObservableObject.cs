using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmuXUI.Models.Internal;

public class ObservableObject : INotifyPropertyChanged
{
    /// <summary>
    /// Triggers the INotifyPropertyChanged event and also modified the caller value
    /// </summary>
    protected void OnPropertyChanged<T>(ref T property, object newValue, [CallerMemberName] string? caller = null)
    {
        property = (T)newValue;

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}