using System;
using System.Windows.Input;

namespace EmuXUI.ViewModels.Internal;

public class Command : ICommand
{
    public Command(Action executeMethod, Predicate<object> isExecutionAllowedMethod)
    {
        _executeMethod = executeMethod;
        _isExecutionAllowedMethod = isExecutionAllowedMethod;
    }

    public bool CanExecute(object? parameter) => _isExecutionAllowedMethod(parameter);
    public void Execute(object? parameter) => _executeMethod();

    private Action _executeMethod;
    private Predicate<object> _isExecutionAllowedMethod;

    public event EventHandler? CanExecuteChanged;
}