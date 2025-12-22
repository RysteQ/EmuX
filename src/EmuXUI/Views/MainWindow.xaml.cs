using EmuXUI.Assets.SyntaxHighlighting;
using EmuXUI.Popups;
using EmuXUI.ViewModels;
using Microsoft.UI.Input;
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
using System.Threading.Tasks;
using TextControlBoxNS;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EmuXUI;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        ViewModel = new MainWindowViewModel();

        InitUI();
    }

    private void InitUI()
    {
        InitTitleBar();
        InitMenuBar();
        InitCodeEditor();
    }

    private void InitTitleBar()
    {
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(TitleBarGrid);
    }

    private void InitMenuBar()
    {
        VirtualMachineMenuBarItem.Tapped += VirtualMachineMenuBarItem_Tapped;
        ExecuteMenuBarItem.Tapped += ExecuteMenuBarItem_Tapped;
        AboutMenuBarItem.Tapped += AboutMenuBarItem_Tapped;
    }

    private void InitCodeEditor()
    {
        SourceCodeTextControlBox.EnableSyntaxHighlighting = true;
        SourceCodeTextControlBox.ShowLineHighlighter = true;
        SourceCodeTextControlBox.SyntaxHighlighting = new x86Assembly();
    }

    private void VirtualMachineMenuBarItem_Tapped(object sender, TappedRoutedEventArgs e)
    {
        ViewModel.CommandConfigureVirtualMachine.Execute(null);
    }

    private void ExecuteMenuBarItem_Tapped(object sender, TappedRoutedEventArgs e)
    {
        ViewModel.CommandExecuteCode.Execute(null);
    }

    private async void AboutMenuBarItem_Tapped(object sender, TappedRoutedEventArgs e)
    {
        ContentDialog noWifiDialog = new()
        {
            Title = "About",
            Content = "Created by RysteQ on GitHub\n\nVersion: 1.0.0",
            CloseButtonText = "Okay"
        };

        noWifiDialog.XamlRoot = this.Content.XamlRoot;

        ContentDialogResult result = await noWifiDialog.ShowAsync();
    }

    private void SourceCodeTextControlBox_PointerPressed(object sender, PointerRoutedEventArgs e)
    {
        UpdateSizeRowColumnTextBlocks();
    }

    private void SourceCodeTextControlBox_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        UpdateSizeRowColumnTextBlocks();
    }

    private void UpdateSizeRowColumnTextBlocks()
    {
        SizeTextBlock.Text = $"Size: {SourceCodeTextControlBox.Text.Length}";
        RowTextBlock.Text = $"Row: {SourceCodeTextControlBox.CurrentLineIndex + 1}";
        ColumnTextBlock.Text = $"Col: {SourceCodeTextControlBox.CursorPosition.CharacterPosition}";
    }

    public MainWindowViewModel ViewModel { get; set; }
}
