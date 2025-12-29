using EmuXUI.Enums;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

namespace EmuXUI.Popups;

public sealed partial class InfoPopup : Window
{
    public InfoPopup(InfoPopupSeverity severity, string message)
    {
        InitializeComponent();

        InitUI();
        InitialisePopupContents(severity, message);
    }

    public static void Show(InfoPopupSeverity severity, string message)
    {
        new InfoPopup(severity, message).Activate();
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

        if (presenter == null)
        {
            return;
        }

        presenter.IsResizable = false;
        presenter.IsMaximizable = false;
    }


    private void InitialisePopupContents(InfoPopupSeverity severity, string message)
    {
        textBlockInfoTitle.Text = severity switch
        {
            InfoPopupSeverity.Info => "Info",
            InfoPopupSeverity.Warning => "Warning",
            InfoPopupSeverity.Error => "Error",
            _ => "Info"
        };

        textBlockInfoContents.Text = message;
    }
}
