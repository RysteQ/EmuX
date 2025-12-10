using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace EmuXUI.ViewModels.Internal;

/// <summary>
/// A base view model that has all the necessary handy methods for all view models
/// </summary>
public class BaseViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Initialises a simple command object that only executes a method and doesn't check if that method is alloed to be executed or not
    /// </summary>
    /// <param name="executeMethod">The method to execute</param>
    /// <returns>The new simple command object</returns>
    protected ICommand GenerateCommand(Action executeMethod)
    {
        return new SimpleCommand(executeMethod);
    }

    /// <summary>
    /// Initialises a command object that only executed a method if that method is alloed to be executed which is determined by another method
    /// </summary>
    /// <param name="executeMethod">The method to execute</param>
    /// <param name="isExecutionAllowedMethod">The method to verify execution is allowed to or not</param>
    /// <returns>The new command object</returns>
    protected ICommand GenerateCommand(Action executeMethod, Predicate<object> isExecutionAllowedMethod)
    {
        return new Command(executeMethod, isExecutionAllowedMethod);
    }

    /// <summary>
    /// Triggers the INotifyPropertyChanged event and also modified the caller value
    /// </summary>
    protected void OnPropertyChanged<T>(ref T property, object newValue, [CallerMemberName] string? caller = null)
    {
        property = (T)newValue;

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
    }

    /// <summary>
    /// Triggers the INotifyPropertyChanged event
    /// </summary>
    protected void OnPropertyChanged([CallerMemberName] string? caller = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// A boolean to represent something is loading, triggers INotifyPropertyChanged
    /// </summary>
    public bool Loading
    {
        get => _loading; 
        set => OnPropertyChanged(ref _loading, value);
    }

    /// <summary>
    /// A boolean to represent something is loading, triggers INotifyPropertyChanged
    /// </summary>
    public bool LoadingTwo
    {
        get => _loadingTwo;
        set => OnPropertyChanged(ref _loadingTwo, value);
    }

    /// <summary>
    /// A boolean to represent something is loading, does not trigger INotifyPropertyChanged
    /// </summary>
    public bool InternalLoading
    {
        get => _internalLoading;
        set => _internalLoading = value;
    }

    private bool _loading;
    private bool _loadingTwo;
    private bool _internalLoading;
}