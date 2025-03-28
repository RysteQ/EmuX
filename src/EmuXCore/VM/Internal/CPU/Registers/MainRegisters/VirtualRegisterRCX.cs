﻿using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRCX : IVirtualRegister
{
    public VirtualRegisterRCX()
    {
        RCX = (ulong)Random.Shared.NextInt64();
    }

    public ulong Get() => RCX;
    public void Set(ulong value) => RCX = value;

    public ulong RCX { get; set; }

    public uint ECX
    {
        get => (uint)(RCX & 0x00000000ffffffff);
        set => RCX = (RCX & 0xffffffff00000000) + value;
    }

    public ushort CX
    {
        get => (ushort)(ECX & 0x0000ffff);
        set => ECX = (ECX & 0xffff0000) + value;
    }

    public byte CH
    {
        get => (byte)((CX & 0xff00) >> 8);
        set => CX = (ushort)((CX & 0x00ff) + (value << 8));
    }

    public byte CL
    {
        get => (byte)(CX & 0x00ff);
        set => CX = (ushort)((CX & 0xff00) + value);
    }

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RCX), Size.Quad },
        { nameof(ECX), Size.Double },
        { nameof(CX), Size.Word },
        { nameof(CH), Size.Byte },
        { nameof(CL), Size.Byte }
    };
}