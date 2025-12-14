using EmuXCore;
using EmuXCore.VM.Interfaces;
using EmuXUI.ViewModels;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EmuXUI.Views;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class VirtualMachineConfigurationWindow : Window
{
    public VirtualMachineConfigurationWindow(ref IVirtualMachine virtualMachine)
    {
        InitializeComponent();

        ViewModel = new(ref virtualMachine);
        
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

    private void SaveVirtualMachineConfigurationsButtonClick(object sender, RoutedEventArgs e)
    {
        ViewModel.CommandSave.Execute(null);

        SavedVirtualMachineConfiguration?.Invoke(this, ViewModel.VirtualMachineConfiguration);
    }

    public event EventHandler? SavedVirtualMachineConfiguration;

    public VirtualMachineConfigurationViewModel ViewModel { get; set; }
}
