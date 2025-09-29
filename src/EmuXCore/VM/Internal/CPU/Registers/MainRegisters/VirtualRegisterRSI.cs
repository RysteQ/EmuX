using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRSI : IVirtualRegister
{
    public VirtualRegisterRSI(IVirtualMachine? parentVirtualMachine = null)
    {
        _rsi = (ulong)Random.Shared.NextInt64();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RSI;
    public void Set(ulong value) => RSI = value;

    private void RegisterRegisterUpdate(byte[] currentValue, byte[] newValue, string registerName)
    {
        ParentVirtualMachine?.Actions.Add([DIFactory.GenerateIVmAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[registerName], currentValue, newValue, registerName)]);
    }

    public ulong RSI
    {
        get => _rsi;
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(RSI), BitConverter.GetBytes(value), nameof(RSI));
            _rsi = value;
        }
    }

    public uint ESI
    {
        get => (uint)(RSI & 0x00000000ffffffff);
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(ESI), BitConverter.GetBytes(value), nameof(ESI));
            _rsi = value;
        }
    }

    public ushort SI
    {
        get => (ushort)(ESI & 0x0000ffff);
        set
        {
            RegisterRegisterUpdate(BitConverter.GetBytes(SI), BitConverter.GetBytes(value), nameof(SI));
            _rsi = (ESI & 0xffff0000) + value;
        }
    }

    public byte SIL
    {
        get => (byte)(SI & 0x00ff);
        set
        {
            RegisterRegisterUpdate([SIL], [value], nameof(SIL));
            _rsi = (ushort)((SI & 0xff00) + value);
        }
    }

    public string Name => "RSI";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RSI), Size.Qword },
        { nameof(ESI), Size.Dword },
        { nameof(SI), Size.Word },
        { nameof(SIL), Size.Byte }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rsi;
}