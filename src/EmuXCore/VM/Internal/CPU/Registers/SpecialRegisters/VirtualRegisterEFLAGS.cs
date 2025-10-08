using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

public class VirtualRegisterEFLAGS : IVirtualRegister
{
    public VirtualRegisterEFLAGS(IVirtualMachine? parentVirtualMachine = null)
    {
        _rflags = 0x0000000000000002;
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RFLAGS;
    public void Set(ulong value) => RFLAGS = value;

    public ulong RFLAGS
    {
        get => _rflags;
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(RFLAGS)], BitConverter.GetBytes(RFLAGS), BitConverter.GetBytes(value), nameof(RFLAGS));
            _rflags = value;
        }
    }

    public uint EFLAGS
    {
        get => (uint)(RFLAGS & 0x00000000ffffffff);
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(EFLAGS)], BitConverter.GetBytes(EFLAGS), BitConverter.GetBytes(value), nameof(EFLAGS));
            _rflags = value;
        }
    }

    public ushort FLAGS
    {
        get => (ushort)(EFLAGS & 0x0000ffff);
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(FLAGS)], BitConverter.GetBytes(FLAGS), BitConverter.GetBytes(value), nameof(FLAGS));
            _rflags = (EFLAGS & 0xffff0000) + value;
        }
    }

    public string Name => "RFLAGS";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RFLAGS), Size.Qword },
        { nameof(EFLAGS), Size.Dword },
        { nameof(FLAGS), Size.Dword }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rflags;
}