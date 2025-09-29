using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRDI : IVirtualRegister
{
    public VirtualRegisterRDI(IVirtualMachine? parentVirtualMachine = null)
    {
        _rdi = (ulong)Random.Shared.NextInt64();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RDI;
    public void Set(ulong value) => RDI = value;

    private void RegisterRegisterUpdate(byte[] currentValue, byte[] newValue, string registerName)
    {
        ParentVirtualMachine?.Actions.Add([DIFactory.GenerateIVmAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[registerName], currentValue, newValue, registerName)]);
    }

    public ulong RDI
    {
        get => _rdi;
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(RDI), BitConverter.GetBytes(value), nameof(RDI));
            _rdi = value;
        }
    }

    public uint EDI
    {
        get => (uint)(RDI & 0x00000000ffffffff);
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(EDI), BitConverter.GetBytes(value), nameof(EDI));
            _rdi = value;
        }
    }

    public ushort DI
    {
        get => (ushort)(EDI & 0x0000ffff);
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(DI), BitConverter.GetBytes(value), nameof(DI));
            _rdi = (EDI & 0xffff0000) + value;
        }
    }

    public byte DIL
    {
        get => (byte)(DI & 0x00ff);
        set
        {
            RegisterRegisterUpdate([DIL], [value], nameof(DIL));
            _rdi = (ushort)((DI & 0xff00) + value);
        }
    }

    public string Name => "RDI";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RDI), Size.Qword },
        { nameof(EDI), Size.Dword },
        { nameof(DI), Size.Word },
        { nameof(DIL), Size.Byte }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rdi;
}