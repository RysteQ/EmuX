using EmuXUI.Models.Logic;
using EmuXUI.ViewModels;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;

namespace EmuXUI.Views;

public sealed partial class VirtualMachineConfigurationWindow : Window
{
    public VirtualMachineConfigurationWindow(VirtualMachineConfiguration currentVirtualMachineConfiguration)
    {
        InitializeComponent();

        ViewModel = new(currentVirtualMachineConfiguration);
        ViewModel.SavedConfiguration += ViewModel_SavedConfiguration;
        
        InitUI();
    }

    private void InitUI()
    {
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(TitleBarGrid);

        AppWindow.Resize(new((int)mainGrid.MaxWidth, (int)mainGrid.MaxHeight));

        DisableWindowResizing();
    }

    private void DisableWindowResizing()
    {
        nint windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(this);
        WindowId windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
        AppWindow? applicationWindow = AppWindow.GetFromWindowId(windowId);
        OverlappedPresenter? presenter = applicationWindow?.Presenter as OverlappedPresenter;
        
        if  (presenter == null)
        {
            return;
        }
        
        presenter.IsResizable = false;
        presenter.IsMaximizable = false;
    }

    private void ViewModel_SavedConfiguration(object? sender, EventArgs e)
    {
        SavedVirtualMachineConfiguration?.Invoke(this, ViewModel.VirtualMachineConfiguration);
    }

    public event EventHandler? SavedVirtualMachineConfiguration;

    public VirtualMachineConfigurationViewModel ViewModel { get; set; }
}
