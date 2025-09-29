using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

public class VirtualRegisterRIP : IVirtualRegister
{
    public VirtualRegisterRIP(IVirtualMachine? parentVirtualMachine = null)
    {
        _rip = 0;
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RIP;
    public void Set(ulong value) => RIP = value;

    private void RegisterRegisterUpdate(byte[] currentValue, byte[] newValue, string registerName)
    {
        ParentVirtualMachine?.Actions.Add([DIFactory.GenerateIVmAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[registerName], currentValue, newValue, registerName)]);
    }

    public ulong RIP
    {
        get => _rip;
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(RIP), BitConverter.GetBytes(value), nameof(RIP));
            _rip = value;
        }
    }

    public uint EIP
    {
        get => (uint)(RIP & 0x00000000ffffffff);
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(EIP), BitConverter.GetBytes(value), nameof(EIP));
            _rip = value;
        }
    }

    public ushort IP
    {
        get => (ushort)(EIP & 0x0000ffff);
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(IP), BitConverter.GetBytes(value), nameof(IP));
            _rip = (EIP & 0xffff0000) + value;
        }
    }

    public string Name => "RIP";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RIP), Size.Qword },
        { nameof(EIP), Size.Dword },
        { nameof(IP), Size.Word }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rip;
}