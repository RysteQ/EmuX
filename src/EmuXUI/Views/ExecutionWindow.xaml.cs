using EmuXCore.Common.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXUI.Models.Events;
using EmuXUI.ViewModels;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
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
public sealed partial class ExecutionWindow : Window
{
    public ExecutionWindow(IList<IInstruction> instructions, IVirtualMachine virtualMachine)
    {
        InitializeComponent();

        ViewModel = new(instructions, virtualMachine);

        InitUI();
    }

    private async void Window_Activated(object sender, WindowActivatedEventArgs args)
    {
        await ViewModel.UpdateBitmap();
    }

    private void InitUI()
    {
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(TitleBarGrid);

        AppWindow.Resize(new(ViewModel.VideoOutput.Width * 2 + 200, ViewModel.VideoOutput.Height * 2 + 100));

        DisableWindowResizing();
    }

    private void DisableWindowResizing()
    {
        nint windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(this);
        WindowId windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
        AppWindow? applicationWindow = AppWindow.GetFromWindowId(windowId);
        OverlappedPresenter? presenter = applicationWindow?.Presenter as OverlappedPresenter;

        if (presenter == null)
        {
            return;
        }

        presenter.IsResizable = false;
        presenter.IsMaximizable = false;
    }

    public ExecutionViewModel ViewModel { get; private set; }
}
