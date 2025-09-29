using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers;

public class VirtualRegisterRSP : IVirtualRegister
{
    public VirtualRegisterRSP(IVirtualMachine? parentVirtualMachine = null)
    {
        _rsp = 0;
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RSP;
    public void Set(ulong value) => RSP = value;

    public ulong RSP
    {
        get => _rsp;
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(RSP)], BitConverter.GetBytes(RSP), BitConverter.GetBytes(value), nameof(RSP));
            _rsp = value;
        }
    }

    public uint ESP
    {
        get => (uint)(RSP & 0x00000000ffffffff);
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(ESP)], BitConverter.GetBytes(ESP), BitConverter.GetBytes(value), nameof(ESP));
            _rsp = value;
        }
    }

    public ushort SP
    {
        get => (ushort)(ESP & 0x0000ffff);
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(SP)], BitConverter.GetBytes(SP), BitConverter.GetBytes(value), nameof(SP));
            _rsp = (ESP & 0xffff0000) + value;
        }
    }

    public byte SPL
    {
        get => (byte)(SP & 0x00ff);
        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(SPL)], [SPL], [value], nameof(SP));
            _rsp = (ushort)((SP & 0xff00) + value);
        }
    }

    public string Name => "RSP";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RSP), Size.Qword },
        { nameof(ESP), Size.Dword },
        { nameof(SP), Size.Word },
        { nameof(SPL), Size.Byte }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rsp;
}