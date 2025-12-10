using System;
using System.Windows.Input;

namespace EmuXUI.ViewModels.Internal;

public class SimpleCommand : ICommand
{
    public SimpleCommand(Action executeMethod)
    {
        _executeMethod = executeMethod;
    }

    public bool CanExecute(object? parameter) => true;
    public void Execute(object? parameter) => _executeMethod();

    private Action _executeMethod;

    public event EventHandler? CanExecuteChanged;
}