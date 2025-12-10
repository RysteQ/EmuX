using EmuXCore.VM.Interfaces.Components;

namespace EmuXUI.Models.Static;

public sealed class CpuSelection
{
    public CpuSelection(string cpuModelName, IVirtualCPU cpu)
    {
        CpuModelName = cpuModelName;
        CPU = cpu;
    }

    public string CpuModelName { get; init; }
    public IVirtualCPU CPU { get; init; }
}