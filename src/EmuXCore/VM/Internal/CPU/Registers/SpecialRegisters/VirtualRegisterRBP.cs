using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers;

public class VirtualRegisterRBP : IVirtualRegister
{
    public VirtualRegisterRBP(IVirtualMachine? parentVirtualMachine = null)
    {
        _rbp = 0;
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RBP;
    public void Set(ulong value) => RBP = value;

    public ulong RBP
    {
        get => _rbp;
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(RBP)], BitConverter.GetBytes(RBP), BitConverter.GetBytes(value), nameof(RBP));
            _rbp = value;
        }
    }

    public uint EBP
    {
        get => (uint)(RBP & 0x00000000ffffffff);
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(EBP)], BitConverter.GetBytes(EBP), BitConverter.GetBytes(value), nameof(RBP));
            _rbp = value;
        }
    }

    public ushort BP
    {
        get => (ushort)(EBP & 0x0000ffff);
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(BP)], BitConverter.GetBytes(BP), BitConverter.GetBytes(value), nameof(BP));
            _rbp = (EBP & 0xffff0000) + value;
        }
    }

    public byte BPL
    {
        get => (byte)(BP & 0x00ff);
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(BPL)], [BPL], [value], nameof(BPL));
            _rbp = (ushort)((BP & 0xff00) + value);
        }
    }

    public string Name => "RBP";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RBP), Size.Qword },
        { nameof(EBP), Size.Dword },
        { nameof(BP), Size.Word },
        { nameof(BPL), Size.Byte }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rbp;
}