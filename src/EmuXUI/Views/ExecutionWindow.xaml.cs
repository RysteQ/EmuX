using EmuXCore.Common.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXUI.ViewModels;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System.Collections.Generic;

namespace EmuXUI.Views;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ExecutionWindow : Window
{
    public ExecutionWindow(IList<IInstruction> instructions, IList<ILabel> labels, IList<int> breakpoints, IVirtualMachine virtualMachine)
    {
        InitializeComponent();

        ViewModel = new(instructions, labels, breakpoints, virtualMachine);

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
