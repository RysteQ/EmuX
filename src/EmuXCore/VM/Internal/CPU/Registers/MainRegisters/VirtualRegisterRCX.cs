﻿using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRCX : IVirtualRegister
{
    public VirtualRegisterRCX(IVirtualMachine? parentVirtualMachine = null)
    {
        _rcx = (ulong)Random.Shared.NextInt64();
        ParentVirtualMachine = parentVirtualMachine;
    }

    public ulong Get() => RCX;
    public void Set(ulong value) => RCX = value;

    public ulong RCX
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RCX), Size.Qword, false));

            return _rcx;
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(RCX)], BitConverter.GetBytes(RCX), BitConverter.GetBytes(value), nameof(RCX));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(RCX), Size.Qword, true, RCX, value));

            _rcx = value;
        }
    }

    public uint ECX
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(ECX), Size.Dword, false));

            return (uint)(RCX & 0x00000000ffffffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(ECX)], BitConverter.GetBytes(ECX), BitConverter.GetBytes(value), nameof(ECX));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(ECX), Size.Dword, true, ECX, value));

            _rcx = value;
        }
    }

    public ushort CX
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(CX), Size.Word, false));

            return (ushort)(ECX & 0x0000ffff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(CX)], BitConverter.GetBytes(CX), BitConverter.GetBytes(value), nameof(CX));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(CX), Size.Word, true, CX, value));

            _rcx = (ECX & 0xffff0000) + value;
        }
    }

    public byte CH
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(CH), Size.Byte, false));

            return (byte)((CX & 0xff00) >> 8);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(CH)], [CH], [value], nameof(CH));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(CH), Size.Byte, true, CH, value));

            _rcx = (ushort)((CX & 0x00ff) + (value << 8));
        }
    }

    public byte CL
    {
        get
        {
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(CL), Size.Byte, false));

            return (byte)(CX & 0x00ff);
        }

        set
        {
            ParentVirtualMachine?.RegisterAction(VmActionCategory.ModifiedRegister, RegisterNamesAndSizes[nameof(CL)], [CL], [value], nameof(CL));
            ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIRegisteAccess(nameof(CL), Size.Byte, true, CL, value));

            _rcx = (ushort)((CX & 0xff00) + value);
        }
    }

    public string Name => "RCX";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RCX), Size.Qword },
        { nameof(ECX), Size.Dword },
        { nameof(CX), Size.Word },
        { nameof(CH), Size.Byte },
        { nameof(CL), Size.Byte }
    };

    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private ulong _rcx;
}