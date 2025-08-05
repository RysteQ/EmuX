namespace EmuXCore.VM.Interfaces.Components;

/// <summary>
/// Due to a lot of components requiring access to the IVirtualMachine they belong to this interface is meant to help the child module find the parent IVirtualMachine implementation. <br/>
/// </summary>
public interface IVirtualComponent
{
    /// <summary>
    /// The parent IVirtualMachine the module is under
    /// Due to unit testing the value can be null.
    /// </summary>
    IVirtualMachine? ParentVirtualMachine { get; set; }
}