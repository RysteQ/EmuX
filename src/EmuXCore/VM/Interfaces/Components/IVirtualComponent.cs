namespace EmuXCore.VM.Interfaces.Components;

/// <summary>
/// The IVirtualComponent module meant to serve as a bases for all other virtualised components.
/// </summary>
public interface IVirtualComponent
{
    /// <summary>
    /// The parent IVirtualMachine the component belongs to. <br/>
    /// Due to unit testing the value can be null.
    /// </summary>
    IVirtualMachine? ParentVirtualMachine { get; set; }
}