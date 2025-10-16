using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using System.Runtime.Intrinsics.Arm;

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

    public ulong RSI
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RSI), Size.Qword, false  ));

            return _rsi;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(RSI)], BitConverter.GetBytes(RSI), BitConverter.GetBytes(value), nameof(RSI));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RSI), Size.Qword, true, RSI, value));

            _rsi = value;
        }
    }

    public uint ESI
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(ESI), Size.Dword, false));

            return (uint)(RSI & 0x00000000ffffffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(ESI)], BitConverter.GetBytes(ESI), BitConverter.GetBytes(value), nameof(ESI));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(ESI), Size.Dword, true, ESI, value));

            _rsi = value;
        }
    }

    public ushort SI
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(SI), Size.Word, false));

            return (ushort)(ESI & 0x0000ffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(SI)], BitConverter.GetBytes(SI), BitConverter.GetBytes(value), nameof(SI));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(SI), Size.Word, true, SI, value));

            _rsi = (ESI & 0xffff0000) + value;
        }
    }

    public byte SIL
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(SIL), Size.Byte, false));

            return (byte)(SI & 0x00ff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(SIL)], [SIL], [value], nameof(SIL));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(SIL), Size.Byte, true, SIL, value));

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