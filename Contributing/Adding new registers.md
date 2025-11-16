# Adding new registers

Adding a new register to the `VirtualCPU` is allowed and encouraged, but to do that correctly you need to follow the these guidlines.

First, identify the register type to place it in the correct folder under  `EmuXCore\VM\Internal\CPU\Registers` folder.

- Segment register: Any register that is a segment register.
- Special register: Any register than is not a segment register and has a special function like pointing to the next instruction (RIP) or the base of the stack (RBP).
- Main register: Any register that does not fit the criteria for a segment or special register.

After you identify the register type create a new class in the subfolder that register belongs to and copy paste the following code.

**Note: The following example will be for a main register type*

```csharp
public class VirtualRegisterXXX : IVirtualRegister
{
    public VirtualRegisterXXX(IVirtualMachine? parentVirtualMachine = null)
    {
        _xxx = (ulong)Random.Shared.NextInt64();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => XXX;

    public void Set(string register, ulong value)
    {
    }

    public ulong XXX
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(XXX), Size.Qword, false));

            return _xxx;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(XXX)], BitConverter.GetBytes(XXX), BitConverter.GetBytes(value), nameof(XXX));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(XXX), Size.Qword, true, XXX, value));

            _xxx = value;
        }
    }

    public string Name => "XXX";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(XXX), Size.Qword }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _xxx;
}
```

This register only allows for 64bit values to be stored, if you want to look how the `RAX` register will look and behave please look into the `VirtualRegisterRAX` class and copy that class instead.

Now after you implement the logic and the `XXX` placeholders which are meant to indicate the name of the register you have to modify the `Registers` property inside of the `IVirtualCPU` implementation.