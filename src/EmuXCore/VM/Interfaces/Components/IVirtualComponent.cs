namespace EmuXCore.VM.Interfaces.Components;

public interface IVirtualComponent
{
    IVirtualMachine? ParentVirtualMachine { get; set; }
}