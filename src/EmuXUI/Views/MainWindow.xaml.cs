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
using System.Threading;
using System.Threading.Tasks;
using TextControlBoxNS;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.PlayTo;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI;
using Windows.UI.Popups;

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

    private async void MenuFlyoutItemOpenFile_Click(object sender, RoutedEventArgs e)
    {
        FileOpenPicker filePicker = new();
        StorageFile pickedFile;

        // Why do I have to do this Microsoft ?
        WinRT.Interop.InitializeWithWindow.Initialize(filePicker, System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle);

        filePicker.SuggestedStartLocation = PickerLocationId.Desktop;
        filePicker.ViewMode = PickerViewMode.List;
        filePicker.FileTypeFilter.Add(".asm");
        filePicker.FileTypeFilter.Add(".txt");

        pickedFile = await filePicker.PickSingleFileAsync();

        if (pickedFile == null)
        {
            return;
        }

        _selectedFile = pickedFile.Path;
        SourceCodeTextControlBox.Text = File.ReadAllText(_selectedFile);
    }

    private async void MenuFlyoutItemSaveFile_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(_selectedFile))
        {
            MenuFlyoutItemSaveFileAs_Click(sender, e);

            return;
        }

        File.WriteAllText(_selectedFile, SourceCodeTextControlBox.Text);
    }

    private async void MenuFlyoutItemSaveFileAs_Click(object sender, RoutedEventArgs e)
    {
        FileSavePicker savePicker = new();
        StorageFile saveLocation;

        WinRT.Interop.InitializeWithWindow.Initialize(savePicker, System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle);

        savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
        savePicker.FileTypeChoices.Add("Assembly", [".asm"]);
        savePicker.FileTypeChoices.Add("Text file", [".txt"]);

        saveLocation = await savePicker.PickSaveFileAsync();

        if (saveLocation == null)
        {
            new InfoPopup(Enums.InfoPopupSeverity.Warning, "The file wasn't saved, a valid save location is required").Activate();

            return;
        }

        _selectedFile = saveLocation.Path;
        File.WriteAllText(_selectedFile, SourceCodeTextControlBox.Text);
    }

    private void MenuFlyoutItemUndo_Click(object sender, RoutedEventArgs e)
    {
        SourceCodeTextControlBox.Undo();
    }

    private void MenuFlyoutItemRedo_Click(object sender, RoutedEventArgs e)
    {
        SourceCodeTextControlBox.Redo();
    }

    private void MenuFlyoutItemFind_Click(object sender, RoutedEventArgs e)
    {
        GridFindStringReferenceControlsGroup.Visibility = Visibility.Visible;
        ButtonFindSingleReferenceToString.Visibility = Visibility.Visible;
        ButtonFindAllReferencesToString.Visibility = Visibility.Visible;
        StackPanelReplaceStringControlsGroup.Visibility = Visibility.Collapsed;

        _findModeEnabled = true;
        _findAllStringReferencesClicked = false;

        TextBoxFindString.Focus(FocusState.Keyboard);
    }

    private void MenuFlyoutItemFindAll_Click(object sender, RoutedEventArgs e)
    {
        GridFindStringReferenceControlsGroup.Visibility = Visibility.Visible;
        ButtonFindSingleReferenceToString.Visibility = Visibility.Visible;
        ButtonFindAllReferencesToString.Visibility = Visibility.Visible;
        StackPanelReplaceStringControlsGroup.Visibility = Visibility.Collapsed;

        _findModeEnabled = true;
        _findAllStringReferencesClicked = true;

        TextBoxFindString.Focus(FocusState.Keyboard);
    }

    private void MenuFlyoutItemReplace_Click(object sender, RoutedEventArgs e)
    {
        GridFindStringReferenceControlsGroup.Visibility = Visibility.Visible;
        ButtonFindSingleReferenceToString.Visibility = Visibility.Collapsed;
        ButtonFindAllReferencesToString.Visibility = Visibility.Collapsed;
        StackPanelReplaceStringControlsGroup.Visibility = Visibility.Visible;

        _findModeEnabled = false;
        _replaceAllStringReferencesClicked = false;

        TextBoxFindString.Focus(FocusState.Keyboard);
    }

    private void MenuFlyoutItemReplaceAll_Click(object sender, RoutedEventArgs e)
    {
        GridFindStringReferenceControlsGroup.Visibility = Visibility.Visible;
        ButtonFindSingleReferenceToString.Visibility = Visibility.Collapsed;
        ButtonFindAllReferencesToString.Visibility = Visibility.Collapsed;
        StackPanelReplaceStringControlsGroup.Visibility = Visibility.Visible;

        _findModeEnabled = false;
        _replaceAllStringReferencesClicked = true;

        TextBoxFindString.Focus(FocusState.Keyboard);
    }

    private void SourceCodeTextControlBox_PointerPressed(object sender, PointerRoutedEventArgs e)
    {
        if (GridFindStringReferenceControlsGroup.Visibility == Visibility.Visible)
        {
            GridFindStringReferenceControlsGroup.Visibility = Visibility.Collapsed;
        }
        
        UpdateSizeRowColumnTextBlocks();
    }

    private void SourceCodeTextControlBox_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (GridFindStringReferenceControlsGroup.Visibility == Visibility.Visible)
        {
            GridFindStringReferenceControlsGroup.Visibility = Visibility.Collapsed;
            SourceCodeTextControlBox.EndSearch();
        }

        UpdateSizeRowColumnTextBlocks();
    }
    
    private void TextBoxFindString_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Escape)
        {
            GridFindStringReferenceControlsGroup.Visibility = Visibility.Collapsed;
            SourceCodeTextControlBox.EndSearch();
            SourceCodeTextControlBox.Focus(FocusState.Keyboard);
        }

        if (e.Key != VirtualKey.Enter)
        {
            return;
        }

        if (_findModeEnabled)
        {
            if (_findAllStringReferencesClicked)
            {
                FindAllTextReferences();
            }
            else
            {
                FindAndNavigateToTextReference();
            }
        }
        else
        {
            TextBoxReplaceString.Focus(FocusState.Keyboard);
        }
    }

    private void TextBoxReplaceString_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Escape)
        {
            GridFindStringReferenceControlsGroup.Visibility = Visibility.Collapsed;
            SourceCodeTextControlBox.EndSearch();
            SourceCodeTextControlBox.Focus(FocusState.Keyboard);
        }

        if (e.Key != VirtualKey.Enter || _findModeEnabled)
        {
            return;
        }

        if (_replaceAllStringReferencesClicked)
        {
            ReplaceAllTextReferences();
        }
        else
        {
            ReplaceNextTextReference();
        }
    }

    private void ButtonFindSingleReferenceToString_Click(object sender, RoutedEventArgs e)
    {
        _findAllStringReferencesClicked = false;

        FindAndNavigateToTextReference();
    }

    private void ButtonFindAllReferencesToString_Click(object sender, RoutedEventArgs e)
    {
        _findAllStringReferencesClicked = true;

        FindAllTextReferences();
    }

    private void FindAndNavigateToTextReference()
    {
        string[] sourceCodeLines = SourceCodeTextControlBox.Text.Split('\n');
        StringComparison comparison = StringComparison.OrdinalIgnoreCase;

        if (string.IsNullOrEmpty(TextBoxFindString.Text))
        {
            Task.Run(() =>
            {
                Console.Beep();
            });

            return;
        }

        if (CheckboxMatchStringReferenceToLookupCase.IsChecked == true)
        {
            comparison = StringComparison.Ordinal;
        }

        for (int i = SourceCodeTextControlBox.CurrentLineIndex; i < sourceCodeLines.Length; i++)
        {
            if (sourceCodeLines[i].Contains(TextBoxFindString.Text, comparison))
            {
                GridFindStringReferenceControlsGroup.Visibility = Visibility.Collapsed;

                SourceCodeTextControlBox.SetCursorPosition(i, sourceCodeLines[i].IndexOf(TextBoxFindString.Text));
                SourceCodeTextControlBox.Focus(FocusState.Keyboard);

                return;
            }
        }

        Task.Run(() =>
        {
            Console.Beep();
        });
    }

    private void FindAllTextReferences()
    {
        SourceCodeTextControlBox.BeginSearch(TextBoxFindString.Text, false, CheckboxMatchStringReferenceToLookupCase.IsChecked == true);
    }

    private void ReplaceNextTextReference()
    {
        SourceCodeTextControlBox.BeginSearch(TextBoxFindString.Text, false, CheckboxMatchStringReferenceToLookupCase.IsChecked == true);
        SourceCodeTextControlBox.ReplaceNext(TextBoxReplaceString.Text);
        SourceCodeTextControlBox.EndSearch();
    }

    private void ReplaceAllTextReferences()
    {
        SourceCodeTextControlBox.BeginSearch(TextBoxFindString.Text, false, CheckboxMatchStringReferenceToLookupCase.IsChecked == true);
        SourceCodeTextControlBox.ReplaceAll(TextBoxFindString.Text, TextBoxReplaceString.Text, CheckboxMatchStringReferenceToLookupCase.IsChecked == true, false);
        SourceCodeTextControlBox.EndSearch();
    }

    private void UpdateSizeRowColumnTextBlocks()
    {
        SizeTextBlock.Text = $"Size: {SourceCodeTextControlBox.Text.Length}";
        RowTextBlock.Text = $"Row: {SourceCodeTextControlBox.CurrentLineIndex + 1}";
        ColumnTextBlock.Text = $"Col: {SourceCodeTextControlBox.CursorPosition.CharacterPosition}";
    }

    public MainWindowViewModel ViewModel { get; set; }

    private string _selectedFile = string.Empty;
    private bool _findModeEnabled = false;
    private bool _findAllStringReferencesClicked = false;
    private bool _replaceAllStringReferencesClicked = false;
}
