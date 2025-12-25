using EmuXCore.Common.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXUI.Models.Observable;
using EmuXUI.ViewModels.Internal;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Streams;
using static System.Net.Mime.MediaTypeNames;

namespace EmuXUI.ViewModels;

public sealed class ExecutionViewModel : BaseViewModel
{
    public ExecutionViewModel(IList<IInstruction> instructions, IVirtualMachine virtualMachine)
    {
        _instructions = instructions;
        _virtualMachine = virtualMachine;

        CommandExecuteCode = GenerateCommand(async () => await ExecuteCode());

        VideoOutput = new(_virtualMachine.GPU.Height, _virtualMachine.GPU.Width);

    }

    private async Task ExecuteCode()
    {
        await VideoOutput.WriteEntireImage(_virtualMachine.GPU.Data);
        VideoOutput.Update();
    }

    public async Task UpdateBitmap()
    {
        await VideoOutput.WriteEntireImage(_virtualMachine.GPU.Data);
        VideoOutput.Update();
    }

    public Bitmap VideoOutput { get; private set; }
    public ICommand CommandExecuteCode { get; private set; }

    private readonly IList<IInstruction> _instructions;
    private readonly IVirtualMachine _virtualMachine;
}