using EmuXCore.VM.Interfaces.Components;

namespace EmuXUI.Models.Static;

public sealed class DiskSelection
{
    public DiskSelection(IVirtualDisk virtualDisk)
    {
        VirtualDisk = virtualDisk;
    }

    public string Caption { get => $"Disk {VirtualDisk.DiskNumber}"; }
    public uint Bytes { get => VirtualDisk.TotalBytes; }

    public IVirtualDisk VirtualDisk { get; init; }
}