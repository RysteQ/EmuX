using EmuXCore.VM.Interfaces.Components;

namespace EmuXUI.Models.Static;

public sealed class GpuSelection
{
    public GpuSelection(string gpuModelName, IVirtualGPU gpu)
    {
        GpuModelName = gpuModelName;
        GPU = gpu;
    }

    public string GpuModelName { get; init; }
    public IVirtualGPU GPU { get; init; }
}